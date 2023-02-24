using ResultCommunication;
using System;
using System.Threading.Tasks;
using AppRazorWeb.Framework.BusinessService.Models;

namespace AppRazorWeb.Framework.BusinessService
{
    public interface IUserBusinessService
    {

        Task<IExecutionResult> CreateUser(UserWriteModel user);

        Task<IExecutionResult> DeleteUser(Guid userId);

        Task<IExecutionResult> GetUser(Guid userId);

        Task<IExecutionResult> GetUsers(Dataservices.Filters.UserFilters filters);

        Task<IExecutionResult> UpdateUser(UserWriteModel user);

    }
}
