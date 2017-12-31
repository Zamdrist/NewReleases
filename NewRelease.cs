using System;
using System.Collections.Generic;
using System.Linq;
using NewReleases.Data;
using NewReleases.Release;

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
					var releaseItem = new ReleaseItem
					{
						ReleaseDate = releaseDate,
						Category = category,
						Publisher = premierePublisher,
						ItemCode = lineItems[0].Trim(),
						Title = lineItems[1].Trim(),
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
	}
}
