using System;
using System.Collections.Generic;
using System.Linq;

namespace NewReleases
{
	public class Program
	{
		public static void Main(string[] args)
		{
			const string premierPublisherCategory = "PREMIER PUBLISHERS";
			const string newReleasesFor = "New Releases For";

			var premierPublishers = Release.GetPremierPublishers();
			var categories = Release.GetCategories();
			var releaseDate = new DateTime();
			var releaseData = new List<ReleaseItem>();
			var releaseLines = Release.GetRelease();
			var category = string.Empty;
			var premierePublisher = string.Empty;

			foreach (var line in releaseLines)
			{
				var releaseItem = new ReleaseItem();

				if (line.Contains(newReleasesFor))
				{
					releaseDate = DateTime.Parse(line.Replace(newReleasesFor, string.Empty).Trim());
				}

				if (premierPublishers.Any(p => p == line.Trim()))
				{
					category = premierPublisherCategory;
					premierePublisher = line.Trim();
				}
				else if (categories.Any(c => c == line.Trim()))
				{
					category = line.Trim();
					premierePublisher = null;
				}

				if (line.Split('\t').Length == 3)
				{
					var lineItems = line.Split('\t');

					releaseItem.ReleaseDate = releaseDate;
					releaseItem.Category = category;
					releaseItem.Publisher = premierePublisher;
					releaseItem.ItemCode = lineItems[0].Trim();
					releaseItem.Title = lineItems[1].Trim();
					releaseItem.Price = decimal.TryParse(lineItems[2].Replace("$", string.Empty), out var price)
						? price
						: 0;
					releaseItem.Note = price != 0 ? null : lineItems[2].Replace("$", string.Empty).Trim();
					releaseData.Add(releaseItem);
				}
			}
			Release.BulkCopyReleaseData(releaseData);
		}
	}
}
