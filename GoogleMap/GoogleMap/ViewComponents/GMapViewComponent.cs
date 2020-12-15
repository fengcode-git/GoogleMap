using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleMap.Libs.Entity;
using GoogleMap.Libs.Services;
using GoogleMap.Libs.Setting;
using GoogleMap.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoogleMap.ViewComponents
{
    public class GMapViewComponent : ViewComponent
    {
        private readonly MarkerService markerService;
        private readonly ConfigService configService;

        public GMapViewComponent(MarkerService markerService, ConfigService configService)
        {
            this.markerService = markerService ?? throw new ArgumentNullException(nameof(markerService));
            this.configService = configService ?? throw new ArgumentNullException(nameof(configService));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<MarkerView> markers = await this.markerService.GetAllAsync();
            SiteConfig config = await this.configService.GetConfigAsync<SiteConfig>();
            MapModel model = new MapModel { MarkerViews = markers, SiteConfig = config };
            return View(model);
        }
    }
}
