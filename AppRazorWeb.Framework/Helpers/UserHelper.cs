using AppRazorWeb.Framework.BusinessService.Models;
using AppRazorWeb.Framework.Dataservices;
using ResultCommunication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AppRazorWeb.Framework.Helpers
{
    internal static class UserHelper
    {
        public static async Task<IExecutionResult> GetUser(Guid userId,
            IUserDataService userDataService, 
            IUserScheduleDataService userScheduleDataService,
            IDbConnection dbConnection,
            IDbTransaction dbTransaction = null)
        {
            User user = (await userDataService.GetUser(userId, dbConnection, dbTransaction))
                .ToBusinessServiceModel();
            if (user == null)
            {
                return new ExecutionResult(
                    Enums.ErrorType.NotFound,
                    nameof(UserHeader),
                    "Attention - The user doesn't exist");
            }

            List<Dataservices.Models.UserScheduleReadModel> userSchedules = await userScheduleDataService
                .GetUserSchedules(dbConnection, dbTransaction, new Dataservices.Filters.UserScheduleFilters()
                {
                    UserId = userId 
                });

            user.UserSchedules = userSchedules
                .Select(q => q.ToBusinessServiceUser())
                .ToList();

            return new ExecutionResult(user);
        }
    }
}
