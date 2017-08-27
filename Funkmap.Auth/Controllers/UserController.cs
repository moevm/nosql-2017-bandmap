﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Funkmap.Auth.Data.Abstract;
using Funkmap.Auth.Data.Objects;
using Funkmap.Common.Auth;
using Funkmap.Common.Filters;
using Funkmap.Common.Models;
using Funkmap.Module.Auth.Models;

namespace Funkmap.Module.Auth.Controllers
{
    [RoutePrefix("api/user")]
    [ValidateRequestModel]
    public class UserController : ApiController
    {
        private readonly IAuthRepository _authRepository;

        public UserController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }


        [HttpGet]
        [Authorize]
        [Route("lastVisit")]
        public async Task<IHttpActionResult> UpdateLastVisitDate()
        {
            var response = new BaseResponse();
            var login = Request.GetLogin();
            await _authRepository.UpdateLastVisitDateAsync(login, DateTime.UtcNow);

            return Ok(response);
        }

        [HttpGet]
        [Route("avatar/{login}")]
        public async Task<IHttpActionResult> GetAvatar(string login)
        {
            var images = await _authRepository.GetAvatarsAsync(new [] {login });
            return Ok(images.Select(x=>x.Avatar).FirstOrDefault());
        }

        [HttpPost]
        [Route("avatars")]
        public async Task<IHttpActionResult> GetAvatars(string[] logins)
        {
            ICollection<UserAvatarResult> avatarResults = await _authRepository.GetAvatarsAsync(logins);
            return Ok(avatarResults);
        }

        [HttpPost]
        [Route("saveAvatar")]
        public async Task<IHttpActionResult> SaveAvatar(SaveImageRequest request)
        {
            var response = new BaseResponse();
            await _authRepository.SaveAvatarAsync(request.Login, request.Avatar);
            response.Success = true;
           
            return Ok(response);
        }
    }
}
