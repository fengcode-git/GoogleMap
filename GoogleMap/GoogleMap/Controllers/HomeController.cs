using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Image;
using FengCode.Libs.Utils.Text;
using GoogleMap.Libs.Extensions;
using GoogleMap.Libs.Services;
using GoogleMap.Libs.Setting;
using GoogleMap.Libs.Utils;
using GoogleMap.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GoogleMap.Controllers
{
    [Route("[action]")]
    public class HomeController : Controller
    {
        private readonly ConfigService configService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,ConfigService service)
        {
            _logger = logger;
            this.configService = service;
        }
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            SiteConfig config = await configService.GetConfigAsync<SiteConfig>();
            HomeIndexModel model = new HomeIndexModel { SiteConfig = config };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> CodeImage()
        {
            string code = RandomHelper.GetVerifyCode();
            ImageWrapper image = ImageWrapper.CreateByVerifyCode(code);
            await HttpContext.Session.SetVerifyCodeAsync(code);
            return new ImageStreamResult(image.ImageBytes);
        }
    }
}
