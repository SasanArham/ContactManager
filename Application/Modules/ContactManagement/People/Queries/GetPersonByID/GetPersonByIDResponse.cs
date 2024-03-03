﻿using Domain.Modules.ContactManagement.People;

namespace Application.Modules.ContactManagement.People.Queries.GetPersonByID
{
    public record GetPersonByIDResponse
    {
        public int ID { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; } = null;
        public string LastName { get; set; } = string.Empty;
        public string NickName { get; set; } = string.Empty;
        public string CourseField { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;
        public DateTimeOffset? BirthDate { get; set; }
        public string NationalCode { get; set; } = string.Empty;
        public Gender? Gender { get; set; } = null;
    }
}
