using System;
using System.Threading.Tasks;

namespace Session
{
	public delegate Session<Eps, Any, P> DepleteAction<S, P>(Session<S, Any, P> session) where S : SessionType where P : ProtocolType;

	public delegate Session<Eps, Any, P> DepleteRecAction<S, P>(Session<S, Any, P> session, DepleteRecAction<S, P> deplete) where S : SessionType where P : ProtocolType;

	public delegate Task<Session<Eps, Any, P>> DepleteAsyncAction<S, P>(Session<S, Any, P> session) where S : SessionType where P : ProtocolType;

	public delegate Task<Session<Eps, Any, P>> DepleteRecAsyncAction<S, P>(Session<S, Any, P> session, DepleteRecAsyncAction<S, P> deplete) where S : SessionType where P : ProtocolType;

	public delegate Session<Eps, Any, P> DepleteAction<S, P, T>(Session<S, Any, P> session, T arg) where S : SessionType where P : ProtocolType;

	public delegate Session<Eps, Any, P> DepleteRecAction<S, P, T>(Session<S, Any, P> session, T arg, DepleteRecAction<S, P, T> deplete) where S : SessionType where P : ProtocolType;

	public delegate Task<Session<Eps, Any, P>> DepleteAsyncAction<S, P, T>(Session<S, Any, P> session, T arg) where S : SessionType where P : ProtocolType;

	public delegate Task<Session<Eps, Any, P>> DepleteRecAsyncAction<S, P, T>(Session<S, Any, P> session, T arg, DepleteRecAsyncAction<S, P, T> deplete) where S : SessionType where P : ProtocolType;

	public delegate (Session<Eps, Any, P> depleted, T result) DepleteFunc<S, P, T>(Session<S, Any, P> session) where S : SessionType where P : ProtocolType;

	public delegate (Session<Eps, Any, P> depleted, T result) DepleteRecFunc<S, P, T>(Session<S, Any, P> session, DepleteRecFunc<S, P, T> deplete) where S : SessionType where P : ProtocolType;

	public delegate Task<(Session<Eps, Any, P> depleted, T result)> DepleteAsyncFunc<S, P, T>(Session<S, Any, P> session) where S : SessionType where P : ProtocolType;

	public delegate Task<(Session<Eps, Any, P> depleted, T result)> DepleteRecAsyncFunc<S, P, T>(Session<S, Any, P> session, DepleteRecAsyncFunc<S, P, T> deplete) where S : SessionType where P : ProtocolType;

	public delegate (Session<Eps, Any, P> depleted, U result) DepleteFunc<S, P, T, U>(Session<S, Any, P> session, T arg) where S : SessionType where P : ProtocolType;

	public delegate (Session<Eps, Any, P> depleted, U result) DepleteRecFunc<S, P, T, U>(Session<S, Any, P> session, T arg, DepleteRecFunc<S, P, T, U> deplete) where S : SessionType where P : ProtocolType;

	public delegate Task<(Session<Eps, Any, P> depleted, U result)> DepleteAsyncFunc<S, P, T, U>(Session<S, Any, P> session, T arg) where S : SessionType where P : ProtocolType;

	public delegate Task<(Session<Eps, Any, P> depleted, U result)> DepleteRecAsyncFunc<S, P, T, U>(Session<S, Any, P> session, T arg, DepleteRecAsyncFunc<S, P, T, U> deplete) where S : SessionType where P : ProtocolType;

	public static class SessionInterface
	{
		public static Session<S, E, P> Send<S, E, P>(this Session<Send<S>, E, P> session) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Send();
			return session.ToNextSession<S>();
		}

