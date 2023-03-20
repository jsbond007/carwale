using Microsoft.AspNetCore.Mvc;
using Carwale.Objects;
using Carwale.API;
using Carwale.Domain.Repositories.ModelRepository;

namespace Carwale.Controllers
{

    [Route("make/{makeUId}/model")]
    public class ModelController : SecuredControler
    {
        private readonly IModelRepository _modelRepository;
        public ModelController(IModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        [HttpGet]        
        public async Task<IActionResult> GetAll(string makeUId)
        {
            var response=await this._modelRepository.GetAll(makeUId);
            return Ok(ApiResponse<IEnumerable<ModelDto>>.SuccessResponse(response));
        }
    }
}