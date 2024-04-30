using Domain.Modules.ContactManagement.People.Events;
using Domain.Modules.FileManagement;

namespace Domain.Modules.ContactManagement.People
{
    public class Person : Contact
    {
        public string LastName { get; set; } = string.Empty;
        public string NickName { get; set; } = string.Empty;
        public string CourseField { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;
        public DateTimeOffset? BirthDate { get; set; }
        public string NationalCode { get; set; } = string.Empty;
        public Gender? Gender { get; set; } = null;
        public int? EducationDegreeID { get; set; }
        public int? MarriageStatusID { get; set; }
        public virtual Person IntroducerPerson { get; set; }
        public virtual List<Person> IntroducdPeople { get; set; }
        public virtual MarriageStatus MarriageStatus { get; set; }
        public virtual EducationDegree EducationDegree { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }

        public Person()
        {
        }

        public void Edit(string name, string lastName, string nationlaCode, Gender? gender)
        {
            Name = name;
            Gender = gender;
            LastName = lastName;
            NationalCode = nationlaCode;
            AddDomainEvent(new PersonEditedEvent
            {
                ID = ID
            });
        }

        public override void Delete()
        {
            base.Delete();
            AddDomainEvent(new PersonDeletedEvent
            {
                ID = ID
            });
        }

        public void EditEducation(int? educationDegreeID)
        {
            EducationDegreeID = educationDegreeID;
        }

        public string  AddAttachment(string name,string url)
        {
            var attachment = Attachment.Create(name, url);
            Attachments.Add(attachment);
            return attachment.Id;
        }
    }
}
