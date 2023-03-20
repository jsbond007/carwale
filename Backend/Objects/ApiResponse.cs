using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Carwale.Objects
{
    public class BaseResponse
    {
        public string EntityUId { get; set; }

        public string StatusMessage { get; set; }

        public bool HasError { get; set; }

        public virtual string ErrorTrace { get; set; }
        public virtual int RecordAffected { get; set; }

        private List<IValidationError> _errors;

		public List<IValidationError> Errors 
        { 
            get
            {
                return _errors;
            }
            set
            {
                if(value?.Count>0)
                {
                    HasError = true;
                }
                _errors = value;
            }
        }
    }

    public class ApiResponse<T> : BaseResponse
    {
        public ApiResponse()
        {
            HasError = true;
            this.Errors = new List<IValidationError>();
        }

        public ApiResponse(IEnumerable<IValidationError> validationErrors)
        {
            this.Errors = validationErrors.ToList();
        }

        public void Success(T data)
        {
            HasError = false;
            Data = data;
        }

        public static ApiResponse<T> SuccessResponse(T data, int recordAffected=1)
        {
            ApiResponse<T> response = new();
            response.HasError = false;
            response.Data = data;
            if (data != null)
            {
                response.RecordAffected = recordAffected;
            }
			return response;
        }


		public static ApiResponse<T> ErrorResponse(params string[] errors)
		{
			ApiResponse<T> response = new();
			response.HasError = true;
            response.Errors.AddRange(errors.Select(p => new ValidationError (p,0 )));
            return response;
			
		}
		public T Data { get; set; }



    }


}
