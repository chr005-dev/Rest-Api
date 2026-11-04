using Microsoft.AspNetCore.Mvc;
using Rest_Api.Model;

namespace Rest_Api.Controllers
{
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<MusicRecord> GetAll()
        {
            return _repo.GetAll();
        }
    }
}
