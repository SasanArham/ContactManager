using Domain.Modules.ContactManagement.People;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Modules.ContactManagement.People.Helpers
{
    public static class PersonAttachmentExtension
    {
        public static string GenerateAttachmentUrl(this Person person, string fileName) => $"People/{person.GuID}/{Guid.NewGuid()}-{fileName}";
    }
}
