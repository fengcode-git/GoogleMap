using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Ado;
using GoogleMap.Libs.Enums;

namespace GoogleMap.Libs.Entity
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : BaseEntity
    {
        public Guid Id { get; set; }

        [Index]
        public string Name { get; set; }

        [Index]
        public RoleType RoleType { get; set; }
    }
}
