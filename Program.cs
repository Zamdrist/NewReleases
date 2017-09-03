using System;
using System.Collections.Generic;
using System.Linq;

namespace NewReleases
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string PremierPublishers = "PREMIER PUBLISHERS";
            const string NewReleasesFor = "New Releases For";

            var releaseItem = new ReleaseItem();
            var premierPublishers = Release.GetPremierPublishers();
            var releaseDate = new DateTime();   

            foreach (var line in Release.GetRelease())
            {
                if (line.Contains(NewReleasesFor))
                {
                    releaseDate = DateTime.Parse(line.Substring(line.Length - 10));
                }
                else
                {
                    if (!line.Any(p => p.ToString().Contains("\t")) && !premierPublishers.Any(p => p == line))
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

                            if (releaseItem.Category != PremierPublishers && !premierPublishers.Any(p => p == line))
                            {
                                releaseItem.Publisher = null;
                            }

                            Release.WriteRelease(releaseDate, releaseItem);
                        }
                    }
                }
            }
        }           
    }
}
