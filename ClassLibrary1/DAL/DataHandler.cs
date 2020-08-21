using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Business_Logic_Layer.BLL;
using System.Configuration;

namespace Data_Access_Layer.DAL
{
    public class DataHandler
    {
        private string connectionString;
        private SqlCommand command;
        private SqlConnection connection;
        private SqlDataAdapter dataAdapter;
        private DataTable dataTable;

        public DataHandler(string connectionStringParam = "SmartSchoolSystemDB")
        {
            this.connectionString = ConfigurationManager.ConnectionStrings[connectionStringParam].ConnectionString;
            this.connection = new SqlConnection(connectionString);
        }

# region ReUse Database  methods
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
            catch (SqlException)
            {
                throw;
            }
            finally
            { connection.Close(); }


            return this.dataTable;
        }

        ////insert to DB

        public bool Insert(string query)
        {
            bool isSuccess = false;

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                command = new SqlCommand(query.ToString(), connection);
                command.ExecuteNonQuery();
                isSuccess = true;
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }

            return isSuccess;
        }

        ////delete on DB

        public bool Delete(string query)
        {
            bool success = false;
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                // Delete Data

                SqlCommand command = new SqlCommand(query.ToString(), connection);
                command.ExecuteNonQuery(); // Here our query will be executed and data saved into the database.
                success = true;
            }
            catch (SqlException ex)
            {
                throw (new MyCustomException("SQL server warning or error triggered, when trying to delete '"+query+"' Details:"+ ex.Message.ToString() ));

            }
            finally
            {
                connection.Close();

            }
            return success;

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
        public bool UpdateData(string query)
        {

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }


                SqlCommand command = new SqlCommand(query.ToString(), connection);
                command.ExecuteNonQuery(); // Here our query will be executed and data saved into the database.
                return true;

            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                connection.Close();
            }

        }

        #endregion

