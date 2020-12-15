using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleMap.Libs.DAL
{
    public class UnitOfWork : IDisposable
    {
        public DbConnection Connection { get; }

        public UnitOfWork(DbConnection connection)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            this.Person = new PersonRepository(this.Connection);
            this.Config = new ConfigRepository(this.Connection);
            this.PersonView = new PersonViewRepository(this.Connection);
            this.Role = new RoleRepository(this.Connection);
            this.InviteCode = new InviteCodeRepository(this.Connection);
            this.Marker = new MarkerRepository(this.Connection);
            this.MarkerView = new MarkerViewRepository(this.Connection);
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        public DbTransaction BeginTransaction()
        {
            return this.Connection.BeginTransaction();
        }

        /// <summary>
        /// 成员
        /// </summary>
        public PersonRepository Person { get; }

        /// <summary>
        /// 配置
        /// </summary>
        public ConfigRepository Config { get; }

        /// <summary>
        /// 成员视图
        /// </summary>
        public PersonViewRepository PersonView { get; }

        /// <summary>
        /// 邀请码
        /// </summary>
        public InviteCodeRepository InviteCode { get; }
        /// <summary>
        /// 角色
        /// </summary>
        public RoleRepository Role { get; }

        /// <summary>
        /// 标记
        /// </summary>
        public MarkerRepository Marker { get; }

        /// <summary>
        /// 标记视图
        /// </summary>
        public MarkerViewRepository MarkerView { get; }

        public void Dispose()
        {
            this.Connection.Dispose();
        }
    }
}
