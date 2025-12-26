using ApiForSud.DTOs;
using ApiForSud.Services.CurtService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiForSud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurtController : ControllerBase
    {
        private readonly ICurtService _curtService;

        public CurtController(ICurtService curtService)
        {
            _curtService = curtService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<CurtDTO>>> GetAllCurst()
        {
            var curst = await _curtService.GetAllCurts();

            if (curst == null)
            {
                return NotFound();
            }

            return Ok(curst);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<bool>> CreateCurt(CurtDTO curtDTO)
        {
            var newCurt = await _curtService.CreateCurtService(curtDTO);
            if (newCurt == false)
            {
                return BadRequest();
            }

            return Ok(newCurt);
        }
    }
}
