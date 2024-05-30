using AutoMapper;
using ISWalksApi.CustomActionFilters;
using ISWalksApi.Models.DTOs;
using ISWalksApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalksApi.Data;
using NZWalksApi.Models.Domain;

namespace NZWalksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly ISWalksDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper mapper;

        public RegionsController(ISWalksDbContext dbContext,IRegionRepository regionRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _regionRepository = regionRepository;
            this.mapper = mapper;
        }
        
        // Get All REgions
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            // Get Data From Database - Domain Models
            var regions = await _regionRepository.GetAllAsync();

            // Map Domain Models to DTOs
            //Return Dto
            return Ok(mapper.Map<List<RegionDto>>(regions));
        }

        // Get Region By Id
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionModel = await _regionRepository.GetByIdAsync(id);
            if (regionModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionModel));
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        //POST : https://localhost:portnumber/api/regions
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or convert DTO to Domain Model
            var regionModel = mapper.Map<Region>(addRegionRequestDto);

            //Use Domain mode to create region
            await _regionRepository.AddAsync(regionModel);

            //Map Main Model to DTO
            var regionDto = mapper.Map<RegionDto>(regionModel);
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            
            //Map DTO to Model 
            var regionModel = mapper.Map<Region>(updateRegionRequestDto);

            //check if region exists
            regionModel = await _regionRepository.UpdateAsync(id, regionModel);
            if (regionModel == null)
            {
                return NotFound();
            }

            //convert Domain Model to DTO
            var regionDto = mapper.Map<RegionDto>(regionModel);

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionModel = await _regionRepository.GetByIdAsync(id);
            if (regionModel == null)
            {
                return NotFound();
            }

            //Delete Region
            await _regionRepository.DeleteAsync(regionModel);
            
            //Map Domain Model to DTO
            var regionDto = mapper.Map<RegionDto>(regionModel);

            return Ok(regionDto);
        }

    }
}
