using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleMap.Areas.Admin.Models
{
    /// <summary>
    /// API Key 模型
    /// </summary>
    public class ApiKeyModel
    {
        /// <summary>
        /// API接口密钥
        /// </summary>
        [Display(Name ="开发KEY")]
        public string Key { get; set; }
    }
}
