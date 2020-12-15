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
    /// OG协议视图组件
    /// </summary>
    public class OgMetaViewComponent: ViewComponent
    {
        private readonly ConfigService configService;

        public OgMetaViewComponent(ConfigService configService)
        {
            this.configService = configService ?? throw new ArgumentNullException(nameof(configService));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            OGConfig config = await this.configService.GetConfigAsync<OGConfig>();
            return View(config);
        }
    }
}
