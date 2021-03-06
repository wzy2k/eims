﻿using Eims.Dto;
using Eims.IBLL;
using Eims.WebApi.Core;
using Eims.WebApi.Filter;
using Eims.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Unity;

namespace Eims.WebApi.Controllers
{
    //无需登录
    [WebApiExceptionFilter, RoutePrefix(prefix: "api/public")]
    public class PublicController : ApiController
    {
        [Dependency]
        public IStaffManager staffManager { get; set; }
        [Dependency]
        public IArticleManager articleManager { get; set; }
        [Dependency]
        public ISuggestManager suggestManager { get; set; }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("login"), HttpPost]
        public async Task<Result> login([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                AccountDto user = await staffManager._login(model.Id, model.Password);
                if (user == null)
                    return Result.Fail("账号或密码错误");
                return Result.Success(new UserViewModel()
                {
                    Token = JwtTools.Encode(new Dictionary<string, object>(){
                        { "Id",user.Id },
                        { "RoleId",user.RoleId }
                    }),
                    Id = user.Id,
                    Role = (int)user.RoleId
                });
            }
            return Result.Fail("输入不规范");
        }

        /// <summary>
        /// 分页获取文章
        /// </summary>
        /// <param name="ps"></param>
        /// <param name="pi"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [Route("article"), HttpGet]
        public async Task<Result> article(int ps, int pi, string key)
        {
            return Result.Success(await articleManager._getPage(ps, pi, key));
        }

        /// <summary>
        /// 公开反馈
        /// </summary>
        /// <param name="ps"></param>
        /// <param name="pi"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [Route("suggest"), HttpGet]
        public async Task<Result> suggest(int ps, int pi, string key)
        {
            return Result.Success(await suggestManager._getPage(ps, pi, key));
        }
    }
}