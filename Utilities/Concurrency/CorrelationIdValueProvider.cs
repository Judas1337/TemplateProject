using System;
using System.Runtime.Remoting.Messaging;

namespace TemplateProject.Utilities.Concurrency
{
    /// <summary>
    /// Used to store and provide access to a CorrelationId in the execution context which is unaffected by asynchronous code that switches threads or context. 
    /// </summary> 
    /// <remarks>Parent/sibling threads/contexts are unaffected by modificaitons made to the CorrelationId. 
    /// This is due to the fact that the execution context is inherited by copying the context to child threads/contexts</remarks>
    /// <remarks>Thread safe</remarks>
    public class CorrelationIdValueProvider : ICorrelationIdValueProvider<Guid?>
    {
        private static readonly string CorrelationIdKey = Guid.NewGuid().ToString();
        private static volatile CorrelationIdValueProvider _instance;
        private static readonly object SyncRoot = new object();

        private CorrelationIdValueProvider() { }


        public static CorrelationIdValueProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new CorrelationIdValueProvider();
                        }
                    }
                }

                return _instance;
            }
        }

        public Guid? GetCorrelationId()
        {
            var result = CallContext.LogicalGetData(CorrelationIdKey) as Guid?;
            return result;
        }

        public void SetCorrelationId(Guid? correlationId)
        {
            CallContext.LogicalSetData(CorrelationIdKey, correlationId);
        }
    }
}
