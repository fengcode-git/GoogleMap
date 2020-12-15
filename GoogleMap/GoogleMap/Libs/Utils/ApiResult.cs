using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleMap.Libs.Utils
{
    /// <summary>
    /// API调用结果
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; } = true;
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = "";
        /// <summary>
        /// 内容
        /// </summary>
        public object Context { get; set; }
    }
}
