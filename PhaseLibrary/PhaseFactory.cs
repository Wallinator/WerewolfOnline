using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhaseLibrary {
	public abstract class PhaseFactory {
		protected internal abstract Phase MakeNextPhase(Phase phase, Action<Phase> SetPhase);
		public abstract Phase MakeFirstPhase(Action<Phase> SetPhase);
	}
}
