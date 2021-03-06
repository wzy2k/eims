﻿using Eims.Dto;
using Eims.IBLL;
using Eims.WebApi.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace Eims.WebApi.Controllers
{
    [RoutePrefix("api/suggest")]
    public class SuggestController : BaseController<SuggestDto, ISuggestManager>
    {
        [Route("staff"), HttpGet]
        public async Task<Result> WithStaff(int ps, int pi, string key)
        {
            return Result.Success(await manager._getPageSuggestWithStaff(ps, pi, key));
        }

        [Route("staff"), HttpGet]
        public async Task<Result> WithStaff(int ps, int pi, int fkid)
        {
            return Result.Success(await manager._getPageSuggestWithStaff(ps, pi, fkid));
        }

        [Route("staff/{id}"), HttpGet]
        public async Task<Result> WithStaff(int id)
        {
            return Result.Success(await manager._getOneSuggestWithStaff(id));
        }
    }
}