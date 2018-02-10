using System;
using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class V_Sales
    {
        [Key]
        public int Order_ID { set; get; }
        public string Order_Number { set; get; }
        public decimal Order_Amount { set; get; }
        public int Order_Status { set; get; }
        public string Product_Name { set; get; }
        public string Category_Name { set; get; }
        public int Register_ID { set; get; }
        public int Member_ID { set; get; }
        public string Product_Pic { set; get; }
        public DateTime Order_Time { set; get; }
        public string Order_Payway { set; get; }
        public string Member_Name { set; get; }
        public string Member_Mobile { set; get; }
        public decimal Product_Price { set; get; }
    }
}
