using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Ado;
using GoogleMap.Libs.Entity;
using GoogleMap.Libs.Enums;

namespace GoogleMap.Libs.DAL
{
    public class PersonRepository : BaseRepository<Person>
    {
        public PersonRepository(DbConnection connection) : base(connection) { }

        /// <summary>
        /// 同时检查账号、昵称是否存在
        /// </summary>
        public async Task<bool> IsExistNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(name);
            }
            name = name.Trim().ToUpper();
            string sql = "select count(*) from person where upper(account_name) =@p;";
            long count = await this.DbConnection.ScalarAsync<long>(sql, new { p = name });
            return count > 0;
        }

        /// <summary>
        /// 通过账号获取用户
        /// </summary>
        public async Task<Person> GetByAccountNameAsync(string accountName)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentNullException(nameof(accountName));
            }
            accountName = accountName.Trim().ToUpper();
            string sql = "select * from person where upper(account_name) = @p limit 1;";
            return await this.DbConnection.FirstOrDefaultAsync<Person>(sql, new { p = accountName });
        }

        /// <summary>
        /// 通过开发Key获取用户
        /// </summary>
        public async Task<Person> GetByKeyAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            key = key.Trim().ToUpper();
            string sql = "select * from person where upper(api_key) = @p limit 1;";
            return await this.DbConnection.FirstOrDefaultAsync<Person>(sql, new { p = key });
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        public async Task<int> ModifyPasswordAsync(Guid personId, string pwdHash, DbTransaction trans)
        {
            if (string.IsNullOrWhiteSpace(pwdHash))
            {
                throw new ArgumentNullException(nameof(pwdHash));
            }
            string sql = "update person set password = @p where id=@id;";
            return await this.DbConnection.ExecuteNonQueryAsync(sql, new { p = pwdHash, id = personId }, trans);
        }

        /// <summary>
        /// 刷新用户的更新时间
        /// </summary>
        public async Task<int> UpdateLastTimeAsync(Guid personId, DbTransaction trans)
        {
            string sql = "update person set last_updated = @p where id=@id;";
            return await this.DbConnection.ExecuteNonQueryAsync(sql, new { p = DateTime.Now, id = personId }, trans);
        }

        /// <summary>
        /// 检查是否存在Key
        /// </summary>
        public async Task<bool> IsExistKeyAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            string sql = "select count(*) from person where upper(api_key) =@p;";
            key = key.Trim().ToUpper();
            long count = await this.DbConnection.ScalarAsync<long>(sql, new { p = key });
            return count > 0;
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        public async Task<int> ModifyAsync(Guid id, Role role, bool isVerify)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            string sql = "update person set role_id = @roleId,is_verify = @isVerify where id = @id";
            return await this.DbConnection.ExecuteNonQueryAsync(sql, new { roleId = role.Id, isVerify = isVerify, id = id });
        }
    }
}
