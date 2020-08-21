using System.Collections.Generic;
using Data_Access_Layer.DAL;

namespace Business_Logic_Layer.BLL
{
    public class Orders
    {
        private int orderID;

        public int OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }

        private int clientID;

        public int ClientID
        {
            get { return clientID; }
            set { clientID = value; }
        }

        private int productID;

        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        private int orderUnitsCopy;

        public int OrderUnitsCopy
        {
            get { return orderUnitsCopy; }
            set { orderUnitsCopy = value; }
        }
        private float cost;

        public float Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        private int discountQualification;

        public int  DiscountQualification
        {
            get { return discountQualification; }
            set { discountQualification = value; }
        }


        public Orders()
        {
                
        }

        public Orders(int orderIDParam, int clientIDParam, int productIDParam, int orderUnitsCopyParam, float costParam, int discountQualificationParam)
        {
            this.orderID = orderIDParam;
            this.clientID = clientIDParam;
            this.productID = productIDParam;
            this.orderUnitsCopy = orderUnitsCopyParam;
            this.cost = costParam;
            this.discountQualification = discountQualificationParam;
        }

        public List<Orders> ReadClientOrders(int clientIDParam )
        {
            List<Orders> ordersFound = new List<Orders>();

            DataHandler handler = new DataHandler();

            ordersFound = handler.GetClientOrders(clientIDParam);

            return ordersFound;
        }

        //events 
        //Events and Delegates

        public delegate void LogHandler(float cost);

        // Define an Event based on the above Delegate
        public event LogHandler Log;

        // Instead of having the Process() function take a delegate

        public void Process(int clientDParam, int productIDParam, float costParam)
        {
            this.ClientID = clientDParam;
            float currentBalance = costParam; // properties 
            DataHandler handler = new DataHandler();

            //Here we control which product qualify for a discount 
            int discount; 

            if ((productIDParam == 1) || (productIDParam == 3))
            {
                discount = 1 ;
            }
            else
            {
                discount = 0;
            }

            handler.InsertToOrders(clientDParam, productIDParam, costParam,discount,orderUnitsCopy);

            //OnLog(cost,productIDParam);
        }

        protected void OnLog(int clientID, int productID, float cost)
        {
            if (Log != null)
            {
                Log(cost);
            }
        }
        public void Logger(float cost)
        {
            
        }

    }
}
