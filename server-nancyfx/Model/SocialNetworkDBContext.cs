namespace SocialNetwork
{
    using System.Data.Entity;
    using Model;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using SocialNetworkServer.Model;

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
        public virtual DbSet<Token> tokens { get; set; }
        public virtual DbSet<Comments> comments { get; set; }
        public virtual DbSet<Notifications> notifications { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
