using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClientSN.Model
{
    class ProfileInfo
    {
        public int userId { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string username { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string pictureURL { get; set; }
        public string coverPictureURL { get; set; }
        public string gender { get; set; }
        public DateTime dateOfBirth { get; set; }

        public ProfileInfo(int i)
        {
            userId = i;
            name = "name";
            lastName = "lastName";
            pictureURL = "https://s-media-cache-ak0.pinimg.com/originals/71/cd/a6/71cda6fbbd8fd00333a426d93c3165d4.jpg";
        }
    }
}
