using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LiveBidsWithSignalR.Models
{
    public class LiveBidItem
    {
        [Key]
        public int LiveBidItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<LiveBid> LiveBids { get; set; }
    }
    public class LiveBid
    {
        [Key]
        public int LiveBidId { get; set; }
        public int LiveBidItemId { get; set; }
        public string Username { get; set; }
        public decimal Bid { get; set; }
        public DateTime BidDateTime { get; set; }

        public virtual LiveBidItem LiveBidItem { get; set; }
    }
}