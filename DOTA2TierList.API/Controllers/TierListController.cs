using AutoMapper;
using DOTA2TierList.API.Contracts.UserContracts;
using DOTA2TierList.API.Contracts;
using DOTA2TierList.Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DOTA2TierList.API.Contracts.TierListContracts;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Store;

namespace DOTA2TierList.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TierListController : Controller
    {
        private readonly TierListService _tierListService;

        private readonly IMapper _mapper;

        private readonly IValidator<TierListRequest> _validator;

        public TierListController(
            TierListService tierListService,
            IMapper mapper,
            IValidator<TierListRequest> validator)
        {
            _tierListService = tierListService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet("[action]/{id:long}")]
        public async Task<ActionResult> GetById(long id)
        {
            var tierList = await _tierListService.GetById(id);

            var response = _mapper.Map<TierListResponse>(tierList);

            return Json(response);
        }

        [HttpGet("[action]/{page:int}")]
        public async Task<ActionResult> GetByFilter(int page, int pageSize, TierListFilter filter)
        {
            var tierLists = await _tierListService.GetByPageFilter(page, pageSize, filter);

            var response = _mapper.Map<List<TierListPreviewResponse>>(tierLists);

            return Json(response);
        }

        [HttpPost("[action]")]
        [Authorize(Policy = "User")]
        public async Task<ActionResult> Create(TierListRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var userId = long.Parse(User.Claims.FirstOrDefault(i => i.Type == "userId")!.Value);

            var tierList = _mapper.Map<TierList>(request);

            tierList.UserId = userId;

            await _tierListService.Add(tierList);

            return Ok();
        }

        [HttpDelete("[action]/{id:long}")]
        [Authorize(Policy = "User")]
        public async Task<ActionResult> Delete(long id)
        {
            await _tierListService.Delete(id);

            return Ok();
        }

        [HttpPut("[action]/{id:long}")]
        [Authorize(Policy = "User")]
        public async Task<ActionResult> Update(TierListRequest request, long id)
        {
            await _validator.ValidateAndThrowAsync(request);

            var userId = long.Parse(User.Claims.FirstOrDefault(i => i.Type == "userId")!.Value);

            var tierList = _mapper.Map<TierList>(request);

            tierList.UserId = userId;

            tierList.Id = id;

            await _tierListService.Update(tierList);

            return Ok();
        }

    }
}
