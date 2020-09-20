using System;
using System.Collections.Generic;
using System.Text;

namespace Session
{
	namespace Types
	{
		public sealed class Unit
		{
			public static Unit unit = new Unit();
		}

		public interface SessionType
		{ }

		public class Send<V, S> : SessionType { }

		public class Recv<V, S> : SessionType { }

		public class Select<SL, SR> : SessionType { }
		public class Select<SL, SM, SR> : SessionType { }
		public class Select<S0, S1, S2, S3> : SessionType { }

		public class Offer<SL, SR> : SessionType { }
		public class Offer<SL, SM, SR> : SessionType { }
		public class Offer<S0, S1, S2, S3> : SessionType { }

		public class Eps : SessionType { }

		public class Goto0 : SessionType { }
		public class Goto1 : SessionType { }
		public class Goto2 : SessionType { }
		public class Goto3 : SessionType { }

		public class Deleg<S0, T0, S> : SessionType { }

		public class DelegRecv<S0, S> : SessionType { }

		public class Dual<S, T>
			where S : SessionType
			where T : SessionType
		{
			public Dual(Dual<S, T> copy) { }

			internal Dual()
			{
			}
		}

		public interface Cli<out D> : SessionType { }
		public interface Srv<out D> : SessionType { }

		public class Val<V> { }

	}
}
