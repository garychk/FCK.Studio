using FCK.Common;
using FCK.Studio.Entity;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FCK.Studio.Core
{
    public class FCKAdmin : FCKBase
    {
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ErrorMsg Login(LoginModel model)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var admin = dbr.FCK_Admin.Where(o => o.Admin_Name == model.UserName).FirstOrDefault();
                if (admin != null)
                {
                    if (admin.Admin_Password == DES.Encrypt(model.Password, DES.sKey))
                    {
                        if (admin.Admin_Status == 0)
                        {
                            result.code = 100;
                            result.id = admin.Admin_ID;
                            result.message = admin.Admin_Name;
                            CookieHelper.setCookie("AdminID", admin.Admin_ID.ToString(), 1);
                            CookieHelper.setCookie("AdminName", admin.Admin_Name, 1);
                            CookieHelper.setCookie("RegisterId", admin.Register_ID.ToString(), 1);
                            Utility.SetSession("RegisterId", admin.Register_ID);
                        }
                        else
                        {
                            result.code = 102;
                            result.id = admin.Admin_ID;
                            result.message = "USER_LOCKED";
                        }
                    }
                    else
                    {
                        result.code = 102;
                        result.message = "PASSWORD_EROR";
                    }
                }
                else
                {
                    result.code = 101;
                    result.message = "USER_NO_EXIST";
                }
            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }

        public ErrorMsg Logout()
        {
            ErrorMsg result = new ErrorMsg();
            CookieHelper.delCookie("AdminID", "");
            CookieHelper.delCookie("AdminName", "");
            result.code = 100;
            result.message = "USER_LOGOUT_SUCCESS";
            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="adminid"></param>
        /// <returns></returns>
        public ErrorMsg DelAdmin(int adminid)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var model = dbr.FCK_Admin.Where(o => o.Admin_ID == adminid).FirstOrDefault();
                if (model != null)
                {
                    db.Entry(model).State = EntityState.Deleted;
                    db.SaveChanges();
                    result.code = 100;
                    result.message = "OK";
                }
                else
                {
                    result.code = 102;
                    result.message = "USER_NO_EXIST";
                }
            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }

        /// <summary>
        /// 更新管理员密码
        /// </summary>
        /// <param name="adminid"></param>
        /// <param name="newpassword"></param>
        /// <returns></returns>
        public ErrorMsg UpdateAdminPsw(int adminid, string newpassword)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var admin = dbr.FCK_Admin.Where(o => o.Admin_ID == adminid).FirstOrDefault();
                if (admin != null)
                {
                    admin.Admin_Password = DES.Encrypt(newpassword, DES.sKey);
                    db.Entry(admin).State = EntityState.Modified;
                    db.SaveChanges();

                    result.code = 100;
                    result.id = admin.Admin_ID;
                    result.message = "OK";
                }
            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }

        /// <summary>
        /// 更新管理员状态
        /// </summary>
        /// <param name="adminid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ErrorMsg UpdateAdminStatus(int adminid, int status)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var admin = dbr.FCK_Admin.Where(o => o.Admin_ID == adminid).FirstOrDefault();

                admin.Admin_Status = status;
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();

                result.code = 100;
                result.id = admin.Admin_ID;
                result.message = "OK";
            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }

        /// <summary>
        /// 编辑管理员
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ErrorMsg EditAdmin(EditAdminM model)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var admin = dbr.FCK_Admin.Where(o => o.Admin_ID == model.Admin_ID).FirstOrDefault();
                if (admin != null)
                {
                    admin.Register_ID = model.Register_ID;
                    admin.Admin_Power = model.Admin_Power;
                    admin.Admin_TrueName = model.Admin_TrueName;
                    admin.Admin_Telphone = model.Admin_Telphone;
                    db.Entry(admin).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.id = admin.Admin_ID;
                    result.message = "SUCCESS";
                }
                else
                {
                    var item = dbr.FCK_Admin.Where(o => o.Admin_Name == model.Admin_Name).FirstOrDefault();
                    if (item == null)
                    {
                        FCK_Admin nadmin = new FCK_Admin();
                        Random rnd = new Random();
                        int icode = rnd.Next(10000, 999999);
                        nadmin.Admin_Code = icode.ToString();
                        nadmin.Admin_Name = model.Admin_Name;
                        nadmin.Admin_Password = DES.Encrypt(model.Admin_Password, DES.sKey);
                        nadmin.Admin_Power = model.Admin_Power;
                        nadmin.Admin_RegTime = DateTime.Now;
                        nadmin.Admin_Status = 0;
                        nadmin.Register_ID = model.Register_ID;
                        nadmin.Admin_TrueName = model.Admin_TrueName;
                        nadmin.Admin_Telphone = model.Admin_Telphone;
                        db.FCK_Admin.Add(nadmin);
                        db.SaveChanges();
                        result.code = 100;
                        result.id = nadmin.Admin_ID;
                        result.message = "SUCCESS";
                    }
                    else
                    {
                        result.code = 101;
                        result.message = "USERNAME_EXIST";
                    }                    
                }                
            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }

        /// <summary>
        /// 获取管理员数据列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="adminname"></param>
        /// <returns></returns>
        public AdminsJson GetAdmins(int page, int pageSize, string adminname = "")
        {
            AdminsJson result = new AdminsJson();
            var lists = dbr.FCK_Admin.ToList();
            if (!string.IsNullOrEmpty(adminname))
            {
                lists = lists.Where(o => o.Admin_Name.Contains(adminname)).ToList();
            }
            int total = lists.Count;
            int pages = 0;
            if (pageSize > 0)
            {
                pages = (total + pageSize - 1) / pageSize;
                int startIndex = pageSize * (page - 1);
                lists = lists.Skip(startIndex).Take(pageSize).ToList();
            }

            var items = Utility.MapTo<List<AdminData>>(lists);

            result.datas = items;
            result.pages = pages;
            result.total = total;

            return result;
        }

        /// <summary>
        /// 获取管理员详情
        /// </summary>
        /// <param name="adminid"></param>
        /// <returns></returns>
        public EditAdminM GetAdmin(int adminid)
        {
            EditAdminM result = new EditAdminM();
            var model = dbr.FCK_Admin.Where(o => o.Admin_ID == adminid).FirstOrDefault();
            if (model != null)
            {
                result = Utility.MapTo<EditAdminM>(model);
            }
            return result;
        }


    }
}
