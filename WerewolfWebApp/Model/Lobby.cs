using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WerewolfWebApp.Model.Structure;

namespace WerewolfWebApp.Model
	{
	public class Lobby {
		public List<Player> Players { get; set; } = new List<Player>();
		public bool accepting;
	}
}
