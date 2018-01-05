using System;

namespace NewReleases
{
	public class Program
	{
		public static void Main(string[] args)
		{

			var address = string.Empty;
			var credentials = string.Empty;

			if (args.Length > 1)
			{
				address = args[0];
				credentials = args[1];
			}

			var release = new NewRelease();
			var results = release.Pull(new Uri("http://www.previewsworld.com/shipping/newreleases.txt"));

			if (!string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(credentials))
			{
				release.MailReleaseResults(address, credentials, results.ToString(), DateTime.Today);
			}

		}
	}
}
