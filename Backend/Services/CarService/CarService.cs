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

        public async Task<BaseResponse> Create(CarCreateRequest car)
        {
			ApiResponse<string> response = new ();

			var carEntity = this.Map<Car>(car);
            carEntity.CreatedBy = this.User.UserName;
            carEntity.TenantId = this.User.TenantId;

            var modelResponse = await this.GetModel(car.ModelUId);
            if (!modelResponse.HasError)
            {                
                carEntity.ModelId = modelResponse.Data.Id;

                try
                {
                    var result = await this._carRepository.Create(carEntity);
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

        public async Task<BaseResponse> Update(CarUpdateRequest car)
        {
			ApiResponse<string> response = new();

			var existingCar = await this._carRepository.GetEntity(car.UId,this.User.TenantUId);
			if (existingCar == null)
            {
                response.HasError = true;
                response.Errors.Add(new ValidationError("Car not found!", 400));
            }
            else
            {
                var modelResponse = await this.GetModel(car.ModelUId);
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
