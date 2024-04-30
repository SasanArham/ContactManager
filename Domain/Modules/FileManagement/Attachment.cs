using Domain.Base;

namespace Domain.Modules.FileManagement
{
    public class Attachment : ValueObject
    {
        public string Name { get; protected set; } = string.Empty;
        public string Url { get; protected set; } = string.Empty;
        public string Id { get; set; } = string.Empty;

        public Attachment()
        {

        }

        public static Attachment Create(string name, string url)
        {
            var attachment = new Attachment
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Url = url
            };
            return attachment;
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
