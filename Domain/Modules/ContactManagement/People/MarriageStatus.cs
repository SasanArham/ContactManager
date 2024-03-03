using Domain.Base;

namespace Domain.Modules.ContactManagement.People
{
    public class MarriageStatus : BaseEntity
    {
        public string Title { get; set; }
        public virtual List<Person> Persons { get; set; } = new();

        public MarriageStatus()
        {

        }

        public MarriageStatus(int userID, string title, int oldSystemID)
        {
            CreatorUserID = userID;
            CreateDate = DateTime.Now;
            Title = title;
        }
    }
}
