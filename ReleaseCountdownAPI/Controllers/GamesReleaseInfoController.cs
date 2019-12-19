using Microsoft.AspNetCore.Mvc;
using ReleaseCountdownAPI.Models;
using ReleaseCountdownAPI.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ReleaseCountdownAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesReleaseInfoController : ControllerBase
    {
        private readonly GamesReleaseInfo _service;

        public GamesReleaseInfoController(GamesReleaseInfo service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ReleaseItemModel>> Get()
        {
            try
            {
                var response = await _service.Get().ConfigureAwait(false);

                return Ok(response.Select(item => new ReleaseItemModel
                {
                    Name = item.Name,
                    ReleaseDate = item.ReleaseDate
                }).ToList());

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReleaseItemModel>> Post(ReleaseItemModel model)
        {
            try
            {
                await _service.Post(model).ConfigureAwait(false);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string gameName)
        {
            try
            {
                var gameToDelete = await _service.Find(gameName).ConfigureAwait(false);

                if (gameToDelete == null) return BadRequest();

                await _service.Delete(gameToDelete.Id).ConfigureAwait(false);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}