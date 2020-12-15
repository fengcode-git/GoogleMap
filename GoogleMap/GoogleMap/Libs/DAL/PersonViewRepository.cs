using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Ado;
using FengCode.Libs.Utils.Paging;
using GoogleMap.Libs.Entity;

namespace GoogleMap.Libs.DAL
{
    public class PersonViewRepository
    {
        private readonly DbConnection connection;

        public PersonViewRepository(DbConnection connection)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public async Task<PersonView> GetByIdAsync(Guid id)
        {
            string sql = "select * from person_view where id=@id limit 1;";
            return await this.connection.FirstOrDefaultAsync<PersonView>(sql, new { id = id });
        }

        public async Task<PersonView> GetByKeyAsync(string key)
        {
            string sql = "select * from person_view where api_key = @key limit 1;";
            return await this.connection.FirstOrDefaultAsync<PersonView>(sql, new { key = key });
        }

        public async Task<PagingResult<PersonView>> GetPagingResultAsync(string searchText, int currentPage = 1, int pageSize = 10)
        {
            SqlBuilder builder = new SqlBuilder();
            builder.AppendToEnd("from person_view where account_name != 'admin'");
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                builder.AppendToEnd("and account_name like @p").AddParameter("p", "%" + searchText.Trim() + "%");
            }
            SqlBuilder countBuilder = builder.Clone() as SqlBuilder;
            countBuilder.InsertToStart("select count(*)");
            long count = await this.connection.ScalarAsync<long>(countBuilder);
            builder.AppendToEnd("order by id");
            builder.SetPaging(currentPage, pageSize);
            builder.InsertToStart("select *");
            List<PersonView> personViews = await this.connection.SelectAsync<PersonView>(builder);
            return new PagingResult<PersonView>(currentPage, pageSize, count, personViews);
        }
    }
}
