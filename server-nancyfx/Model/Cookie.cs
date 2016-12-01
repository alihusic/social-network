using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
