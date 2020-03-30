using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WerewolfOnline.Model;

namespace WerewolfOnline.Services {
	public
		class DataService {
		private int i = 0;
		public List<GameManager> GameList;
		public DataService() {
			GameList = new List<GameManager>();
		}
		public int GetNextGameId() {
			return ++i;
		}
	}
}
