using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleMap.Libs.Entity;

namespace GoogleMap.Areas.Admin.Models
{
    /// <summary>
    /// 导航栏组件模型
    /// </summary>
    public class NavbarComponentModel
    {
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public PersonView PersonView { get; set; }
    }
}
