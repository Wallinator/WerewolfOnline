using WerewolfOnline.Model;

namespace WerewolfOnline.Services {
	public interface IDataService {
		GameManager Game { get; set; }
		bool GameExists { get; set; }
	}
}