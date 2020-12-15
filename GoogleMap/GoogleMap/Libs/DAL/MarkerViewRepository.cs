using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Ado;
using FengCode.Libs.Utils.Paging;
using GoogleMap.Libs.Entity;
using GoogleMap.Libs.Enums;

namespace GoogleMap.Libs.DAL
{
    public class MarkerViewRepository
    {
        private readonly DbConnection connection;

        public MarkerViewRepository(DbConnection connection)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="search"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagingResult<MarkerView>> GetPagingResultAsync(string searchText, PersonView person = null, int currentPage = 1, int pageSize = 10)
        {
            SqlBuilder builder = new SqlBuilder();
            builder.AppendToEnd("from marker_view where 1=1");
            // 增加指定用户的搜索条件
            if (person != null)
            {
                builder.AppendToEnd("and person_id = @personId").AddParameter("personId", person.Id);
            }
            // 当搜索文本不为空时，添加搜索条件
            searchText = string.IsNullOrWhiteSpace(searchText) ? "" : searchText.Trim();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                builder.AppendToEnd("and (");
                builder.AppendToEnd("title like @title").AddParameter("title", "%" + searchText + "%");
                builder.AppendToEnd("or explain like @explain").AddParameter("explain", "%" + searchText + "%");
                builder.AppendToEnd("or address like @address").AddParameter("address", "%" + searchText + "%");
                builder.AppendToEnd(")");
            }
            SqlBuilder countBuilder = builder.Clone() as SqlBuilder;
            // 查询总数
            countBuilder.InsertToStart("select count(*)");
            long count = await this.connection.ScalarAsync<long>(countBuilder);
            // 获取分页数据
            builder.AppendToEnd("order by create_time desc");
            builder.SetPaging(currentPage, pageSize);
            builder.InsertToStart("select *");
            List<MarkerView> markers = await this.connection.SelectAsync<MarkerView>(builder);
            PagingResult<MarkerView> result = new PagingResult<MarkerView>(currentPage, pageSize, count, markers);
            return result;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        public async Task<List<MarkerView>> GetAllAsync(DisplayType display)
        {
            SqlBuilder builder = new SqlBuilder();
            builder.AppendToEnd("select * from marker_view where 1=1");
            if (display == DisplayType.Member)
            {
                builder.AppendToEnd("and role_type = @roleType").AddParameter("roleType",RoleType.Member);
            }
            if (display == DisplayType.User)
            {
                builder.AppendToEnd("and role_type = @roleType").AddParameter("roleType", RoleType.User);
            }
            List<MarkerView> markers = await this.connection.SelectAsync<MarkerView>(builder);
            return markers;
        }

        /// <summary>
        /// 通过ID获取标记视图
        /// </summary>
        public async Task<MarkerView> GetByIdAsync(Guid id)
        {
            string sql = "select * from marker_view where id=@id;";
            return await this.connection.FirstOrDefaultAsync<MarkerView>(sql, new { id = id });
        }
    }
}
