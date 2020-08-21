using System.Collections.Generic;
using System.Data;
using System.Linq;
using Data_Access_Layer;
using Data_Access_Layer.DAL;

namespace Business_Logic_Layer.BLL
{
    public class Product
    {
        private int productID;

        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        private float productPrice;

        public float ProductPrice
        {
            get { return productPrice; }
            set { productPrice = value; }
        }

        private string productDescription;

        public string ProductDescription
        {
            get { return productDescription; }
            set { productDescription = value; }
        }
        private string productFeatures;

        public string ProductFeatures
        {
            get { return productFeatures; }
            set { productFeatures = value; }
        }
        private string productLicense;

        public string ProductLicense
        {
            get { return productLicense; }
            set { productLicense = value; }
        }


        //Overrides
/*
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        */
        public override string ToString()
        {
            //return base.ToString();

            return this.productName;    //+ " " +
            //this.productPrice + " " +
            //this.productDescription + " " +
            //this.productFeatures + " " +
            //this.productLicense;
        }

        //Constructor
        public Product()
        {

        }

        public Product(int productIDParam, string productNameParam, float productPriceParam, string productDescriptionParam, string productFeaturesParam, string productLicenseParam)
        {
            this.productID      = productIDParam;
            this.productName    = productNameParam;
            this.productPrice   = productPriceParam;
            this.productDescription = productDescriptionParam;
            this.productFeatures = productFeaturesParam;
            this.productLicense = productLicenseParam;
        }

        public Product(string productNameParam, float productPriceParam, string productDescriptionParam, string productFeaturesParam, string productLicenseParam)
        {

            this.productName = productNameParam;
            this.productPrice = productPriceParam;
            this.productDescription = productDescriptionParam;
            this.productFeatures = productFeaturesParam;
            this.productLicense = productLicenseParam;
        }

        //method

        //Select all data

        //Delete product

        public bool DeleteProduct(int ProductIDParam = 0)
        {
           DataHandler handler = new DataHandler();
           return handler.DeleteProduct(ProductIDParam); //add if statement to voidify execution success or not (true or false)
        }

        public List<Product> ReadProduct()
        {
            DataHandler handler = new DataHandler();
            List<Product> productsFound = new List<Product>();

            productsFound = handler.GetAllProducts();
            return productsFound;
        }

        //update 
        /*     public bool UpdateProduct(int productIDParam, string productNameParam = "", float productPriceParam = 0, string productDescriptionParam = "", string productFeaturesParam = "", string productLicenseParam = "") // productID changes
             {
                 DataHandler handler = new DataHandler();
                 handler.UpdateProducts();
             }
     */

        public bool UpdateProduct(Product productToDeleteParam) // productID changes
        {
            DataHandler handler = new DataHandler();
            return  handler.UpdateProducts(productToDeleteParam);
        }


        //insert


        public void InsertProduct(Product productObjParam)
        {

            DataHandler handler = new DataHandler();
            handler.InsertProduct(productObjParam);
        }

       
    }
}