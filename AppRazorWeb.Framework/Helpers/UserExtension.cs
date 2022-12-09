namespace AppRazorWeb.Framework.Helpers
{
    public static class UserExtensions
    {

        public static BusinessService.Models.User ToBusinessServiceModel(
            this Dataservices.Models.UserReadModel user)
        {
            if (user == null)
            {
                return null;
            }

            return new BusinessService.Models.User()
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                DNI = user.DNI,
                Age = user.Age,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };

        }

        public static BusinessService.Models.UserHeader ToBusinessServiceHeaderModel(
            this Dataservices.Models.UserReadModel user)
        {
            if (user == null)
            {
                return null;
            }

            return new BusinessService.Models.UserHeader()
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                DNI = user.DNI,
                Age = user.Age,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };
        }

    }
}