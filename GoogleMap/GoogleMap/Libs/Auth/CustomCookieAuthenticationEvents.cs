﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Utils.Text;
using GoogleMap.Libs.DAL;
using GoogleMap.Libs.Entity;
using GoogleMap.Libs.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GoogleMap.Libs.Auth
{
    /// <summary>
    /// 验证客户端cookie的真实性
    /// </summary>
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly DbFactory dbFactory;

        public CustomCookieAuthenticationEvents(DbFactory dbFactory)
        {
            this.dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
        }

        public async override Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            Guid? userId = context.Principal.GetUserId();
            var aa = context.HttpContext;
            if (userId == null)
            {
                context.RejectPrincipal();
                return;
            }
            else
            {
                using (var work = this.dbFactory.StartWork())
                {
                    Person person = await work.Person.SingleByIdAsync(userId.Value);
                    if (person == null)
                    {
                        // 用户不存在，或已被删除，则拒绝这个cookie
                        context.RejectPrincipal();
                        await context.HttpContext.LogoutAsync();
                    }
                    else
                    {
                        var userPrincipal = context.Principal;
                        string lastChanged = (from c in userPrincipal.Claims where c.Type == nameof(person.LastUpdated) select c.Value).FirstOrDefault();
                        if (FormatHelper.ToTime(person.LastUpdated) != lastChanged)
                        {
                            context.RejectPrincipal(); // 验证失败
                            await context.HttpContext.LogoutAsync();
                        }
                    }
                }
            }
        }
    }
}
