using Data_Access_Layer.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.BLL
{
   public class Componentss
    {

        private int componentsID;

        public int ComponentsID
        {
            get { return componentsID; }
            // set { componentID = value; }
        }

        private string componentsName;

        public string CompinentsName
        {
            get { return componentsName; }
            set { componentsName = value; }
        }

        private string componentsDetails;

        public string ComponentsDetails
        {
            get { return componentsDetails; }
            set { componentsDetails = value; }
        }

        //constructor
        public Componentss()
        {
                    
        }

        public Componentss( int componetIDParam, string componentNameParam, string componentDetailsParam)
        {
            this.componentsID = componetIDParam;
            this.componentsName = componentNameParam;
            this.componentsDetails = componentDetailsParam;
        }

        public Componentss( string componentNameParam, string componentDetailsParam)
        {
            this.componentsName = componentNameParam;
            this.componentsDetails = componentDetailsParam;
        }


        //methods

        public bool Delete(Componentss componentObjParam)
        {
            bool success = false;
            DataHandler handler = new DataHandler();
            
            if (handler.DeleteComponent(componentObjParam))
            {

                success = true;
            }
            else
            {
                success = false;

            }

            return success;
        }



        public bool UpdateComponent(Componentss componentObjParam)
        {
             DataHandler handler = new DataHandler();
            return handler.UpdateComponentss(componentObjParam);
        }

        public List<Componentss> ReadComponents()
        {
            List<Componentss> componentsFound = new List<Componentss>();

            DataHandler handler = new DataHandler();
            return componentsFound = handler.GetAllComponents();


        }

        public bool InsertComponents(Componentss componentsObjParam)
        {
            DataHandler handler = new DataHandler();

            return handler.InsertComponents(componentsObjParam);
        }

        public override string ToString()
        {
            return this.componentsName  ;
        }
    }
}
