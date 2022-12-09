using System;

namespace AppRazorWeb.Framework.Dataservices.Models
{
    public class UserScheduleWriteModel
    {

        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ScheduleId { get; set; }

        public Guid ActivityId { get; set; }

    }

    public class UserScheduleReadModel : UserScheduleWriteModel
    {
        public string UserName { get; set; }
        public string ActivityName { get; set; }
    }

}
