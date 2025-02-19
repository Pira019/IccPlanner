namespace Domain.Abstractions
{
    public static class MinistryError
    {
        static public readonly Error NAME_EXISTS = new("NAME_EXISTS", "This name is already taken.");
        static public readonly Error NAME_NOT_FOUND = new("NAME_NOT_FOUND", "Ministry not found.");
    }
}
