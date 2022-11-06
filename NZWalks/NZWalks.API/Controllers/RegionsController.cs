using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;
using NZWalks.API.Models.DTO;
using AutoMapper;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionRepository.GetAllAsync();           
            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id); 
            if(region == null)
            {
                return NotFound();
            }
            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.Region region)
        {
            if(region == null)
            {
                return BadRequest();
            }

            var regionDomain = mapper.Map<Models.Domain.Region>(region);
            var addedRegion = await regionRepository.AddAsync(regionDomain);
            var regionDTOResult = mapper.Map<Models.DTO.Region>(addedRegion);

            return Ok(regionDTOResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            var region = await this.regionRepository.DeleteAsync(id);   

            return Ok(region);
        }

        [HttpPut("")]
        public async Task<ActionResult> UpdateRegionAsync(Models.DTO.Region region)
        {            
            var regionDTo = mapper.Map<Models.Domain.Region>(region);
            var updatedRegion = await this.regionRepository.UpdateAsync(regionDTo);
            if(updatedRegion == null) { return BadRequest(); }
            var regionDTOResult = mapper.Map<Models.DTO.Region>(updatedRegion);

            return Ok(regionDTOResult);
        }
    }
}
