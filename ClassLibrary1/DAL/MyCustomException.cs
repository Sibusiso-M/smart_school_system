using System;
using System.Windows.Forms;

namespace Data_Access_Layer.DAL
{
    class MyCustomException : Exception
    {
        public MyCustomException() : base() { }
        public MyCustomException(string message) : base(message)
        {
            MessageBox.Show(message);
        }
    }
}
