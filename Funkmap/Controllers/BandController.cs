﻿using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Funkmap.Common.Auth;
using Funkmap.Common.Models;
using Funkmap.Data.Entities;
using Funkmap.Data.Repositories.Abstract;
using Funkmap.Mappers;
using Funkmap.Models;
using Funkmap.Tools;

namespace Funkmap.Controllers
{
    [RoutePrefix("api/band")]
    public class BandController: ApiController
    {
        private readonly IBandRepository _bandRepository;

        public BandController(IBandRepository bandRepository)
        {
            _bandRepository = bandRepository;
        }
        
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IHttpActionResult> GetBand(string id)
        {
            var bandEntity = await _bandRepository.GetAsync(id);
            var band = bandEntity.ToModelPreview();
            return Content(HttpStatusCode.OK, band);
        }

        [HttpGet]
        [Route("getFull/{id}")]
        public async Task<IHttpActionResult> GetFullMusician(string id)
        {
            var musicianEntity = await _bandRepository.GetAsync(id);
            BandModel musican = musicianEntity.ToModel();
            return Content(HttpStatusCode.OK, musican);

        }

        [Authorize]
        [HttpPost]
        [Route("save")]
        public async Task<IHttpActionResult> SaveMusician(BandModel model)
        {
            var entity = model.ToBandEntity();
            var response = new BaseResponse();

            var existingBand = await _bandRepository.GetAsync(model.Login);
            if (existingBand != null)
            {
                return Content(HttpStatusCode.OK, response);
            }

            var userLogin = Request.GetLogin();
            entity.UserLogin = userLogin;

            await _bandRepository.CreateAsync(entity);
            response.Success = true;
            return Content(HttpStatusCode.OK, response);

        }


        [Authorize]
        [HttpPost]
        [Route("edit")]
        public async Task<IHttpActionResult> EditMusician(BandModel model)
        {
            var entity = model.ToBandEntity();
            var response = new BaseResponse();

            var existingBand = await _bandRepository.GetAsync(model.Login);
            if (existingBand == null)
            {
                return Content(HttpStatusCode.OK, response);
            }

            var userLogin = Request.GetLogin();
            entity.UserLogin = userLogin;

            existingBand.FillEntity<BandEntity>(entity);

            await _bandRepository.UpdateAsync(existingBand);
            response.Success = true;
            return Content(HttpStatusCode.OK, response);

        }
    }
}
