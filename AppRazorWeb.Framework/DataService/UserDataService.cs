using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AppRazorWeb.Framework.Dataservices.Filters;
using AppRazorWeb.Framework.Dataservices.Models;
using AppRazorWeb.Framework.Repositories;

namespace AppRazorWeb.Framework.Dataservices
{
    public class UserDataService : IUserDataService
    {

        #region Fields

        private readonly IUsersRepository _usersRepository;
        private readonly IConnectionCrud _connectionCrud;

        #endregion Fields

        #region Constructor

        public UserDataService(IUsersRepository usersRepository, IConnectionCrud connectionCrud)
        {
            _connectionCrud = connectionCrud;
            _usersRepository = usersRepository;
        }

        #endregion Constructor

        #region Deletes

        public async Task<int> DeleteUser(Guid userId, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", userId, DbType.Guid);

            string query = _usersRepository.GetDeleteUser();

            return await _connectionCrud.Delete(dbConnection, query, parameters, dbTransaction);
        }

        #endregion Deletes

        #region Gets

        public async Task<UserReadModel> GetUser(Guid userId, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", userId, DbType.Guid);

            string query = _usersRepository.GetSelectUser();

            UserReadModel user = (await _connectionCrud
            .Load<UserReadModel, DynamicParameters>(dbConnection, query, parameters, dbTransaction))
            .FirstOrDefault();

            return user;
        }

        public async Task<List<UserReadModel>> GetUsers(IDbConnection dbConnection, IDbTransaction dbTransaction = null, UserFilters filters = null)
        {
            List<UserReadModel> users;
            if (filters == null)
            {
                string query = _usersRepository.GetSelectUsers();

                users = (await _connectionCrud.Load<UserReadModel>(dbConnection, query, dbTransaction))
                    .ToList();
            }
            else
            {
                DynamicParameters parameters = new DynamicParameters();

                bool filterByName = !string.IsNullOrWhiteSpace(filters.Name);
                if (filterByName)
                {
                    parameters.Add(nameof(UserFilters.Name), filters.Name, DbType.String);
                }

                bool filterByLastName = !string.IsNullOrWhiteSpace(filters.LastName);
                if (filterByLastName)
                {
                    parameters.Add(nameof(UserFilters.LastName), filters.LastName, DbType.String);
                }

                bool filterByDNI = !string.IsNullOrWhiteSpace(filters.DNI);
                if (filterByDNI)
                {
                    parameters.Add(nameof(UserFilters.DNI), filters.DNI, DbType.String);
                }

                bool filterByAge = filters.Age != null;
                if (filterByAge)
                {
                    parameters.Add(nameof(UserFilters.Age), filters.Age, DbType.Int64);
                }

                bool filterByPhoneNumber = !string.IsNullOrWhiteSpace(filters.PhoneNumber);
                if (filterByPhoneNumber)
                {
                    parameters.Add(nameof(UserFilters.PhoneNumber), filters.PhoneNumber, DbType.String);
                }

                bool filterByEmail = !string.IsNullOrWhiteSpace(filters.Email);
                if (filterByEmail)
                {
                    parameters.Add(nameof(UserFilters.Email), filters.Email, DbType.String);
                }
                string query = _usersRepository.GetSelectUsers(filterByName, filterByLastName, filterByDNI, filterByAge,
                    filterByPhoneNumber, filterByEmail);

                users = (await _connectionCrud.Load<UserReadModel, DynamicParameters>(dbConnection, query, parameters, dbTransaction))
                    .ToList();
            }
            return users;
        }

        #endregion Gets

        #region Inserts

        public async Task InsertUser(UserWriteModel user, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {

            DynamicParameters parameters = GetUserDynamicParameters(user);

            string query = _usersRepository.GetInsertUser();

            await _connectionCrud.Save<UserWriteModel, DynamicParameters>
            (dbConnection, query, parameters, dbTransaction);

        }

        #endregion Inserts

        #region Updates

        public async Task UpdateUser(UserWriteModel user, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {

            DynamicParameters parameters = GetUserDynamicParameters(user);

            string query = _usersRepository.GetUpdateUser();

            await _connectionCrud.Save<UserWriteModel, DynamicParameters>
                (dbConnection, query, parameters, dbTransaction);

        }

        #endregion Updates

        #region Private Helpers

        private DynamicParameters GetUserDynamicParameters(UserWriteModel user)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add(nameof(UserWriteModel.Id), user.Id, DbType.Guid);
            parameters.Add(nameof(UserWriteModel.Name), user.Name, DbType.String);
            parameters.Add(nameof(UserWriteModel.LastName), user.LastName, DbType.String);
            parameters.Add(nameof(UserWriteModel.DNI), user.DNI, DbType.String);
            parameters.Add(nameof(UserWriteModel.Age), user.Age, DbType.Int64);
            parameters.Add(nameof(UserWriteModel.PhoneNumber), user.PhoneNumber, DbType.String);
            parameters.Add(nameof(UserWriteModel.Email), user.Email, DbType.String);

            return parameters;
        }

        #endregion Private Helpers

    }
}
