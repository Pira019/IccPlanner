namespace Domain.Abstractions
{
    public static class DepartmentError
    { 
        static public readonly Error DEPARTMENT_LIST_NOT_EXISTS = new("DEPARTMENT_LIST_NOT_EXISTS", "The following id departments do not exist: {0}.");
        static public readonly Error DEPARTMENT_INVALID = new("DEPARTMENT_INVALID", "Invalid department Id");
    }
} 