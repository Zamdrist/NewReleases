using System;
using System.Linq;

namespace NewReleases
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string premierPublisherCategory = "PREMIER PUBLISHERS";
            const string newReleasesFor = "New Releases For";

            var releaseItem = new ReleaseItem();
            var premierPublishers = Release.GetPremierPublishers();
            var releaseDate = new DateTime();

            foreach (var line in Release.GetRelease())
            {
                if (line.Contains(newReleasesFor))
                {
                    releaseDate = DateTime.Parse(line.Replace(newReleasesFor,string.Empty).Trim());
                }
                else
                {
                    if (!line.Any(p => p.ToString().Contains("\t")) && premierPublishers.Any(p => p != line))
                    {
                        releaseItem.Category = line.Trim();
                    }
                    else if (premierPublishers.Any(p => p == line))
                    {
                        releaseItem.Publisher = line.Trim();
                    }
                    else
                    {
                        var lineItems = line.Split('\t');

                        if (lineItems.Length == 3)
                        {
                            releaseItem.ItemCode = lineItems[0].Trim();
                            releaseItem.Title = lineItems[1].Trim();
                            releaseItem.Price = lineItems[2].Trim();

                            if (releaseItem.Category != premierPublisherCategory && !premierPublishers.Any(p => p == line))
                            {
                                releaseItem.Publisher = null;
                            }
							//TODO: https://stackoverflow.com/questions/3913371/sqlbulkcopy-from-a-list
							//TODO: https://www.nuget.org/packages/FastMember/
							Release.WriteRelease(releaseDate, releaseItem);
                        }
                    }
                }
            }
        }
    }
}
