namespace Application.Modules.ContactManagement.People.Helpers
{
    public static class PersonCacheHelper
    {
        public static string Person(int ID) => $"person-{ID}";
        public static class DefaultList
        {
            public const string Key = "people-default-list";
            public const int MaxLen = 10;
        }
    }
}
