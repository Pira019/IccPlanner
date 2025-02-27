namespace Infrastructure.Security.Constants
{
    public static class ClaimsConstants
    {
       
        public const string PERMISSION = "Permission";

        // Permissions liées aux rôles
        public const string CAN_CREATE_ROLE = "CanCreateRole";
        public const string CAN_READ_ROLE = "CanReadRole";

        // Permissions liées aux Ministères
        public const string CAN_CREATE_MINISTRY = "CanCreateMinistry";

        // Permissions liées aux Départements 
        public const string CAN_CREATE_DEPARTMENT = "CanCreateDepartement";
        public const string CAN_ATTRIBUT_DEPARTMENT_CHEF = "CanAttributDepartmentChef";


        // Permissions départements 
        public const string CAN_CREATE_PROGRAM = "CanCreateProgram";

    }
}
