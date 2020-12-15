using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleMap.Libs.Services;
using GoogleMap.Libs.Setting;
using Microsoft.AspNetCore.Mvc;

namespace GoogleMap.ViewComponents
{
    /// <summary>
    /// 页脚说明
    /// </summary>
    public class CopyrightViewComponent: ViewComponent
    {
        private readonly ConfigService configService;

        public CopyrightViewComponent(ConfigService configService)
        {
            this.configService = configService ?? throw new ArgumentNullException(nameof(configService));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            SiteConfig config = await this.configService.GetConfigAsync<SiteConfig>();
            this.ViewData["_SiteFooter"] = config.SiteFooter;
            return View();
        }
    }
}
