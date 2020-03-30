using System;

namespace WerewolfOnline.Model.Phases {
	public abstract class AbstractPhase {
		protected AbstractPhase NextPhase;
		protected PhaseType NextPhaseType;
		protected GameManager gm;
		public bool IsSetup { get; protected set; } = false;
		public int MsTimer { get; set; }
		protected abstract int DefaultDuration { get; }

		private AbstractPhase(GameManager gameManager) {
			gm = gameManager;
			MsTimer = DefaultDuration;
		}
		protected AbstractPhase(GameManager gameManager, AbstractPhase nextPhase) : this(gameManager) {
			NextPhase = nextPhase;
		}
		protected AbstractPhase(GameManager gameManager, PhaseType nextPhaseType) : this(gameManager) {
			NextPhaseType = nextPhaseType;
		}
		public void SetNextPhase(AbstractPhase phase) {
			AbstractPhase TempNextPhase = NextPhase;
			AbstractPhase TempCurrentPhase = this;
			while (!TempNextPhase.Equals(null)) {
				TempCurrentPhase = TempNextPhase;
				TempNextPhase = TempNextPhase.NextPhase;
			}
			TempCurrentPhase.NextPhase = phase;
		}
		public AbstractPhase GetNextPhase() {
			if (NextPhase is null) {
				PhaseType nextnextphase = gm.gs.Options.NextPhaseDict[NextPhaseType];
				switch (NextPhaseType) {
					case PhaseType.Werewolf:
						return new WerewolfPhase(gm, nextnextphase);
					case PhaseType.Seer:
						return new SeerPhase(gm, nextnextphase);
					case PhaseType.Story:
						return new StoryPhase(gm, nextnextphase);
					case PhaseType.Discussion:
						return new DiscussionPhase(gm, nextnextphase);
					case PhaseType.Vote:
						return new VotePhase(gm, nextnextphase);
					case PhaseType.VoteResult:
						return new VoteResultPhase(gm, nextnextphase);
				}
			}
			return NextPhase;
		}
		public static void PhaseDictInit(GameState gs) {
			PhaseType previous;
			gs.Options.NextPhaseDict[PhaseType.Introduction] = PhaseType.Werewolf;

			previous = PhaseType.Werewolf;
			if (gs.Options.Roles.Contains(RoleName.Seer)) {
				gs.Options.NextPhaseDict[previous] = PhaseType.Seer;
				previous = PhaseType.Seer;
			}

			gs.Options.NextPhaseDict[previous] = PhaseType.Story;
			gs.Options.NextPhaseDict[PhaseType.Story] = PhaseType.Discussion;
			gs.Options.NextPhaseDict[PhaseType.Discussion] = PhaseType.Vote;
			gs.Options.NextPhaseDict[PhaseType.Vote] = PhaseType.VoteResult;
			gs.Options.NextPhaseDict[PhaseType.VoteResult] = PhaseType.Werewolf;
		}

		public void ForceResolve() {
			PreForceResolve();
			Resolve();
			gm.gs.Phase = GetNextPhase();
		}
		public bool ResolveIfAble() {
			if (CanResolve()) {
				gm.TimerSource.Cancel();
				gm.TimerSource.Dispose();
				Resolve();
				gm.gs.Phase = GetNextPhase();
				return true;
			}
			return false;
		}
		public abstract void SetUp();
		protected abstract bool CanResolve();
		protected abstract void Resolve();
		protected abstract void PreForceResolve();
	}


	public enum PhaseType {
		Introduction,
		Werewolf,
		Seer,
		Story,
		Discussion,
		Vote,
		VoteResult
	}
}
