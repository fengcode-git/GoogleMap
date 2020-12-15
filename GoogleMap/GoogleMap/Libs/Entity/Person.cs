using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Ado;

namespace GoogleMap.Libs.Entity
{
    public class Person : BaseEntity
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId { get; set; }

        public string AccountName { get; set; }

        public string Password { get; set; }
        /// <summary>
        /// API 接口密钥
        /// </summary>
        public string ApiKey { get; set; }
        /// <summary>
        /// 仅验证的用户才能调用API接口
        /// </summary>
        public bool IsVerify { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 最近修改时间
        /// </summary>
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}
