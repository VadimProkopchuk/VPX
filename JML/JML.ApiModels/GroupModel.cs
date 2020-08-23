using System;
using System.Collections.Generic;

namespace JML.ApiModels
{
    public class GroupModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public List<UserModel> Users { get; set; }
    }
}
