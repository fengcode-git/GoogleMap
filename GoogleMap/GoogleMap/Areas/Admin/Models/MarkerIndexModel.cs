using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Utils.Paging;
using GoogleMap.Libs.Entity;

namespace GoogleMap.Areas.Admin.Models
{
    /// <summary>
    /// 地图标记首页视图模型
    /// </summary>
    public class MarkerIndexModel
    {
        /// <summary>
        /// 搜索文本
        /// </summary>
        public string Search { get; set; }
        /// <summary>
        /// 分页结果
        /// </summary>
        public PagingResult<MarkerView> PagingResult { get; set; }
    }
}
