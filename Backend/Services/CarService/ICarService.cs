using Carwale.Objects;

namespace Carwale.Services.CarService
{
    public interface ICarService
    {
        Task<ApiResponse<IEnumerable<CarDto>>> GetAll(int? status);
        Task<ApiResponse<CarDto>> GetDetailByUId(string uid);
        Task<BaseResponse> Create(CarCreateRequest car);
        Task<BaseResponse> Update(CarUpdateRequest car);
        Task<BaseResponse> Delete(string uId);
    }
}
