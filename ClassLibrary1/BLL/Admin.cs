using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer.DAL;

namespace Business_Logic_Layer.BLL
{
public class Admin : Person
    {

        private int adminID;

        public int AdminID
        {
            get { return adminID; }
            set { adminID = value; }
        }

        private string adminName;

        public string AdminName
        {
            get { return adminName; }
            set { adminName = value; }
        }

        private string adminPassWord;

        public string AdminPassword
        {
            get { return adminPassWord; }
            set { adminPassWord = value; }
        }

        private string adminSurname;

        public string AdminSurname
        {
            get { return adminSurname; }
            set { adminSurname = value; }
        }

        //constructor
        public Admin()
        {

        }

        public Admin(int adminIDParam, string adminNameParam, string adminPasswordParam, string adminSurnameParam) :base(adminNameParam, adminSurnameParam)
        {
            this.adminID = adminIDParam;
            this.adminName = adminNameParam;
            this.adminPassWord = adminPasswordParam;
            this.adminSurname = adminSurnameParam;
        }

        public Admin(int adminIDParam,string adminPasswordParam) : base()
        {
            this.adminID = adminIDParam;
            this.adminPassWord = adminPasswordParam;
        }

        //methods

        public bool AdminLogin(int adminIDParam , string adminPasswordParam)
        {
           bool isValid = false;
            List<Admin> adminFound = new List<Admin>();

            DataHandler handler = new DataHandler();
            adminFound = handler.GetAdminsLogin();

            foreach (var item in adminFound)
            {
                if (item.adminID == adminIDParam)
                {
                    if (item.adminPassWord == adminPasswordParam)
                    {
                        isValid = true;
                    }
                }
            }

            return isValid;
        }

    }
}
