using FCK.Common;
using FCK.Studio.Entity;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FCK.Studio.Core
{
    public class FCKMembers : FCKBase
    {
        /// <summary>
        /// 获取会员数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public PageDatas<MemberData> GetPageList(int page, int pageSize, string keywords = "")
        {
            PageDatas<MemberData> result = new PageDatas<MemberData>();
            var lists = dbr.FCK_Members.Where(o => o.Register_ID == RegisterID).ToList();
            if (!string.IsNullOrEmpty(keywords))
            {
                lists = lists.Where(o => o.Member_Name.Contains(keywords) || o.Member_Mobile.Contains(keywords) || o.Member_UserName.Contains(keywords)).ToList();
            }
            lists = lists.OrderByDescending(o => o.Member_ID).ToList();

            int total = lists.Count;
            int pages = 0;
            if (pageSize > 0)
            {
                pages = (total + pageSize - 1) / pageSize;
                int startIndex = pageSize * (page - 1);
                lists = lists.Skip(startIndex).Take(pageSize).ToList();
            }
            List<MemberData> items = new List<MemberData>();
            foreach (var item in lists)
            {
                MemberData obj = new MemberData();
                obj.Member_Header = item.Member_Header;
                obj.Member_ID = item.Member_ID;
                obj.Member_Mobile = item.Member_Mobile;
                obj.Member_Name = item.Member_Name;
                obj.Member_NickName = item.Member_NickName;
                obj.Member_UserName = item.Member_UserName;
                obj.Member_Status = item.Member_Status;
                obj.Member_Amount = item.Member_Amount.ToString("0.00");
                var group = dbr.FCK_Groups.Where(o => o.Group_ID == item.Group_ID).FirstOrDefault();
                if (group != null)
                {
                    obj.Group_Name = group.Group_Name;
                    obj.Group_Discount = group.Group_Discount.ToString("0.00");
                }
                items.Add(obj);
            }

            result.datas = items;
            result.pages = pages;
            result.total = total;

            return result;
        }

        public MemberDto GetModel(int ID)
        {
            MemberDto model = new MemberDto();
            FCK_Members item = dbr.FCK_Members.Where(o => o.Member_ID == ID).FirstOrDefault();
            if (item != null)
            {
                model = Utility.MapTo<MemberDto>(item);
                var group = GetModelGroup(item.Group_ID);
                if (group != null)
                {
                    model.Group_Discount = group.Group_Discount;
                }
            }
            return model;
        }

        public ReturnData<MemberDto> GetModel(string username, string password)
        {
            ReturnData<MemberDto> result = new ReturnData<MemberDto>();
            try
            {
                MemberDto model = new MemberDto();
                FCK_Members item = dbr.FCK_Members.Where(o => o.Member_UserName == username && o.Member_Password == password).FirstOrDefault();
                if (item != null)
                {
                    model = Utility.MapTo<MemberDto>(item);
                    var group = GetModelGroup(item.Group_ID);
                    if (group != null)
                    {
                        model.Group_Discount = group.Group_Discount;
                    }
                    result.code = 100;
                    result.datas = model;
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
        /// 添加或编辑会员
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ErrorMsg CreateOrUpdate(MemberDto model)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var obj = dbr.FCK_Members.Where(o => o.Member_ID == model.Member_ID).FirstOrDefault();
                if (obj != null)
                {
                    obj = Utility.MapTo<FCK_Members>(model);
                    obj.Register_ID = RegisterID;
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.id = obj.Member_ID;
                    result.message = "SUCCESS";
                }
                else
                {
                    var item = dbr.FCK_Members.Where(o => o.Member_UserName == model.Member_UserName || o.Member_Mobile == model.Member_Mobile).FirstOrDefault();
                    if (item == null)
                    {
                        string header =  model.Member_Header;
                        if (string.IsNullOrEmpty(header))
                        {
                            Random rnd = new Random();
                            int num = rnd.Next(1, 12);
                            header = "/Content/header/head" + num + ".jpg";
                        }
                        FCK_Members nobj = new FCK_Members();
                        nobj = Utility.MapTo<FCK_Members>(model);
                        nobj.Member_JoinTime = DateTime.Now;
                        nobj.Member_Password = DES.Encrypt(model.Member_Password, DES.sKey);
                        
                        db.FCK_Members.Add(nobj);
                        db.SaveChanges();
                        result.code = 100;
                        result.id = nobj.Member_ID;
                        result.message = "SUCCESS";
                    }
                    else
                    {
                        result.code = 101;
                        result.message = "USERNAME_OR_MOBILE_IS_EXIST";
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
        /// 更新会员状态
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ErrorMsg UpdateStatus(int memberid, int status)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var item = dbr.FCK_Members.Where(o => o.Member_ID == memberid).FirstOrDefault();
                if (item != null)
                {
                    item.Member_Status = status;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.message = "UPDATE_SUCCESS";
                }
                else
                {
                    result.code = 101;
                    result.message = "OBJECT_NOT_EXIST";
                }
            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }

        public ErrorMsg UpdateMoney(int memberid, int money)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var item = dbr.FCK_Members.Where(o => o.Member_ID == memberid).FirstOrDefault();
                if (item != null)
                {
                    item.Member_Amount += money;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.id = item.Member_ID;
                    result.message = "UPDATE_SUCCESS";
                }
                else
                {
                    result.code = 101;
                    result.message = "OBJECT_NOT_EXIST";
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
        /// 更新会员积分
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        public ErrorMsg UpdatePoints(int memberid, int points)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var item = dbr.FCK_Members.Where(o => o.Member_ID == memberid).FirstOrDefault();
                if (item != null)
                {
                    item.Member_Points += points;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.id = item.Member_ID;
                    result.message = "UPDATE_SUCCESS";
                }
                else
                {
                    result.code = 101;
                    result.message = "OBJECT_NOT_EXIST";
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
        /// 修改用户API权限
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="powers"></param>
        /// <returns></returns>
        public ErrorMsg UpdatePower(int memberid, string powers)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var item = dbr.FCK_Members.Where(o => o.Member_ID == memberid).FirstOrDefault();
                if (item != null)
                {
                    item.Member_ApiPower = powers;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.id = item.Member_ID;
                    result.message = "UPDATE_SUCCESS";
                }
                else
                {
                    result.code = 101;
                    result.message = "OBJECT_NOT_EXIST";
                }
            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }


        public ErrorMsg RechargeAfter(int memberid, int money)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                FCKFinance finance = new FCKFinance();
                EditFinance item = new EditFinance();
                item.Finance_Amount = money.ToString();
                item.Finance_Type = enumFinanceType.comein.ToString();
                item.Member_ID = memberid;
                item.Finance_Memo = "充值";
                finance.AddFinance(item);
                result.code = 100;
                result.message = "SUCCESS";
            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }
        /// <summary>
        /// 删除会员
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public ErrorMsg Del(int memberid)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var model = dbr.FCK_Members.Where(o => o.Member_ID == memberid).FirstOrDefault();
                if (model != null)
                {
                    db.Entry(model).State = EntityState.Deleted;
                    db.SaveChanges();
                    result.code = 100;
                    result.message = "SUCCESS";
                }
                else
                {
                    result.code = 101;
                    result.message = "MEMBER_NO_EXIST";
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
        /// 重置密码
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public ErrorMsg ResetPassword(int memberid, string newPassword)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var item = dbr.FCK_Members.Where(o => o.Member_ID == memberid).FirstOrDefault();
                if (item != null)
                {
                    if (item.Member_Password == DES.Encrypt(newPassword, DES.sKey))
                    {
                        result.code = 102;
                        result.message = "THE_SAMEAS_OLD_PASSWORD";
                    }
                    else
                    {
                        item.Member_Password = DES.Encrypt(newPassword, DES.sKey);
                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();
                        result.code = 100;
                        result.message = "UPDATE_SUCCESS";
                    }
                }
                else
                {
                    result.code = 101;
                    result.message = "OBJECT_NOT_EXIST";
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
        /// 添加或编辑分组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ErrorMsg EditGroup(GroupData model)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var obj = dbr.FCK_Groups.Where(o => o.Group_ID == model.Group_ID).FirstOrDefault();
                if (obj != null)
                {
                    obj = Utility.MapTo<FCK_Groups>(model);
                    obj.Register_ID = RegisterID;
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.id = obj.Group_ID;
                    result.message = "SUCCESS";
                }
                else
                {
                    var item = dbr.FCK_Groups.Where(o => o.Group_Name == model.Group_Name).FirstOrDefault();
                    if (item == null)
                    {
                        FCK_Groups nobj = new FCK_Groups();
                        nobj = Utility.MapTo<FCK_Groups>(model);
                        nobj.Register_ID = RegisterID;
                        db.FCK_Groups.Add(nobj);
                        db.SaveChanges();
                        result.code = 100;
                        result.id = nobj.Group_ID;
                        result.message = "SUCCESS";
                    }
                    else
                    {
                        result.code = 101;
                        result.message = "GROUP_IS_EXIST";
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
        /// 删除分组
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public ErrorMsg DelGroup(int groupid)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var model = dbr.FCK_Groups.Where(o => o.Group_ID == groupid).FirstOrDefault();
                if (model != null)
                {
                    db.Entry(model).State = EntityState.Deleted;
                    db.SaveChanges();
                    result.code = 100;
                    result.message = "SUCCESS";
                }
                else
                {
                    result.code = 101;
                    result.message = "GROUP_NO_EXIST";
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
        /// 获取会员分组数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="groupname"></param>
        /// <returns></returns>
        public GroupJson GetGroups(int page, int pageSize, string groupname = "")
        {
            GroupJson result = new GroupJson();
            var lists = dbr.FCK_Groups.Where(o => o.Register_ID == RegisterID).ToList();
            if (!string.IsNullOrEmpty(groupname))
            {
                lists = lists.Where(o => o.Group_Name.Contains(groupname)).ToList();
            }
            int total = lists.Count;
            int pages = 0;
            if (pageSize > 0)
            {
                pages = (total + pageSize - 1) / pageSize;
                int startIndex = pageSize * (page - 1);
                lists = lists.Skip(startIndex).Take(pageSize).ToList();
            }
            List<GroupData> items = new List<GroupData>();
            foreach (var item in lists)
            {
                GroupData obj = new GroupData();
                obj.Group_Discount = item.Group_Discount;
                obj.Group_ID = item.Group_ID;
                obj.Group_Name = item.Group_Name;
                obj.Group_Points = item.Group_Points;
                items.Add(obj);
            }

            result.datas = items;
            result.pages = pages;
            result.total = total;

            return result;
        }

        public GroupData GetModelGroup(int groupid)
        {
            GroupData model = new GroupData();
            FCK_Groups item = dbr.FCK_Groups.Where(o => o.Group_ID == groupid).FirstOrDefault();
            if (item != null)
            {
                model.Group_Discount = item.Group_Discount;
                model.Group_ID = item.Group_ID;
                model.Group_Name = item.Group_Name;
                model.Group_Points = item.Group_Points;
            }
            return model;
        }

        public ErrorMsg Login(LoginModel model)
        {
            ErrorMsg result = new ErrorMsg();
            try {
                var member = dbr.FCK_Members.Where(o=>o.Member_Email==model.UserName || o.Member_Mobile==model.UserName || o.Member_UserName==model.UserName).FirstOrDefault();
                if (member != null)
                {
                    if (member.Member_Password == DES.Encrypt(model.Password, DES.sKey))
                    {
                        result.code = 100;
                        result.id = member.Member_ID;
                        result.message = member.Member_UserName;
                    }
                    else
                    {
                        result.code = 102;
                        result.message = "PASSWORD ERROR";
                    }
                }
                else
                {
                    result.code = 101;
                    result.message = "NO EXIST";
                }
            }
            catch(Exception e)
            {
                result.code = 102;
                result.message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="headerurl"></param>
        /// <returns></returns>
        public ErrorMsg UpdateHeader(int memberid, string headerurl)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var item = dbr.FCK_Members.Where(o => o.Member_ID == memberid).FirstOrDefault();
                if (item != null)
                {
                    item.Member_Header = headerurl;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.message = headerurl;
                }
                else
                {
                    result.code = 101;
                    result.message = "OBJECT_NOT_EXIST";
                }
            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }
    }
}
