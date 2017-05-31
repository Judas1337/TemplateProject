using System.Runtime.Serialization;

namespace TemplateProject.Sl.Wcf.Model.Exceptions
{
    [DataContract]
    public abstract class Fault
    {
        [DataMember]
        public string Message { get; set; }
    }
}