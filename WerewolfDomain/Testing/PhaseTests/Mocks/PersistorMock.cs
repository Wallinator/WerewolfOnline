using System;
using System.Collections.Generic;
using WerewolfDomain.Entities;
using WerewolfDomain.Phases.Shared;

namespace WerewolfDomainTests.PhaseTests.Mocks {
	internal class PersistorMock : PersisterObject {
		internal Dictionary<PhaseType, bool> AllPhasesExist() {
			Dictionary<PhaseType, bool> dict = new Dictionary<PhaseType, bool>();
			foreach (PhaseType type in Enum.GetValues(typeof(PhaseType))) {
				dict[type] = true;
			}
			return dict;
		}
	}
}
