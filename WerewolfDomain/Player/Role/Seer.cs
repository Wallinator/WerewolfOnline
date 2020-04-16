using System.Collections.Generic;
using WerewolfDomain.Play;

namespace WerewolfDomain.Player.Role {
    public class Seer : Role {
        public List<Player> Checked = new List<Player>();
        public Seer() {
            Name = RoleName.Seer;
        }
    }
}
























