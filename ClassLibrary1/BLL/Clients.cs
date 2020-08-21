using Data_Access_Layer.DAL;
using System.Collections.Generic;

namespace Business_Logic_Layer.BLL
{
    public class Clients 
        //Clinent does not need to inherit from Person. Since it represents an organization
    {
        private int clientID;

        public int ClientID
        {
            get { return clientID; }
            set { clientID = value; }
        }

        private string clientName;

        public string ClientName
        {
            get { return clientName; }
            set { clientName = value; }
        }

        private int organisationSize;

        public int OrganisationSize
        {
            get { return organisationSize; }
            set { organisationSize = value; }
        }

        private string location;

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        private string clientPassword;

        public string ClientPassword
        {
            get { return clientPassword; }
            set { clientPassword = value; }
        }

        private float clientBalance;

        public float ClientBalance
        {
            get { return clientBalance; }
            set { clientBalance = value; }
        }


        //Events and Delegates
        // Define a delegate named LogHandler, which will encapsulate
        // any method that takes a string as the parameter and returns no value
        public delegate void LogHandler(float updatedBalance);

        // Define an Event based on the above Delegate
        public event LogHandler Log;

        // Instead of having the Process() function take a delegate
        // as a parameter, we've declared a Log event. Call the Event,
        // using the OnLog Method,is the name of the Event.
        public void Process(int clientDParam,int productIDParam, float cost)
        {
            //float currentBalance = 0;
            this.ClientID = clientDParam;
            float currentBalance = this.ClientBalance; // properties 
            DataHandler handler = new DataHandler();
            handler.UpdateClientBalance(clientID, productIDParam, currentBalance);




            OnLog(cost,productIDParam);
        }

        // By Default, OnLog Method, to call the Event
        protected void OnLog(float updatedBalance, int productIDParam)
        {
            if (Log != null)
            {
            }
        
        }

        //Constructor
        public Clients()
        {

        }

        public Clients(int clientIDParam, string clientPasswordParam)
        {
            this.clientID = clientIDParam;
            this.clientPassword = clientPasswordParam;
        }

        public Clients(int clientIDParam, string clientNameParam, string clientPasswordParam ) 
        {
            this.clientID   = clientIDParam;
            this.clientName = clientNameParam;
            this.clientPassword = clientPasswordParam;
        }

        public Clients(float balanceParam)
        {
            this.clientBalance = balanceParam;
        }

        //methods

        public bool clientLogin(int clientIDParam, string clientPasswordParam)
        {
            bool isValid = false;
            List<Clients> clientsFound = new List<Clients>();

            DataHandler handler = new DataHandler();
            clientsFound = handler.GetClientLogin();

            foreach (var item in clientsFound)
            {
                if (item.clientID == clientIDParam)
                {
                    if (item.clientPassword == clientPasswordParam)
                    {
                        isValid = true;
                        return isValid;
                    }
                }
            }
            

            return isValid;
        }

        public bool BalanceSufficiant(int clientIDParam, float totalPrice)
        {
            bool isSufficiant = false; 


            //Clients clientObj = new Clients();

            DataHandler handler = new DataHandler();

            List<Clients> clientsFound =  handler.GetClientBalance(clientIDParam);



            if (clientsFound[0].clientBalance >= totalPrice)
            {
                ClientBalance = clientsFound[0].clientBalance;

                this.clientBalance = clientsFound[0].clientBalance - totalPrice;
                //float newBalance = clientsFound[0].clientBalance - totalPrice;
                

                isSufficiant = true;
            }
            

            return isSufficiant;
        }

        public float GetBalance(int clientID)
        {
            DataHandler handler = new DataHandler();

            return handler.GetClientAccBalance(clientID);
        }


        
    }
}
