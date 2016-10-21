using System;
using Newtonsoft.Json;
using PropertyChanged;

namespace XOCV.Models
{
    [ImplementPropertyChanged]
    public class ImageModel
    {
        [JsonProperty(PropertyName = "guid")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "base64Image")]
        public string Base64String { get; set; }
    }
}