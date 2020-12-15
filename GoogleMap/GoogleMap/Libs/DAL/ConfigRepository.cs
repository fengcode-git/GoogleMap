using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Ado;
using FengCode.Libs.Utils.Text;
using GoogleMap.Libs.Entity;
using GoogleMap.Libs.Utils;

namespace GoogleMap.Libs.DAL
{
    /// <summary>
    /// 网站配置存储库
    /// </summary>
    public class ConfigRepository : BaseRepository<Config>
    {
        public ConfigRepository(DbConnection connection) : base(connection) { }

        /// <summary>
        /// 通过名称获取配置
        /// </summary>
        public async Task<Config> GetConfigFormKeyAsync(string key)
        {
            return await this.DbConnection.FirstOrDefaultAsync<Config>("SELECT * FROM config WHERE config_key = @key;", new { key = key });
        }

        /// <summary>
        /// 设置配置项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> SetConfigAsync<T>(T obj, DbTransaction transaction = null) where T : class, new()
        {
            string keyName = typeof(T).Name;
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            string json = JsonHelper.ToJson<T>(obj);
            Config config = await this.GetConfigFormKeyAsync(keyName);
            if (config == null)
            {
                config = new Config
                {
                    Id = GuidHelper.CreateSequential(),
                    ConfigKey = keyName,
                    ConfigValue = json
                };
                return await this.InsertAsync(config, transaction);
            }
            else
            {
                config.ConfigValue = json;
                return await this.UpdateAsync(config, transaction);
            }
        }
        /// <summary>
        /// 获取配置项
        /// </summary>
        public async Task<T> GetConfigAsync<T>() where T : class, new()
        {
            string keyName = typeof(T).Name;
            Config config = await this.GetConfigFormKeyAsync(keyName);
            if (config == null || string.IsNullOrWhiteSpace(config.ConfigValue))
            {
                return new T();
            }
            else
            {
                return JsonHelper.FromJson<T>(config.ConfigValue);
            }
        }
    }
}
