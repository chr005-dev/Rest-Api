using Microsoft.AspNetCore.Mvc;
using Rest_Api.Model;

namespace Rest_Api.Controllers
{
    public class MusicRecordController : ControllerBase
    {
        private IMusicRecords _repo;

        public MusicRecordController(IMusicRecords repo)
        {
            this._repo = repo;
        }

    }
}
