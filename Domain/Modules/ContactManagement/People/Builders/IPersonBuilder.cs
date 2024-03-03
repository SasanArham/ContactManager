namespace Domain.Modules.ContactManagement.People.Builders
{
    public interface IPersonBuilder
    {
            IPersonBuilder Reset();
            IPersonBuilder SetNationalCode(string nationalCode);
            IPersonBuilder SetGender(Gender? gender);
            IPersonBuilder SetName(string name);
            IPersonBuilder SetLastName(string lastName);
            IPersonBuilder SetMoiles(List<string> mobiles);
            IPersonBuilder SetMoile(string mobile);
            IPersonBuilder SetPhones(List<string> phones);
            IPersonBuilder SetFaxes(List<string> faxes);
            IPersonBuilder SetAddresses(int[] cityIDs, string[] addresses);
            IPersonBuilder AddAddresses(int? cityID, string address);
            IPersonBuilder AddDefaultAddresses(int? cityID, string address);

            Person Get();
    }
}
