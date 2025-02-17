namespace Domain.Abstractions
{
    public static class MinistryError
    {
        static public readonly Error NAME_EXISTS = new("NAME_EXISTS", "This name is already taken.");
    }
}
