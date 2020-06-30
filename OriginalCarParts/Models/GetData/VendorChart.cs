using System.Runtime.Serialization;

namespace OriginalCarParts.Models.GetData
{
    // модель для сериализации данных о приходах в графики
    [DataContract]
    public class VendorChart 
    {

        [DataMember(Name = "label")]
        public string Name { get; set; }

        [DataMember(Name = "y")]
        public double Sum { get; set; }  

    }
}