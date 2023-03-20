using Carwale.Services.CarService;
using Carwale.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Carwale.Objects;
using Carwale.API;

namespace Carwale.Controllers
{

    [Route("[controller]")]
    public class CarController : SecuredControler
    {
        private readonly ICarService _carService;
        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet()]
		[ProducesResponseType(typeof(ApiResponse<IEnumerable<CarDto>>), 200)]
		public async Task<IActionResult> GetAll(int? status)
		{
            var response = await this._carService.GetAll(status);
            return Ok(response);
        }

        [HttpGet("{uid}")]
		[ProducesResponseType(typeof(ApiResponse<CarDto>), 200)]
		public async Task<IActionResult> Get(string uid)
        {
            var response=await this._carService.GetDetailByUId(uid);
            return Ok(response);
        }

        [HttpPost]
        [Uniform]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        public async Task<IActionResult> Create(CarCreateRequest car)
        {
            this.ValidateModel(ModelState.Values.SelectMany(v => v.Errors), ModelState.IsValid);
            var response = await this._carService.Create(car);
            return Ok(response);
        }

        [HttpPut]
        [Uniform]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        public async Task<IActionResult> Update(CarUpdateRequest car)
        {
			this.ValidateModel(ModelState.Values.SelectMany(v => v.Errors), ModelState.IsValid);
			var response = await this._carService.Update(car);
            return Ok(response);
        }

        [HttpDelete]
		[ProducesResponseType(typeof(ApiResponse<int>), 200)]
		[ProducesResponseType(typeof(BaseResponse), 400)]
		public async Task Delete(string uid)
        {
            await this._carService.Delete(uid);
        }
    }
}