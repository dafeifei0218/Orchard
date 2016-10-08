using System;

namespace Orchard.Validation {
    /// <summary>
    /// 参数
    /// </summary>
    public class Argument {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="name">名称</param>
        public static void Validate(bool condition, string name) {
            if (!condition) {
                throw new ArgumentException("Invalid argument", name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public static void Validate(bool condition, string name, string message) {
            if (!condition) {
                throw new ArgumentException(message, name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="name"></param>
        public static void ThrowIfNull<T>(T value, string name) where T : class {
            if (value == null) {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public static void ThrowIfNull<T>(T value, string name, string message) where T : class {
            if (value == null) {
                throw new ArgumentNullException(name, message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        public static void ThrowIfNullOrEmpty(string value, string name) {
            if (string.IsNullOrEmpty(value)) {
                throw new ArgumentException("Argument must be a non empty string", name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public static void ThrowIfNullOrEmpty(string value, string name, string message) {
            if (string.IsNullOrEmpty(value)) {
                throw new ArgumentException(message, name);
            }
        }
    }
}
