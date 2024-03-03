using Domain.Base;
using System.Text;

namespace Domain.Modules.Shared
{
    public class Adress : ValueObject
    {
        public string Details { get; init; }
        public bool IsDefault { get; init; }
        public string PostalCode { get; init; }
        public int? CityID { get; init; }
        public virtual City City { get; init; }
        public int? CreatorUserID { get; init; }
        public DateTime CreateDate { get; protected set; }

        public Adress()
        {

        }

        public Adress(int? creatorUserID, int? cityID, string details, bool isDefault, string postalCode)
        {
            CreatorUserID = creatorUserID;
            CreateDate = DateTime.Now;
            CityID = cityID;
            Details = details;
            IsDefault = isDefault;
            PostalCode = postalCode;
        }

        public Adress(int? creatorUserID, int? cityID, string details, bool isDefault)
        {
            CreatorUserID = creatorUserID;
            CreateDate = DateTime.Now;
            CityID = cityID;
            Details = details;
            IsDefault = isDefault;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            if (string.IsNullOrEmpty(PostalCode))
            {
                yield return CityID;
                yield return Details;
            }
            else
            {
                yield return PostalCode;
            }
        }

        public Adress Copy()
        {
            var copy = new Adress(CreatorUserID, CityID, Details, IsDefault, PostalCode);
            return copy;
        }

        public Adress Edit(int? cityID, string details, bool isDefault, string postalCode)
        {
            var newAddress = new Adress(CreatorUserID, cityID, details, isDefault, postalCode);
            newAddress.CreateDate = CreateDate;
            return newAddress;
        }

        public override string ToString()
        {
            StringBuilder addressBuilder = new StringBuilder();
            if (CityID.HasValue)
            {
                addressBuilder.Append(City.Province.Title + " , " + City.Title + ",");
            }
            addressBuilder.Append(Details + " , " + PostalCode);
            return addressBuilder.ToString();
        }
    }
}
