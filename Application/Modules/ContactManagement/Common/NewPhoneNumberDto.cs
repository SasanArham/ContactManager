using Domain.Modules.Shared;

namespace Application.Modules.ContactManagement.Common
{
    public record NewPhoneNumberDto
    {
        public string Number { get; init; } = string.Empty;
        public PhoneNumberType Type { get; init; }
    }
}
