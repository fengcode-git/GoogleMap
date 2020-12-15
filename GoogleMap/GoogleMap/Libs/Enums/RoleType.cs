using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleMap.Libs.Enums
{
    /// <summary>
    /// 角色类型
    /// </summary>
    public enum RoleType
    {
        /// <summary>
        /// 管理员
        /// </summary>
        [Description("Admin")]
        [Display(Name = "管理员")]
        Admin = 1,

        /// <summary>
        /// 正式会员
        /// </summary>
        [Description("Member")]
        [Display(Name = "正式会员")]
        Member = 2,

        /// <summary>
        /// 普通会员
        /// </summary>
        [Description("User")]
        [Display(Name = "普通会员")]
        User = 3
    }
}
