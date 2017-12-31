using System;

namespace NewReleases
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var release = new Release.NewRelease();
			var results = release.Pull(new Uri("http://www.previewsworld.com/shipping/newreleases.txt"));
			Console.WriteLine($"{results} rows written to database.");
			Console.ReadLine();
		}
	}
}
