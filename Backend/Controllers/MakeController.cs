using Microsoft.AspNetCore.Mvc;
using Carwale.Objects;
using Carwale.API;
using Carwale.Domain.Repositories.MakeRepository;

namespace Carwale.Controllers
{

    [Route("[controller]")]
    public class MakeController : SecuredControler
    {
        private readonly IMakeRepository _makeRepository;
        public MakeController(IMakeRepository makeRepository)
        {
            _makeRepository = makeRepository;
        }

        [HttpGet]        
        public async Task<IActionResult> GetAll()
        {
            var response=await this._makeRepository.GetAll();
			return Ok(ApiResponse<IEnumerable<MakeDto>>.SuccessResponse(response));
		}
    }
}