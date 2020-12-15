using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleMap.Models
{
    public class AccountLoginModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "用户名不能为空")]
        [RegularExpression("^[0-9a-zA-Z]+$", ErrorMessage = "必须为字母或数字")]
        [StringLength(20, ErrorMessage = "不少于4个字符", MinimumLength = 4)]
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Display(Name = "登录密码")]
        [Required(ErrorMessage = "登录密码不能为空")]
        [RegularExpression("^[0-9a-zA-Z]+$", ErrorMessage = "必须为字母或数字")]
        [StringLength(20, ErrorMessage = "不少于6个字符", MinimumLength = 6)]
        public string Password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Display(Name = "验证码")]
        [Required(ErrorMessage = "登录密码不能为空")]
        [RegularExpression("^[0-9a-zA-Z]+$", ErrorMessage = "必须为字母或数字")]
        public string Code { get; set; }

        /// <summary>
        /// 记住我
        /// </summary>
        [Display(Name = "记住我")]
        public bool IsRememberMe { get; set; } = false;
        /// <summary>
        /// 网站名称
        /// </summary>
        public string SiteName { get; set; }
        /// <summary>
        /// 返回URL
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
