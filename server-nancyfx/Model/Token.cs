using System.ComponentModel.DataAnnotations;

namespace SocialNetwork2.Model
{
    public class Token
    {
        /// <summary>
        /// Class used as database model for Token table.
        /// Class created by Ermin.
        /// </summary>
        [Key]
        public int tokenId { get; set; }
        public int userId { get; set; }
        public string tokenHash { get; set; }


        //setting up 1:1 relationship with a User
        [Required]
        public virtual User user { get; set; }

    }
}
