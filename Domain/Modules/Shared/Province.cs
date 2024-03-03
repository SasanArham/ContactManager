using Domain.Base;

namespace Domain.Modules.Shared
{
    public class Province : BaseEntity
    {
        public string Title { get; set; }
        public virtual List<City> Cities { get; set; } = new();

        public Province()
        {

        }

        public Province(int userID, string title)
        {
            this.CreatorUserID = userID;
            Title = title;
            CreateDate = DateTime.Now;
        }

        public Province(int userID, string title, string description)
        {
            this.CreatorUserID = userID;
            Title = title;
            CreateDate = DateTime.Now;
            Description = description;
        }
    }
}
