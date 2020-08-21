namespace Business_Logic_Layer.BLL
{
    public class Person
    {
        private string personName;

        public string PersonName
        {
            get { return personName; }
            set { personName = value; }
        }

        private string personSurname;

        public string PersonSurname
        {
            get { return personSurname; }
            set { personSurname = value; }
        }


        public Person()
        {

        }
        public Person(string personNameParam , string personSurnameParam) 
        {
            this.personName = personNameParam;
            this.personSurname = personSurnameParam;
        }

    }
}
