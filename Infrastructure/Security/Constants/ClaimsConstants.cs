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
        public const string CAN_MANANG_DEPART = "CanManagDepart";


        // Permissions départements 
        public const string CAN_CREATE_PROGRAM = "CanCreateProgram";
        public const string MANAGE_PRG_DETAIL = "manage_program_details";
        public const string DEPART_MANAGER = "depart:manager";

        // Permissions programmes 
        public const string CAN_MANAGER_PRG = "prg:manager"; 

        // Permissions liées aux Service 
        public const string MANAGER_SERVICE = "ManagerService";

    }
}
