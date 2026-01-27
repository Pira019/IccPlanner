using System.Runtime.Intrinsics.X86;

namespace Infrastructure.Security.Constants
{
    public static class PolicyConstants
    {
        // Permissions liées aux rôles
        public const string CAN_CREATE_ROLE = "CAN_CREATE_ROLE";
        public const string CAN_READ_ROLE = "CAN_READ_ROLE";

        // Permissions liées aux ministère 
        public const string CAN_CREATE_MINISTRY = "CAN_CREATE_MINISTRY";
        
        // Permissions liées aux Départements  
        public const string CAN_CREATE_DEPARTMENT = "CAN_CREATE_DEPARTMENT";
        public const string CAN_ATTRIBUT_DEPARTMENT_CHEF = "CAN_ATTRIBUT_DEPARTMENT_CHEF";
        public const string CAN_MANG_DEPART = "CanManagDepart";
        
        
        // Permissions liées aux Programmes  
        public const string CAN_MANAG_PROGRAM = "CAN_MANAG_PROGRAM";
        public const string CAN_MANG_DEPART_DETAIL = "CAN_MANG_DEPART_DETAIL";

        // Permissions liées aux Service 
        public const string MANAGER_SERVICE = "ManagerService";
    }
}
