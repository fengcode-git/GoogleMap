using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleMap.Libs.Services;
using GoogleMap.Libs.Setting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoogleMap.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ConfigController : BaseController
    {
        private readonly ConfigService configService;

        public ConfigController(ConfigService configService)
        {
            this.configService = configService ?? throw new ArgumentNullException(nameof(configService));
        }

        /// <summary>
        /// 网站配置
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> SiteConfig()
        {
            SiteConfig config = await this.configService.GetConfigAsync<SiteConfig>();
            return View(config);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SiteConfig(SiteConfig model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                try
                {
                    await this.configService.SetConfigAsync(model);
                    this.SetTempMessage("操作成功");
                    return this.RedirectToAction("SiteConfig", "Config", new { Area = "Admin" });
                }
                catch (Exception e)
                {
                    this.SetTempMessage(e.Message);
                    return this.View(model);
                }
            }
        }

        /// <summary>
        /// OG协议配置
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> OGConfig()
        {
            OGConfig config = await this.configService.GetConfigAsync<OGConfig>();
            return View(config);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OGConfig(OGConfig model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                try
                {
                    await this.configService.SetConfigAsync(model);
                    this.SetTempMessage("操作成功");
                    return this.RedirectToAction("OGConfig", "Config", new { Area = "Admin" });
                }
                catch (Exception e)
                {
                    this.SetTempMessage(e.Message);
                    return this.View(model);
                }
            }
        }
    }
}
