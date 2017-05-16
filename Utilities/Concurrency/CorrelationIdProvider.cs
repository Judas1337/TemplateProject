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
    public class CorrelationIdProvider : ICorrelationIdProvider<Guid?>
    {
        private static readonly string CorrelationIdKey = Guid.NewGuid().ToString();
        private static volatile CorrelationIdProvider _instance;
        private static readonly object SyncRoot = new object();

        private CorrelationIdProvider() { }


        public static CorrelationIdProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new CorrelationIdProvider();
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
            lock (CorrelationIdKey)
            {
                CallContext.LogicalSetData(CorrelationIdKey, correlationId); 
            }
        }
    }
}
