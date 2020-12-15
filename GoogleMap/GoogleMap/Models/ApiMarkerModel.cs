using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleMap.Models
{
    public class ApiMarkerModel
    {
        /// <summary>
        /// 纬度
        /// </summary>
        [RegularExpression(@"^[\-\+]?([0-8]?\d{1}\.\d{1,7}|90\.0{1,7})$", ErrorMessage = "纬度的整数部分为0～90，必须输入1到7位小数")]
        [Display(Name = "纬度")]
        public decimal Lat { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [RegularExpression(@"^[\-\+]?(0?\d{1,2}\.\d{1,7}|1[0-7]?\d{1}\.\d{1,7}|180\.0{1,7})$", ErrorMessage = "经度的整数部分为0～180，必须输入1到7位小数")]
        [Display(Name = "经度")]
        public decimal Lng { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(20, ErrorMessage = "标题最多20个字符")]
        [Display(Name = "标题")]
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        [MaxLength(50, ErrorMessage = "描述最多50个字符")]
        public string Explain { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name = "地址")]
        [MaxLength(50, ErrorMessage = "地址最多50个字符")]
        [Required(ErrorMessage = "地址不能为空")]
        public string Address { get; set; }

        [Required(ErrorMessage = "开发者密钥不能为空")]
        public string Key { get; set; }
    }
}
