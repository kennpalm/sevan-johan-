using System.Collections.Generic;
using Newtonsoft.Json;
using PropertyChanged;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using XOCV.Enums;

namespace XOCV.Models.ResponseModels
{
    [ImplementPropertyChanged]
    public class FormModel
    {
        [PrimaryKey, AutoIncrement, JsonIgnore]
        public int FormModelID { get; set; }

        [JsonIgnore, ForeignKey(typeof(ComplexFormsModel))]
        public int ComplexFormsModelID { get; set; }

        [JsonProperty (PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty (PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty (PropertyName = "status")]
        public FormStatus Status { get; set; }

        [JsonProperty (PropertyName = "storeNumber")]
        public string StoreNumber { get; set; }

        [JsonProperty (PropertyName = "brandingPackage")]
        public string BrandingPackage { get; set; }

        [JsonProperty (PropertyName = "complexItems"), OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<ComplexItem> ComplexItems { get; set; }

        public FormModel ()
        {
            ComplexItems = new List<ComplexItem> ();
        }
    }
}