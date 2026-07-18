using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalksAPI.Repositories;
using System.Text.Json;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] // This is responsible for authorizing the JWT token. We can see that practically as well inside the postman software.
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;

        private readonly IMapper _mapper;

        private readonly ILogger<RegionsController> _logger;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/regions
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Get all Regions actions method was invoked");
            var regions = await _regionRepository.GetAllAsync();
            var regionDtos = _mapper.Map<List<RegionDto>>(regions);
            _logger.LogInformation($"Finished GetAllRegions request with data: {JsonSerializer.Serialize(regionDtos)}");        // We're using the json serializer here
            return Ok(regionDtos);
        }

        // GET: api/regions/{id}
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await _regionRepository.GetByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDto = _mapper.Map<RegionDto>(region);
            return Ok(regionDto);
        }

        // POST: api/regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] CreateRegionRequestDto createRegionRequestDto)
        {
            var region = _mapper.Map<Region>(createRegionRequestDto);
            region = await _regionRepository.CreateAsync(region);

            var regionDto = _mapper.Map<RegionDto>(region);
            return CreatedAtAction(nameof(GetById), new { id = region.Id }, regionDto);
        }

        // PUT: api/regions/{id}
        [HttpPut("{id:guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var region = _mapper.Map<Region>(updateRegionRequestDto);
            region = await _regionRepository.UpdateAsync(id, region);

            if (region == null)
            {
                return NotFound();
            }

            var regionDto = _mapper.Map<RegionDto>(region);
            return Ok(regionDto);
        }

        // DELETE: api/regions/{id}
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _regionRepository.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
