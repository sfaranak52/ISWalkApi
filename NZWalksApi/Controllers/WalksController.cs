using AutoMapper;
using ISWalksApi.CustomActionFilters;
using ISWalksApi.Interfaces;
using ISWalksApi.Models.DTOs;
using ISWalksApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using NZWalksApi.Data;
using NZWalksApi.Models.Domain;

namespace NZWalksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }

        // Get Walks
        // Get: /api/walks?filterOn=Name&filterQuery=Jolfa&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pagenumber = 1, [FromQuery] int pageSize = 1000)
        {
            // Get Data From Database - Domain Models
            var walks = await _walkRepository.GetAllAsync(filterOn,filterQuery,sortBy,isAscending ?? true, pagenumber, pageSize);

            // Map Domain Models to DTOs
            //Return Dto
            return Ok(_mapper.Map<List<WalkDto>>(walks));
        }

        // Get Region By Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkModel = await _walkRepository.GetByIdAsync(id);
            if (walkModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDto>(walkModel));
        }

        [HttpPost]
        [ValidateModel]
        //POST : https://localhost:portnumber/api/walks
        public async Task<IActionResult> Create([FromBody] AddWalksRequestDto addWalksRequestDto)
        {
            //Map or convert DTO to Domain Model
            var walkModel = _mapper.Map<Walk>(addWalksRequestDto);

            //Use Domain mode to create region
            await _walkRepository.AddAsync(walkModel);

            //Map Main Model to DTO
            var walkDto = _mapper.Map<AddWalksRequestDto>(walkModel);
            return Ok(_mapper.Map<WalkDto>(walkModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalksRequestDto updateWalksRequestDto)
        {
            //Map DTO to Model 
            var walkModel = _mapper.Map<Walk>(updateWalksRequestDto);

            //check if region exists
            walkModel = await _walkRepository.UpdateAsync(id, walkModel);
            if (walkModel == null)
            {
                return NotFound();
            }

            //convert Domain Model to DTO
            var regionDto = _mapper.Map<RegionDto>(walkModel);

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionModel = await _walkRepository.GetByIdAsync(id);
            if (regionModel == null)
            {
                return NotFound();
            }

            //Delete Region
            await _walkRepository.DeleteAsync(regionModel);
            
            //Map Domain Model to DTO
            var regionDto = _mapper.Map<RegionDto>(regionModel);

            return Ok(regionDto);
        }

    }
}
