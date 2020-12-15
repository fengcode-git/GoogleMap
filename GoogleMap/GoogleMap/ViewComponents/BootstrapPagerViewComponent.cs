using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleMap.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoogleMap.ViewComponents
{
    /// <summary>
    /// bootstrap分页组件
    /// </summary>
    public class BootstrapPagerViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int forPageSize, long forRowCount)
        {
            BootstrapPagerModel model = new BootstrapPagerModel(forPageSize, forRowCount, this.HttpContext);
            return View(model);
        }
    }
}