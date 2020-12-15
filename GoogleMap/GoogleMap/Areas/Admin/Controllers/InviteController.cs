using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleMap.Areas.Admin.Models;
using GoogleMap.Libs.Entity;
using GoogleMap.Libs.Extensions;
using GoogleMap.Libs.Services;
using GoogleMap.Libs.Setting;
using GoogleMap.Libs.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoogleMap.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InviteController : BaseController
    {
        private readonly InviteService inviteService;
        private readonly PersonService personService;
        private readonly ConfigService configService;

        public InviteController(InviteService inviteService, PersonService personService, ConfigService configService)
        {
            this.inviteService = inviteService ?? throw new ArgumentNullException(nameof(inviteService));
            this.personService = personService ?? throw new ArgumentNullException(nameof(personService));
            this.configService = configService ?? throw new ArgumentNullException(nameof(configService));
        }

        public async Task<IActionResult> Index()
        {
            RegisterConfig config = await this.configService.GetConfigAsync<RegisterConfig>();
            InviteIndexModel model = new InviteIndexModel { IsEnableInviteCode = config.IsEnableInviteCode };
            return View(model);
        }

        public IActionResult Create()
        {
            InviteEditModel model = new InviteEditModel { Code = "" };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<InviteCode> codes = await this.inviteService.GetAllAsync();
                return this.Json(AjaxResult.CreateByContext(codes));
            }
            catch (Exception ex)
            {
                return this.Json(AjaxResult.CreateByMessage(ex.Message, false));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(InviteEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    PersonView person = await this.personService.GetCurrentPersonViewAsync();
                    InviteCode code = await this.inviteService.InsertAsync(model, person);
                    return this.Json(AjaxResult.CreateByContext(code));
                }
                catch (ModelException ex)
                {
                    return this.Json(ex.ToAjaxResult());
                }
                catch (Exception ex)
                {
                    return this.Json(AjaxResult.CreateByMessage(ex.Message, false));
                }
            }
            else
            {
                return this.Json(ModelState.ToAjaxResult());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string code)
        {
            try
            {
                await this.inviteService.DeleteAsync(code);
                return this.Json(AjaxResult.CreateDefaultSuccess());
            }
            catch (Exception ex)
            {
                return this.Json(AjaxResult.CreateByMessage(ex.Message, false));
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangeCodeRegister(bool status)
        {
            try
            {
                await this.inviteService.ChangeCodeRegister(status);
                return this.Json(AjaxResult.CreateDefaultSuccess());
            }
            catch (Exception ex)
            {
                return this.Json(AjaxResult.CreateByMessage(ex.Message, false));
            }
        }

    }
}
