using PhaseLibrary;
using WerewolfDomain.Phases.Shared;

namespace WerewolfDomain.Interfaces.Persisters {
	public interface PhasePersister {
		bool NextPhaseTypeExists();
		void PushNextPhaseType(PhaseType type);
		PhaseType PopNextPhaseType();

		bool IsPhaseSetup();
		void SetPhaseSetup(bool IsSetup);

		PhaseType GetCurrentPhaseType();
		void SetCurrentPhaseType(PhaseType type);
		PhaseType GetLastOrderedPhaseType();
		void SetLastOrderedPhaseType(PhaseType phaseType);
	}
}