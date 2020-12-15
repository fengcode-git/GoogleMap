using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleMap.Libs.Entity;

namespace GoogleMap.Models
{
    /// <summary>
    /// 顶部导航组件模型
    /// </summary>
    public class TopNavbarComponentModel
    {
        /// <summary>
        /// 登录用户
        /// </summary>
        public PersonView PersonView { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        public string SiteName { get; set; }
    }
}
