using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleMap.Libs.Auth;
using GoogleMap.Libs.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GoogleMap
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSiteSession();
            services.AddCookieAuth(); // ���cookie��֤����
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>(); //ע�������ķ���������taghelperʹ��
            services.AddHttpClient();
            services.AddResponseCaching();
            services.AddResponseCompression();
            services.AddSiteService();
            services.AddSiteMvc();
            services.AddScoped<IAuthorizationHandler, RolesInDBAuthorizationHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePagesWithReExecute("/Error", "?statusCode={0}");
            app.UseDefaultFiles();
            app.UseResponseCaching(); // ��Ӧ����
            app.UseResponseCompression(); // ��Ӧѹ��
            app.UseStaticFilesWithCache(); // ��̬�ļ�����
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSiteSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "area", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id:guid?}");
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id:guid?}");
            });
        }
    }
}
