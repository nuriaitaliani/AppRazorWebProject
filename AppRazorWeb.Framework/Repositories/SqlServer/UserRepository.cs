using System.Text;
using Filters = AppRazorWeb.Framework.Dataservices.Filters.UserFilters;
using InsertModel = AppRazorWeb.Framework.Dataservices.Models.UserWriteModel;
using ReadModel = AppRazorWeb.Framework.Dataservices.Models.UserReadModel;
using UpdateModel = AppRazorWeb.Framework.Dataservices.Models.UserWriteModel;

namespace AppRazorWeb.Framework.Repositories.SqlServer
{
    public class UserRepository : IUsersRepository
    {
        #region Deletes

        ///<inheritdoc/>
        public string GetDeleteUser()
        {
            return @"
            DELETE
            FROM ""user""
            WHERE id = @Id";
        }

        #endregion Deletes

        #region Inserts

        ///<inheritdoc/>
        public string GetInsertUser()
        {
            return $@"
INSERT INTO ""user""
            (id, name, last_name,
            dni, age, phone_number,
            email)
VALUES      (@{nameof(InsertModel.Id)}, @{nameof(InsertModel.Name)},
            @{nameof(InsertModel.LastName)}, @{nameof(InsertModel.DNI)},
            @{nameof(InsertModel.Age)}, @{nameof(InsertModel.PhoneNumber)},
            @{nameof(InsertModel.Email)})";
        }

        #endregion Inserts

        #region Selects

        ///<inheritdoc/>
        public string GetSelectUser()
        {
            StringBuilder queryBuilder = new StringBuilder(GetSelectUserRawQuery());
            queryBuilder.Append(@"
            WHERE ""user"".id = @Id");

            return queryBuilder.ToString();
        }

        ///<inheritdoc/>
        public string GetSelectUsers(bool filterByName = false, bool filterByLastname = false, bool filterByDni = false,
            bool filterByAge = false, bool filterByPhonenumber = false, bool filterByEmail = false)
        {

            StringBuilder queryBuilder = new StringBuilder(GetSelectUserRawQuery());

            if (filterByName || filterByLastname || filterByDni
                || filterByAge || filterByPhonenumber || filterByEmail)
            {

                queryBuilder.Append($@"
                WHERE ");

                if (filterByName)
                {
                    queryBuilder.Append($@" ""user"".name = @{nameof(Filters.Name)} AND ");
                }

                if (filterByLastname)
                {
                    queryBuilder.Append($@" ""user"".last_name = @{nameof(Filters.LastName)} AND ");
                }

                if (filterByDni)
                {
                    queryBuilder.Append($@" ""user"".dni = @{nameof(Filters.DNI)} AND ");
                }

                if (filterByAge)
                {
                    queryBuilder.Append($@" ""user"".age = @{nameof(Filters.Age)} AND ");
                }

                if (filterByPhonenumber)
                {
                    queryBuilder.Append($@" ""user"".phone_number = @{nameof(Filters.PhoneNumber)} AND ");
                }

                if (filterByEmail)
                {
                    queryBuilder.Append($@" ""user"".email = @{nameof(Filters.Email)} AND ");
                }

                queryBuilder.Remove(queryBuilder.Length - 4, 3);

            }

            return queryBuilder.ToString();

        }

        #region Helpers

        ///<inheritdoc/>
        public string GetSelectUserRawQuery()
        {
            return $@"
SELECT      ""user"".id AS ""{nameof(ReadModel.Id)}"", ""user"".name AS ""{nameof(ReadModel.Name)}"",
            ""user"".last_name AS ""{nameof(ReadModel.LastName)}"", ""user"".dni AS ""{nameof(ReadModel.DNI)}"",
            ""user"".age AS ""{nameof(ReadModel.Age)}"", ""user"".phone_number AS ""{nameof(ReadModel.PhoneNumber)}"",
            ""user"".email AS ""{nameof(ReadModel.Email)}""
FROM        ""user""";
        }

        #endregion Helpers

        #endregion Selects

        #region Updates

        ///<inheritdoc/>
        public string GetUpdateUser()
        {
            return $@"
UPDATE  ""user""
SET     name = @{nameof(UpdateModel.Name)}, last_name = @{nameof(UpdateModel.LastName)},
        dni = @{nameof(UpdateModel.DNI)}, age = @{nameof(UpdateModel.Age)},
        phone_number = @{nameof(UpdateModel.PhoneNumber)}, email = @{nameof(UpdateModel.Email)},
        update_date = GETUTCDATE()
WHERE   id = @{nameof(UpdateModel.Id)}";
        }

        #endregion Updates
    }
}
