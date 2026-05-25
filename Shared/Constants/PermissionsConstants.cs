namespace Shared.Constants
{
    /// <summary>
    ///     Constantes des permissions du systeme.
    /// </summary>
    public static class PermissionsConstants
    {
        // Roles
        public const string CAN_READ_ROLE = "CanReadRole";
        public const string CAN_READ_ROLE_DESC = "Peut lire un role";
        public const string CAN_CREATE_ROLE = "CanCreateRole";
        public const string CAN_CREATE_ROLE_DESC = "Peut creer un role";
        public const string CAN_MANAG_ROLES = "CanManagRoles";
        public const string CAN_MANAG_ROLES_DESC = "Peut gerer les roles";

        // Ministere
        public const string CAN_CREATE_MINISTRY = "CanCreateMinistry";
        public const string CAN_CREATE_MINISTRY_DESC = "Peut gerer un ministere";

        // Departement
        public const string CAN_MANAG_DEPART = "CanManagDepart";
        public const string CAN_MANAG_DEPART_DESC = "Peut gerer un departement (Creer, Supprimer et Modifier)";
        public const string CAN_ATTRIBUT_DEPARTMENT_CHEF = "CanAttributDepartmentChef";
        public const string CAN_ATTRIBUT_DEPARTMENT_CHEF_DESC = "Peut attribuer un chef de departement";
        public const string CAN_CREATE_DEPARTMENT = "CanCreateDepartement";
        public const string CAN_CREATE_DEPARTMENT_DESC = "Peut creer un departement";
        public const string DEPART_MANAGER = "depart:manager";
        public const string DEPART_MANAGER_DESC = "Droit de gestion sur le departement auquel l'utilisateur est rattache";
        public const string MANAGE_PRG_DETAILS = "manage_program_details";
        public const string MANAGE_PRG_DETAILS_DESC = "Permet de gerer les details des programmes d'un departement";

        // Programme
        public const string PRG_MANAGER = "prg:manager";
        public const string PRG_MANAGER_DESC = "Peut gerer un programme (Creer, Supprimer et Modifier)";

        // Service
        public const string MANAGER_SERVICE = "ManagerService";
        public const string MANAGER_SERVICE_DESC = "Peut gerer les services";

        // Categories
        public const string CAT_ROLE = "Role";
        public const string CAT_MINISTRY = "Ministry";
        public const string CAT_DEPT = "Dept";
        public const string CAT_PRG = "Prg";
        public const string CAT_SERVICE = "Service";
    }
}
