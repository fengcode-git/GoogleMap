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
    [Authorize]
    public class MarkerController : BaseController
    {
        private readonly PersonService personService;
        private readonly MarkerService markerService;

        public MarkerController(PersonService personService, MarkerService markerService)
        {
            this.personService = personService ?? throw new ArgumentNullException(nameof(personService));
            this.markerService = markerService ?? throw new ArgumentNullException(nameof(markerService));
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search = "", int page = 1)
        {
            PersonView person = await this.personService.GetCurrentPersonViewAsync();
            PagingResult<MarkerView> result = null;
            if (person.RoleType == RoleType.Admin)
            {
                // 如果是管理员，则查询所有标记
                result = await this.markerService.GetPagingResultAsync(search, null, page);
            }
            else
            {
                // 如果不是管理员，则只能查看自己创建的标记
                result = await this.markerService.GetPagingResultAsync(search, person, page);
            }
            MarkerIndexModel model = new MarkerIndexModel { Search = search, PagingResult = result };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            MarkerEditModel model = new MarkerEditModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MarkerEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Lng == 0)
                    {
                        ModelState.AddModelError(nameof(model.Lng), "不能为0");
                        return View(model);
                    }
                    else if (model.Lat == 0)
                    {
                        ModelState.AddModelError(nameof(model.Lat), "不能为0");
                        return View(model);
                    }
                    else
                    {
                        PersonView person = await this.personService.GetCurrentPersonViewAsync();
                        await this.markerService.InsertAsync(model, person);
                        this.SetTempMessage("提交成功");
                        return this.RedirectToAction(nameof(Create));
                    }
                }
                catch (Exception ex)
                {
                    this.SetTempMessage(ex.Message);
                    return View(model);
                }
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
                PersonView person = await this.personService.GetCurrentPersonViewAsync();
                await this.markerService.DeleteAsync(id, person);
                return this.Json(AjaxResult.CreateDefaultSuccess());
            }
            catch (Exception ex)
            {
                return this.Json(AjaxResult.CreateByMessage(ex.Message, false));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            PersonView person = await this.personService.GetCurrentPersonViewAsync();
            MarkerView markerView = await this.markerService.GetByIdAsync(id);
            if (markerView == null)
            {
                return this.NotFound();
            }
            else if (markerView.PersonId == person.Id || person.RoleType == RoleType.Admin)
            {
                MarkerEditModel model = new MarkerEditModel
                {
                    Id = markerView.Id,
                    Address = markerView.Address,
                    Explain = markerView.Explain,
                    Lat = markerView.Lat,
                    Lng = markerView.Lng,
                    Title = markerView.Title
                };
                return View(model);
            }
            else
            {
                return this.Forbid();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MarkerEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    PersonView person = await this.personService.GetCurrentPersonViewAsync();
                    await this.markerService.ModifyAsync(model, person);
                    this.SetTempMessage("修改成功");
                    return this.RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    this.SetTempMessage(ex.Message);
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
    }
}
