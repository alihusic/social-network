﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Model
{

    //model for Posts table
    //used for storage of posts 

    public class Posts
    {
        [Key]
        public int postsId { get; set; }
        public string postContent { get; set; }
        public DateTime postCreationDate { get; set; }
        public int userId { get; set; }
        public int numOfLikes { get; set; }

        //creating 1:n connection with user
        public virtual User user { get; set; }

    }
}
