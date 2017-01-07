using SocialNetwork2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkServer.Builder
{
    /// <summary>
    /// Class created by Ermin & Ali.
    /// </summary>
    class UserBuilder
    {
        private string name;
        private string lastName;
        private string username;
        private string password;
        private string country;
        private string city;
        private string pictureURL;
        private string coverPictureURL;
        private string gender;
        private DateTime dateOfBirth;


        public UserBuilder Name(string name)
        {
            this.name = name;
            return this;
        }

        public UserBuilder LastName(string lastName)
        {
            this.lastName = lastName;
            return this;
        }

        public UserBuilder Username(string username)
        {
            this.username = username;
            return this;
        }

        public UserBuilder Password(string password)
        {
            this.password = password;
            return this;
        }

        public UserBuilder Country(string country)
        {
            this.country = country;
            return this;
        }



        public UserBuilder City(string city)
        {
            this.city = city;
            return this;
        }



        public UserBuilder PictureURL(string pictureURL)
        {
            this.pictureURL = pictureURL;
            return this;
        }



        public UserBuilder CoverPictureURL(string coverPictureURL)
        {
            this.coverPictureURL = coverPictureURL;
            return this;
        }



        public UserBuilder Gender(string gender)
        {
            this.gender = gender;
            return this;
        }


        public UserBuilder DateOfBirth(DateTime dateOfBirth)
        {
            this.dateOfBirth = dateOfBirth;
            return this;
        }

        public User Build()
        {
            return new User()
            {
                name = name,
                lastName = lastName,
                username = username,
                password = password,
                country = country,
                city = city,
                pictureURL = pictureURL,
                coverPictureURL = coverPictureURL,
                gender = gender,
                dateOfBirth = dateOfBirth
            };
        }
    }
}
