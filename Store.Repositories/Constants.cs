namespace Store.Repositories
{
    public class Constants
    {
        public class ProcedureNames
        {
            public const string Login = "Login_SP";
            public const string UserRole = "GetUserRole_SP";
            public const string UserPermissionCodes = "SelectUserPermissionCodes_SP";
            public const string AssignUserToRole = "AssignUserToRole_SP";
            public const string UnassignUserToRole = "UnassignUserToRole_SP";
            public const string ChangeUserPassword = "ChangeUserPassword_SP";
            public const string EmptyEmployees = "SelectEmptyEmployees_SP";
            public const string RolePermissions = "SelectRolePermissions_SP";
            public const string AssignPermissionToRole = "AssignPermissionToRole_SP";
            public const string UnassignPermissionToRole = "UnassignPermissionToRole_SP";
            public const string ProductCategory = "GetProductCategory_SP";
        }
    }
}