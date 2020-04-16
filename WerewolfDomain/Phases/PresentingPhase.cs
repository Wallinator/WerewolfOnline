using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhaseLibrary;
using WerewolfDomain.Interfaces;

namespace WerewolfDomain.Phases {
	internal abstract class PresentingPhase : Phase {
		protected Presentor presentor;
		protected Persistor persistor;

		protected PresentingPhase(PhaseFactory factory, Action<Phase> setPhaseAction, Presentor presentor, Persistor persistor) : base(factory, setPhaseAction) {
			this.presentor = presentor;
			this.persistor = persistor;
		}
	}
}
