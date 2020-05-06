using PhaseLibrary;
using WerewolfDomain.Phases.Shared;

namespace WerewolfDomain.Interfaces.Persisters {
	public interface PhasePersister {
		void AddNextPhase(Phase phase);
		Phase GetNextPhase(PhaseType currentPhaseType);
		bool IsPhaseSetup();
		bool NextPhaseExists();
		void SetPhaseSetup(bool IsSetup);
	}
}