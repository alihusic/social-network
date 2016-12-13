using SocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestClientSN.Model;

namespace SocialNetworkServer.Builder
{
    class ProfileInfoBuilder
    {
        private string name;
        private string lastName;
        private string username;
        private string country;
        private string city;
        private string pictureURL;
        private string coverPictureURL;
        private string gender;
        private DateTime dateOfBirth;


        public ProfileInfoBuilder Name(string name)
        {
            this.name = name;
            return this;
        }

        public ProfileInfoBuilder LastName(string lastName)
        {
            this.lastName = lastName;
            return this;
        }

        public ProfileInfoBuilder Username(string username)
        {
            this.username = username;
            return this;
        }

        public ProfileInfoBuilder Country(string country)
        {
            this.country = country;
            return this;
        }



        public ProfileInfoBuilder City(string city)
        {
            this.city = city;
            return this;
        }



        public ProfileInfoBuilder PictureURL(string pictureURL)
        {
            this.pictureURL = pictureURL;
            return this;
        }



        public ProfileInfoBuilder CoverPictureURL(string coverPictureURL)
        {
            this.coverPictureURL = coverPictureURL;
            return this;
        }



        public ProfileInfoBuilder Gender(string gender)
        {
            this.gender = gender;
            return this;
        }


        public ProfileInfoBuilder DateOfBirth(DateTime dateOfBirth)
        {
            this.dateOfBirth = dateOfBirth;
            return this;
        }

        public ProfileInfo Build()
        {
            return new ProfileInfo()
            {
                name = name,
                lastName = lastName,
                username = username,
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
