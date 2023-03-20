using Carwale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carwale.Objects;
using AutoMapper;
using Carwale.UserIdentity;
using Carwale.API;
using Carwale.Domain.Repositories.CarRepository;
using Microsoft.EntityFrameworkCore;
using Carwale.Domain.Repositories.ModelRepository;
using System.Runtime.ConstrainedExecution;

namespace Carwale.Services.CarService
{
    public class CarService : BaseService, ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IModelRepository _modelRepository;
        public CarService(IMapper mapper,
            CWUserIdentity userIdentity,
            ICarRepository carRepository,
            IModelRepository modelRepository)
            : base(mapper, userIdentity)
        {
            _carRepository = carRepository;
            _modelRepository = modelRepository;
        }

        public async Task<ApiResponse<IEnumerable<CarDto>>> GetAll(int? status)
        {
            var cars = await this._carRepository.GetAll(status,this.User.TenantUId);
            return ApiResponse<IEnumerable<CarDto>>.SuccessResponse(cars,cars.Count());
        }

        public async Task<ApiResponse<CarDto>> GetDetailByUId(string uid)
        {
            var result = await this._carRepository.GetDetailByUId(uid, this.User.TenantUId);
            return ApiResponse<CarDto>.SuccessResponse(result);
        }

		/// <summary>
		/// Creates new Car and returns the ApiResponse with UId of newly created Car
		/// </summary>
		/// <param name="car">All the call properties</param>
		/// <returns>if successful it will return the ApiResponse with UId of newly created Car. If fails then it returns BaseResponse with errors</returns>
		public async Task<BaseResponse> Create(CarCreateRequest car)
        {
			ApiResponse<string> response = new ();

            /*Map to carEntity and lets modify all necessary properties */
			var carEntity = this.Map<Car>(car);

            carEntity.CreatedBy = this.User.UserName;
            carEntity.TenantId = this.User.TenantId;

            /*Returns model or Error if model is not found for given modelUid*/
            var modelResponse = await this.GetModel(car.ModelUId);

            /*if error then set Response Errors to modelResponse Errors*/
            if (!modelResponse.HasError)
            {                
                carEntity.ModelId = modelResponse.Data.Id;

                try
                {
                    var result = await this._carRepository.Create(carEntity);

                    /*sets data and HasError=false*/
                    response.Success(result.Item1);
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                response.Errors = modelResponse.Errors;
			}

            return response;
        }

        /// <summary>
        /// It will return ApiResponse with Model or errors if Model is not found for given ModelUId
        /// </summary>
        /// <param name="modelUId">An appropriate modelUid that exists in the syste,</param>
        /// <returns>Returns Data or Errors with ApiResponse<></returns>
        private async Task<ApiResponse<Model>> GetModel(string modelUId)
        {
            ApiResponse<Model> response = new();
            var model = await _modelRepository.GetByUId(modelUId);

            if (model == null)
            {
                response.HasError = true;
                response.Errors.Add(new ValidationError("You have sent an invalid model", 400));
            }
            else
            {
                response.Success(model);
            }

            return response;
        }

		/// <summary>
		/// Update the Car in the database if no errors
		/// </summary>
		/// <param name="car">Car properties to be updated</param>
		/// <returns>if successful it will return the ApiResponse with UId of updated Car. If fails then it returns BaseResponse with errors</returns>
		public async Task<BaseResponse> Update(CarUpdateRequest car)
        {
			ApiResponse<string> response = new();

            /* Check if Car existing with given UId and User has access to this Uid by validating TenantUId*/
			var existingCar = await this._carRepository.GetEntity(car.UId,this.User.TenantUId);
			if (existingCar == null)
            {
                response.HasError = true;
                response.Errors.Add(new ValidationError("Car not found!", 400));
            }
            else
            {
				/*Returns model or Error if model is not found for given modelUid*/
				var modelResponse = await this.GetModel(car.ModelUId);

				/*if error then set Response Errors to modelResponse Errors*/
				if (!modelResponse.HasError)
                {
                    try
                    {
                        existingCar.ModelId = modelResponse.Data.Id;
                        existingCar.Colour = car.Colour;
                        existingCar.Status = (byte)car.Status;
                        existingCar.CurrentValue = car.CurrentValue;
                        existingCar.RegistrationNumber = car.RegistrationNumber;
                        existingCar.Notes = car.Notes;
                        existingCar.Year = car.Year;                        
						existingCar.ModifiedBy = this.User.UserName;
                        existingCar.ModifiedDateTime = DateTime.UtcNow;
						await this._carRepository.Update(existingCar);

                        /*set response with Data as UId and HasError=false*/
                        response.Success(car.UId);
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    response.Errors = modelResponse.Errors;                    
                }
            }

            return response;
        }

        /// <summary>
        /// It deletes the entity from the database for given UId, the logged in User must have access to UId
        /// </summary>
        /// <param name="uId"></param>
        /// <returns></returns>
        public async Task<BaseResponse> Delete(string uId)
        {
            ApiResponse<int> response = new();

            var existingCar = await this._carRepository.GetEntity(uId, this.User.TenantUId);
			if (existingCar == null)
            {
                response.HasError = true;
                response.Errors.Add(new ValidationError("Car not found!", 400));
            }
            else
            {             
                await this._carRepository.Delete(existingCar.Id);
                response.Data = 1;
                response.RecordAffected = 1;
            }

            return response;
        }
       
    }
}