#region  Products
        public List<Product> GetAllProducts()
        {
            List<Product> productsFound = new List<Product>();
            string query = "SELECT * FROM tblProducts";

            DataHandler handler = new DataHandler();
            DataTable dataRaw = handler.Select(query);

            foreach (DataRow item in dataRaw.Rows)
            {
                Product productObj = new Product(
                    //values to read
                    //item[1],item[2],item[3]...
                    int.Parse(item["ProductID"].ToString()), item["ProductName"].ToString(),
                    float.Parse(item["ProductPrice"].ToString()),
                    item["ProductDescription"].ToString(),
                    item["ProductFeatures"].ToString(),
                    item["ProductLicense"].ToString()
                    );
                // ProductID and will be needed for reporting

                productsFound.Add(productObj);
            }

            return productsFound;
        }

        public List<Product> GetProductsDetails()
        {
            List<Product> productsFound = new List<Product>();
            string query = "SELECT ProductID, ProductName, ProductPrice, ProductDescription, ProductFeatures, ProductLicense FROM tblProducts";

            DataHandler handler = new DataHandler();
            DataTable dataRaw = handler.Select(query);

            foreach (DataRow item in dataRaw.Rows)
            {
                Product productObj = new Product(
                    //values to read
                    //item[1],item[2],item[3]...
                    int.Parse(item["ProductID"].ToString()), item["ProductName"].ToString(),
                    float.Parse(item["ProductPrice"].ToString()),
                    item["ProductDescription"].ToString(),
                    item["ProductFeatures"].ToString(),
                    item["ProductLicense"].ToString()
                    );
                // ProductID and will be needed for reporting

                productsFound.Add(productObj);
            }

            return productsFound;
        }
        public bool DeleteProduct(int ProductID = 0)
        {
            string query = "DELETE FROM tblProducts where ProductID='" + ProductID + "'";
            DataHandler handler = new DataHandler();
            return handler.Delete(query);
        }


        public bool UpdateProducts(Product productToUpdateParam)
        {
            bool isSuccess = false;

            List<Product> productList = new List<Product>();
            DataHandler handler = new DataHandler();

            string query = "UPDATE tblProducts " +
                            "SET ProductName ='" + productToUpdateParam.ProductName + "'" +
                            ",ProductDescription ='" + productToUpdateParam.ProductDescription + "'" +
                            ",ProductLicense ='" + productToUpdateParam.ProductLicense + "'" +
                            ",ProductPrice = " + productToUpdateParam.ProductPrice +

                            "WHERE ProductID ='" + productToUpdateParam.ProductID + "'";

            if (handler.UpdateData(query))
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }

            return isSuccess;
            // handler.UpdateData(query);   // use unery operrators to check whether data changes were successful or not and display message         
        }

        public bool InsertProduct(Product productToInsertParam)
        {
            bool isSuccess = false;

            DataHandler handler = new DataHandler();
            string queryLastID = "SELECT TOP 1 ProductID FROM tblProducts ORDER BY ProductID DESC";
            int lastProductID = 0;
            DataTable dataRaw = handler.Select(queryLastID);

            List<Product> productObj = new List<Product>();

            foreach (DataRow item in dataRaw.Rows)
            {
                lastProductID = (int)item["ProductID"];
            }
            int latestProductID = lastProductID + 1;  //Get last ID number and increment by one for new record, get ID of last record

            //Insert Record

            string query = "INSERT INTO tblProducts (ProductID, ProductName , ProductPrice, ProductDescription, ProductFeatures, ProductLicense) VALUES (" + latestProductID + ",'" + productToInsertParam.ProductName + "'," + productToInsertParam.ProductPrice + ",'" + productToInsertParam.ProductDescription + "','" + productToInsertParam.ProductFeatures + "','" + productToInsertParam.ProductLicense + "')";

            if (handler.Insert(query))
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            return isSuccess;
        }
        #endregion

        #region Admin

        public List<Admin> GetAdminsLogin()
        {
            string query = "SELECT AdminID, AdminPassword FROM tblAdmins";
            List<Admin> adminFound = new List<Admin>();
            DataHandler handler = new DataHandler();

            DataTable dataRaw = handler.Select(query);

            foreach (DataRow item in dataRaw.Rows)
            {
                Admin adminObj = new Admin(int.Parse(item["AdminID"].ToString()), item["AdminPassword"].ToString());
                adminFound.Add(adminObj);
            }

            return adminFound;
        }

        public List<Admin> GetAllAdmins()
        {
            string query = "SELECT * FROM tblAdmins";

            List<Admin> adminFound = new List<Admin>();
            DataHandler handler = new DataHandler();

            DataTable dataRaw = handler.Select(query);

            foreach (DataRow item in dataRaw.Rows)
            {
                Admin adminObj = new Admin(int.Parse(item["AdminID"].ToString()), item["AdminName"].ToString(), item["AdminPassword"].ToString(), item["AdminSurname"].ToString());
                adminFound.Add(adminObj);
            }

            return adminFound;
        }

        #endregion
# region CLient


        public List<Clients> GetClientLogin()
        {
            string query = "SELECT ClientID, ClientPassword FROM tblClients";
            List<Clients> clientFound = new List<Clients>();
            DataHandler handler = new DataHandler();

            DataTable dataRaw = handler.Select(query);

            foreach (DataRow item in dataRaw.Rows)
            {
                Clients clientObj = new Clients(int.Parse(item["ClientID"].ToString()), item["ClientPassword"].ToString());
                clientFound.Add(clientObj);
            }

            return clientFound;
        }
        #endregion

