﻿using System.Collections.Generic;

namespace WerewolfDomain.Roles {
    public class Seer : Role {
        public List<Player> Checked = new List<Player>();
        public Seer() {
            Name = RoleName.Seer;
        }
    }
}
























