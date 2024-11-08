﻿using WerewolfWebApp.Model;

namespace WerewolfWebApp.Services {
	public interface IDataService {
		GameManager Game { get; set; }
		Lobby Lobby { get; set; }
		bool GameExists { get; set; }
		bool LobbyExists { get; set; }
	}
}