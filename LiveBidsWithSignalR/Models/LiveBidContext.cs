using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LiveBidsWithSignalR.Models
{
    public class LiveBidContext : DbContext
    {
        public LiveBidContext()
            : base("LiveBids")
        {
            this.Database.Initialize(true);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<LiveBidContext>(new LiveBidInitializer());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<LiveBidItem> LiveBidItems { get; set; }
        public DbSet<LiveBid> LiveBid { get; set; }
    }

    public class LiveBidInitializer : DropCreateDatabaseIfModelChanges<LiveBidContext>
    {
    }
}