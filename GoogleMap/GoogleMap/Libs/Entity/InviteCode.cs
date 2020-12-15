using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Ado;

namespace GoogleMap.Libs.Entity
{
    /// <summary>
    /// 邀请码
    /// </summary>
    public class InviteCode : BaseEntity
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 归属用户
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// 邀请码
        /// </summary>
        public string Code { get; set; }
    }
}
