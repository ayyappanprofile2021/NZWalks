using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            var walks = await this.walkRepository.GetAllAsync();
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walks);

            return Ok(walksDTO);
        }

        [HttpGet("{id}")]
        [ActionName("GetWalkByIdAsync")]
        public async Task<IActionResult> GetWalkByIdAsync(Guid id)
        {
            var walk = await this.walkRepository.GetByIdAsync(id);
            if(walk == null) { return BadRequest(); }

            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);
            return Ok(walkDTO);
        }

        [HttpPost("")]
        public async Task<ActionResult> AddWalkAsync(Models.DTO.NewWalk walk)
        {
            var walkDomain = mapper.Map<Models.Domain.Walk>(walk);
            var walkDomainAdded = await this.walkRepository.AddAsync(walkDomain);
            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomainAdded);
            if(walkDTO == null) { return BadRequest(); }

            return CreatedAtAction(nameof(GetWalkByIdAsync), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            var walkDeleted = await this.walkRepository.DeleteAsync(id);
            if (walkDeleted == null) { return BadRequest(); }

            return CreatedAtAction(nameof(GetWalkByIdAsync), new { id = walkDeleted.Id }, walkDeleted);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateWalkAsync(NewWalk walk)
        {
            var walkDomain = mapper.Map<Models.Domain.Walk>(walk);
            var walkUpdated = await this.walkRepository.UpdateAsync(walkDomain);
            if (walkUpdated == null) { return BadRequest(); }
            var walkDTO = mapper.Map<Models.DTO.Walk>(walkUpdated);

            return CreatedAtAction(nameof(GetWalkByIdAsync), new { id = walkDTO.Id }, walkDTO);
        }

    }
}
