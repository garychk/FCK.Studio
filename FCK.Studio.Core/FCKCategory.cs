using FCK.Studio.Entity;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FCK.Studio.Core
{
    public class FCKCategory : FCKBase
    {
        public List<TreeNode> GetCateTree(int parentid = 0, string ctype = "")
        {
            List<TreeNode> childrens = new List<TreeNode>();
            var items = dbr.FCK_Category.Where(o => o.Category_ParentID == parentid && o.Register_ID == RegisterID).ToList();
            if (!string.IsNullOrEmpty(ctype))
            {
                items = items.Where(o => o.Category_Type == ctype).ToList();
            }
            foreach (var item in items)
            {
                TreeNode node = new TreeNode();
                node.id = item.Category_ID;
                node.text = item.Category_Name;
                var n = dbr.FCK_Category.Where(o => o.Category_ParentID == node.id && o.Register_ID == RegisterID).ToList();
                if (n.Count > 0)
                {
                    node.nodes = GetCateTree(node.id, ctype);
                }
                childrens.Add(node);
            }
            return childrens;
        }

        public List<CategoryDto> GetList(int parentid, string ctype = "", int rid = -1)
        {
            List<CategoryDto> lists = new List<CategoryDto>();
            if (rid >= 0)
            {
                RegisterID = rid;
            }
            var items = dbr.FCK_Category.Where(o => o.Register_ID == RegisterID).ToList();
            if (parentid >= 0)
            {
                items = items.Where(o => o.Category_ParentID == parentid).ToList();
            }
            if (!string.IsNullOrEmpty(ctype))
            {
                items = items.Where(o => o.Category_Type == ctype).ToList();
            }
            foreach (var item in items)
            {
                CategoryDto node = new CategoryDto();
                node.Category_Depth = item.Category_Depth;
                node.Category_ID = item.Category_ID;
                node.Category_Index = item.Category_Index;
                node.Category_Name = item.Category_Name;
                node.Category_OrderID = item.Category_OrderID;
                node.Category_ParentID = item.Category_ParentID;
                node.Category_Type = item.Category_Type;
                node.Children_Num = GetCateTree(item.Category_ID).Count;
                lists.Add(node);
            }
            return lists;
        }

        public ErrorMsg CreateOrUpdate(CategoryDto model)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var obj = dbr.FCK_Category.Where(o => o.Category_ID == model.Category_ID).FirstOrDefault();
                if (obj != null)
                {
                    obj = Common.Utility.MapTo<FCK_Category>(model);
                    obj.Register_ID = RegisterID;
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.id = obj.Category_ID;
                    result.message = "SUCCESS";
                }
                else
                {
                    FCK_Category nobj = new FCK_Category();
                    nobj = Common.Utility.MapTo<FCK_Category>(model);
                    nobj.Register_ID = RegisterID;
                    db.FCK_Category.Add(nobj);
                    db.SaveChanges();
                    result.code = 100;
                    result.id = nobj.Category_ID;
                    result.message = "SUCCESS";
                }

            }
            catch (Exception e)
            {
                result.code = 102;
                result.message = e.Message;
            }
            return result;
        }

        public ErrorMsg Del(int id)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var childrens = dbr.FCK_Category.Where(o => o.Category_ParentID == id).ToList();
                if (childrens.Count > 0)
                {
                    result.code = 102;
                    result.message = "Please delete childrens first";
                }
                else
                {
                    var model = GetModel(id);
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
                        result.message = "NOT_EXIST";
                    }
                }
            }
            catch (Exception e)
            {
                result.code = 102;
                result.message = e.Message;
            }
            return result;
        }

        public FCK_Category GetModel(int id)
        {
            var model = dbr.FCK_Category.Where(o => o.Category_ID == id).FirstOrDefault();
            return model;
        }

        public CategoryDto GetCate(int id)
        {
            CategoryDto result = new CategoryDto();
            var model = GetModel(id);
            if (model != null)
            {
                result.Category_Depth = model.Category_Depth;
                result.Category_ID = model.Category_ID;
                result.Category_Index = model.Category_Index;
                result.Category_Name = model.Category_Name;
                result.Category_OrderID = model.Category_OrderID;
                result.Category_ParentID = model.Category_ParentID;
                result.Category_Type = model.Category_Type;
            }
            return result;
        }
        /// <summary>
        /// 分类排序
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public ErrorMsg OrderCate(int id, int orderid)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var model = GetModel(id);
                model.Category_OrderID = orderid;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                result.code = 100;
                result.message = "SUCCESS";
            }
            catch (Exception e)
            {
                result.code = 102;
                result.message = e.Message;
            }
            return result;
        }
        /// <summary>
        /// 获取分类名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCateName(int id)
        {
            var obj = GetCate(id);
            return obj != null ? obj.Category_Name : "";
        }
        /// <summary>
        /// 获取分类ID
        /// </summary>
        /// <param name="catename"></param>
        /// <returns></returns>
        public int GetCateId(string catename)
        {
            int result = 0;
            var model = dbr.FCK_Category.Where(o => o.Category_Name == catename).FirstOrDefault();
            if (model != null)
                result = model.Category_ID;
            return result;
        }
    }
}
