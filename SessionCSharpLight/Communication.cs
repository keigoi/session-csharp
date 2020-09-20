using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Session
{
	using Session.Types;

	public interface Session<out S>
		where S : SessionType
	{
	}

	public static class Communication
	{
		public static Session<S> ForkThread<S, T>(this Dual<S, T> p, Action<Session<T>> func)
			where S : SessionType
			where T : SessionType
		{
			throw new NotImplementedException();
		}


		// Send, Recv ====

		public static Session<S> Send<V, S>(this Session<Send<V, S>> p, V v)
			where S : SessionType
		{
			throw new NotImplementedException();
		}
		// Send() for unit
		public static Session<S> Send<S>(this Session<Send<Unit, S>> p)
			where S : SessionType
		{
			throw new NotImplementedException();
		}

		public static Session<S> Receive<V, S>(this Session<Recv<V, S>> p, out V v)
			where S : SessionType
		{
			throw new NotImplementedException();
		}
		// Recv() for unit
		public static Session<S> Receive<V, S>(this Session<Recv<V, S>> p)
			where S : SessionType
		{
			throw new NotImplementedException();
		}

		// convenient overloading for tuples
		public static Session<S> Receive<V1, V2, S>(this Session<Recv<(V1,V2), S>> p, out V1 v1, out V2 v2)
			where S : SessionType
		{
			throw new NotImplementedException();
		}
		public static Session<S> Receive<V1, V2, V3, S>(this Session<Recv<(V1,V2,V3), S>> p, out V1 v1, out V2 v2, out V3 v3)
			where S : SessionType
		{
			throw new NotImplementedException();
		}

		// ReceiveAsync

		public static Session<S> ReceiveAsync<V, S>(this Session<Recv<V, S>> p, out Task<V> v)
			where S : SessionType
		{
			throw new NotImplementedException();
		}
		// RecvAsync() for unit
		public static Session<S> ReceiveAsync<S>(this Session<Recv<Unit, S>> p, out Task v)
			where S : SessionType
		{
			throw new NotImplementedException();
		}

		public static void Close(this Session<Eps> p)
		{
			throw new NotImplementedException();
		}

		// SelectLeft ====

		public static Session<SL> SelectLeft<SL, SR>(this Session<Select<SL, SR>> session)
			where SL : SessionType
			where SR : SessionType
		{
			throw new NotImplementedException();
		}
		public static Session<SL> SelectLeft<SL, SM, SR>(this Session<Select<SL, SM, SR>> session)
			where SL : SessionType
			where SR : SessionType
		{
			throw new NotImplementedException();
		}

		// SelectRight ====

		public static Session<SR> SelectRight<SL, SR>(this Session<Select<SL, SR>> session)
			where SL : SessionType
			where SR : SessionType
		{
			throw new NotImplementedException();
		}
		public static Session<SR> SelectRight<SL, SM, SR>(this Session<Select<SL, SM, SR>> session)
			where SL : SessionType
			where SR : SessionType
		{
			throw new NotImplementedException();
		}

		// SelectMiddle for ternary Select

		public static Session<SM> SelectMiddle<SL, SM, SR>(this Session<Select<SL, SM, SR>> session)
			where SL : SessionType
			where SM : SessionType
			where SR : SessionType
		{
			throw new NotImplementedException();
		}

		// Quaternary Select ====

		public static Session<S0> SelectFirst<S0, S1, S2, S3>(this Session<Select<S0, S1, S2, S3>> session)
			where S0 : SessionType
			where S1 : SessionType
			where S2 : SessionType
			where S3 : SessionType
		{
			throw new NotImplementedException();
		}
		public static Session<S1> SelectSecond<S0, S1, S2, S3>(this Session<Select<S0, S1, S2, S3>> session)
			where S0 : SessionType
			where S1 : SessionType
			where S2 : SessionType
			where S3 : SessionType
		{
			throw new NotImplementedException();
		}
		public static Session<S2> SelectThird<S0, S1, S2, S3>(this Session<Select<S0, S1, S2, S3>> session)
			where S0 : SessionType
			where S1 : SessionType
			where S2 : SessionType
			where S3 : SessionType
		{
			throw new NotImplementedException();
		}
		public static Session<S3> SelectFourth<S0, S1, S2, S3>(this Session<Select<S0, S1, S2, S3>> session)
			where S0 : SessionType
			where S1 : SessionType
			where S2 : SessionType
			where S3 : SessionType
		{
			throw new NotImplementedException();
		}

		// Offer ====

		public static void Offer<SL, SR>(this Session<Offer<SL, SR>> session, Action<Session<SL>> left, Action<Session<SR>> right)
			where SL : SessionType
			where SR : SessionType
		{
			throw new NotImplementedException();
		}
		public static T Offer<SL, SR, T>(this Session<Offer<SL, SR>> session, Func<Session<SL>, T> left, Func<Session<SR>, T> right)
			where SL : SessionType
			where SR : SessionType
		{
			throw new NotImplementedException();
		}
		public static void Offer<SL, SM, SR>(this Session<Offer<SL, SM, SR>> session, Action<Session<SL>> left, Action<Session<SM>> middle, Action<Session<SR>> right)
			where SL : SessionType
			where SM : SessionType
			where SR : SessionType
		{
			throw new NotImplementedException();
		}
		public static void Offer<S0, S1, S2, S3>(this Session<Offer<S0, S1, S2, S3>> session, Action<Session<S0>> first, Action<Session<S1>> second, Action<Session<S2>> third, Action<Session<S3>> fourth)
			where S0 : SessionType
			where S1 : SessionType
			where S2 : SessionType
			where S3 : SessionType
		{
			throw new NotImplementedException();
		}

		// delegation ====

		public static Session<S> Deleg<S, S0, T0>(this Session<Deleg<S0, T0, S>> session, Session<S0> deleg)
			where S0 : SessionType
			where S : SessionType
		{
			throw new NotImplementedException();
		}
		public static Session<S> DelegNew<S, S0, T0>(this Session<Deleg<S0, T0, S>> session, out Session<T0> deleg)
			where T0 : SessionType
			where S : SessionType
		{
			throw new NotImplementedException();
		}
		public static Session<S> DelegRecv<S, S0>(this Session<DelegRecv<S0, S>> session, out Session<S0> deleg)
			where S0 : SessionType
			where S : SessionType
		{
			throw new NotImplementedException();
		}

		public static Session<T> Expand<S,T>(this Session<Srv<Dual<S, T>>> session)
			where S : SessionType
			where T: SessionType
		{
			throw new NotImplementedException();
		}
		public static Session<S> Expand<S, T>(this Session<Cli<Dual<S, T>>> session)
			where S : SessionType
			where T : SessionType
		{
			throw new NotImplementedException();
		}
	}
}
