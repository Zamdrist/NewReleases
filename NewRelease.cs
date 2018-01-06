using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using NewReleases.Data;

namespace NewReleases
{
	public class NewRelease
	{
		private readonly ReleaseData _releaseDataClass = new ReleaseData();
		private const string PremierPublisherCategory = "PREMIER PUBLISHERS";
		private const string NewReleasesFor = "New Releases For";

		public int Pull(Uri releaseUrl)
		{
			var releaseDate = new DateTime();
			var releaseData = new List<ReleaseItem>();
			var releaseLines = this._releaseDataClass.GetRelease(releaseUrl);
			var category = string.Empty;
			var premierePublisher = string.Empty;

			foreach (var line in releaseLines)
			{
				if (line.Contains(NewRelease.NewReleasesFor))
				{
					releaseDate = DateTime.Parse(line.Replace(NewRelease.NewReleasesFor, string.Empty).Trim());
				}

				if (this._releaseDataClass.PremierPublishers.Any(p => p == line.Trim()))
				{
					category = NewRelease.PremierPublisherCategory;
					premierePublisher = line.Trim();
				}
				else if (this._releaseDataClass.Categories.Any(c => c == line.Trim()))
				{
					category = line.Trim();
					premierePublisher = null;
				}

				if (line.Split('\t').Length == 3)
				{
					var lineItems = line.Split('\t');
					var issueNumber = this.FindIssueInTitle(lineItems[1]);
					var releaseItem = new ReleaseItem
					{
						ReleaseDate = releaseDate,
						Category = category,
						Publisher = premierePublisher,
						ItemCode = lineItems[0].Trim(),
						Title = lineItems[1].Trim(),
						IssueNumber = issueNumber == string.Empty ? null : issueNumber,
						Price = decimal.TryParse(lineItems[2].Replace("$", string.Empty), out var price)
							? price
							: 0,
						Note = price != 0 ? null : lineItems[2].Replace("$", string.Empty).Trim()
					};

					releaseData.Add(releaseItem);
				}
			}

			try
			{
				this._releaseDataClass.BulkCopyReleaseData(releaseData);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			return releaseData.Count;
		}

		public void MailReleaseResults(string address, string credentials, string results, DateTime releaseDate)
		{
			var fromAddress = new MailAddress(address, "New Release Import");
			var toAddress = new MailAddress(address);

			var smtp = new SmtpClient
			{
				Port = 587,
				Host = "smtp.gmail.com",
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(fromAddress.Address, credentials)

			};

			using (var message = new MailMessage(fromAddress, toAddress)
			{
				Subject = $"New releases for {releaseDate : M/d/yyyy}",

				Body =
					$"Results of New Release import: {results} rows imported.",
				IsBodyHtml = true
			})

			{
				smtp.Send(message);
			}
		}

		private string FindIssueInTitle(string title)
		{
			var regex = new Regex(@"\#\d+");
			var match = regex.Match(title);
			return match.Success ? match.Value : string.Empty;
		}

		public bool CredentialsSent(string address, string credentials) =>
			!string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(credentials);
	}
}
