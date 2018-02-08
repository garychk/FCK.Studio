using FCK.Common;
using FCK.Studio.Entity;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FCK.Studio.Core
{
    public class FCKProducts : FCKBase
    {
        /// <summary>
        /// 获取产品服务数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="keywords"></param>
        /// <param name="pricerange"></param>
        /// <param name="orderindex"></param>
        /// <returns></returns>
        public PageDatas<ProductDto> GetPageList(int page, int pageSize, int cateid = 0, string keywords = "", string pricerange = "", string orderindex = "pricedesc", int isrec = 0, int regid = -1, int isonsale = -1)
        {
            PageDatas<ProductDto> result = new PageDatas<ProductDto>();
            RegisterID = regid > 0 ? regid : RegisterID;
            var lists = dbr.FCK_Products.ToList();
            if (isonsale >= 0)
            {
                lists = lists.Where(o => o.IsOnSale == isonsale).ToList();
            }
            if (RegisterID > 0)
            {
                lists = lists.Where(o => o.Register_ID == RegisterID).ToList();
            }
            if (cateid > 0)
            {
                lists = lists.Where(o => o.Product_CateID == cateid).ToList();
            }
            if (!string.IsNullOrEmpty(keywords))
            {
                lists = lists.Where(o => o.Product_Name.Contains(keywords)).ToList();
            }
            if (!string.IsNullOrEmpty(Utility.GetSessionValue("RegisterId")))
            {
                int RegisterId = Utility.cInt(Utility.GetSessionValue("RegisterId"));
                lists = lists.Where(o => o.Register_ID == RegisterId).ToList();
            }
            if (!string.IsNullOrEmpty(pricerange))
            {
                string[] priceArr = pricerange.Split('_');
                if (priceArr.Length == 2)
                {
                    int minP = int.Parse(priceArr[0]);
                    int maxP = int.Parse(priceArr[1]);
                    lists = lists.Where(o => o.Product_Price >= minP && o.Product_Price <= maxP).ToList();
                }
            }
            if (isrec > 0)
            {
                lists = lists.Where(o => o.IsRec == isrec).ToList();
            }

            if (orderindex == "price")
                lists = lists.OrderBy(o => o.Product_Price).ToList();
            else if (orderindex == "pricedesc")
                lists = lists.OrderByDescending(o => o.Product_Price).ToList();
            else if (orderindex == "order")
                lists = lists.OrderBy(o => o.Product_Order).ToList();
            else if (orderindex == "orderdesc")
                lists = lists.OrderByDescending(o => o.Product_Order).ToList();
            else
                lists = lists.OrderByDescending(o => o.Product_ID).ToList();

            int total = lists.Count;
            int pages = 0;
            if (pageSize > 0)
            {
                pages = (total + pageSize - 1) / pageSize;
                int startIndex = pageSize * (page - 1);
                lists = lists.Skip(startIndex).Take(pageSize).ToList();
            }
            List<ProductDto> items = new List<ProductDto>();


            result.datas = Utility.MapTo<List<ProductDto>>(lists);
            result.pages = pages;
            result.total = total;

            return result;
        }

        public ProductDto GetPrice(string name, string grade)
        {
            ProductDto result = new ProductDto();
            try
            {
                var item = dbr.FCK_Products.Where(o => o.Product_Name == name && o.Product_Grade == grade).FirstOrDefault();
                if (item != null)
                {
                    result = GetModel(item.Product_ID);
                }
            }
            catch { }
            return result;
        }

        public ErrorMsg CreateOrUpdate(ProductDto model)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var obj = dbr.FCK_Products.Where(o => o.Product_ID == model.Product_ID).FirstOrDefault();
                if (obj != null)
                {
                    obj = Utility.MapTo<FCK_Products>(model);
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.id = obj.Product_ID;
                    result.message = "SUCCESS";
                }
                else
                {
                    FCK_Products nobj = new FCK_Products();
                    var item = dbr.FCK_Products.Where(o => o.Product_Name == model.Product_Name && o.Product_Grade == model.Product_Grade).FirstOrDefault();
                    if (item == null)
                    {
                        nobj = Utility.MapTo<FCK_Products>(model);
                        db.FCK_Products.Add(nobj);
                        db.SaveChanges();
                        result.code = 100;
                        result.id = nobj.Product_ID;
                        result.message = "SUCCESS";
                    }
                    else
                    {
                        result.code = 101;
                        result.message = "EXIST";
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

        public ProductDto GetModel(int ID)
        {
            FCK_Products item = dbr.FCK_Products.Where(o => o.Product_ID == ID).FirstOrDefault();
            ProductDto model = new ProductDto();
            if (item != null)
            {
                model = Utility.MapTo<ProductDto>(item);
                FCKCategory fckcate = new FCKCategory();
                model.Product_CateName = fckcate.GetCateName(item.Product_CateID);
            }
            return model;
        }

        public ErrorMsg Del(int ID)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var model = dbr.FCK_Products.Where(o => o.Product_ID == ID).FirstOrDefault();
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
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }

        public ErrorMsg Recommend(string ids)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                string[] idArr = ids.Split(',');
                foreach (string id in idArr)
                {
                    int ID = int.Parse(id);
                    var model = dbr.FCK_Products.Where(o => o.Product_ID == ID).FirstOrDefault();
                    if (model != null)
                    {
                        model.IsRec = model.IsRec == 0 ? 1 : 0;
                        db.Entry(model).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
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

        public ErrorMsg OnSale(string ids, int status = 0)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                string[] idArr = ids.Split(',');
                foreach (string id in idArr)
                {
                    int ID = int.Parse(id);
                    var model = dbr.FCK_Products.Where(o => o.Product_ID == ID).FirstOrDefault();
                    if (model != null)
                    {
                        model.IsOnSale = status;
                        db.Entry(model).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
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

        public ErrorMsg UpdateSales(int id, int sales)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var model = dbr.FCK_Products.Where(o => o.Product_ID == id).FirstOrDefault();
                if (model != null)
                {
                    model.Product_Sales += sales;
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                }
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
        /// 获取分类
        /// </summary>
        /// <param name="cates"></param>
        /// <returns></returns>
        public List<string> GetCates(string cates, string sname = "")
        {
            List<string> lists = new List<string>();
            PageDatas<ProductDto> items = GetPageList(1, 0, 0, sname);
            if (items.total > 0)
            {
                if (cates.ToLower() == "color")
                {
                    foreach (var item in items.datas)
                    {
                        if (!lists.Contains(item.Product_Color))
                        {
                            lists.Add(item.Product_Color);
                        }
                    }
                }
                else if (cates.ToLower() == "grade")
                {
                    foreach (var item in items.datas)
                    {
                        if (!lists.Contains(item.Product_Grade))
                        {
                            lists.Add(item.Product_Grade);
                        }
                    }
                }
                else if (cates.ToLower() == "name")
                {
                    foreach (var item in items.datas)
                    {
                        if (!lists.Contains(item.Product_Name))
                        {
                            lists.Add(item.Product_Name);
                        }
                    }
                }
            }
            return lists;
        }
    }
}
