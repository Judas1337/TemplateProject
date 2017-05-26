using System;

using System.Collections.ObjectModel;

using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;


namespace TemplateProject.Sl.Wcf.Attribute
{
    public class ErrorHandlingAttribute : System.Attribute, IServiceBehavior
    {
        private readonly Type _errorHandlerType;

        public ErrorHandlingAttribute(Type errorHandlingType)
        {
            _errorHandlerType = errorHandlingType;
        }       

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            IErrorHandler handler = (IErrorHandler)Activator.CreateInstance(_errorHandlerType);

            foreach (ChannelDispatcherBase chanDispBase in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher channelDispatcher = chanDispBase as ChannelDispatcher;
                if (channelDispatcher != null) channelDispatcher.ErrorHandlers.Add(handler);                
            }
        }

        #region UnimplementedMethods
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {

        }
        #endregion
    }
}