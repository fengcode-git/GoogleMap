using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Utils.Text;
using GoogleMap.Areas.Admin.Models;
using GoogleMap.Libs.DAL;
using GoogleMap.Libs.Entity;
using GoogleMap.Libs.Setting;
using GoogleMap.Libs.Utils;
using GoogleMap.Models;

namespace GoogleMap.Libs.Services
{
    /// <summary>
    /// 邀请码服务
    /// </summary>
    public class InviteService
    {
        private readonly DbFactory dbFactory;

        public InviteService(DbFactory dbFactory)
        {
            this.dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
        }

        /// <summary>
        /// 插入邀请码
        /// </summary>
        public async Task<InviteCode> InsertAsync(InviteEditModel model, PersonView person)
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
                if (await work.InviteCode.IsExistCodeAsync(model.Code))
                {
                    throw new ModelException(nameof(model.Code), "该邀请码已存在");
                }
                else
                {
                    InviteCode code = new InviteCode { Code = model.Code, Id = GuidHelper.CreateSequential(), PersonId = person.Id };
                    await work.InviteCode.InsertAsync(code);
                    return code;
                }
            }
        }

        /// <summary>
        /// 删除邀请码
        /// </summary>
        public async Task<int> DeleteAsync(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException(code);
            }
            using (var work = this.dbFactory.StartWork())
            {
                return await work.InviteCode.DeleteAsync(code);
            }
        }

        /// <summary>
        /// 获取所有邀请码
        /// </summary>
        public async Task<List<InviteCode>> GetAllAsync()
        {
            using (var work = this.dbFactory.StartWork())
            {
                return await work.InviteCode.GetAllAsync();
            }
        }

        /// <summary>
        /// 启用或禁止邀请码注册
        /// </summary>
        public async Task ChangeCodeRegister(bool isEnableInviteCode)
        {
            using (var work = this.dbFactory.StartWork())
            {
                RegisterConfig config = new RegisterConfig { IsEnableInviteCode = isEnableInviteCode };
                await work.Config.SetConfigAsync<RegisterConfig>(config);
            }
        }
    }
}
