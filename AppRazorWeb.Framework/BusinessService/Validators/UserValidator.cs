using ResultCommunication;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AppRazorWeb.Framework.BusinessService.Models;
using AppRazorWeb.Framework.Dataservices;

namespace AppRazorWeb.Framework.BusinessService.Validators
{
    public class UserValidator
    {

        public static async Task<IExecutionResult> ValidateUser(UserWriteModel user,
            IUserDataService userDataService, IDbConnection dbConnection,
            IDbTransaction dbTransaction = null)
        {

            #region Mandatory Fields

            if (string.IsNullOrWhiteSpace(user.Name))
            {
                return new ExecutionResult(
                    Enums.ErrorType.MandatoryField,
                    nameof(UserHeader.Name),
                    "Attention - The name is mandatory");
            }

            if (string.IsNullOrWhiteSpace(user.LastName))
            {
                return new ExecutionResult(
                    Enums.ErrorType.MandatoryField,
                    nameof(UserHeader.LastName),
                    "Attention - The last name is mandatory");
            }

            if (string.IsNullOrWhiteSpace(user.DNI))
            {
                return new ExecutionResult(
                    Enums.ErrorType.MandatoryField,
                    nameof(UserHeader.DNI),
                    "Attention - The dni is mandatory");
            }

            if (string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                return new ExecutionResult(
                    Enums.ErrorType.MandatoryField,
                    nameof(UserHeader.PhoneNumber),
                    "Attention - The phone number is mandatory");
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                return new ExecutionResult(
                    Enums.ErrorType.MandatoryField,
                    nameof(UserHeader.Email),
                    "Attention - The email is mandatory");
            }

            #endregion Mandatory Fields

            if (user.Name.Length > 50)
            {
                return new ExecutionResult(
                    Enums.ErrorType.InvalidField,
                    nameof(UserHeader.Name),
                    "Attention - The name can't have more than 50 characteres");
            }

            if (user.LastName.Length > 50)
            {
                return new ExecutionResult(
                    Enums.ErrorType.InvalidField,
                    nameof(UserHeader.LastName),
                    "Attention - The last name can't have more than 50 characteres");
            }

            string patternDni = @"^(\d{8}([A-Za-z]))$";
            Regex r = new Regex(patternDni);

            if (!r.IsMatch(user.DNI))
            {
                return new ExecutionResult(
                    Enums.ErrorType.InvalidField,
                    nameof(user.DNI),
                    "Attention - The DNI can't be different than 8 digits and a letter");
            }

            if (user.Age > 130)
            {
                return new ExecutionResult(
                    Enums.ErrorType.InvalidField,
                    nameof(UserHeader.Age),
                    "Attention - The age can't be greater than 130 years");
            }

            if (user.Age < 0)
            {
                return new ExecutionResult(
                    Enums.ErrorType.InvalidField,
                    nameof(UserHeader.Age),
                    "Attention - The age can't be less than 0 years");
            }

            string patternTelefono = @"^(\+[\/.()-]*){0,3}?(\d[\/.()-]*){9,12}$";
            r = new Regex(patternTelefono);
            if (!r.IsMatch(user.PhoneNumber))
            {
                return new ExecutionResult(
                    Enums.ErrorType.InvalidField,
                    nameof(user.PhoneNumber),
                    "Attention - That's an incorrect format");
            }

            string patternNameIsLetter = @"^([a-zA-ZùÙüÜäàáëèéïìíöòóüùúÄÀÁËÈÉÏÌÍÖÒÓÜÚñÑ\s]+)$";
            r = new Regex(patternNameIsLetter);
            if (!r.IsMatch(user.Name))
            {
                return new ExecutionResult(
                    Enums.ErrorType.InvalidField,
                    nameof(user.Name),
                    "Attention - The name only have to contain letters");
            }

            if (!r.IsMatch(user.LastName))
            {
                return new ExecutionResult(
                    Enums.ErrorType.InvalidField,
                    nameof(user.LastName),
                    "Attention - The last name only have to contain letters");
            }

            string patternEmail = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}";
            r = new Regex(patternEmail);
            if (!r.IsMatch(user.Email))
            {
                return new ExecutionResult(
                    Enums.ErrorType.InvalidField,
                    nameof(user.Email),
                    "Attention - That's an incorrect format");
            }

            return new ExecutionResult();

        }

    }
}
