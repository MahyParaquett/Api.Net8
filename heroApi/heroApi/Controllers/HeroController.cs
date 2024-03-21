using Domain.Model;
using heroApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace heroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly IHeroService heroService;
        public HeroController(IHeroService heroService)
        {
            this.heroService = heroService;
        }

        [HttpGet]
        [Route("getAllHeroes")]
        public async Task<IActionResult> GetHeros()
        {
            return Ok(await heroService.GetHeros());
        }

        [HttpGet]
        [Route("getHero")]
        public async Task<IActionResult> GetHero(string heroId)
        {
            if (string.IsNullOrEmpty(heroId))
            {
                return BadRequest("HeroId is invalid");
            }

            return Ok(await heroService.GetHero(Guid.Parse(heroId)));
        }

        [HttpPost]
        [Route("insertHero")]
        public async Task<IActionResult> Hero([FromBody] HeroDto heroDto)
        {
            if (string.IsNullOrEmpty(heroDto.Name) || string.IsNullOrEmpty(heroDto.Power))
            {
                return BadRequest("Hero Name or Hero Power are invalid.");
            }
            return Ok(await heroService.InsertHero(new Hero(Guid.NewGuid(), heroDto.Name, heroDto.Power)));
        }

        [HttpPut]
        [Route("updateHero")]
        public async Task<IActionResult> UpdateHero([FromBody] HeroDto heroDto)
        {
            if (string.IsNullOrEmpty(heroDto.HeroId.ToString()))
            {
                return BadRequest("Hero Id is invalid.");
            }

            if (string.IsNullOrEmpty(heroDto.Name) || string.IsNullOrEmpty(heroDto.Power))
            {
                return BadRequest("Hero Name or Hero Power are invalid.");
            }

            return Ok(await heroService.UpdateHero(new Hero(heroDto.HeroId, heroDto.Name, heroDto.Power)));
        }

        [HttpDelete]
        [Route("deleteHero")]
        public async Task<IActionResult> Delete(string heroId)
        {
            if (string.IsNullOrEmpty(heroId))
            {
                return BadRequest("HeroId is invalid");
            }

            return Ok(await heroService.DeleteHero(Guid.Parse(heroId)));
        }

    }
}
