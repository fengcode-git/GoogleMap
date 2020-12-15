using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GoogleMap.Libs.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GoogleMap.Areas.Admin.Models
{
    /// <summary>
    /// 用户编辑模型
    /// </summary>
    public class UserEditModel
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 账号名称
        /// </summary>
        [Display(Name = "账号名称")]
        public string AccountName { get; set; }
        /// <summary>
        /// 是否验证
        /// </summary>
        [Display(Name = "是否验证")]
        public bool IsVerify { get; set; }

        [Display(Name = "角色类型")]
        public RoleType RoleType { get; set; }

        public List<SelectListItem> SelectListItems { get; set; } = new List<SelectListItem>();
    }
}
