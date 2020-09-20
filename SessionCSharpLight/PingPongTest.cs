using System;
using System.Collections.Generic;
using System.Text;
using Session;
using Session.Types;
using SessionTest;

namespace SessionCSharpLight
{
	public class PingPongTest
	{
		public static void Main()
		{
			var proto = new PingPong();
			var cliCh = proto.ForkThread(srvCh => RunServer(srvCh));

		}

		public static void RunServer(Session<Recv<int, Send<int, Srv<PingPong>>>> ch)
		{
			RunServer(ch.Receive(out int x).Send(x + 1).Expand());
		}

		public static void RunClient(Session<Send<int, Recv<int, Cli<PingPong>>>> ch)
		{
			RunClient(ch.Send(100).Receive(out int x).Expand());
		}
	}
}
