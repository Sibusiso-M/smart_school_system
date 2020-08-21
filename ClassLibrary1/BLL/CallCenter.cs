using Data_Access_Layer.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.BLL
{
    public class CallCenter
    {
        private int callLogID;

        public int CallLogID
        {
            get { return callLogID; }
            set { callLogID = value; }
        }

        private int clientId;

        public int ClientID
        {
            get { return clientId; }
            set { clientId = value; }
        }
        private string callDuration;

        public string CallDuration
        {
            get { return callDuration; }
            set { callDuration = value; }
        }

        private string clientName;

        public string ClientName
        {
            get { return clientName; }
            set { clientName = value; }
        }

        private string callDate;

        public string CallDate
        {
            get { return callDate; }
            set { callDate = value; }
        }


        public CallCenter()
        {

        }

        public CallCenter(int clientIDParam, string callDurationParam, string clientNameParam, string callDateParam)
        {
            this.clientId = clientIDParam;
            this.callDuration = callDurationParam;
            this.clientName = clientNameParam;
            this.callDate = callDateParam;
        }
        public CallCenter(int clientCallLogParam, int clientIDParam, string callDurationParam, string clientNameParam, string callDateParam)
        {
            this.callLogID = clientCallLogParam;
            this.clientId = clientIDParam;
            this.callDuration = callDurationParam;
            this.clientName = clientNameParam;
            this.callDate = callDateParam;
        }


        // methods 

        public void LogCall(int clientIDParam, string callDepartmentParam, string callDurationParam, string callDateParam)
        {
            DataHandler handler = new DataHandler();

            handler.InsertToCallCenterLogs(clientIDParam, callDepartmentParam, callDurationParam, callDateParam);
        }

        public List<CallCenter> CallHistory(int clientIDParam)
        {
            List<CallCenter> callsFound = new List<CallCenter>();

            DataHandler handler = new DataHandler();
            callsFound = handler.GetClientCallHistory(clientIDParam);
            

            return callsFound;

        }
    }
}