		public static Session<S, E, P> Send<T, S, E, P>(this Session<Send<T, S>, E, P> session, T value) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Send(value);
			return session.ToNextSession<S>();
		}

		public static async Task<Session<S, E, P>> SendAsync<S, E, P>(this Session<Send<S>, E, P> session) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			await session.SendAsync();
			return session.ToNextSession<S>();
		}

		public static async Task<Session<S, E, P>> SendAsync<T, S, E, P>(this Session<Send<T, S>, E, P> session, T value) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			await session.SendAsync(value);
			return session.ToNextSession<S>();
		}

		public static Session<S, E, P> Receive<S, E, P>(this Session<Receive<S>, E, P> session) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Receive();
			return session.ToNextSession<S>();
		}

		public static (Session<S, E, P> continuation, T value) Receive<T, S, E, P>(this Session<Receive<T, S>, E, P> session) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			return (session.ToNextSession<S>(), session.Receive<T>());
		}

		public static Session<S, E, P> Receive<T, S, E, P>(this Session<Receive<T, S>, E, P> session, out T value) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			value = session.Receive<T>();
			return session.ToNextSession<S>();
		}

		public static async Task<Session<S, E, P>> ReceiveAsync<S, E, P>(this Session<Receive<S>, E, P> session) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			await session.ReceiveAsync();
			return session.ToNextSession<S>();
		}

		public static async Task<(Session<S, E, P> continuation, T value)> ReceiveAsync<T, S, E, P>(this Session<Receive<T, S>, E, P> session) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			return (session.ToNextSession<S>(), await session.ReceiveAsync<T>());
		}

		public static Session<L, E, P> SelectLeft<L, R, E, P>(this Session<Select<L, R>, E, P> session) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Select(Selection.Left);
			return session.ToNextSession<L>();
		}

		public static Session<R, E, P> SelectRight<L, R, E, P>(this Session<Select<L, R>, E, P> session) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Select(Selection.Right);
			return session.ToNextSession<R>();
		}

		public static Session<L, E, P> SelectLeft<L, C, R, E, P>(this Session<Select<L, C, R>, E, P> session) where L : SessionType where C : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Select(Selection.Left);
			return session.ToNextSession<L>();
		}

		public static Session<C, E, P> SelectCenter<L, C, R, E, P>(this Session<Select<L, C, R>, E, P> session) where L : SessionType where C : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Select(Selection.Center);
			return session.ToNextSession<C>();
		}

		public static Session<R, E, P> SelectRight<L, C, R, E, P>(this Session<Select<L, C, R>, E, P> session) where L : SessionType where C : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Select(Selection.Right);
			return session.ToNextSession<R>();
		}

		public static async Task<Session<L, E, P>> SelectLeftAsync<L, R, E, P>(this Session<Select<L, R>, E, P> session) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			await session.SelectAsync(Selection.Left);
			return session.ToNextSession<L>();
		}

		public static async Task<Session<R, E, P>> SelectRightAsync<L, R, E, P>(this Session<Select<L, R>, E, P> session) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			await session.SelectAsync(Selection.Right);
			return session.ToNextSession<R>();
		}

		public static async Task<Session<L, E, P>> SelectLeftAsync<L, C, R, E, P>(this Session<Select<L, C, R>, E, P> session) where L : SessionType where C : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			await session.SelectAsync(Selection.Left);
			return session.ToNextSession<L>();
		}

		public static async Task<Session<C, E, P>> SelectCenterAsync<L, C, R, E, P>(this Session<Select<L, C, R>, E, P> session) where L : SessionType where C : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			await session.SelectAsync(Selection.Center);
			return session.ToNextSession<C>();
		}

		public static async Task<Session<R, E, P>> SelectRightAsync<L, C, R, E, P>(this Session<Select<L, C, R>, E, P> session) where L : SessionType where C : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			await session.SelectAsync(Selection.Right);
			return session.ToNextSession<R>();
		}

		public static void Follow<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Action<Session<L, E, P>> leftAction, Action<Session<R, E, P>> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftAction is null) throw new ArgumentNullException(nameof(leftAction));
			if (rightAction is null) throw new ArgumentNullException(nameof(rightAction));
			switch (session.Follow())
			{
				case Selection.Left:
					leftAction(session.ToNextSession<L>());
					break;
				case Selection.Right:
					rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task Follow<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Func<Session<L, E, P>, Task> leftAction, Action<Session<R, E, P>> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftAction is null) throw new ArgumentNullException(nameof(leftAction));
			if (rightAction is null) throw new ArgumentNullException(nameof(rightAction));
			switch (session.Follow())
			{
				case Selection.Left:
					await leftAction(session.ToNextSession<L>());
					break;
				case Selection.Right:
					rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task Follow<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Action<Session<L, E, P>> leftAction, Func<Session<R, E, P>, Task> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftAction is null) throw new ArgumentNullException(nameof(leftAction));
			if (rightAction is null) throw new ArgumentNullException(nameof(rightAction));
			switch (session.Follow())
			{
				case Selection.Left:
					leftAction(session.ToNextSession<L>());
					break;
				case Selection.Right:
					await rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task Follow<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Func<Session<L, E, P>, Task> leftAction, Func<Session<R, E, P>, Task> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftAction is null) throw new ArgumentNullException(nameof(leftAction));
			if (rightAction is null) throw new ArgumentNullException(nameof(rightAction));
			switch (session.Follow())
			{
				case Selection.Left:
					await leftAction(session.ToNextSession<L>());
					break;
				case Selection.Right:
					await rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static T Follow<L, R, E, P, T>(this Session<Follow<L, R>, E, P> session, Func<Session<L, E, P>, T> leftFunc, Func<Session<R, E, P>, T> rightFunc) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftFunc is null) throw new ArgumentNullException(nameof(leftFunc));
			if (rightFunc is null) throw new ArgumentNullException(nameof(rightFunc));
			switch (session.Follow())
			{
				case Selection.Left:
					return leftFunc(session.ToNextSession<L>());
				case Selection.Right:
					return rightFunc(session.ToNextSession<R>());
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task<T> Follow<L, R, E, P, T>(this Session<Follow<L, R>, E, P> session, Func<Session<L, E, P>, Task<T>> leftFunc, Func<Session<R, E, P>, T> rightFunc) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftFunc is null) throw new ArgumentNullException(nameof(leftFunc));
			if (rightFunc is null) throw new ArgumentNullException(nameof(rightFunc));
			switch (session.Follow())
			{
				case Selection.Left:
					return await leftFunc(session.ToNextSession<L>());
				case Selection.Right:
					return rightFunc(session.ToNextSession<R>());
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task<T> Follow<L, R, E, P, T>(this Session<Follow<L, R>, E, P> session, Func<Session<L, E, P>, T> leftFunc, Func<Session<R, E, P>, Task<T>> rightFunc) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftFunc is null) throw new ArgumentNullException(nameof(leftFunc));
			if (rightFunc is null) throw new ArgumentNullException(nameof(rightFunc));
			switch (session.Follow())
			{
				case Selection.Left:
					return leftFunc(session.ToNextSession<L>());
				case Selection.Right:
					return await rightFunc(session.ToNextSession<R>());
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task<T> Follow<L, R, E, P, T>(this Session<Follow<L, R>, E, P> session, Func<Session<L, E, P>, Task<T>> leftFunc, Func<Session<R, E, P>, Task<T>> rightFunc) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftFunc is null) throw new ArgumentNullException(nameof(leftFunc));
			if (rightFunc is null) throw new ArgumentNullException(nameof(rightFunc));
			switch (session.Follow())
			{
				case Selection.Left:
					return await leftFunc(session.ToNextSession<L>());
				case Selection.Right:
					return await rightFunc(session.ToNextSession<R>());
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task FollowAsync<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Action<Session<L, E, P>> leftAction, Action<Session<R, E, P>> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftAction is null) throw new ArgumentNullException(nameof(leftAction));
			if (rightAction is null) throw new ArgumentNullException(nameof(rightAction));
			switch (await session.FollowAsync())
			{
				case Selection.Left:
					leftAction(session.ToNextSession<L>());
					break;
				case Selection.Right:
					rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task FollowAsync<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Func<Session<L, E, P>, Task> leftAction, Action<Session<R, E, P>> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftAction is null) throw new ArgumentNullException(nameof(leftAction));
			if (rightAction is null) throw new ArgumentNullException(nameof(rightAction));
			switch (await session.FollowAsync())
			{
				case Selection.Left:
					await leftAction(session.ToNextSession<L>());
					break;
				case Selection.Right:
					rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task FollowAsync<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Action<Session<L, E, P>> leftAction, Func<Session<R, E, P>, Task> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftAction is null) throw new ArgumentNullException(nameof(leftAction));
			if (rightAction is null) throw new ArgumentNullException(nameof(rightAction));
			switch (await session.FollowAsync())
			{
				case Selection.Left:
					leftAction(session.ToNextSession<L>());
					break;
				case Selection.Right:
					await rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task FollowAsync<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Func<Session<L, E, P>, Task> leftAction, Func<Session<R, E, P>, Task> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftAction is null) throw new ArgumentNullException(nameof(leftAction));
			if (rightAction is null) throw new ArgumentNullException(nameof(rightAction));
			switch (await session.FollowAsync())
			{
				case Selection.Left:
					await leftAction(session.ToNextSession<L>());
					break;
				case Selection.Right:
					await rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static Session<S, E, S> Goto<S, E>(this Session<Call0, E, S> session) where S : SessionType where E : SessionStack
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S>();
		}

		public static Session<S0, E, Cons<S0, L>> Goto<S0, E, L>(this Session<Call0, E, Cons<S0, L>> session) where S0 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S0>();
		}

		public static Session<S1, E, Cons<S0, Cons<S1, L>>> Goto<S0, S1, E, L>(this Session<Call1, E, Cons<S0, Cons<S1, L>>> session) where S0 : SessionType where S1 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S1>();
		}

		public static Session<S2, E, Cons<S0, Cons<S1, Cons<S2, L>>>> Goto<S0, S1, S2, E, L>(this Session<Call2, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S2>();
		}

		public static Session<S3, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> Goto<S0, S1, S2, S3, E, L>(this Session<Call3, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S3>();
		}

		public static Session<S4, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> Goto<S0, S1, S2, S3, S4, E, L>(this Session<Call4, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S4>();
		}

		public static Session<S5, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> Goto<S0, S1, S2, S3, S4, S5, E, L>(this Session<Call5, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S5>();
		}

		public static Session<S6, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> Goto<S0, S1, S2, S3, S4, S5, S6, E, L>(this Session<Call6, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S6>();
		}

		public static Session<S7, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> Goto<S0, S1, S2, S3, S4, S5, S6, S7, E, L>(this Session<Call7, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S7>();
		}

		public static Session<S8, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> Goto<S0, S1, S2, S3, S4, S5, S6, S7, S8, E, L>(this Session<Call8, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S8>();
		}

		public static Session<S9, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> Goto<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, E, L>(this Session<Call9, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S9>();
		}

		public static Session<S, Push<Z, E>, S> Call<S, Z, E, L>(this Session<Call0<Z>, E, S> session) where S : SessionType where Z : SessionType where E : SessionStack
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S, Push<Z, E>>();
		}

		public static Session<S0, Push<Z, E>, Cons<S0, L>> Call<S0, Z, E, L>(this Session<Call0<Z>, E, Cons<S0, L>> session) where S0 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S0, Push<Z, E>>();
		}

		public static Session<S1, Push<Z, E>, Cons<S0, Cons<S1, L>>> Call<S0, S1, Z, E, L>(this Session<Call1<Z>, E, Cons<S0, Cons<S1, L>>> session) where S0 : SessionType where S1 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S1, Push<Z, E>>();
		}

		public static Session<S2, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, L>>>> Call<S0, S1, S2, Z, E, L>(this Session<Call2<Z>, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S2, Push<Z, E>>();
		}

		public static Session<S3, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> Call<S0, S1, S2, S3, Z, E, L>(this Session<Call3<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S3, Push<Z, E>>();
		}

		public static Session<S4, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> Call<S0, S1, S2, S3, S4, Z, E, L>(this Session<Call4<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S4, Push<Z, E>>();
		}

		public static Session<S5, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> Call<S0, S1, S2, S3, S4, S5, Z, E, L>(this Session<Call5<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S5, Push<Z, E>>();
		}

		public static Session<S6, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, Z, E, L>(this Session<Call6<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S6, Push<Z, E>>();
		}

		public static Session<S7, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, Z, E, L>(this Session<Call7<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S7, Push<Z, E>>();
		}

		public static Session<S8, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z, E, L>(this Session<Call8<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S8, Push<Z, E>>();
		}

		public static Session<S9, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z, E, L>(this Session<Call9<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Call();
			return session.ToNextSession<S9, Push<Z, E>>();
		}

		#region DepleteAction

		public static Session<Z, E, S> Call<S, Z, E, L>(this Session<Call0<Z>, E, S> session, DepleteAction<S, S> deplete) where S : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, L>> Call<S0, Z, E, L>(this Session<Call0<Z>, E, Cons<S0, L>> session, DepleteAction<S0, Cons<S0, L>> deplete) where S0 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S0, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, L>>> Call<S0, S1, Z, E, L>(this Session<Call1<Z>, E, Cons<S0, Cons<S1, L>>> session, DepleteAction<S1, Cons<S0, Cons<S1, L>>> deplete) where S0 : SessionType where S1 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S1, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, L>>>> Call<S0, S1, S2, Z, E, L>(this Session<Call2<Z>, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session, DepleteAction<S2, Cons<S0, Cons<S1, Cons<S2, L>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S2, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> Call<S0, S1, S2, S3, Z, E, L>(this Session<Call3<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session, DepleteAction<S3, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S3, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> Call<S0, S1, S2, S3, S4, Z, E, L>(this Session<Call4<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session, DepleteAction<S4, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S4, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> Call<S0, S1, S2, S3, S4, S5, Z, E, L>(this Session<Call5<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session, DepleteAction<S5, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S5, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, Z, E, L>(this Session<Call6<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session, DepleteAction<S6, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S6, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, Z, E, L>(this Session<Call7<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session, DepleteAction<S7, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S7, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z, E, L>(this Session<Call8<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session, DepleteAction<S8, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S8, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z, E, L>(this Session<Call9<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session, DepleteAction<S9, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S9, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}





		public static Session<Z, E, S> Call<S, Z, E, L>(this Session<Call0<Z>, E, S> session, DepleteRecAction<S, S> deplete) where S : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, L>> Call<S0, Z, E, L>(this Session<Call0<Z>, E, Cons<S0, L>> session, DepleteRecAction<S0, Cons<S0, L>> deplete) where S0 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S0, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, L>>> Call<S0, S1, Z, E, L>(this Session<Call1<Z>, E, Cons<S0, Cons<S1, L>>> session, DepleteRecAction<S1, Cons<S0, Cons<S1, L>>> deplete) where S0 : SessionType where S1 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S1, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, L>>>> Call<S0, S1, S2, Z, E, L>(this Session<Call2<Z>, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session, DepleteRecAction<S2, Cons<S0, Cons<S1, Cons<S2, L>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S2, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> Call<S0, S1, S2, S3, Z, E, L>(this Session<Call3<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session, DepleteRecAction<S3, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S3, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> Call<S0, S1, S2, S3, S4, Z, E, L>(this Session<Call4<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session, DepleteRecAction<S4, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S4, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> Call<S0, S1, S2, S3, S4, S5, Z, E, L>(this Session<Call5<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session, DepleteRecAction<S5, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S5, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, Z, E, L>(this Session<Call6<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session, DepleteRecAction<S6, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S6, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, Z, E, L>(this Session<Call7<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session, DepleteRecAction<S7, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S7, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z, E, L>(this Session<Call8<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session, DepleteRecAction<S8, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S8, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z, E, L>(this Session<Call9<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session, DepleteRecAction<S9, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S9, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}








		public static async Task<Session<Z, E, S>> Call<S, Z, E, L>(this Session<Call0<Z>, E, S> session, DepleteAsyncAction<S, S> deplete) where S : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, L>>> Call<S0, Z, E, L>(this Session<Call0<Z>, E, Cons<S0, L>> session, DepleteAsyncAction<S0, Cons<S0, L>> deplete) where S0 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S0, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, L>>>> Call<S0, S1, Z, E, L>(this Session<Call1<Z>, E, Cons<S0, Cons<S1, L>>> session, DepleteAsyncAction<S1, Cons<S0, Cons<S1, L>>> deplete) where S0 : SessionType where S1 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S1, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, L>>>>> Call<S0, S1, S2, Z, E, L>(this Session<Call2<Z>, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session, DepleteAsyncAction<S2, Cons<S0, Cons<S1, Cons<S2, L>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S2, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>>> Call<S0, S1, S2, S3, Z, E, L>(this Session<Call3<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session, DepleteAsyncAction<S3, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S3, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>>> Call<S0, S1, S2, S3, S4, Z, E, L>(this Session<Call4<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session, DepleteAsyncAction<S4, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S4, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>>> Call<S0, S1, S2, S3, S4, S5, Z, E, L>(this Session<Call5<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session, DepleteAsyncAction<S5, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S5, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, Z, E, L>(this Session<Call6<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session, DepleteAsyncAction<S6, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S6, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, Z, E, L>(this Session<Call7<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session, DepleteAsyncAction<S7, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S7, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z, E, L>(this Session<Call8<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session, DepleteAsyncAction<S8, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S8, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z, E, L>(this Session<Call9<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session, DepleteAsyncAction<S9, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S9, Any>());
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}





		public static async Task<Session<Z, E, S>> Call<S, Z, E, L>(this Session<Call0<Z>, E, S> session, DepleteRecAsyncAction<S, S> deplete) where S : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, L>>> Call<S0, Z, E, L>(this Session<Call0<Z>, E, Cons<S0, L>> session, DepleteRecAsyncAction<S0, Cons<S0, L>> deplete) where S0 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S0, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, L>>>> Call<S0, S1, Z, E, L>(this Session<Call1<Z>, E, Cons<S0, Cons<S1, L>>> session, DepleteRecAsyncAction<S1, Cons<S0, Cons<S1, L>>> deplete) where S0 : SessionType where S1 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S1, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, L>>>>> Call<S0, S1, S2, Z, E, L>(this Session<Call2<Z>, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session, DepleteRecAsyncAction<S2, Cons<S0, Cons<S1, Cons<S2, L>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S2, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>>> Call<S0, S1, S2, S3, Z, E, L>(this Session<Call3<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session, DepleteRecAsyncAction<S3, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S3, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>>> Call<S0, S1, S2, S3, S4, Z, E, L>(this Session<Call4<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session, DepleteRecAsyncAction<S4, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S4, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>>> Call<S0, S1, S2, S3, S4, S5, Z, E, L>(this Session<Call5<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session, DepleteRecAsyncAction<S5, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S5, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, Z, E, L>(this Session<Call6<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session, DepleteRecAsyncAction<S6, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S6, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, Z, E, L>(this Session<Call7<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session, DepleteRecAsyncAction<S7, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S7, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z, E, L>(this Session<Call8<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session, DepleteRecAsyncAction<S8, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S8, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z, E, L>(this Session<Call9<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session, DepleteRecAsyncAction<S9, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S9, Any>(), deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}


		#endregion


		#region DepleteActionT




		public static Session<Z, E, S> Call<S, Z, E, L, T>(this Session<Call0<Z>, E, S> session, T arg, DepleteAction<S, S, T> deplete) where S : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, L>> Call<S0, Z, E, L, T>(this Session<Call0<Z>, E, Cons<S0, L>> session, T arg, DepleteAction<S0, Cons<S0, L>, T> deplete) where S0 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S0, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, L>>> Call<S0, S1, Z, E, L, T>(this Session<Call1<Z>, E, Cons<S0, Cons<S1, L>>> session, T arg, DepleteAction<S1, Cons<S0, Cons<S1, L>>, T> deplete) where S0 : SessionType where S1 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S1, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, L>>>> Call<S0, S1, S2, Z, E, L, T>(this Session<Call2<Z>, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session, T arg, DepleteAction<S2, Cons<S0, Cons<S1, Cons<S2, L>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S2, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> Call<S0, S1, S2, S3, Z, E, L, T>(this Session<Call3<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session, T arg, DepleteAction<S3, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S3, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> Call<S0, S1, S2, S3, S4, Z, E, L, T>(this Session<Call4<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session, T arg, DepleteAction<S4, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S4, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> Call<S0, S1, S2, S3, S4, S5, Z, E, L, T>(this Session<Call5<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session, T arg, DepleteAction<S5, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S5, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, Z, E, L, T>(this Session<Call6<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session, T arg, DepleteAction<S6, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S6, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, Z, E, L, T>(this Session<Call7<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session, T arg, DepleteAction<S7, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S7, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z, E, L, T>(this Session<Call8<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session, T arg, DepleteAction<S8, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S8, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z, E, L, T>(this Session<Call9<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session, T arg, DepleteAction<S9, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S9, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}





		public static Session<Z, E, S> Call<S, Z, E, L, T>(this Session<Call0<Z>, E, S> session, T arg, DepleteRecAction<S, S, T> deplete) where S : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, L>> Call<S0, Z, E, L, T>(this Session<Call0<Z>, E, Cons<S0, L>> session, T arg, DepleteRecAction<S0, Cons<S0, L>, T> deplete) where S0 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S0, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, L>>> Call<S0, S1, Z, E, L, T>(this Session<Call1<Z>, E, Cons<S0, Cons<S1, L>>> session, T arg, DepleteRecAction<S1, Cons<S0, Cons<S1, L>>, T> deplete) where S0 : SessionType where S1 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S1, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, L>>>> Call<S0, S1, S2, Z, E, L, T>(this Session<Call2<Z>, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session, T arg, DepleteRecAction<S2, Cons<S0, Cons<S1, Cons<S2, L>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S2, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> Call<S0, S1, S2, S3, Z, E, L, T>(this Session<Call3<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session, T arg, DepleteRecAction<S3, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S3, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> Call<S0, S1, S2, S3, S4, Z, E, L, T>(this Session<Call4<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session, T arg, DepleteRecAction<S4, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S4, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> Call<S0, S1, S2, S3, S4, S5, Z, E, L, T>(this Session<Call5<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session, T arg, DepleteRecAction<S5, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S5, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, Z, E, L, T>(this Session<Call6<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session, T arg, DepleteRecAction<S6, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S6, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, Z, E, L, T>(this Session<Call7<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session, T arg, DepleteRecAction<S7, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S7, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z, E, L, T>(this Session<Call8<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session, T arg, DepleteRecAction<S8, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S8, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z, E, L, T>(this Session<Call9<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session, T arg, DepleteRecAction<S9, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = deplete(session.ToNextSession<S9, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}








		public static async Task<Session<Z, E, S>> Call<S, Z, E, L, T>(this Session<Call0<Z>, E, S> session, T arg, DepleteAsyncAction<S, S, T> deplete) where S : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, L>>> Call<S0, Z, E, L, T>(this Session<Call0<Z>, E, Cons<S0, L>> session, T arg, DepleteAsyncAction<S0, Cons<S0, L>, T> deplete) where S0 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S0, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, L>>>> Call<S0, S1, Z, E, L, T>(this Session<Call1<Z>, E, Cons<S0, Cons<S1, L>>> session, T arg, DepleteAsyncAction<S1, Cons<S0, Cons<S1, L>>, T> deplete) where S0 : SessionType where S1 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S1, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, L>>>>> Call<S0, S1, S2, Z, E, L, T>(this Session<Call2<Z>, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session, T arg, DepleteAsyncAction<S2, Cons<S0, Cons<S1, Cons<S2, L>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S2, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>>> Call<S0, S1, S2, S3, Z, E, L, T>(this Session<Call3<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session, T arg, DepleteAsyncAction<S3, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S3, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>>> Call<S0, S1, S2, S3, S4, Z, E, L, T>(this Session<Call4<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session, T arg, DepleteAsyncAction<S4, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S4, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>>> Call<S0, S1, S2, S3, S4, S5, Z, E, L, T>(this Session<Call5<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session, T arg, DepleteAsyncAction<S5, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S5, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, Z, E, L, T>(this Session<Call6<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session, T arg, DepleteAsyncAction<S6, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S6, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, Z, E, L, T>(this Session<Call7<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session, T arg, DepleteAsyncAction<S7, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S7, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z, E, L, T>(this Session<Call8<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session, T arg, DepleteAsyncAction<S8, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S8, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z, E, L, T>(this Session<Call9<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session, T arg, DepleteAsyncAction<S9, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S9, Any>(), arg);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}





		public static async Task<Session<Z, E, S>> Call<S, Z, E, L, T>(this Session<Call0<Z>, E, S> session, T arg, DepleteRecAsyncAction<S, S, T> deplete) where S : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, L>>> Call<S0, Z, E, L, T>(this Session<Call0<Z>, E, Cons<S0, L>> session, T arg, DepleteRecAsyncAction<S0, Cons<S0, L>, T> deplete) where S0 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S0, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, L>>>> Call<S0, S1, Z, E, L, T>(this Session<Call1<Z>, E, Cons<S0, Cons<S1, L>>> session, T arg, DepleteRecAsyncAction<S1, Cons<S0, Cons<S1, L>>, T> deplete) where S0 : SessionType where S1 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S1, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, L>>>>> Call<S0, S1, S2, Z, E, L, T>(this Session<Call2<Z>, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session, T arg, DepleteRecAsyncAction<S2, Cons<S0, Cons<S1, Cons<S2, L>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S2, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>>> Call<S0, S1, S2, S3, Z, E, L, T>(this Session<Call3<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session, T arg, DepleteRecAsyncAction<S3, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S3, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>>> Call<S0, S1, S2, S3, S4, Z, E, L, T>(this Session<Call4<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session, T arg, DepleteRecAsyncAction<S4, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S4, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>>> Call<S0, S1, S2, S3, S4, S5, Z, E, L, T>(this Session<Call5<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session, T arg, DepleteRecAsyncAction<S5, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S5, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, Z, E, L, T>(this Session<Call6<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session, T arg, DepleteRecAsyncAction<S6, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S6, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, Z, E, L, T>(this Session<Call7<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session, T arg, DepleteRecAsyncAction<S7, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S7, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z, E, L, T>(this Session<Call8<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session, T arg, DepleteRecAsyncAction<S8, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S8, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}

		public static async Task<Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z, E, L, T>(this Session<Call9<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session, T arg, DepleteRecAsyncAction<S9, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var depleted = await deplete(session.ToNextSession<S9, Any>(), arg, deplete);
			depleted.Call();
			return depleted.ToNextSession<Z, E>();
		}


		#endregion








		#region DepleteFunc

		public static (Session<Z, E, S> resumed, T result) Call<S, Z, E, L, T>(this Session<Call0<Z>, E, S> session, DepleteFunc<S, S, T> deplete) where S : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, L>> resumed, T result) Call<S0, Z, E, L, T>(this Session<Call0<Z>, E, Cons<S0, L>> session, DepleteFunc<S0, Cons<S0, L>, T> deplete) where S0 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S0, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, L>>> resumed, T result) Call<S0, S1, Z, E, L, T>(this Session<Call1<Z>, E, Cons<S0, Cons<S1, L>>> session, DepleteFunc<S1, Cons<S0, Cons<S1, L>>, T> deplete) where S0 : SessionType where S1 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S1, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, L>>>> resumed, T result) Call<S0, S1, S2, Z, E, L, T>(this Session<Call2<Z>, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session, DepleteFunc<S2, Cons<S0, Cons<S1, Cons<S2, L>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S2, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> resumed, T result) Call<S0, S1, S2, S3, Z, E, L, T>(this Session<Call3<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session, DepleteFunc<S3, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S3, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> resumed, T result) Call<S0, S1, S2, S3, S4, Z, E, L, T>(this Session<Call4<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session, DepleteFunc<S4, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S4, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> resumed, T result) Call<S0, S1, S2, S3, S4, S5, Z, E, L, T>(this Session<Call5<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session, DepleteFunc<S5, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S5, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> resumed, T result) Call<S0, S1, S2, S3, S4, S5, S6, Z, E, L, T>(this Session<Call6<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session, DepleteFunc<S6, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S6, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> resumed, T result) Call<S0, S1, S2, S3, S4, S5, S6, S7, Z, E, L, T>(this Session<Call7<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session, DepleteFunc<S7, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S7, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> resumed, T result) Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z, E, L, T>(this Session<Call8<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session, DepleteFunc<S8, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S8, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> resumed, T result) Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z, E, L, T>(this Session<Call9<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session, DepleteFunc<S9, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S9, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}





		public static (Session<Z, E, S> resumed, T result) Call<S, Z, E, L, T>(this Session<Call0<Z>, E, S> session, DepleteRecFunc<S, S, T> deplete) where S : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, L>> resumed, T result) Call<S0, Z, E, L, T>(this Session<Call0<Z>, E, Cons<S0, L>> session, DepleteRecFunc<S0, Cons<S0, L>, T> deplete) where S0 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S0, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, L>>> resumed, T result) Call<S0, S1, Z, E, L, T>(this Session<Call1<Z>, E, Cons<S0, Cons<S1, L>>> session, DepleteRecFunc<S1, Cons<S0, Cons<S1, L>>, T> deplete) where S0 : SessionType where S1 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S1, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, L>>>> resumed, T result) Call<S0, S1, S2, Z, E, L, T>(this Session<Call2<Z>, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session, DepleteRecFunc<S2, Cons<S0, Cons<S1, Cons<S2, L>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S2, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> resumed, T result) Call<S0, S1, S2, S3, Z, E, L, T>(this Session<Call3<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session, DepleteRecFunc<S3, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S3, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> resumed, T result) Call<S0, S1, S2, S3, S4, Z, E, L, T>(this Session<Call4<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session, DepleteRecFunc<S4, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S4, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> resumed, T result) Call<S0, S1, S2, S3, S4, S5, Z, E, L, T>(this Session<Call5<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session, DepleteRecFunc<S5, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S5, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> resumed, T result) Call<S0, S1, S2, S3, S4, S5, S6, Z, E, L, T>(this Session<Call6<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session, DepleteRecFunc<S6, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S6, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> resumed, T result) Call<S0, S1, S2, S3, S4, S5, S6, S7, Z, E, L, T>(this Session<Call7<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session, DepleteRecFunc<S7, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S7, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> resumed, T result) Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z, E, L, T>(this Session<Call8<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session, DepleteRecFunc<S8, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S8, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> resumed, T result) Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z, E, L, T>(this Session<Call9<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session, DepleteRecFunc<S9, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S9, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}












		public static async Task<(Session<Z, E, S> resumed, T result)> Call<S, Z, E, L, T>(this Session<Call0<Z>, E, S> session, DepleteAsyncFunc<S, S, T> deplete) where S : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, L>> resumed, T result)> Call<S0, Z, E, L, T>(this Session<Call0<Z>, E, Cons<S0, L>> session, DepleteAsyncFunc<S0, Cons<S0, L>, T> deplete) where S0 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S0, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, L>>> resumed, T result)> Call<S0, S1, Z, E, L, T>(this Session<Call1<Z>, E, Cons<S0, Cons<S1, L>>> session, DepleteAsyncFunc<S1, Cons<S0, Cons<S1, L>>, T> deplete) where S0 : SessionType where S1 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S1, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, L>>>> resumed, T result)> Call<S0, S1, S2, Z, E, L, T>(this Session<Call2<Z>, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session, DepleteAsyncFunc<S2, Cons<S0, Cons<S1, Cons<S2, L>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S2, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> resumed, T result)> Call<S0, S1, S2, S3, Z, E, L, T>(this Session<Call3<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session, DepleteAsyncFunc<S3, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S3, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> resumed, T result)> Call<S0, S1, S2, S3, S4, Z, E, L, T>(this Session<Call4<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session, DepleteAsyncFunc<S4, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S4, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> resumed, T result)> Call<S0, S1, S2, S3, S4, S5, Z, E, L, T>(this Session<Call5<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session, DepleteAsyncFunc<S5, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S5, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> resumed, T result)> Call<S0, S1, S2, S3, S4, S5, S6, Z, E, L, T>(this Session<Call6<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session, DepleteAsyncFunc<S6, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S6, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> resumed, T result)> Call<S0, S1, S2, S3, S4, S5, S6, S7, Z, E, L, T>(this Session<Call7<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session, DepleteAsyncFunc<S7, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S7, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> resumed, T result)> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z, E, L, T>(this Session<Call8<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session, DepleteAsyncFunc<S8, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S8, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> resumed, T result)> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z, E, L, T>(this Session<Call9<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session, DepleteAsyncFunc<S9, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S9, Any>());
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}





		public static async Task<(Session<Z, E, S> resumed, T result)> Call<S, Z, E, L, T>(this Session<Call0<Z>, E, S> session, DepleteRecAsyncFunc<S, S, T> deplete) where S : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, L>> resumed, T result)> Call<S0, Z, E, L, T>(this Session<Call0<Z>, E, Cons<S0, L>> session, DepleteRecAsyncFunc<S0, Cons<S0, L>, T> deplete) where S0 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S0, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, L>>> resumed, T result)> Call<S0, S1, Z, E, L, T>(this Session<Call1<Z>, E, Cons<S0, Cons<S1, L>>> session, DepleteRecAsyncFunc<S1, Cons<S0, Cons<S1, L>>, T> deplete) where S0 : SessionType where S1 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S1, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, L>>>> resumed, T result)> Call<S0, S1, S2, Z, E, L, T>(this Session<Call2<Z>, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session, DepleteRecAsyncFunc<S2, Cons<S0, Cons<S1, Cons<S2, L>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S2, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> resumed, T result)> Call<S0, S1, S2, S3, Z, E, L, T>(this Session<Call3<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session, DepleteRecAsyncFunc<S3, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S3, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> resumed, T result)> Call<S0, S1, S2, S3, S4, Z, E, L, T>(this Session<Call4<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session, DepleteRecAsyncFunc<S4, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S4, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> resumed, T result)> Call<S0, S1, S2, S3, S4, S5, Z, E, L, T>(this Session<Call5<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session, DepleteRecAsyncFunc<S5, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S5, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> resumed, T result)> Call<S0, S1, S2, S3, S4, S5, S6, Z, E, L, T>(this Session<Call6<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session, DepleteRecAsyncFunc<S6, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S6, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> resumed, T result)> Call<S0, S1, S2, S3, S4, S5, S6, S7, Z, E, L, T>(this Session<Call7<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session, DepleteRecAsyncFunc<S7, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S7, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> resumed, T result)> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z, E, L, T>(this Session<Call8<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session, DepleteRecAsyncFunc<S8, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S8, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> resumed, T result)> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z, E, L, T>(this Session<Call9<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session, DepleteRecAsyncFunc<S9, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>, T> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S9, Any>(), deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		#endregion





		#region DepleteFuncT

		public static (Session<Z, E, S> resumed, U result) Call<S, Z, E, L, T, U>(this Session<Call0<Z>, E, S> session, T arg, DepleteFunc<S, S, T, U> deplete) where S : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, L>> resumed, U result) Call<S0, Z, E, L, T, U>(this Session<Call0<Z>, E, Cons<S0, L>> session, T arg, DepleteFunc<S0, Cons<S0, L>, T, U> deplete) where S0 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S0, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, L>>> resumed, U result) Call<S0, S1, Z, E, L, T, U>(this Session<Call1<Z>, E, Cons<S0, Cons<S1, L>>> session, T arg, DepleteFunc<S1, Cons<S0, Cons<S1, L>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S1, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, L>>>> resumed, U result) Call<S0, S1, S2, Z, E, L, T, U>(this Session<Call2<Z>, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session, T arg, DepleteFunc<S2, Cons<S0, Cons<S1, Cons<S2, L>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S2, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> resumed, U result) Call<S0, S1, S2, S3, Z, E, L, T, U>(this Session<Call3<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session, T arg, DepleteFunc<S3, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S3, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> resumed, U result) Call<S0, S1, S2, S3, S4, Z, E, L, T, U>(this Session<Call4<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session, T arg, DepleteFunc<S4, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S4, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> resumed, U result) Call<S0, S1, S2, S3, S4, S5, Z, E, L, T, U>(this Session<Call5<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session, T arg, DepleteFunc<S5, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S5, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> resumed, U result) Call<S0, S1, S2, S3, S4, S5, S6, Z, E, L, T, U>(this Session<Call6<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session, T arg, DepleteFunc<S6, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S6, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> resumed, U result) Call<S0, S1, S2, S3, S4, S5, S6, S7, Z, E, L, T, U>(this Session<Call7<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session, T arg, DepleteFunc<S7, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S7, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> resumed, U result) Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z, E, L, T, U>(this Session<Call8<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session, T arg, DepleteFunc<S8, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S8, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> resumed, U result) Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z, E, L, T, U>(this Session<Call9<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session, T arg, DepleteFunc<S9, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S9, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}





		public static (Session<Z, E, S> resumed, U result) Call<S, Z, E, L, T, U>(this Session<Call0<Z>, E, S> session, T arg, DepleteRecFunc<S, S, T, U> deplete) where S : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, L>> resumed, U result) Call<S0, Z, E, L, T, U>(this Session<Call0<Z>, E, Cons<S0, L>> session, T arg, DepleteRecFunc<S0, Cons<S0, L>, T, U> deplete) where S0 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S0, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, L>>> resumed, U result) Call<S0, S1, Z, E, L, T, U>(this Session<Call1<Z>, E, Cons<S0, Cons<S1, L>>> session, T arg, DepleteRecFunc<S1, Cons<S0, Cons<S1, L>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S1, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, L>>>> resumed, U result) Call<S0, S1, S2, Z, E, L, T, U>(this Session<Call2<Z>, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session, T arg, DepleteRecFunc<S2, Cons<S0, Cons<S1, Cons<S2, L>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S2, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> resumed, U result) Call<S0, S1, S2, S3, Z, E, L, T, U>(this Session<Call3<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session, T arg, DepleteRecFunc<S3, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S3, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> resumed, U result) Call<S0, S1, S2, S3, S4, Z, E, L, T, U>(this Session<Call4<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session, T arg, DepleteRecFunc<S4, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S4, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> resumed, U result) Call<S0, S1, S2, S3, S4, S5, Z, E, L, T, U>(this Session<Call5<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session, T arg, DepleteRecFunc<S5, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S5, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> resumed, U result) Call<S0, S1, S2, S3, S4, S5, S6, Z, E, L, T, U>(this Session<Call6<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session, T arg, DepleteRecFunc<S6, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S6, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> resumed, U result) Call<S0, S1, S2, S3, S4, S5, S6, S7, Z, E, L, T, U>(this Session<Call7<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session, T arg, DepleteRecFunc<S7, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S7, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> resumed, U result) Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z, E, L, T, U>(this Session<Call8<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session, T arg, DepleteRecFunc<S8, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S8, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static (Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> resumed, U result) Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z, E, L, T, U>(this Session<Call9<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session, T arg, DepleteRecFunc<S9, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = deplete(session.ToNextSession<S9, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}












		public static async Task<(Session<Z, E, S> resumed, U result)> Call<S, Z, E, L, T, U>(this Session<Call0<Z>, E, S> session, T arg, DepleteAsyncFunc<S, S, T, U> deplete) where S : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, L>> resumed, U result)> Call<S0, Z, E, L, T, U>(this Session<Call0<Z>, E, Cons<S0, L>> session, T arg, DepleteAsyncFunc<S0, Cons<S0, L>, T, U> deplete) where S0 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S0, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, L>>> resumed, U result)> Call<S0, S1, Z, E, L, T, U>(this Session<Call1<Z>, E, Cons<S0, Cons<S1, L>>> session, T arg, DepleteAsyncFunc<S1, Cons<S0, Cons<S1, L>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S1, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, L>>>> resumed, U result)> Call<S0, S1, S2, Z, E, L, T, U>(this Session<Call2<Z>, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session, T arg, DepleteAsyncFunc<S2, Cons<S0, Cons<S1, Cons<S2, L>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S2, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> resumed, U result)> Call<S0, S1, S2, S3, Z, E, L, T, U>(this Session<Call3<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session, T arg, DepleteAsyncFunc<S3, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S3, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> resumed, U result)> Call<S0, S1, S2, S3, S4, Z, E, L, T, U>(this Session<Call4<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session, T arg, DepleteAsyncFunc<S4, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S4, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> resumed, U result)> Call<S0, S1, S2, S3, S4, S5, Z, E, L, T, U>(this Session<Call5<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session, T arg, DepleteAsyncFunc<S5, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S5, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> resumed, U result)> Call<S0, S1, S2, S3, S4, S5, S6, Z, E, L, T, U>(this Session<Call6<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session, T arg, DepleteAsyncFunc<S6, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S6, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> resumed, U result)> Call<S0, S1, S2, S3, S4, S5, S6, S7, Z, E, L, T, U>(this Session<Call7<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session, T arg, DepleteAsyncFunc<S7, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S7, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> resumed, U result)> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z, E, L, T, U>(this Session<Call8<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session, T arg, DepleteAsyncFunc<S8, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S8, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> resumed, U result)> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z, E, L, T, U>(this Session<Call9<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session, T arg, DepleteAsyncFunc<S9, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S9, Any>(), arg);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}





		public static async Task<(Session<Z, E, S> resumed, U result)> Call<S, Z, E, L, T, U>(this Session<Call0<Z>, E, S> session, T arg, DepleteRecAsyncFunc<S, S, T, U> deplete) where S : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, L>> resumed, U result)> Call<S0, Z, E, L, T, U>(this Session<Call0<Z>, E, Cons<S0, L>> session, T arg, DepleteRecAsyncFunc<S0, Cons<S0, L>, T, U> deplete) where S0 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S0, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, L>>> resumed, U result)> Call<S0, S1, Z, E, L, T, U>(this Session<Call1<Z>, E, Cons<S0, Cons<S1, L>>> session, T arg, DepleteRecAsyncFunc<S1, Cons<S0, Cons<S1, L>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S1, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, L>>>> resumed, U result)> Call<S0, S1, S2, Z, E, L, T, U>(this Session<Call2<Z>, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session, T arg, DepleteRecAsyncFunc<S2, Cons<S0, Cons<S1, Cons<S2, L>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S2, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> resumed, U result)> Call<S0, S1, S2, S3, Z, E, L, T, U>(this Session<Call3<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session, T arg, DepleteRecAsyncFunc<S3, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S3, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> resumed, U result)> Call<S0, S1, S2, S3, S4, Z, E, L, T, U>(this Session<Call4<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session, T arg, DepleteRecAsyncFunc<S4, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S4, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> resumed, U result)> Call<S0, S1, S2, S3, S4, S5, Z, E, L, T, U>(this Session<Call5<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session, T arg, DepleteRecAsyncFunc<S5, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S5, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> resumed, U result)> Call<S0, S1, S2, S3, S4, S5, S6, Z, E, L, T, U>(this Session<Call6<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session, T arg, DepleteRecAsyncFunc<S6, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S6, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> resumed, U result)> Call<S0, S1, S2, S3, S4, S5, S6, S7, Z, E, L, T, U>(this Session<Call7<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session, T arg, DepleteRecAsyncFunc<S7, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S7, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> resumed, U result)> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z, E, L, T, U>(this Session<Call8<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session, T arg, DepleteRecAsyncFunc<S8, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S8, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		public static async Task<(Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> resumed, U result)> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z, E, L, T, U>(this Session<Call9<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session, T arg, DepleteRecAsyncFunc<S9, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>, T, U> deplete) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (deplete is null) throw new ArgumentNullException(nameof(deplete));
			session.Call();
			var (depleted, result) = await deplete(session.ToNextSession<S9, Any>(), arg, deplete);
			depleted.Call();
			return (depleted.ToNextSession<Z, E>(), result);
		}

		#endregion

		public static (Session<S, E, P> continuation, Session<Z, Empty, Q> newSession) ThrowNewChannel<Z, Q, S, E, P>(this Session<ThrowNewChannel<Z, Q, S>, E, P> session) where Z : SessionType where Q : ProtocolType where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			return (session.ToNextSession<S>(), session.ThrowNewChannel<Z, Q>());
		}

		public static Session<S, E, P> ThrowNewChannel<Z, Q, S, E, P>(this Session<ThrowNewChannel<Z, Q, S>, E, P> session, out Session<Z, Empty, Q> newSession) where Z : SessionType where Q : ProtocolType where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			newSession = session.ThrowNewChannel<Z, Q>();
			return session.ToNextSession<S>();
		}

		public static async Task<(Session<S, E, P> continuation, Session<Z, Empty, Q> newSession)> ThrowNewChannelAsync<Z, Q, S, E, P>(this Session<ThrowNewChannel<Z, Q, S>, E, P> session) where Z : SessionType where Q : ProtocolType where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			return (session.ToNextSession<S>(), await session.ThrowNewChannelAsync<Z, Q>());
		}

		public static (Session<S, E, P> continuation, Session<Z, Empty, Q> newSession) CatchNewChannel<Z, Q, S, E, P>(this Session<CatchNewChannel<Z, Q, S>, E, P> session) where Z : SessionType where Q : ProtocolType where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			return (session.ToNextSession<S>(), session.CatchNewChannel<Z, Q>());
		}

		public static Session<S, E, P> CatchNewChannel<Z, Q, S, E, P>(this Session<CatchNewChannel<Z, Q, S>, E, P> session, out Session<Z, Empty, Q> newSession) where Z : SessionType where Q : ProtocolType where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			newSession = session.CatchNewChannel<Z, Q>();
			return session.ToNextSession<S>();
		}

		public static async Task<(Session<S, E, P> continuation, Session<Z, Empty, Q> newSession)> CatchNewChannelAsync<Z, Q, S, E, P>(this Session<CatchNewChannel<Z, Q, S>, E, P> session) where Z : SessionType where Q : ProtocolType where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			return (session.ToNextSession<S>(), await session.CatchNewChannelAsync<Z, Q>());
		}

		public static Session<F, E, P> Return<F, E, P>(this Session<Eps, Push<F, E>, P> session) where F : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			return session.ToNextSession<F, E>();
		}

		public static void Close<P>(this Session<Eps, Empty, P> session) where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Close();
		}
	}
}
