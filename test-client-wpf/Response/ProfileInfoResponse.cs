using SocialNetwork2.Model;

namespace SocialNetworkServerNV1.Response
{
    public class ProfileInfoResponse:StatusResponse
    {
        public ProfileInfo profileInfo { get; set; }
        public ProfileInfoResponse(ProfileInfo profileInfo):base(true)
        {
            this.profileInfo = profileInfo;
        }
    }
}