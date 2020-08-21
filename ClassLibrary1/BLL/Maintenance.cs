using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.BLL
{
    class Maintenance
    {
        private int maintenanceID;

        public int MaintenanceID
        {
            get { return maintenanceID; }
            set { maintenanceID = value; }
        }
        private string maintenanceDetails;

        public string MaintenanceDetails
        {
            get { return maintenanceDetails; }
            set { maintenanceDetails = value; }
        }
        private DateTime maintenanceDate;

        public DateTime MaintenanceDate
        {
            get { return maintenanceDate; }
            set { maintenanceDate = value; }
        }

        //constructor

        public Maintenance()
        {

        }

        //methods

    }
}
