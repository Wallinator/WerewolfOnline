using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WerewolfOnline.Model {
    public class Player {
        public string Id;
        public string Name;
        public Role Role = new Role();
        public Player(string id, string name) {
            Id = id;
            Name = name;
        }

        public void SetRole(RoleName name) {
            switch (name) {
                case RoleName.None:
                    Role = new Role();
                    break;
                case RoleName.Spectator:
                    Role = new Spectator();
                    break;
                case RoleName.Villager:
                    Role = new Villager();
                    break;
                case RoleName.Werewolf:
                    Role = new Werewolf();
                    break;
                case RoleName.Seer:
                    var s = new Seer();
                    s.Checked.Add(this);
                    Role = s;
                    break;
                default:
                    Role = null;
                    break;
            }
        }
    }
    
    public enum RoleName {
        //None must be on top
        None,
        Spectator,
        Villager,
        Werewolf,
        Seer
    }
    public class Role {
        public RoleName Name = RoleName.None;
    }
    public class Spectator : Role {
        public Role OldRole;
        public Spectator(Role oldRole = null) {
            OldRole = oldRole;
            Name = RoleName.Spectator;
        }
    }
    public class Villager : Role {
        public Villager() {
            Name = RoleName.Villager;
        }
    }
    public class Werewolf : Role {
        public Werewolf() {
            Name = RoleName.Werewolf;
        }
    }
    public class Seer : Role {
        public List<Player> Checked = new List<Player>();
        public Seer() {
            Name = RoleName.Seer;
        }
    }
}
























