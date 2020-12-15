using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Mapper;
using GoogleMap.Areas.Admin.Models;
using GoogleMap.Libs.Entity;
using GoogleMap.Libs.Extensions;
using GoogleMap.Libs.Services;
using GoogleMap.Libs.Utils;
using GoogleMap.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoogleMap.Controllers
{
    public class ApiController : Controller
    {
        private readonly PersonService personService;
        private readonly MarkerService markerService;

        public ApiController(PersonService personService, MarkerService markerService)
        {
            this.personService = personService ?? throw new ArgumentNullException(nameof(personService));
            this.markerService = markerService ?? throw new ArgumentNullException(nameof(markerService));
        }

        [HttpPost]
        public async Task<IActionResult> InsertMarker([FromBody]ApiMarkerModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    PersonView person = await this.personService.GetPersonViewByKeyAsync(model.Key);
                    if (person == null)
                    {
                        throw new Exception("指定密钥不存在");
                    }
                    if (!person.IsVerify)
                    {
                        throw new Exception("该账户没有验证，无权调用API");
                    }
                    MarkerEditModel editModel = ObjectMapper.Map<ApiMarkerModel, MarkerEditModel>(model);
                    await this.markerService.InsertAsync(editModel, person);
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
    }
}
