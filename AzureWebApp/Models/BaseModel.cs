using AzureWebApp.stub;
using System;

namespace AzureWebApp.Models
{
    public abstract class BaseModel
    {
        
        public string EnvironmentInfo
        {
            get
            {
                return DebugUtil.IsDebug() ? "デバッグ！" : "Azure !! Beat AWS !!";
            }
        }

        public string Message { get; set; }

        public Exception Exception { get; set; }

        protected string displayName;

        public abstract string  DisplayName
        {
            get; set;
        }

        public string BirthDay { get; set; }

        public string Email { get; set; }

        private int rowCount = 0;

        public int RowCount
        {
            set
            {
                this.rowCount = value;
            }
        }

        public bool Changed
        {
            get
            {
                return this.rowCount != 0;
            }
        }

        public string UserId { get; set; }

        public string Alias { get; set; }

        public string FamilyName { get; set; }

        public string FirstName { get; set; }

        public RequestInfo RequestInfo { get; set; }

        public ResponseInfo ResponseInfo { get; set; }

    }
}