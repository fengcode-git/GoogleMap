using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FengCode.Libs.Utils.Paging;
using FengCode.Libs.Utils.Text;
using GoogleMap.Areas.Admin.Models;
using GoogleMap.Libs.DAL;
using GoogleMap.Libs.Entity;
using GoogleMap.Libs.Extensions;
using GoogleMap.Libs.Setting;
using GoogleMap.Libs.Utils;
using GoogleMap.Models;
using Microsoft.AspNetCore.Http;

namespace GoogleMap.Libs.Services
{
    public class PersonService
    {
        private readonly DbFactory dbFactory;
        private readonly EncryptService encryptService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public PersonService(DbFactory dbFactory, EncryptService encryptService, IHttpContextAccessor httpContextAccessor)
        {
            this.dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
            this.encryptService = encryptService ?? throw new ArgumentNullException(nameof(encryptService));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        /// <summary>
        /// 获取当前登录的用户，未登录将返回NULL
        /// </summary>
        public async Task<PersonView> GetCurrentPersonViewAsync()
        {
            Guid? id = this.httpContextAccessor.HttpContext.User.GetUserId();
            if (id == null)
            {
                return null;
            }
            else
            {
                using (var work = this.dbFactory.StartWork())
                {
                    return await work.PersonView.GetByIdAsync(id.Value);
                }
            }
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        public async Task<PersonView> GetPersonViewAsync(Guid id)
        {
            using (var work = this.dbFactory.StartWork())
            {
                return await work.PersonView.GetByIdAsync(id);
            }
        }

        /// <summary>
        /// 通过密钥获取用户
        /// </summary>
        public async Task<PersonView> GetPersonViewByKeyAsync(string key)
        {
            using (var work = this.dbFactory.StartWork())
            {
                return await work.PersonView.GetByKeyAsync(key);
            }
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        public async Task<Person> Register(AccountRegisterModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            string apiKey = this.encryptService.CreateNewHash();
            using (UnitOfWork work = this.dbFactory.StartWork())
            {
                RegisterConfig config = await work.Config.GetConfigAsync<RegisterConfig>();
                #region 检查邀请码
                if (config.IsEnableInviteCode)
                {
                    if (string.IsNullOrWhiteSpace(model.InviteCode))
                    {
                        throw new ModelException(nameof(model.InviteCode), "请填写邀请码");
                    }
                    else
                    {
                        string inputCode = model.InviteCode.Trim();
                        if (!(await work.InviteCode.IsExistCodeAsync(inputCode)))
                        {
                            throw new ModelException(nameof(model.InviteCode), "该邀请码不存在，请重新输入");
                        }
                    }
                }
                #endregion
                #region 检查账户
                if (model.UserName.ToLower().Contains("admin"))
                {
                    throw new ModelException(nameof(model.UserName), "该账户被系统保留，禁止使用该名称");
                }
                if (await work.Person.IsExistNameAsync(model.UserName))
                {
                    throw new ModelException(nameof(model.UserName), "该账户已被注册");
                }
                #endregion
                if (await work.Person.IsExistKeyAsync(apiKey))
                {
                    throw new Exception("请重新提交表单");
                }
                Role role = await work.Role.GetSingleAsync(Enums.RoleType.User);
                DateTime now = DateTime.Now;
                using (var trans = work.BeginTransaction())
                {
                    try
                    {
                        Person person = new Person
                        {
                            Id = GuidHelper.CreateSequential(),
                            AccountName = model.UserName.Trim(),
                            CreateTime = now,
                            RoleId = role.Id,
                            Password = this.encryptService.PasswordHash(model.Password),
                            ApiKey = apiKey,
                            IsVerify = true
                        };
                        await work.Person.InsertAsync(person, trans);
                        trans.Commit();
                        return person;
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 登录账号
        /// </summary>
        public async Task Login(AccountLoginModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            using (var work = this.dbFactory.StartWork())
            {               
                Person person = await work.Person.GetByAccountNameAsync(model.UserName);
                if (person == null)
                {
                    throw new ModelException(nameof(model.Password), "登录账号或密码错误");
                }
                else
                {
                    string password = this.encryptService.PasswordHash(model.Password);
                    if (person.Password != password)
                    {
                        throw new ModelException(nameof(model.Password), "登录账号或密码错误");
                    }
                    else
                    {
                        await this.httpContextAccessor.HttpContext.LoginAsync(person, model.IsRememberMe);
                    }
                }
            }
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        public async Task ModifyPasswordAsync(ModifyPasswordModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            string pwdHash = this.encryptService.PasswordHash(model.NewPassword);
            PersonView person = await this.GetCurrentPersonViewAsync();
            using (var work = this.dbFactory.StartWork())
            {
                if (person.Password != this.encryptService.PasswordHash(model.Password))
                {                  
                    throw new ModelException(nameof(model.Password), "登录密码错误");
                }
                using (var trans = work.BeginTransaction())
                {
                    try
                    {
                        await work.Person.ModifyPasswordAsync(person.Id, pwdHash, trans);
                        await work.Person.UpdateLastTimeAsync(person.Id, trans);
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 获取用户的分页数据
        /// </summary>
        public async Task<PagingResult<PersonView>> GetPagingResultAsync(string searchText, int currentPage = 1)
        {
            using (var work = this.dbFactory.StartWork())
            {
                return await work.PersonView.GetPagingResultAsync(searchText, currentPage, 10);
            }
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        public async Task ModifyAsync(UserEditModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            using (var work = this.dbFactory.StartWork())
            {
                if (model.RoleType == Enums.RoleType.Admin)
                {
                    throw new Exception("不能设置为管理员");
                }
                Role role = await work.Role.GetSingleAsync(model.RoleType);
                if (role == null)
                {
                    throw new Exception("该角色不存在");
                }
                await work.Person.ModifyAsync(model.Id, role, model.IsVerify);
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        public async Task DeleteAsync(Guid id)
        {
            using (var work = this.dbFactory.StartWork())
            {
                using (var trans = work.BeginTransaction())
                {
                    try
                    {
                        await work.Person.DeleteByIdAsync(id, trans);
                        await work.Marker.DeleteByPerson(id, trans);
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
