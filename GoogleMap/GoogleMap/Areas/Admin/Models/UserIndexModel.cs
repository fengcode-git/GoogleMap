using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Utils.Paging;
using GoogleMap.Libs.Entity;

namespace GoogleMap.Areas.Admin.Models
{
    /// <summary>
    /// 用户管理首页模型
    /// </summary>
    public class UserIndexModel
    { 
        /// <summary>
      /// 搜索文本
      /// </summary>
        public string Search { get; set; }
        /// <summary>
        /// 分页结果
        /// </summary>
        public PagingResult<PersonView> PagingResult { get; set; }
    }
}
