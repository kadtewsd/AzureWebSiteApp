using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureWebApp.Service.Message
{
    public class MessageService : IMessageService
    {
        private string _divId = null;

        public string DivID
        {
            get
            {
                return this._divId != null ? this._divId : this.GetType().Name;
            }

            set
            {
                _divId = value;
            }
        }
    }
}