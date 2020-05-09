using PhaseLibrary;
using System;

namespace PhaseTests.Mocks {
	internal class PhaseTestImpl : Phase {
		public int Id;
		public bool ShouldResolve;
		public bool CanResolveCalled = false;
		public bool PhaseResolveCalled = false;
		public bool PhaseSetUpCalled = false;
		public bool PreForceResolveCalled = false;
		public PhaseTestImpl(PhaseFactory factory, int Id, bool ShouldResolve) : base(factory) {
			this.Id = Id;
			this.ShouldResolve = ShouldResolve;
		}


		protected override bool CanResolve() {
			CanResolveCalled = true;
			return ShouldResolve;
		}

		protected override void PhaseResolve() {
			PhaseResolveCalled = true; ;
		}

		protected override void PhaseSetUp() {
			PhaseSetUpCalled = true; ;
		}

		protected override void PreForceResolve() {
			PreForceResolveCalled = true;
		}
	}
}
