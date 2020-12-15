using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleMap.Libs.Entity;
using GoogleMap.Libs.Extensions;
using GoogleMap.Libs.Services;
using GoogleMap.Libs.Setting;
using GoogleMap.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoogleMap.ViewComponents
{
    public class TopNavbarViewComponent: ViewComponent
    {
        private readonly PersonService personService;
        private readonly ConfigService configService;

        public TopNavbarViewComponent(PersonService personService, ConfigService configService)
        {
            this.personService = personService ?? throw new ArgumentNullException(nameof(personService));
            this.configService = configService ?? throw new ArgumentNullException(nameof(configService));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            PersonView person = await this.personService.GetCurrentPersonViewAsync();
            SiteConfig config = await this.configService.GetConfigAsync<SiteConfig>();
            TopNavbarComponentModel model = new TopNavbarComponentModel
            {
                PersonView = person,
                SiteName = config.SiteName
            };
            return View(model);
        }
    }
}
