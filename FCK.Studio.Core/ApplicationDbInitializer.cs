using FCK.Common;
using FCK.Studio.Dal;
using FCK.Studio.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace FCK.Studio.Core
{
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<DataBaseContent>
    {
        protected override void Seed(DataBaseContent context)
        {
            base.Seed(context);
            var roles = new List<FCK_Admin>
            {
                new FCK_Admin{
                    Admin_Name="administrator",
                    Admin_Password=DES.Encrypt("admin", DES.sKey)
                },
                new FCK_Admin{
                    Admin_Name="admin",
                    Admin_Password=DES.Encrypt("admin", DES.sKey)
                }
            };
            roles.ForEach(l => context.FCK_Admin.Add(l));
            context.SaveChanges();
        }
    }
}
