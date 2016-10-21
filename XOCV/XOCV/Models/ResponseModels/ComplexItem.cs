using System.Collections.Generic;
using Newtonsoft.Json;
using PropertyChanged;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace XOCV.Models.ResponseModels
{
    [ImplementPropertyChanged]
    public class ComplexItem
    {
        [PrimaryKey, AutoIncrement, JsonIgnore]
        public int ComplexItemID { get; set; }

        [JsonIgnore, ForeignKey(typeof(FormModel))]
        public int FormModelID { get; set; }

        [JsonProperty (PropertyName = "items"), OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Item> Items { get; set; }

        [JsonProperty (PropertyName = "isMultipleSection")]
        public bool IsMultipleSection { get; set; }

        public ComplexItem()
        {
            Items = new List<Item>();
        }
    }
}