#region Components

        public List<Componentss> GetAllComponents()
        {
            string query = "SELECT [ComponentID], [ComponentName],[ComponentDetails] FROM [tblComponents]";
            List<Componentss> componentFound = new List<Componentss>();
            DataHandler handler = new DataHandler();

            DataTable dataRaw = handler.Select(query);

            foreach (DataRow item in dataRaw.Rows)
            {
                Componentss componentObj = new Componentss(int.Parse(item["ComponentID"].ToString()), item["ComponentName"].ToString(), item["ComponentDetails"].ToString());
                componentFound.Add(componentObj);
            }

            return componentFound;
        }

        public bool InsertComponents(Componentss componentsObjParam)
        {
            bool isSuccess = false;
            DataHandler handler = new DataHandler();
            string queryLastID = "SELECT TOP 1 [ComponentID] FROM [tblComponents] ORDER BY ComponentID DESC";
            int lastComponentID = 0;
            DataTable dataRaw = handler.Select(queryLastID);

            List<Componentss> componentObj = new List<Componentss>();

            foreach (DataRow item in dataRaw.Rows)
            {
                lastComponentID = (int)item["ComponentID"];
            }
            int latestComponentID = lastComponentID + 1;  //Get last ID number and increment by one for new record, get ID of last record

            string query = @"INSERT INTO [dbo].[tblComponents]  ([ComponentID],[ComponentName],[ComponentDetails]) VALUES  (" + latestComponentID + ",'" + componentsObjParam.CompinentsName + "' ,'" + componentsObjParam.ComponentsDetails + "' )"; // New Components are made available initialy

            if (handler.Insert(query))
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }

            return isSuccess;

        }

        public bool UpdateComponentss(Componentss componentToUpdateParam)
        {
            bool isSuccess = false;

            List<Componentss> componentList = new List<Componentss>();
            DataHandler handler = new DataHandler();

            string query = "UPDATE [tblComponents] " +
                            "SET [ComponentName] ='" + componentToUpdateParam.CompinentsName + "'" +
                            ",[ComponentDetails] ='" + componentToUpdateParam.ComponentsDetails + "'" +

                            "WHERE [ComponentID] ='" + componentToUpdateParam.ComponentsID + "'";

            if (handler.UpdateData(query))
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }

            return isSuccess;
            // handler.UpdateData(query);   // use unery operrators to check whether data changes were successful or not and display message         
        }

        public bool DeleteComponent(Componentss componentObjParam)
        {
            string query = "DELETE FROM [tblComponents] where [ComponentID]='" + componentObjParam.ComponentsID + "'";
            DataHandler handler = new DataHandler();
            return handler.Delete(query);
        }



        #endregion

