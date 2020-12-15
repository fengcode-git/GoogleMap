using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleMap.Areas.Admin.Models
{
    /// <summary>
    /// 标记编辑模型
    /// </summary>
    public class MarkerEditModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [RegularExpression(@"^[\-\+]?([0-8]?\d{1}\.\d{1,7}|90\.0{1,7})$", ErrorMessage = "整数部分为0～90，必须输入1到7位小数")]
        [Required]
        [Display(Name = "纬度")]
        public decimal Lat { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [RegularExpression(@"^[\-\+]?(0?\d{1,2}\.\d{1,7}|1[0-7]?\d{1}\.\d{1,7}|180\.0{1,7})$", ErrorMessage = "整数部分为0～180，必须输入1到7位小数")]
        [Display(Name = "经度")]
        public decimal Lng { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(20, ErrorMessage = "最多20个字符")]
        [Display(Name = "标题")]
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        [MaxLength(50, ErrorMessage = "最多50个字符")]
        public string Explain { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name = "地址")]
        [MaxLength(50, ErrorMessage = "最多50个字符")]
        [Required(ErrorMessage = "不能为空")]
        public string Address { get; set; }
    }
}
