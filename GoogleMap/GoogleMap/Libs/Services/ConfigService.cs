using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleMap.Libs.DAL;

namespace GoogleMap.Libs.Services
{
    public class ConfigService
    {
        private DbFactory DbFactory { get; set; }


        public ConfigService(DbFactory dbFactory)
        {
            DbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
        }

        public async Task<int> SetConfigAsync<T>(T config) where T : class, new()
        {
            using (var work = this.DbFactory.StartWork())
            {
                return await work.Config.SetConfigAsync<T>(config);
            }
        }

        public async Task<T> GetConfigAsync<T>() where T : class, new()
        {
            using (var work = this.DbFactory.StartWork())
            {
                return await work.Config.GetConfigAsync<T>();
            }
        }
    }
}