#region  Technicians
        public List<Technician> GetTechnicians()
        {
            List<Technician> techniciansFound = new List<Technician>();

            DataHandler handler = new DataHandler();

            DataTable dataRaw;
            string query = "SELECT [TechnicianID],[TechnicianName],[TechnicianSurname],[TechnicianSpecialty],[TechnicianAvailable] FROM [tblTechnicians]";
            dataRaw = handler.Select(query);

            foreach (DataRow item in dataRaw.Rows)
            {
                Technician technicianObj = new Technician(int.Parse(item["TechnicianID"].ToString()), item["TechnicianName"].ToString(), item["TechnicianSurname"].ToString(), item["TechnicianSpecialty"].ToString(), int.Parse(item["TechnicianAvailable"].ToString()));
                techniciansFound.Add(technicianObj);
            }

            return techniciansFound;
        }

        public bool InsertTechnicians(Technician technicianObjParam)
        {
            bool isSuccess = false;
            DataHandler handler = new DataHandler();
            string queryLastID = "SELECT TOP 1 TechnicianID FROM tblTechnicians ORDER BY TechnicianID DESC";
            int lastTechnicianID = 0;
            DataTable dataRaw = handler.Select(queryLastID);

            List<Technician> productObj = new List<Technician>();

            foreach (DataRow item in dataRaw.Rows)
            {
                lastTechnicianID = (int)item["TechnicianID"];
            }
            int latestTechnicianID = lastTechnicianID + 1;  //Get last ID number and increment by one for new record, get ID of last record

            string query = @"INSERT INTO [dbo].[tblTechnicians]  ([TechnicianID],[TechnicianName],[TechnicianSurname],[TechnicianSpecialty],[TechnicianAvailable]) VALUES  (" + latestTechnicianID + ",'" + technicianObjParam.TechnicianName + "' ,'" + technicianObjParam.TechnicianSurname +"','" + technicianObjParam.TechnicianSpeciality + "', 1" +")"; // New Technicians are made available initialy

            if (handler.Insert(query))
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }

            return isSuccess;

        }

        public bool UpdateTechniceans(Technician techniciansToUpdateParam)
        {
            bool isSuccess = false;

            List<Technician> techniciansList = new List<Technician>();
            DataHandler handler = new DataHandler();

            string query = "UPDATE [tblTechnicians] " +
                            "SET [TechnicianName] ='" + techniciansToUpdateParam.TechnicianName + "'" +
                            ",[TechnicianSurname] ='" + techniciansToUpdateParam.TechnicianSurname + "'" +
                            ",[TechnicianSpecialty] ='" + techniciansToUpdateParam.TechnicianSpeciality + "'" +

                            "WHERE [TechnicianID] ='" + techniciansToUpdateParam.TechnicianID + "'";

            if (handler.UpdateData(query))
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }

            return isSuccess;
            // handler.UpdateData(query);   // use unery operrators to check whether data changes were successful or not and display message         
        }

        public bool DeleteTechnician(Technician technicianObjParam)
        {
            string query = "DELETE FROM [tblTechnicians] where [TechnicianID]='" + technicianObjParam.TechnicianID + "'";
            DataHandler handler = new DataHandler();
            return handler.Delete(query);
        }
