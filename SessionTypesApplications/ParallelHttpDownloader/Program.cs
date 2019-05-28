﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using SessionTypes;
using SessionTypes.Binary;
using SessionTypes.Binary.Threading;

namespace ParallelHttpDownloader
{
	public class Program
	{
		private static void Main(string[] args)
		{
			Console.WriteLine("Parallel HTTP Downloader");
			var n = 2;
			args = new string[]
			{
				"http://www.toei-anim.co.jp/tv/precure/images/special/wallpaper/01_sp1080_1920.jpg",
				"http://www.toei-anim.co.jp/tv/precure/images/special/wallpaper/01_pc1920_1080.jpg",
			};
			

			var ids = Enumerable.Range(1, n).ToArray();

			var clients = BinarySessionChannel<Cons<RequestChoice<Request<string, Respond<byte[], Jump<Zero>>>, Close>, Nil>>.Distribute((server, id) =>
			{
				var s = server.Enter();
				var http = new HttpClient();
				while (true)
				{
					var flag = false;
					s.Follow(
						left => { s = left.Receive(out var url).Send(Download(http, url)).Zero(); },
						right => { flag = true; right.Close(); }
					);
					if (flag) break;
				}
			}
			, ids);


			var entries = clients.Select(c => c.Enter()).ToList();
			var working = new List<Task<(Client<Jump<Zero>, Cons<RequestChoice<Request<string, Respond<byte[], Jump<Zero>>>, Close>, Nil>>, byte[])>>();
			var data = new List<byte[]>();
			foreach (var url in args)
			{
				if (!entries.Any())
				{
					var wait = Task.WhenAny(working);
					var task = wait.Result;
					working.Remove(task);
					var e = task.Result.Bind(out var bytes);
					data.Add(bytes);
					entries.Add(e.Zero());
				}

				working.Add(entries[0].ChooseLeft().Send(url).ReceiveAsync());
				entries.RemoveAt(0);
			}

			foreach (var entry in entries)
			{
				entry.ChooseRight().Close();
			}

			while (working.Any())
			{
				var wait = Task.WhenAny(working);
				var task = wait.Result;
				working.Remove(task);
				var e = task.Result.Bind(out var bytes);
				data.Add(bytes);
				e.Zero().ChooseRight().Close();
			}

			for (int i = 0; i < data.Count; i++)
			{
				if (data[i] != null)
				{
					File.WriteAllBytes($@"{i}.jpg", data[i]);
				}
			}
		}

		private static byte[] Download(HttpClient client, string url)
		{
			try
			{
				return client.GetByteArrayAsync(url).Result;
			}
			catch
			{
				return null;
			}
		}
	}
}
