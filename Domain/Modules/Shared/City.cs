using Domain.Base;

namespace Domain.Modules.Shared
{
    public class City : BaseEntity
    {
        public string Title { get; set; }
        public int ProvinceID { get; set; }
        public virtual Province Province { get; set; }

        public City()
        {

        }

        public City(int userID, string title, int provinceID)
        {
            CreatorUserID = userID;
            CreateDate = DateTime.Now;
            Title = title;
            ProvinceID = provinceID;
        }

        public City(int userID, string title, int provinceID, string description)
        {
            CreatorUserID = userID;
            CreateDate = DateTime.Now;
            Title = title;
            ProvinceID = provinceID;
            Description = description;
        }


    }
}
