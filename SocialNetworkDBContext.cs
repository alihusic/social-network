namespace SocialNetwork
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Model;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class SocialNetworkDBContext : DbContext
    {
        public SocialNetworkDBContext()
            : base("name=SocialNetworkDBContext")
        {
        }

        public virtual DbSet<User> users { get; set; }
        public virtual DbSet<PrivateMessages> privateMessages { get; set; }
        public virtual DbSet<PrivateChat> privateChat { get; set; }
        public virtual DbSet<PendingFriendRequests> friendRequest { get; set; }
        public virtual DbSet<Posts> posts { get; set; }
        public virtual DbSet<UnreadMessages> unreadMessages { get; set; }
        public virtual DbSet<Likes> likes { get; set; }
        public virtual DbSet<Cookie> cookies { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
