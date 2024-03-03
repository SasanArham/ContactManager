using Domain.Base;

namespace Domain.Modules.Shared
{
    public class PhoneNumber : ValueObject
    {
        public string Number { get; init; }
        public bool IsDefault { get; init; }
        public PhoneNumberType type { get; init; }
        public DateTime CreateDate { get; private set; }
        public int? CreatorUserID { get; private set; }


        public PhoneNumber()
        {
        }

        public static PhoneNumber CreateMobile(int creatorUserID, string number, bool isDefault)
        {
            var phoneNumber = new PhoneNumber
            {
                CreateDate = DateTime.Now,
                CreatorUserID = creatorUserID,
                Number = number,
                IsDefault = isDefault,
                type = PhoneNumberType.mobile
            };
            return phoneNumber;
        }

        public static PhoneNumber CreatePhone(int creatorUserID, string number, bool isDefault)
        {
            var phoneNumber = new PhoneNumber
            {
                CreateDate = DateTime.Now,
                CreatorUserID = creatorUserID,
                Number = number,
                IsDefault = isDefault,
                type = PhoneNumberType.phone
            };
            return phoneNumber;
        }

        public static PhoneNumber CreateFax(int creatorUserID, string number, bool isDefault)
        {
            var phoneNumber = new PhoneNumber
            {
                CreateDate = DateTime.Now,
                CreatorUserID = creatorUserID,
                Number = number,
                IsDefault = isDefault,
                type = PhoneNumberType.fax
            };
            return phoneNumber;
        }

        public static PhoneNumber CreateMobile(string number, bool isDefault)
        {
            var phoneNumber = new PhoneNumber
            {
                CreateDate = DateTime.Now,
                Number = number,
                IsDefault = isDefault,
                type = PhoneNumberType.mobile
            };
            return phoneNumber;
        }

        public static PhoneNumber CreatePhone(string number, bool isDefault)
        {
            var phoneNumber = new PhoneNumber
            {
                CreateDate = DateTime.Now,
                Number = number,
                IsDefault = isDefault,
                type = PhoneNumberType.phone
            };
            return phoneNumber;
        }

        public static PhoneNumber CreateFax(string number, bool isDefault)
        {
            var phoneNumber = new PhoneNumber
            {
                CreateDate = DateTime.Now,
                Number = number,
                IsDefault = isDefault,
                type = PhoneNumberType.fax
            };
            return phoneNumber;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }

        public override string ToString()
        {
            return Number;
        }
    }
}
