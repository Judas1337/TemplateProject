using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using TemplateProject.Bll.Contract.Bll.Model.Exceptions;
using TemplateProject.Sl.Wcf.Model.Exceptions;

namespace TemplateProject.Sl.Wcf.ExceptionHandler
{
    public class ErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error is FaultException) return;

            FaultException faultException;

            if (error is NotFoundException)
            {
                faultException = new FaultException<NotFoundFault>(new NotFoundFault { Message = error.Message }, "NotFound");
               
            }else if (error is InputException)
            {
                faultException = new FaultException<InputFault>(new InputFault { Message = error.Message }, "InputFault");
            }
            else
            {
                faultException = new FaultException<GenericFault>(new GenericFault { Message = error.Message }, "Internal Error");
            }

            MessageFault messageFault = faultException.CreateMessageFault();
            fault = Message.CreateMessage(version, messageFault, null);           
        }
    }
}