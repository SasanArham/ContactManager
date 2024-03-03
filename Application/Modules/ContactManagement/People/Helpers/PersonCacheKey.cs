namespace Application.Modules.ContactManagement.People.Helpers
{
    public static class PersonCacheKey
    {
        public static string Person(int ID) => $"person-{ID}";
    }
}
