using System.Collections.Generic;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Roles {
	internal class Seer : Role {
		internal List<Player> Checked = new List<Player>();
		internal Seer() {
			Name = RoleName.Seer;
		}
	}
}
























