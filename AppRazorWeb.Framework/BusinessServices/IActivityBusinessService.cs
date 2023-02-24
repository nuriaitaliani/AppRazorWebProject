using ResultCommunication;
using System;
using System.Threading.Tasks;

namespace AppRazorWeb.Framework.BusinessService
{
    public interface IActivityBusinessService
    {

        Task<IExecutionResult> CreateActivity(Models.ActivityClassWriteModel activity);

        Task<IExecutionResult> DeleteActivity(Guid activityId);

        Task<IExecutionResult> GetActivity(Guid activityId);

        Task<IExecutionResult> GetActivities(Dataservices.Filters.ActivityFilters filters);

        Task<IExecutionResult> UpdateActivity(Models.ActivityClassWriteModel activity);

    }
}
