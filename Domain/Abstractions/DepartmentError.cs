namespace Domain.Abstractions
{
    public static class DepartmentError
    {
        static public readonly Error NAME_EXISTS = new("NAME_EXISTS", "This department name is already taken.");
        static public readonly Error DEPARTMENT_NOT_EXISTS = new("DEPARTMENT_NOT_EXISTS", "This department not exists.");
    }
} 