using Domain.Base;
using Domain.Modules.Shared;

namespace Domain.Modules.ContactManagement
{
    public class Contact : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; } = null;
        public string IntroducerName { get; set; } = string.Empty;
        public int? IntroducerPersonID { get; set; }
        public int? AccountManagerID { get; set; }
        public List<Adress> Addresses { get; set; } = new();
        public List<PhoneNumber> PhoneNumbers { get; set; } = new();

        public void AddAddress(int creatorUserID, int? cityID, string address, string postalCode)
        {
            bool mustBeDefAddress = !Addresses.Any();
            var newaAddress = new Adress(creatorUserID, cityID, address, mustBeDefAddress, postalCode);
            Addresses.Add(newaAddress);
        }

        public virtual Adress GetDefaultAddress(bool returnFirstAddressIfNonWasDefault = true)
        {
            var defaultAddress = Addresses.FirstOrDefault(c => c.IsDefault);
            if (defaultAddress is null)
            {
                if (!returnFirstAddressIfNonWasDefault)
                {
                    return null;
                }
                defaultAddress = Addresses.FirstOrDefault();
            }
            return defaultAddress;
        }

        public virtual void AddPhoneNumber(int creatorUserID, string number)
        {
            bool mustBeDefault = !PhoneNumbers.Any();
            var phoneNumber = PhoneNumber.CreatePhone(creatorUserID, number, mustBeDefault);
            PhoneNumbers.Add(phoneNumber);
        }

        public virtual void AddMobile(int creatorUserID, string number)
        {
            bool mustBeDefault = !PhoneNumbers.Any();
            var phoneNumber = PhoneNumber.CreateMobile(creatorUserID, number, mustBeDefault);
            PhoneNumbers.Add(phoneNumber);
        }

        public virtual void AddFax(int creatorUserID, string number)
        {
            bool mustBeDefault = !PhoneNumbers.Any();
            var phoneNumber = PhoneNumber.CreateFax(creatorUserID, number, mustBeDefault);
            PhoneNumbers.Add(phoneNumber);
        }

        public virtual void AddPhoneNumber(string number)
        {
            bool mustBeDefault = !PhoneNumbers.Any();
            var phoneNumber = PhoneNumber.CreatePhone(number, mustBeDefault);
            PhoneNumbers.Add(phoneNumber);
        }

        public virtual void AddMobile(string number)
        {
            bool mustBeDefault = !PhoneNumbers.Any();
            var phoneNumber = PhoneNumber.CreateMobile(number, mustBeDefault);
            PhoneNumbers.Add(phoneNumber);
        }

        public virtual void AddFax(string number)
        {
            bool mustBeDefault = !PhoneNumbers.Any();
            var phoneNumber = PhoneNumber.CreateFax(number, mustBeDefault);
            PhoneNumbers.Add(phoneNumber);
        }

        public virtual PhoneNumber GetDefaultPhone(bool returnFirstAddressIfNonWasDefault = true)
        {
            var number = PhoneNumbers.FirstOrDefault(c => c.type == PhoneNumberType.phone && c.IsDefault);
            if (number is null)
            {
                if (returnFirstAddressIfNonWasDefault)
                {
                    number = PhoneNumbers.FirstOrDefault(c => c.type == PhoneNumberType.phone);
                }
            }
            return number;
        }

        public virtual PhoneNumber GetDefaultMobile(bool returnFirstAddressIfNonWasDefault = true)
        {
            var number = PhoneNumbers.FirstOrDefault(c => c.type == PhoneNumberType.mobile && c.IsDefault);
            if (number is null)
            {
                if (returnFirstAddressIfNonWasDefault)
                {
                    number = PhoneNumbers.FirstOrDefault(c => c.type == PhoneNumberType.mobile);
                }
            }
            return number;
        }

        public virtual PhoneNumber GetDefaultFax(bool returnFirstAddressIfNonWasDefault = true)
        {
            var number = PhoneNumbers.FirstOrDefault(c => c.type == PhoneNumberType.fax && c.IsDefault);
            if (number is null)
            {
                if (returnFirstAddressIfNonWasDefault)
                {
                    number = PhoneNumbers.FirstOrDefault(c => c.type == PhoneNumberType.fax);
                }
            }
            return number;
        }

        public IEnumerable<PhoneNumber> GetMobiles()
        {
            return PhoneNumbers.Where(c => c.type == PhoneNumberType.mobile);
        }
        public IEnumerable<PhoneNumber> GetFaxes()
        {
            return PhoneNumbers.Where(c => c.type == PhoneNumberType.fax);
        }
        public IEnumerable<PhoneNumber> GetPhones()
        {
            return PhoneNumbers.Where(c => c.type == PhoneNumberType.phone);
        }

        public bool HasNumber(PhoneNumber number) => PhoneNumbers.Contains(number);
    }
}
