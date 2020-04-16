using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhaseLibrary {
	public abstract class PhaseFactory {
		protected internal abstract Phase MakeNextPhase(Phase phase);
		public abstract Phase MakeFirstPhase();
	}
}
