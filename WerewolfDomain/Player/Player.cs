using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WerewolfDomain.Player.Roles;

namespace WerewolfDomain.Player {
    public class Player {
        public string Id;
        public string Name;
        public Role Role = new Role();
        public Player(string id, string name) {
            Id = id;
            Name = name;
        }

        public void SetRole(RoleName name) {
            Role = RoleFactory.MakeRole(name);
        }

        public override bool Equals(object obj) {
            return obj is Player player &&
                   Id == player.Id &&
                   Name == player.Name;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Id, Name);
        }


    }
}

























