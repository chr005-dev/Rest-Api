using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rest_Api.Model;

namespace Rest_Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MusicRecordController : ControllerBase
    {
        private IMusicRecords _repo;

        public MusicRecordController(IMusicRecords repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MusicRecord>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<MusicRecord>> GetAll(
                 [FromQuery] string? titleFilter = null,
                 [FromQuery] string? artistFilter = null,
                 [FromQuery] int? publicationYear = null)
        {
            try
            {
                IEnumerable<MusicRecord> result =
                    _repo.GetAllMusicRecords(titleFilter, artistFilter, publicationYear);

                if (!result.Any())
                {
                    return NoContent();
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

