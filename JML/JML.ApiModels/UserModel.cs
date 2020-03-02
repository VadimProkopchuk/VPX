using System;
using JML.Domain.Enums;

namespace JML.ApiModels
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GroupName { get; set; }
        public string[] Roles { get; set; }
        public Role[] EnumRoles { get; set; }
    }
}
