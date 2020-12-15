using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GoogleMap.Libs.Enums;

namespace GoogleMap.Libs.Setting
{
    /// <summary>
    /// 战斗配置
    /// </summary>
    public class SiteConfig
    {
        /// <summary>
        /// 网站名称
        /// </summary>
        [Display(Name = "网站名称")]
        [Required(ErrorMessage = "不能为空")]
        public string SiteName { get; set; } = "谷歌地图";

        /// <summary>
        /// 网站Footer
        /// </summary>
        [Display(Name = " 网站Footer")]
        [Required(ErrorMessage = "不能为空")]
        public string SiteFooter { get; set; } = "map";

        /// <summary>
        /// GOOGLE Key
        /// </summary>
        [Display(Name ="谷歌Key")]
        [Required(ErrorMessage = "不能为空")]
        public string GoogleKey { get; set; } = "";

        /// <summary>
        /// 默认放大
        /// </summary>
        [Display(Name = "默认放大")]
        [Required(ErrorMessage = "默认放大")]
        [RegularExpression("^[1-9]$",ErrorMessage = "必须是1-9之间的整数")]
        public int DefaultZoom { get; set; } = 5;

        /// <summary>
        /// 标记显示类型
        /// </summary>
        [Display(Name = "标记显示类型")]
        public DisplayType DisplayType { get; set; } = DisplayType.All;
    }
}
