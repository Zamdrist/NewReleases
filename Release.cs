using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using FastMember;

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

	            if (stream != null)
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

	    public static List<string> GetCategories()
	    {
		    using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewReleases"].ConnectionString))
		    {
			    return connection.Query<string>("Select Category From Categories").ToList();
		    }
	    }

        public static List<string> GetPremierPublishers()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewReleases"].ConnectionString))
            {
                return connection.Query<string>("Select PremierPublisher From PremierPublishers").ToList();
            }
        }

	    public static void BulkCopyReleaseData(List<ReleaseItem> releaseData)
	    {
		    using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewReleases"].ConnectionString))
		    {
				connection.Open();
				using (var bcp = new SqlBulkCopy(connection))
				{
					bcp.DestinationTableName = "ReleaseItems";
					bcp.WriteToServer(ObjectReader.Create(releaseData ,"ReleaseDate", "Category", "Publisher", "ItemCode", "Title", "Price", "Note"));
				}
			}

	    }
    }
}
