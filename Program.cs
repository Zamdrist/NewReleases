using System;

namespace NewReleases
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var release = new NewRelease();
			var results = release.Pull(new Uri("http://www.previewsworld.com/shipping/newreleases.txt"));

			if (args.Length  == 2 && release.CredentialsSent(args[0], args[1]))
			{
				release.MailReleaseResults(args[0], args[1], results.ToString(), DateTime.Now);
			}

		}
	}
}
