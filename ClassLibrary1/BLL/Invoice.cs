using System;

namespace Business_Logic_Layer.BLL
{
    public  class Invoice
    {
        private int invoiceID;

        public int InvoiceID
        {
            get { return invoiceID; }
            set { invoiceID = value; }
        }

        private float totalCost;

        public float TotalCost
        {
            get { return totalCost; }
            set { totalCost = value; }
        }

        private DateTime purchaseDate;

        public DateTime PurchaseDate
        {
            get { return purchaseDate; }
            set { purchaseDate = value; }
        }

        //constructor

        public Invoice()
        {

        }

        //methods

    }
}
