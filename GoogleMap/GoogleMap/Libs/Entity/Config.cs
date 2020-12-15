using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Ado;

namespace GoogleMap.Libs.Entity
{
    /// <summary>
    /// 网站配置
    /// </summary>
    public class Config : BaseEntity
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 键
        /// </summary>
        public string ConfigKey { get; set; }
        /// <summary>
        /// 值（一般存储JSON格式）
        /// </summary>
        public string ConfigValue { get; set; }
    }
}
