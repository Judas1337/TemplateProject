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
        private readonly IErrorHandler _erroHandler;

        public ErrorHandlingAttribute(Type errorHandlingType)
        {
            _errorHandlerType = errorHandlingType;
            _erroHandler = (IErrorHandler)Activator.CreateInstance(_errorHandlerType);
        }       

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcherBase chanDispBase in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher channelDispatcher = chanDispBase as ChannelDispatcher;
                if (channelDispatcher != null) channelDispatcher.ErrorHandlers.Add(_erroHandler);                
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