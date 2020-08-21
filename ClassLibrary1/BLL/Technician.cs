using Data_Access_Layer.DAL;
using System.Collections.Generic;


namespace Business_Logic_Layer.BLL
{
    public class Technician : Person
    {
        private int technicianID;

        public int TechnicianID
        {
            get { return technicianID; }
            //set { technicianID = value; }
        }

        private string technicianName;

        public string TechnicianName
        {
            get { return technicianName; }
            set { technicianName = value; }
        }

        private string technicianSurname;

        public string TechnicianSurname
        {
            get { return technicianSurname; }
            set { technicianSurname = value; }
        }
        private string technicianSpeciality;

        public string TechnicianSpeciality
        {
            get { return technicianSpeciality; }
            set { technicianSpeciality = value; }
        }

        private int technicianAvailable;

        public int TechnicianAvailable
        {
            get { return technicianAvailable; }
            set { technicianAvailable = value; }
        }

        private string technicianPassword;

        public string TechnicianPassword
        {
            get { return technicianPassword; }
            set { technicianPassword = value; }
        }

        //constructor
        public Technician() : base()
        {

        }


        public Technician(string technicianNameParam, string technicianSurnameParam, string technicianSpecialityParam) : base(technicianNameParam,technicianSurnameParam)
        {
            this.technicianName = technicianNameParam;
            this.technicianSurname = technicianSurnameParam;
            this.technicianSpeciality = technicianSpecialityParam;
        }

        public Technician(int technicianIDParam, string technicianNameParam, string technicianSurnameParam, string technicianSpecialityParam) : base(technicianNameParam, technicianSurnameParam)
        {
            this.technicianID = technicianIDParam;
            this.technicianName = technicianNameParam;
            this.technicianSurname = technicianSurnameParam;
            this.technicianSpeciality = technicianSpecialityParam;
        }
        public Technician(int technicianIDParam, string technicianNameParam, string technicianSurnameParam, string technicianSpecialityParam, int technicianAvailableParam ) : base(technicianNameParam, technicianSurnameParam)
        {
            this.technicianID = technicianIDParam;
            this.technicianName = technicianNameParam;
            this.technicianSurname = technicianSurnameParam;
            this.technicianSpeciality = technicianSpecialityParam;
            this.technicianAvailable = technicianAvailableParam;
        }

        public Technician(int technicianIDParam, string technicianPassowrdParam)
        {
            this.technicianID = technicianIDParam;
            this.technicianPassword = technicianPassowrdParam;
        }
        //methods

        public List<Technician> ReadTechnicians()
        {
            List<Technician> technicianFound = new List<Technician>();
            DataHandler hanldler = new DataHandler();
            technicianFound = hanldler.GetTechnicians();
            return technicianFound;
        }


        public bool InsertTechnicians(Technician technicianObjParam)
        {
            DataHandler handler = new DataHandler();

            return handler.InsertTechnicians(technicianObjParam);
        }

        public bool UpdateTechnicians(Technician technicianObjParam)
        {
            DataHandler handler = new DataHandler();

            return handler.UpdateTechniceans(technicianObjParam);
        }

        public override string ToString()
        {
            return technicianName;
        }


        //re use 
        public List<string> GetDepartments()
        {
            List<Technician> techniciansFound = ReadTechnicians();
            List<string> departmentsFound = new List<string>();

            foreach (var item in techniciansFound)
            {
                departmentsFound.Add(item.technicianSpeciality);
            }

            return departmentsFound;
        }
 

        public bool DeleteTechnician(Technician technicianObjParam)
        {
            bool success = false;
            DataHandler handler = new DataHandler();

            if (handler.DeleteTechnician(technicianObjParam))
            {

                success = true;
            }
            else
            {
                success = false;

            }

            return success;
        }


        //Varify using stored procedure

        public bool technicianLoginValid(int technicianIDParam, string technicianPasswordParam)
        {
            bool isValid = false;
            List<Technician> technicianFound = new List<Technician>();

            DataHandler handler = new DataHandler();
            technicianFound = handler.GetTechnicianLogin(technicianIDParam, technicianPasswordParam);

            foreach (var item in technicianFound)
            {
                if (item.technicianID == technicianIDParam)
                {
                    if (item.technicianPassword == technicianPasswordParam)
                    {
                        isValid = true;
                        return isValid;
                    }
                }
            }


            return isValid;
        }

    }
}
