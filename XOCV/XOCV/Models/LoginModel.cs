using Newtonsoft.Json;
using PropertyChanged;
using XOCV.ViewModels.Base;

namespace XOCV.Models
{
    [ImplementPropertyChanged]
    public class LoginModel : AbstractNpcObject
    {
        [JsonProperty("usernameOrEmailAddress")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}