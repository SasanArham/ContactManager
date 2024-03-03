using Domain.Base;

namespace Domain.Modules.ContactManagement.People
{
    public class EducationDegree : BaseEntity
    {
        public string Title { get; set; }
        public virtual List<Person> Persons { get; set; } = new();
        public EducationDegree()
        {


        }

        public EducationDegree(int userID, string title, int oldSystemID)
        {
            CreatorUserID = userID;
            CreateDate = DateTime.Now;
            Title = title;
        }
    }
}
