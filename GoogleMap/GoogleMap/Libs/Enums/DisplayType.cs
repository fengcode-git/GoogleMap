using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleMap.Libs.Enums
{
    /// <summary>
    /// 显示类型
    /// </summary>
    public enum DisplayType
    {
        /// <summary>
        /// 所有
        /// </summary>
        [Display(Name = "所有标记")]
        All,
        /// <summary>
        /// 正式会员的标记
        /// </summary>
        [Display(Name = "正式会员的标记")]
        Member,
        /// <summary>
        /// 普通会员的标记
        /// </summary>
        [Display(Name = "普通会员的标记")]
        User
    }
}
