using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Ado;
using GoogleMap.Libs.Entity;

namespace GoogleMap.Libs.DAL
{
    /// <summary>
    /// 地图标记存储库
    /// </summary>
    public class MarkerRepository : BaseRepository<Marker>
    {
        public MarkerRepository(DbConnection connection) : base(connection) { }

        /// <summary>
        /// 删除指定用户的所有标记
        /// </summary>
        public async Task<int> DeleteByPerson(Guid personId,DbTransaction transaction = null)
        {
            string sql = "delete from marker where person_id = @p;";
            return await this.DbConnection.ExecuteNonQueryAsync(sql, new { p = personId }, transaction);
        }

        /// <summary>
        /// 通过坐标获取标记
        /// </summary>
        public async Task<Marker> GetMarkerAsync(decimal lat,decimal lng)
        {
            string sql = "select * from marker where lng = @lng and lat = @lat limit 1;";
            lat = Math.Round(lat, 7);
            lng = Math.Round(lng, 7);
            return await this.DbConnection.FirstOrDefaultAsync<Marker>(sql, new { lat = lat, lng = lng });
        }

        /// <summary>
        /// 是否存在该坐标
        /// </summary>
        public async Task<bool> IsExistAsync(decimal lat, decimal lng)
        {
            string sql = "select count(*) from marker where lng = @lng and lat = @lat limit 1;";
            lat = Math.Round(lat, 7);
            lng = Math.Round(lng, 7);
            long count = await this.DbConnection.ScalarAsync<long>(sql, new { lat = lat, lng = lng });
            return count > 0;
        }
    }
}