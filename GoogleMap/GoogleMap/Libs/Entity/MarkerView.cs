using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Ado;
using GoogleMap.Libs.Enums;

namespace GoogleMap.Libs.Entity
{
    /// <summary>
    /// 地图标记视图
    /// </summary>
    public class MarkerView : BaseEntity
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 标识发布者
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public decimal Lng { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
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

        /// <summary>
        /// 账号名称
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 仅验证的用户才能调用API接口
        /// </summary>
        public bool IsVerify { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色类型
        /// </summary>
        public RoleType RoleType { get; set; }
    }
}
