using WerewolfDomain.Phases.Shared;

namespace WerewolfDomain.Interfaces.Persisters {
	public interface PhasePersister {
		bool InterruptingPhaseTypeExists();
		void PushInterruptingPhaseType(PhaseType type);
		PhaseType PopInterruptingPhaseType();

		bool IsPhaseSetup();
		void SetPhaseSetup(bool IsSetup);

		PhaseType GetCurrentPhaseType();
		void SetCurrentPhaseType(PhaseType type);
		PhaseType GetLastOrderedPhaseType();
		void SetLastOrderedPhaseType(PhaseType phaseType);
	}
}