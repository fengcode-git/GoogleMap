using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Mapper;
using FengCode.Libs.Utils.Paging;
using FengCode.Libs.Utils.Text;
using GoogleMap.Areas.Admin.Models;
using GoogleMap.Libs.DAL;
using GoogleMap.Libs.Entity;
using GoogleMap.Libs.Enums;
using GoogleMap.Libs.Setting;
using GoogleMap.Libs.Utils;

namespace GoogleMap.Libs.Services
{
    /// <summary>
    /// 标记服务
    /// </summary>
    public class MarkerService
    {
        private readonly DbFactory dbFactory;
        private readonly HtmlService htmlService;
        private readonly ConfigService configService;

        public MarkerService(DbFactory dbFactory, HtmlService htmlService, ConfigService configService)
        {
            this.dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
            this.htmlService = htmlService ?? throw new ArgumentNullException(nameof(htmlService));
            this.configService = configService ?? throw new ArgumentNullException(nameof(configService));
        }

        /// <summary>
        /// 获取标记的分页结果
        /// </summary>
        public async Task<PagingResult<MarkerView>> GetPagingResultAsync(string searchText, PersonView person = null, int currentPage = 1)
        {
            using (var work = this.dbFactory.StartWork())
            {
                return await work.MarkerView.GetPagingResultAsync(searchText, person, currentPage, 10);
            }
        }

        /// <summary>
        /// 获取所有标记
        /// </summary>
        public async Task<List<MarkerView>> GetAllAsync()
        {
            using (var work = this.dbFactory.StartWork())
            {
                SiteConfig config = await work.Config.GetConfigAsync<SiteConfig>();
                return await work.MarkerView.GetAllAsync(config.DisplayType);
            }
        }

        /// <summary>
        /// 增加标记
        /// </summary>
        public async Task<int> InsertAsync(MarkerEditModel model,PersonView person)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }
            if (model.Lat == 0)
            {
                throw new ModelException(nameof(model.Lat), "纬度不能为0");
            }
            if (model.Lng == 0)
            {
                throw new ModelException(nameof(model.Lng), "经度不能为0");
            }
            using (var work = this.dbFactory.StartWork())
            {

                if (await work.Marker.IsExistAsync(model.Lat,model.Lng))
                {
                    throw new Exception("该经纬度的标识已存在，请勿重复添加");
                }
                Marker marker = new Marker
                {
                    Id = GuidHelper.CreateSequential(),
                    Address = this.htmlService.HtmlToText(model.Address).Trim(),
                    CreateTime = DateTime.Now,
                    Explain = this.htmlService.HtmlToText(model.Explain).Trim(),
                    Lat = model.Lat,
                    Lng = model.Lng,
                    PersonId = person.Id,
                    Title = this.htmlService.HtmlToText(model.Title).Trim()
                };
                return await work.Marker.InsertAsync(marker);
            }
        }

        public async Task<int> ModifyAsync(MarkerEditModel model,PersonView person)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }
            using (var work = this.dbFactory.StartWork())
            {
                Marker dbMarker = await work.Marker.SingleByIdAsync(model.Id);
                if (person.RoleType == RoleType.Admin || dbMarker.PersonId == person.Id)
                {
                    ObjectMapper.CopyValues<MarkerEditModel, Marker>(model, dbMarker);
                    return await work.Marker.UpdateAsync(dbMarker);
                }
                else
                {
                    throw new Exception("只能本人或管理员修改该标记");
                }
            }
        }

        /// <summary>
        /// 删除标记
        /// </summary>
        public async Task<int> DeleteAsync(Guid id,PersonView person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }
            using (var work = this.dbFactory.StartWork())
            {
                Marker marker = await work.Marker.SingleByIdAsync(id);
                if (marker == null)
                {
                    throw new Exception("该标记不存在或已被删除");
                }
                if (marker.PersonId == person.Id || person.RoleType== RoleType.Admin)
                {
                    return await work.Marker.DeleteByIdAsync(id);
                }
                else
                {
                    throw new Exception("该标记仅本人或管理员才能删除");
                }               
            }
        }

        /// <summary>
        /// 通过ID获取标记
        /// </summary>
        public async Task<MarkerView> GetByIdAsync(Guid id)
        {
            using (var work = this.dbFactory.StartWork())
            {
                return await work.MarkerView.GetByIdAsync(id);
            }
        }
    }
}
