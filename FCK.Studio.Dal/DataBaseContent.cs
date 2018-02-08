using FCK.Studio.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace FCK.Studio.Dal
{
    public class DataBaseContent : DbContext
    {
        public DataBaseContent()
            : base("DefaultConnection")
        {

        }

        public DbSet<FCK_Admin> FCK_Admin { get; set; }
        public DbSet<FCK_Code> FCK_Code { get; set; }
        public DbSet<FCK_Finance> FCK_Finance { get; set; }
        public DbSet<FCK_Groups> FCK_Groups { get; set; }
        public DbSet<FCK_Log> FCK_Log { get; set; }
        public DbSet<FCK_Members> FCK_Members { get; set; }
        public DbSet<FCK_OrderDetail> FCK_OrderDetail { get; set; }
        public DbSet<FCK_Orders> FCK_Orders { get; set; }
        public DbSet<FCK_Registers> FCK_Registers { get; set; }
        public DbSet<FCK_Products> FCK_Products { get; set; }
        public DbSet<FCK_Articles> FCK_Articles { get; set; }
        public DbSet<FCK_Category> FCK_Category { get; set; }
        public DbSet<FCK_Comments> FCK_Comments { get; set; }
        public DbSet<FCK_Collections> FCK_Collections { get; set; }
        public DbSet<V_FinanceChart> V_FinanceChart { get; set; }
        public DbSet<FCK_API> FCK_API { get; set; }
        public DbSet<FCK_APIPara> FCK_APIPara { get; set; }
        public DbSet<FCK_Extra> FCK_Extra { get; set; }
        public DbSet<FCK_Events> FCK_Events { get; set; }
        public DbSet<FCK_Houses> FCK_Houses { get; set; }
        public DbSet<FCK_HouseImages> FCK_HouseImages { get; set; }
        public DbSet<FCK_Customers> FCK_Customers { get; set; }
        public DbSet<FCK_Shares> FCK_Shares { get; set; }
        public DbSet<V_Sales> V_Sales { get; set; }
    }
}
