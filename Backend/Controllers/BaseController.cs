using Carwale.API;
using Carwale.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Carwale.Controllers
{
    [ApiController]
    public class BaseController:ControllerBase
    {
        protected virtual void ValidateModel(IEnumerable<ModelError> allErrors, bool isValidModelState, bool validateRequiredFields = true)
        {
            // if ModelState id not valid then throw an exception 
            if (!isValidModelState)
            {
                List<IValidationError> errors = new List<IValidationError>();

                foreach (var error in allErrors)
                {
                    string errorMessage = error.ErrorMessage;
                    // if model is null then find an exception with exact message
                    if ((string.IsNullOrEmpty(errorMessage) || string.IsNullOrWhiteSpace(errorMessage)) && error.Exception != null)
                    {
                        errorMessage = error.Exception.Message;
                        string innerMessage = error.Exception.InnerException?.Message;
                        errorMessage = string.IsNullOrEmpty(innerMessage) ? errorMessage : $"{errorMessage} See InnerException = {innerMessage}";
                        errors.Add(new ValidationError(errorMessage, 400));
                    }
                    if (validateRequiredFields && !string.IsNullOrEmpty(errorMessage) && !string.IsNullOrWhiteSpace(errorMessage))
                        errors.Add(new ValidationError(errorMessage, 400));
                }
                // If errors found in model then throw an exception
                if (errors.Count() > 0)
                    throw new EntityValidationException("Record rejected due to following errors - ", errors);
            }
        }
    }
}
