using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace NewReleases
{
    public static class Release
    {
        private const string RemoteReleaseFile = "http://www.previewsworld.com/shipping/newreleases.txt";

        public static IEnumerable<string> GetRelease()
        {
            using (var webClient = new WebClient())
            {
                var stream = webClient.OpenRead(RemoteReleaseFile);
                var lines = new List<string>();

                using (var streamReader = new StreamReader(stream))
                {
                    while (!streamReader.EndOfStream)
                    {
                        lines.Add(streamReader.ReadLine());
                        
                    }
                }

                return lines.Where(f => !string.IsNullOrWhiteSpace(f));
            }
        }

        public static List<string> GetPremierPublishers()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewReleases"].ConnectionString))
            {
                return connection.Query<string>("Select PremierPublisher From PremierPublishers").ToList();
            }
        }
        
        public static void WriteRelease(DateTime releaseDate, ReleaseItem releaseItem)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewReleases"].ConnectionString))
            {
                string note = null;
                
                if (!decimal.TryParse(releaseItem.Price.Replace("$", string.Empty), out var price))
                {
                    note = releaseItem.Price.Replace("$", String.Empty);
                }
                else
                {
                    price = Convert.ToDecimal(releaseItem.Price.Replace("$", string.Empty));
                }
                
                connection.Execute(
                    "InsertReleaseItem",
                    new
                    {
                        releaseDate,
                        releaseItem.Category,
                        releaseItem.Publisher,
                        releaseItem.ItemCode,
                        releaseItem.Title,
                        price,
                        note                                                
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
