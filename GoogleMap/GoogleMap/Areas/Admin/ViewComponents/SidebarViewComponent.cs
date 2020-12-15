using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleMap.Areas.Admin.Models;
using GoogleMap.Libs.Entity;
using GoogleMap.Libs.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoogleMap.Areas.Admin.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly PersonService personService;

        public SidebarViewComponent(PersonService personService)
        {
            this.personService = personService ?? throw new ArgumentNullException(nameof(personService));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            PersonView person = await this.personService.GetCurrentPersonViewAsync();
            SidebarComponentModel model = new SidebarComponentModel { PersonView = person };
            return View(model);
        }
    }
}
