using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleMap.Areas.Admin.Models;
using GoogleMap.Libs.Entity;
using GoogleMap.Libs.Extensions;
using GoogleMap.Libs.Services;
using GoogleMap.Libs.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoogleMap.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly PersonService personService;

        public HomeController(PersonService personService)
        {
            this.personService = personService ?? throw new ArgumentNullException(nameof(personService));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ModifyPassword()
        {
            ModifyPasswordModel model = new ModifyPasswordModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ModifyPassword(ModifyPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await this.personService.ModifyPasswordAsync(model);
                    return this.Json(AjaxResult.CreateDefaultSuccess());
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

        [HttpGet]
        public async Task<IActionResult> ApiKey()
        {
            PersonView person = await this.personService.GetCurrentPersonViewAsync();
            ApiKeyModel model = new ApiKeyModel { Key = person.ApiKey };
            return View(model);
        }
    }
}
