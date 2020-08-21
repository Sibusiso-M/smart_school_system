using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Data_Accss_Layer
{
    public class DataHandler
    {
        private string connectionString;
        private SqlCommand command;
        private SqlConnection connection;
        private SqlDataAdapter dataAdapter;
        private DataTable dataTable;

        public DataHandler(string connectionStringParam = "default")
        {
            this.connectionString = ConfigurationManager.ConnectionStrings[connectionStringParam].ConnectionString;
            this.connection = new SqlConnection(connectionString);
        }

        //read data
        public DataTable Select(string query)
        {
            //DataHandler handler = new DataHandler();
            try
            {

                if (connection.State != ConnectionState.Open)
                {
                    this.connection.Open();
                }

                this.dataTable = new DataTable();
                this.command = new SqlCommand(query, connection);

                this.dataAdapter = new SqlDataAdapter(command);
                this.dataAdapter.Fill(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            { connection.Close(); }


            return this.dataTable;
        }

        ////insert to DB

        public void Insert(string query)
        {

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                command = new SqlCommand(query.ToString(), connection);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }


        }

        ////delete on DB

        public void Delete(string query)
        {

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                // Delete Data

                SqlCommand command = new SqlCommand(query.ToString(), connection);
                command.ExecuteNonQuery(); // Here our query will be executed and data saved into the database.

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }


        }


        //search database
        public DataTable Search(string query)
        {
            try
            {

                if (connection.State != ConnectionState.Open)
                {
                    this.connection.Open();
                }

                this.dataTable = new DataTable();
                this.command = new SqlCommand(query, connection);

                this.dataAdapter = new SqlDataAdapter(command);
                this.dataAdapter.Fill(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }


            return this.dataTable;
        }
        //update Database
        public void UpdateData(string query)
        {

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }


                SqlCommand command = new SqlCommand(query.ToString(), connection);
                command.ExecuteNonQuery(); // Here our query will be executed and data saved into the database.
                MessageBox.Show("Data Updated");
                
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
     
        }

    }
}
