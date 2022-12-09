namespace AppRazorWeb.Framework.Repositories
{
    public interface IUsersRepository
    {

        #region Deletes

        /// <summary>
        /// Get the query to delete an user
        /// </summary>
        /// <returns>The query</returns>
        string GetDeleteUser();

        #endregion Deletes

        #region Inserts

        /// <summary>
        /// Get the query to insert an user
        /// </summary>
        /// <returns>The query</returns>
        string GetInsertUser();

        #endregion Inserts

        #region Selects

        /// <summary>
        /// Get the query to select an user by identifier
        /// </summary>
        /// <returns>The query</returns>
        string GetSelectUser();

        /// <summary>
        /// Get the query to select an user
        /// Filters may be applied
        /// </summary>
        /// <param name="name">Filter by name</param>
        /// <param name="lastname">Filter by lastname</param>
        /// <param name="dni">Filter by dni</param>
        /// <param name="age">Filter by age</param>
        /// <param name="phonenumber">Filter by phone number</param>
        /// <param name="email">Filter by email</param>
        /// <returns>The query</returns>
        string GetSelectUsers(bool filterByName = false, bool filterByLastname = false,
            bool filterByDni = false, bool filterByAge = false, bool filterByPhonenumber = false,
            bool filterByEmail = false);

        #region Helpers

        /// <summary>
        /// Get the raw query to select users
        /// </summary>
        /// <returns>The query</returns>
        string GetSelectUserRawQuery();

        #endregion Helpers

        #endregion Selects

        #region Updates

        /// <summary>
        /// Get the query to update an user
        /// </summary>
        /// <returns>The query</returns>
        string GetUpdateUser();

        #endregion Updates

    }
}
