using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using Dapper;
using FastMember;

namespace NewReleases.Data
{
	public class ReleaseData
	{
		public IReadOnlyCollection<string> PremierPublishers;
		public IReadOnlyCollection<string> Categories;

		public ReleaseData()
		{
			this.PremierPublishers = this.GetPremierPublishers();
			this.Categories = this.GetCategories();
		}

		private SqlConnection GetDatabaseConnection()
		{
			return new SqlConnection(ConfigurationManager.
				ConnectionStrings["NewReleases"].ConnectionString);
		}

		public IEnumerable<string> GetRelease(Uri releaseUrl)
		{
			using (var webClient = new WebClient())
			{
				var stream = webClient.OpenRead(releaseUrl);
				var lines = new List<string>();

				if (stream != null)
				{
					using (var streamReader = new StreamReader(stream))
					{
						while (!streamReader.EndOfStream)
						{
							lines.Add(streamReader.ReadLine());
						}
					}
				}

				return lines.Where(f => !string.IsNullOrWhiteSpace(f));
			}
		}

		private IReadOnlyCollection<string> GetCategories()
		{
			using (var connection = this.GetDatabaseConnection())
			{
				return (IReadOnlyCollection<string>)connection.
					Query<string>("Select Category From Categories");
			}
		}

		private IReadOnlyCollection<string> GetPremierPublishers()
		{
			using (var connection = this.GetDatabaseConnection())
			{
				return (IReadOnlyCollection<string>)connection.
					Query<string>("Select PremierPublisher From PremierPublishers");
			}
		}

		public void BulkCopyReleaseData(List<ReleaseItem> releaseData)
		{
			using (var connection = this.GetDatabaseConnection())
			{
				connection.Open();
				using (var bcp = new SqlBulkCopy(connection))
				{
					bcp.DestinationTableName = "ReleaseItems";
					bcp.WriteToServer(
					ObjectReader.Create(
					releaseData,
					"ReleaseDate",
					"Category",
					"Publisher",
					"ItemCode",
					"Title",
					"IssueNumber",
					"Price",
					"Note"));
				}
			}

		}
	}

}
