using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTemplateProject.Utilities.Concurrency
{
    public class ExecutionContextValueProvider : IExecutionContextValueProvider
    {
        private readonly string _correlationIdKey = "correlationId";

        public string GetCorrelationId()
        {
            object correlationId = CallContext.LogicalGetData(_correlationIdKey);
            return correlationId.ToString();
        }

        public void SetCorrelationId(string correlationId)
        {
            CallContext.LogicalSetData(_correlationIdKey, correlationId);
        }

        public object GetData(string name)
        {
            return CallContext.LogicalGetData(name);
        }

        public void SetData(string name, object data)
        {
            CallContext.LogicalSetData(name, data);
        }
    }
}
