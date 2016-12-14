using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Model
{
    public class Token
    {
        public int tokenId { get; set; }
        public int userId { get; set; }
        public string tokenHash { get; set; }

    }
}
