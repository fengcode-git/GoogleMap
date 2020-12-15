using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Ado;
using FengCode.Libs.Utils.Text;
using GoogleMap.Libs.DAL;
using GoogleMap.Libs.Entity;
using GoogleMap.Libs.Enums;
using GoogleMap.Libs.Setting;
using GoogleMap.Libs.Utils;
using Microsoft.Extensions.Logging;

namespace GoogleMap.Libs.Services
{
    /// <summary>
    /// 网站数据服务
    /// </summary>
    public class DbService
    {
        private readonly DbFactory dbFactory;
        private readonly ILogger<DbService> logger;
        private readonly EncryptService encryptService;
        public DbService(DbFactory factory, ILogger<DbService> logger, EncryptService encrypt)
        {
            this.dbFactory = factory;
            this.logger = logger;
            this.encryptService = encrypt;
        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        public async Task InitAsync()
        {
            using (DbConnection conn = this.dbFactory.Create())
            {
                conn.Open();
                long count = await conn.ScalarAsync<long>("select count(*) from pg_tables where schemaname='public';");
                if (count > 0)
                {
                    this.logger.LogInformation("The data table already exists in the database, the initialization process will be skipped.");
                    // 如果数据库存在数据表，则不跳过初始化
                    return;
                }
                else
                {
                    //    // 创建数据表
                    this.logger.LogInformation("Start creating data table");
                    await conn.CreateTableAsync<Person>();
                    await conn.CreateTableAsync<Marker>();
                    await conn.CreateTableAsync<Role>();
                    await conn.CreateTableAsync<Config>();
                    await conn.CreateTableAsync<InviteCode>();
                    // 创建视图
                    await conn.ExecuteNonQueryAsync("create view person_view as select p.*,r.name as role_name,r.role_type from person as p inner join role r on r.id = p.role_id;");
                    await conn.ExecuteNonQueryAsync("create view marker_view as select m.*,p.account_name,p.is_verify,r.name as role_name,r.role_type from marker as m inner join person p on p.id = m.person_id  inner join role r on p.role_id = r.id;");
                    //    // 创建角色
                    Role adminRole = new Role() { Id = GuidHelper.CreateSequential(), Name = "管理员", RoleType = RoleType.Admin };
                    Role memberRole = new Role() { Id = GuidHelper.CreateSequential(), Name = "正式会员", RoleType = RoleType.Member };
                    Role userRole = new Role() { Id = GuidHelper.CreateSequential(), Name = "普通会员", RoleType = RoleType.User };
                    DateTime now = DateTime.Now;
                    Guid id = GuidHelper.CreateSequential();
                    Person admin = new Person()
                    {
                        Id = id,
                        AccountName = "admin",
                        RoleId = adminRole.Id,
                        Password = this.encryptService.PasswordHash("12345678"),
                        CreateTime = now,
                        ApiKey = this.encryptService.CreateNewHash()
                    };
                    DbConfig dbConfig = new DbConfig { Version = new Version(1, 0, 0, 0) };
                    Config config = new Config
                    {
                        Id = GuidHelper.CreateSequential(),
                        ConfigKey = nameof(DbConfig),
                        ConfigValue = JsonHelper.ToJson<DbConfig>(dbConfig)
                    };
                    using (DbTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            this.logger.LogInformation("Start initializing data");
                            await conn.InsertAsync(adminRole, trans);
                            await conn.InsertAsync(memberRole, trans);
                            await conn.InsertAsync(userRole, trans);
                            await conn.InsertAsync(admin, trans);
                            await conn.InsertAsync(config, trans);
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            this.logger.LogError(ex, "Initialization data error");
                            trans.Rollback();
                            throw;
                        }
                    }
                }
            }
        }
    }
}
