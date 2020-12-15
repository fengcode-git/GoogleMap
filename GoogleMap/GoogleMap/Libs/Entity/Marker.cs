using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Ado;

namespace GoogleMap.Libs.Entity
{
    /// <summary>
    /// 地图标识
    /// </summary>
    public class Marker : BaseEntity
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 标识发布者
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [Column(DbType = "numeric(12,8)")]
        public decimal Lng { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [Column(DbType = "numeric(12,8)")]
        public decimal Lat { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Explain { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}

