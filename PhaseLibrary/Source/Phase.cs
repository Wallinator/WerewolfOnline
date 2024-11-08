﻿using System;
using System.Threading;
namespace PhaseLibrary {
	public abstract class Phase {
		protected PhaseFactory PhFactory;
		private bool isSetup = false;

		protected virtual bool IsSetup {
			get => isSetup; set => isSetup = value;
		}


		protected Phase(PhaseFactory factory) {
			PhFactory = factory;
		}

		public virtual Phase StateHasChanged() {
			if (CanResolve()) {
				Resolve();
				return NextPhase();
			}
			return this;
		}


		public virtual void SetUp() {
			if (!IsSetup) {
				PhaseSetUp();
				IsSetup = true;
			}
		}

		public virtual Phase ForceResolve() {
			PreForceResolve();
			Resolve();
			return NextPhase();
		}
		private void Resolve() {
			PhaseResolve();
		}
		protected virtual Phase NextPhase() {
			CleanUp();
			return PhFactory.MakeNextPhase(this);
		}
		protected virtual void CleanUp() {
		}
		protected abstract void PhaseSetUp();
		protected abstract bool CanResolve();
		protected abstract void PhaseResolve();
		protected abstract void PreForceResolve();
	}
}
