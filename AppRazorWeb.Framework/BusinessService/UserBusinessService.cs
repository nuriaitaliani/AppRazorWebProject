using Microsoft.Data.SqlClient;
using ResultCommunication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AppRazorWeb.Framework.BusinessService.Models;
using AppRazorWeb.Framework.BusinessService.Validators;
using AppRazorWeb.Framework.Dataservices;
using AppRazorWeb.Framework.Dataservices.Filters;
using AppRazorWeb.Framework.Helpers;

namespace AppRazorWeb.Framework.BusinessService
{
    public class UserBusinessService : IUserBusinessService
    {

        #region Fields

        private readonly IUserDataService _userDataService;
        private readonly SqlConnection _connection;

        #endregion Fields

        #region Constructor

        public UserBusinessService(IUserDataService userDataService, string connectionString)
        {
            _userDataService = userDataService;
            _connection = new SqlConnection(connectionString);
        }

        #endregion Constructor

        #region Creates

        public async Task<IExecutionResult> CreateUser(UserWriteModel user)
        {
            try
            {
                _connection.Open();

                IExecutionResult result = await UserValidator.ValidateUser(user, _userDataService, _connection);

                if (!result.Success)
                {
                    return result;
                }

                if (user.Id.Equals(Guid.Empty))
                {
                    user.Id = Guid.NewGuid();
                }

                await _userDataService.InsertUser(user.ToDataServiceModel(), _connection);

                return new ExecutionResult();
            }
            catch (Exception exception)
            {
                return new ExecutionResult(
                    Enums.ErrorType.GeneralException,
                    exception.GetType().ToString(),
                    exception.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        #endregion Creates

        #region Deletes

        public async Task<IExecutionResult> DeleteUser(Guid userId)
        {
            try
            {
                _connection.Open();

                if (await _userDataService.GetUser(userId, _connection) == null)
                {
                    return new ExecutionResult(
                        Enums.ErrorType.NotFound,
                        nameof(UserHeader),
                        "Attention - The user doesn't exist");
                }

                await _userDataService.DeleteUser(userId, _connection);

                return new ExecutionResult();
            }
            catch (Exception exception)
            {
                return new ExecutionResult(
                    Enums.ErrorType.GeneralException,
                    exception.GetType().ToString(),
                    exception.Message);
            }
            finally
            {
                _connection.Close();
            }

        }

        #endregion Deletes

        #region Gets

        public async Task<IExecutionResult> GetUser(Guid userId)
        {
            try
            {
                _connection.Open();

                User user = (await _userDataService
                    .GetUser(userId, _connection))
                    .ToBusinessServiceModel();

                if (user == null)
                {
                    return new ExecutionResult(
                        Enums.ErrorType.NotFound,
                        nameof(UserHeader),
                        "Attention - The user doesn't exist");
                }
                              

                return new ExecutionResult(user);

            }
            catch (Exception exception)
            {
                return new ExecutionResult(
                    Enums.ErrorType.GeneralException,
                    exception.GetType().ToString(),
                    exception.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public async Task<IExecutionResult> GetUsers(UserFilters filters)
        {
            try
            {
                _connection.Open();

                List<UserHeader> users = (await _userDataService.GetUsers(_connection, filters: filters))
                    .Select(user => user.ToBusinessServiceHeaderModel()).ToList();

                return new ExecutionResult(users);

            }
            catch (Exception exception)
            {
                return new ExecutionResult(
                    Enums.ErrorType.GeneralException,
                    exception.GetType().ToString(),
                    exception.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        #endregion Gets

        #region Updates

        public async Task<IExecutionResult> UpdateUser(UserWriteModel user)
        {
            try
            {
                _connection.Open();

                if (await _userDataService.GetUser(user.Id, _connection) == null)
                {
                    return new ExecutionResult(
                        Enums.ErrorType.NotFound,
                        nameof(UserHeader),
                        "Attention - The user doesn't exist");
                }

                IExecutionResult result = await UserValidator.ValidateUser(user, _userDataService, _connection);

                if (!result.Success)
                {
                    return result;
                }

                await _userDataService.UpdateUser(user.ToDataServiceModel(), _connection);

                return new ExecutionResult();

            }
            catch (Exception exception)
            {
                return new ExecutionResult(
                    Enums.ErrorType.GeneralException,
                    exception.GetType().ToString(),
                    exception.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        #endregion Updates

    }
}
