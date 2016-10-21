using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace XOCV.Models.ResponseModels
{
    public class PropertyModel
    {
        [PrimaryKey, AutoIncrement, JsonIgnore]
        public int PropertyModelId { get; set; }

        
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
    }
}
