using System;
using System.Runtime.Serialization;

namespace TemplateProject.Sl.Wcf.Model
{
    [DataContract]
    public class Product
    {
        [DataMember(IsRequired = true)]
        public int Id { get; set; }

        [DataMember(IsRequired = true)]
        public string Name { get; set; }

        [DataMember(IsRequired = true)]
        public string Category { get; set; }

        [DataMember(IsRequired = true)]
        public decimal Price { get; set; }

        [DataMember(IsRequired = true)]
        public DateTimeOffset DateAdded { get; set; }
    }
}