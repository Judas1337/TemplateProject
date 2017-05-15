using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TemplateProject.Sl.Wcf
{
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        string GetData(int value);
    }
}
