namespace SessionTypes.Binary
{
	public abstract class Choice<L, R> where L : SessionType where R : SessionType
	{
		internal Choice() { }
	}

	public sealed class Left<L, R> : Choice<L, R> where L : SessionType where R : SessionType
	{

	}

	public sealed class Right<L, R> : Choice<L, R> where L : SessionType where R : SessionType
	{

	}
}
