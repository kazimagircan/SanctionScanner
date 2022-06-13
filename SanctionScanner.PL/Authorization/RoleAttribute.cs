using System;

namespace SanctionScanner.PL.Authorization
{
    [AttributeUsage(AttributeTargets.All)]
    public class RoleAttribute : Attribute
    {
        string role;
        public RoleAttribute(string Role)
        {
            this.role = Role;
        }

        public string RoleGroupID
        {
            get { return role; }
        }

    }
}
