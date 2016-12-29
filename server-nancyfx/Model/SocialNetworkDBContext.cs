using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SocialNetworkServer.Model;
using SocialNetwork2.Model;

namespace SocialNetwork2
{
    public partial class SocialNetworkDBContext : DbContext
    {
        public SocialNetworkDBContext()
            : base("name=SocialNetworkDBContext")
        {
        }

        public virtual DbSet<User> users { get; set; }
        public virtual DbSet<PrivateMessage> privateMessages { get; set; }
        public virtual DbSet<PrivateChat> privateChat { get; set; }
        public virtual DbSet<PendingFriendRequest> friendRequest { get; set; }
        public virtual DbSet<Post> posts { get; set; }
        public virtual DbSet<UnreadMessage> unreadMessages { get; set; }
        public virtual DbSet<Like> likes { get; set; }
        public virtual DbSet<Token> tokens { get; set; }
        public virtual DbSet<Comment> comments { get; set; }
        public virtual DbSet<Notification> notifications { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
