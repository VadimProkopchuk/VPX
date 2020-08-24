using System;
using System.Collections.Generic;

namespace VPX.ApiModels
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GroupName { get; set; }
        public string Email { get; set; }
        public List<RoleModel> Roles { get; set; }
        public string Image { get; set; }
    }
}
