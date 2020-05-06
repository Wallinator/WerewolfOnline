using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Interfaces.Persisters {
	public interface Persister : PollPersister, PlayerPersister, PhasePersister {
	}
}
