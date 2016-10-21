using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using PropertyChanged;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace XOCV.Models.ResponseModels
{
    [ImplementPropertyChanged]
    public class Item
    {
        
        [PrimaryKey, AutoIncrement, JsonIgnore]
        public int ID { get; set; }

        [JsonIgnore, ForeignKey(typeof(ComplexItem))]
        public int ComplexItemID { get; set; }

        [JsonProperty (PropertyName = "itemId")]
        public int ItemId { get; set; }

        [JsonProperty (PropertyName = "itemName")]
        public string Name { get; set; }

        [ForeignKey(typeof(PropertyModel)), JsonIgnore]
        public int PropertyModelId { get; set; }

        [OneToOne, JsonProperty(PropertyName = "properties")]
        public PropertyModel Properties { get; set; }

        [JsonProperty (PropertyName = "itemsOfDictionaryIntToString"), TextBlob("RadioButtonItemsBlobbed")]
        public ObservableCollection<string> RadioButtonItemsSource { get; set; }

        [JsonIgnore]
        public string RadioButtonItemsBlobbed { get; set; }

        [JsonProperty (PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty (PropertyName = "imageGuids"), Ignore]
        public ObservableCollection<Guid> ListOfImageGuids { get; set; }

        public Item ()
        {
            ListOfImageGuids = new ObservableCollection<Guid> ();
            RadioButtonItemsSource = new ObservableCollection<string> ();
            Properties = new PropertyModel ();
        }
    }
}