using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using XOCV.Models.ResponseModels;

namespace XOCV.Models
{
    public class ContentModel
    {
        //[PrimaryKey, AutoIncrement, JsonIgnore]
        //public int ContentModelID { get; set; }

        [JsonProperty (PropertyName = "listOfForms")/*, OneToMany(CascadeOperations = CascadeOperation.All)*/]
        public List<ComplexFormsModel> SetOfForms { get; set; }
    }
}