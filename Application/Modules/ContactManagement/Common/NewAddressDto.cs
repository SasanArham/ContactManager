namespace Application.Modules.ContactManagement.Common
{
    public record NewAddressDto
    {
        public int? CityID { get; init; }

        public string Address { get; init; } = string.Empty;

        public string PostalCode { get; init; } = string.Empty;

        public bool IsDefault { get; init; } = false;
    }
}
