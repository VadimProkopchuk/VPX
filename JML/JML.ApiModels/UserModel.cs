using System;
using System.Collections.Generic;

namespace JML.ApiModels
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GroupName { get; set; }
        public List<RoleModel> Roles { get; set; }
    }
}
