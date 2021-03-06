﻿using System;

namespace NewReleases.Data
{
    public class ReleaseItem
    {
		public DateTime ReleaseDate { get; set; }
		public string Category { get; set; }
        public string Publisher { get; set; }
        public string ItemCode { get; set; }
        public string Title { get; set; }
		public string IssueNumber { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }
    }
}
