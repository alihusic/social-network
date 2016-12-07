using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Model
{
    /// <summary>
    /// obsolete
    /// </summary>
    public class Cookie
    {
        [Key]
        public int cookieId { get; set; }
        public int userId { get; set; }
        public string cookieHash { get; set; }


        public virtual User user { get; set; }

    }
}
