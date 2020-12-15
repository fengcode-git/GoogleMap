using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleMap.Libs.Setting
{
    /// <summary>
    /// OG协议配置
    /// </summary>
    public class OGConfig
    {
        /// <summary>
        /// 页面标题
        /// </summary>
        [Display(Name = "页面标题")]
        [Required(ErrorMessage = "不能为空")]
        public string Title { get; set; }

        /// <summary>
        /// 网站URL
        /// </summary>
        [Display(Name = "网站URL")]
        [Required(ErrorMessage = "不能为空")]
        public string Url { get; set; }

        [Display(Name = "网站名称")]
        [Required(ErrorMessage = "不能为空")]
        public string SiteName { get; set; }

        [Display(Name = "网站描述")]
        [Required(ErrorMessage = "不能为空")]
        public string Description { get; set; }

        [Display(Name = "图片路径")]
        [Required(ErrorMessage = "不能为空")]
        public string Image { get; set; }
    }
}
