using System;
using System.Runtime.Serialization;

namespace TemplateProject.Sl.Wcf.Model
{
    [DataContract]
    public class Product
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public DateTimeOffset DateAdded { get; set; }
    }
}