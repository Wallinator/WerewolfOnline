using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WerewolfWebApp.Model;

namespace WerewolfWebApp.Services.Implementation {
	public
		class DataService : IDataService {
		public GameManager Game { get; set; }
		public bool GameExists { get; set; } = false;
		public bool LobbyExists { get; set; } = false;
		public Lobby Lobby { get; set; }
	}
}