# endregion


        public List<Orders> GetClientOrders(int OrdersClientIDParam)
        {
            List<Orders> ordersFound = new List<Orders>();
            string query = "SELECT * FROM [tblOrders]   WHERE ClientID =" + OrdersClientIDParam + ";";

            DataHandler handler = new DataHandler();
            DataTable dataRaw = handler.Select(query);

            foreach (DataRow item in dataRaw.Rows)
            {
                Orders ordersObj = new Orders(int.Parse(item["OrderID"].ToString()), int.Parse(item["ClientID"].ToString()), int.Parse(item["ProductID"].ToString()), int.Parse(item["OrderUnitsCopy"].ToString()), float.Parse(item["Cost"].ToString()), int.Parse(item["DiscountQualification"].ToString()));
       
                ordersFound.Add(ordersObj);
            }

            return ordersFound;
        }

        public List<Clients> GetClientBalance(int clientIDObjParam)
        {
            string query = "SELECT ClientBalance FROM tblClients WHERE  ClientID = " + clientIDObjParam ;

            List<Clients> clientsFound = new List<Clients>();
            DataTable dataRaw =  Select(query);

            foreach (DataRow item in dataRaw.Rows)
            {
                Clients clientsObj = new Clients(float.Parse(item["ClientBalance"].ToString()));
                clientsFound.Add(clientsObj);
            }

            return clientsFound; 
        }


        public bool UpdateClientBalance(int clientIDParam, int productIDParam, float newBalanceParam)
        {
            bool isSuccess = false;

            List<Product> productList = new List<Product>();
            DataHandler handler = new DataHandler();

            string query = "UPDATE [tblClients] " +
                            "SET [ProductID] ='" + productIDParam + "'" +
                            ",[ClientBalance] ='" + newBalanceParam + "'" +
                            "WHERE [ClientID] ='" + clientIDParam + "'";

            if (handler.UpdateData(query))
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }

            return isSuccess;
            // handler.UpdateData(query);   // use unery operrators to check whether data changes were successful or not and display message         
        }

        public float GetClientAccBalance(int clientIDParam)
        {
            string query = "SELECT ClientBalance FROM tblClients WHERE  ClientID = " + clientIDParam;

            List<Clients> clientsFound = new List<Clients>();
            DataTable dataRaw = Select(query);

            foreach (DataRow item in dataRaw.Rows)
            {
                Clients clientsObj = new Clients(float.Parse(item["ClientBalance"].ToString()));
                clientsFound.Add(clientsObj);
            }

            return clientsFound[0].ClientBalance ;
        }

        public void InsertToOrders(int clientID, int  productIDParam, float costParam, int discountParam, int orderUnitsCopyParam)
        {
            DataHandler handler = new DataHandler();

            string queryLastID = "SELECT TOP 1 OrderID FROM tblOrders ORDER BY OrderID DESC";
            int lastOrderID = 0;

            DataTable dataRaw = handler.Select(queryLastID);

            List<Orders> OrdersObj = new List<Orders>();

            foreach (DataRow item in dataRaw.Rows)
            {
                lastOrderID = (int)item["OrderID"];
            }

            int latestProductID = lastOrderID + 1;  //Get last ID number and increment by one for new record, get ID of last record

            //OrderID,latestProductID
            string query = "INSERT INTO tblOrders (OrderID,ProductID,ClientID,Cost,DiscountQualification,OrderUnitsCopy) VALUES (" + latestProductID + "," + productIDParam + "," + clientID + "," + costParam + "," + discountParam + ","+ orderUnitsCopyParam + ")";
            handler.Insert(query);
        }

        //Technician Stored Procedure Log in specific account 

        public List<Technician> GetTechnicianLogin(int userID,string userPassword)
        {
            string query = "EXEC dbo.uspGetTechnicianLogIn  @ID =" + userID + ",@Password ='" + userPassword+ "'";
            List<Technician> technicianFound = new List<Technician>();
            DataHandler handler = new DataHandler();
            
            DataTable dataRaw = handler.Select(query);
            foreach (DataRow item in dataRaw.Rows)
            {
                Technician technicianObj = new Technician(int.Parse(item["TechnicianID"].ToString()), item["TechnicianPassword"].ToString());
                technicianFound.Add(technicianObj);
            }

            return technicianFound;
        }

        //Log Client Calls 

        public void InsertToCallCenterLogs(int clientIDParam,string callDepartmentParam, string callDurationParam, string callDateParam)
        {
            DataHandler handler = new DataHandler();

            string queryLastID = "SELECT TOP 1 CallLogID FROM tblCallCenter ORDER BY CallLogID DESC";
            int lastCallID = 0;

            DataTable dataRaw = handler.Select(queryLastID);

            List<CallCenter> callCenterObj = new List<CallCenter>();

            foreach (DataRow item in dataRaw.Rows)
            {
                lastCallID = (int)item["CallLogID"];
            }

            int latestCallID = lastCallID + 1;  //Get last ID number and increment by one for new record, get ID of last record

            
            string query = @"INSERT INTO [tblCallCenter] ([CallLogID],[ClientID],[Department],[CallDate],[CallDuration]) VALUES (" + latestCallID + "," + clientIDParam + ",'" + callDepartmentParam + "','" + callDateParam + "','" + callDurationParam + "')";
            handler.Insert(query);
        }

        public List<CallCenter> GetClientCallHistory(int ClientIDParam)
        {
            List<CallCenter> callLogsFound = new List<CallCenter>();
            string query = "SELECT * FROM [tblCallCenter]   WHERE ClientID =" + ClientIDParam + ";";

            DataHandler handler = new DataHandler();
            DataTable dataRaw = handler.Select(query);

            foreach (DataRow item in dataRaw.Rows)
            {
                CallCenter callsObj = new CallCenter(int.Parse(item["CallLogID"].ToString()), int.Parse(item["ClientID"].ToString()), item["CallDuration"].ToString(), item["Department"].ToString(), item["CallDate"].ToString());
                callLogsFound.Add(callsObj);
            }

            return callLogsFound;
            
        }

    }
    }

