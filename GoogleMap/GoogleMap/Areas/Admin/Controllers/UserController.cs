using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Utils.Paging;
using GoogleMap.Areas.Admin.Models;
using GoogleMap.Libs.Entity;
using GoogleMap.Libs.Enums;
using GoogleMap.Libs.Services;
using GoogleMap.Libs.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoogleMap.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : BaseController
    {
        private readonly PersonService personService;

        public UserController(PersonService personService)
        {
            this.personService = personService ?? throw new ArgumentNullException(nameof(personService));
        }

        public async Task<IActionResult> Index(string search = "", int page = 1)
        {
            PagingResult<PersonView> result = await this.personService.GetPagingResultAsync(search, page);
            UserIndexModel model = new UserIndexModel
            {
                Search = search,
                PagingResult = result
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            PersonView person = await this.personService.GetPersonViewAsync(id);
            if (person == null)
            {
                return this.NotFound();
            }
            UserEditModel model = new UserEditModel
            {
                Id = id,
                AccountName = person.AccountName,
                IsVerify = person.IsVerify,
                RoleType = person.RoleType
            };
            model.SelectListItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem("正式会员", ((int)RoleType.Member).ToString()));
            model.SelectListItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem("普通会员", ((int)RoleType.User).ToString()));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.personService.ModifyAsync(model);
                this.SetTempMessage("修改成功");
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await this.personService.DeleteAsync(id);
                return this.Json(AjaxResult.CreateDefaultSuccess());
            }
            catch (Exception ex)
            {
                return this.Json(AjaxResult.CreateByMessage(ex.Message, false));
            }
        }
    }
}
