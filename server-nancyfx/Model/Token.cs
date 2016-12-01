using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Model
{
    public class Token
    {
        [Key]
        public int tokenId { get; set; }
        public int userId { get; set; }
        public string tokenHash { get; set; }

        [Required]
        public virtual User user { get; set; }

    }
}
