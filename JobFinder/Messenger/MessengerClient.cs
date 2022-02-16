using JobFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder
{
    public abstract class MessengerClient : IMessenger
    {
        public Message message;
        public virtual void SendMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
