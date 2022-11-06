using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalkDifficultiesController : ControllerBase
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            var walkDifficulties = await this.walkDifficultyRepository.GetAllAsync();
            return Ok(walkDifficulties);
        }

        [HttpGet("{id}")]
        [ActionName("GetWalkByIdAsync")]
        public async Task<IActionResult> GetWalkByIdAsync(Guid id)
        {
            var existingWalkDifficult = await this.walkDifficultyRepository.GetByIdAsync(id);

            return Ok(existingWalkDifficult);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddWalkDifficultyAsync(Models.DTO.WalkDifficulty walkDifficulty)
        {
            var walkDifficultyDomain = mapper.Map<Models.Domain.WalkDifficulty>(walkDifficulty);
            var addedWalkDifficulty = await this.walkDifficultyRepository.AddAsync(walkDifficultyDomain);
            var resultWalkDiffculty = mapper.Map<Models.DTO.WalkDifficulty>(addedWalkDifficulty);

            return CreatedAtAction(nameof(GetWalkByIdAsync), new { id = resultWalkDiffculty.Id }, resultWalkDiffculty);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            var deletedWalkDiffDomain = await this.walkDifficultyRepository.DeleteAsync(id);

            if (deletedWalkDiffDomain == null) { return BadRequest(); }
            
            var deletedDiffDTO = mapper.Map<Models.DTO.WalkDifficulty>(deletedWalkDiffDomain);

            return CreatedAtAction(nameof(GetWalkByIdAsync), new { id = deletedDiffDTO.Id }, deletedDiffDTO);

        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Models.DTO.WalkDifficulty walkDifficulty)
        {
            var walkDifficultyDomain = mapper.Map<Models.Domain.WalkDifficulty>(walkDifficulty);
            var updatedWalkDifficulty = await this.walkDifficultyRepository.UpdateAsync(walkDifficultyDomain);
            if (updatedWalkDifficulty == null) { return BadRequest(); }

            var resultWalkDiffculty = mapper.Map<Models.DTO.WalkDifficulty>(updatedWalkDifficulty);

            return CreatedAtAction(nameof(GetWalkByIdAsync), new { id = resultWalkDiffculty.Id }, resultWalkDiffculty);

        }
    }
}
