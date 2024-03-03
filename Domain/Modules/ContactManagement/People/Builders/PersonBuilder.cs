using Domain.Modules.ContactManagement.People.Events;
using Domain.Modules.Shared;

namespace Domain.Modules.ContactManagement.People.Builders
{
    public class PersonBuilder : IPersonBuilder
    {
        private Person person;

        public PersonBuilder()
        {
            Reset();
        }

        public IPersonBuilder Reset()
        {
            person = new Person();
            person.CreateDate = DateTime.Now;
            person.Deleted = false;
            person.GuID = Guid.NewGuid().ToString();
            return this;
        }

        public IPersonBuilder AddDefaultAddresses(int? cityID, string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return this;
            }
            person.Addresses ??= new List<Adress>();
            Adress ad = new Adress(person.CreatorUserID, cityID, address, true);
            person.Addresses.Add(ad);

            return this;
        }

        public IPersonBuilder SetName(string name)
        {
            person.Name = name;
            return this;
        }

        public IPersonBuilder SetLastName(string lastName)
        {
            person.LastName = lastName;
            return this;
        }

        public IPersonBuilder SetMoiles(List<string> mobiles)
        {
            if (mobiles == null)
            {
                return this;
            }

            if (!mobiles.Any())
            {
                return this;
            }

            for (int i = 0; i < mobiles.Count; i++)
            {
                person.AddMobile(person.CreatorUserID, mobiles[i]);
            }

            return this;
        }

        public IPersonBuilder SetMoile(string mobile)
        {
            if (mobile == null)
            {
                return this;
            }

            if (mobile.Trim() == "")
            {
                return this;
            }
            person.AddMobile(person.CreatorUserID, mobile);

            return this;
        }

        public IPersonBuilder SetPhones(List<string> phones)
        {
            if (phones == null)
            {
                return this;
            }

            if (!phones.Any())
            {
                return this;
            }

            for (int i = 0; i < phones.Count; i++)
            {
                person.AddPhoneNumber(person.CreatorUserID, phones[i]);
            }

            return this;
        }

        public IPersonBuilder SetFaxes(List<string> faxes)
        {
            if (faxes == null)
            {
                return this;
            }

            if (!faxes.Any())
            {
                return this;
            }

            for (int i = 0; i < faxes.Count; i++)
            {
                person.AddFax(person.CreatorUserID, faxes[i]);
            }

            return this;
        }

        public IPersonBuilder SetNationalCode(string nationalCode)
        {
            person.NationalCode = nationalCode;
            return this;
        }

        public IPersonBuilder SetCreatorUserID(int creatorUserID)
        {
            person.CreatorUserID = creatorUserID;
            return this;
        }

        public IPersonBuilder SetGender(Gender? gender)
        {
            person.Gender = gender;
            return this;
        }

        public IPersonBuilder SetAddresses(int[] cityIDs, string[] addresses)
        {
            if (addresses == null)
            {
                return this;
            }

            if (addresses.Length == 0)
            {
                return this;
            }
            person.Addresses = new List<Adress>();
            Adress def = new Adress(person.CreatorUserID, cityIDs[0], addresses[0], true);
            person.Addresses.Add(def);


            for (int i = 1; i < addresses.Length; i++)
            {
                Adress address = new Adress(person.CreatorUserID, cityIDs[i], addresses[i], false);
                person.Addresses.Add(address);
            }

            return this;
        }

        public IPersonBuilder AddAddresses(int? cityID, string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return this;
            }
            person.Addresses ??= new List<Adress>();
            Adress ad = new Adress(person.CreatorUserID, cityID, address, false);
            person.Addresses.Add(ad);

            return this;
        }
        
        public Person Get()
        {
            person.AddDomainEvent(new PersonCreatedEvent(person.GuID));
            return person;
        }
    }
}
