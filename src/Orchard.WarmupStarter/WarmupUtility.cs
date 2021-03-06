﻿using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace Orchard.WarmupStarter {
    /// <summary>
    /// 启动初始化工具类
    /// </summary>
    public static class WarmupUtility {

        public static readonly string WarmupFilesPath = "~/App_Data/Warmup/";

        /// <summary>
        /// return true to put request on hold (until call to Signal()) - return false to allow pipeline to execute immediately
        /// 返回一个bool值表示是否已经在该方法内处理了BeginRequest事件
        /// true：请求暂停（直到Signal()）；false：允许执行管道
        /// </summary>
        /// <param name="httpApplication">ASP.NET应用程序</param>
        /// <returns></returns>
        public static bool DoBeginRequest(HttpApplication httpApplication) {
            // use the url as it was requested by the client
            // the real url might be different if it has been translated (proxy, load balancing, ...)
            // 使用该网址，因为它是由客户端请求
            // 真正的网址可能是不同的，如果它已被翻译（代理，负载平衡，…）
            var url = ToUrlString(httpApplication.Request);
            var virtualFileCopy = WarmupUtility.EncodeUrl(url.Trim('/'));
            var localCopy = Path.Combine(HostingEnvironment.MapPath(WarmupFilesPath), virtualFileCopy);

            if (File.Exists(localCopy)) {
                // result should not be cached, even on proxies
                // 结果不应该被缓存，即使代理
                httpApplication.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                httpApplication.Response.Cache.SetValidUntilExpires(false);
                httpApplication.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                httpApplication.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                httpApplication.Response.Cache.SetNoStore();

                httpApplication.Response.WriteFile(localCopy);
                httpApplication.Response.End();
                return true;
            }

            // there is no local copy and the file exists
            // serve the static file
            // 没有本地复制文件存在
            // 提供静态文件
            if (File.Exists(httpApplication.Request.PhysicalPath)) {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 等效于Request.Url.AbsoluteUri属性的值，
        /// 源码注释说如果使用了代理请求、负载平衡等获取不了真是的绝对Url地址
        /// </summary>
        /// <param name="request">Web请求</param>
        /// <returns></returns>
        public static string ToUrlString(HttpRequest request) {
            return string.Format("{0}://{1}{2}", request.Url.Scheme, request.Headers["Host"], request.RawUrl);
        }

        /// <summary>
        /// Url编码，将一个url字符串转换成另一个字包含数字、字母和下划线的字符串。
        /// </summary>
        /// <param name="url">url</param>
        /// <returns></returns>
        public static string EncodeUrl(string url) {
            if (String.IsNullOrWhiteSpace(url)) {
                throw new ArgumentException("url can't be empty");
            }

            var sb = new StringBuilder();
            foreach (var c in url.ToLowerInvariant()) {
                // only accept alphanumeric chars
                //只接受字母数字字符
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9')) {
                    sb.Append(c);
                }
                // otherwise encode them in UTF8
                //否则UTF8编码
                else
                {
                    sb.Append("_");
                    foreach (var b in Encoding.UTF8.GetBytes(new[] { c })) {
                        sb.Append(b.ToString("X"));
                    }
                }
            }

            return sb.ToString();
        }
    }
}