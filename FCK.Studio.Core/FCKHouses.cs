using FCK.Common;
using FCK.Studio.Entity;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FCK.Studio.Core
{
    public class FCKHouses : FCKBase
    {
        public PageDatas<HouseDto> GetPageList(int page, int pageSize, int grade = 0, string keywords = "", string itype = "", int maxP = 0, int minP = 0, string orderindex = "grade_desc")
        {
            PageDatas<HouseDto> result = new PageDatas<HouseDto>();
            var lists = dbr.FCK_Houses.ToList();
            if (grade > 0)
            {
                lists = lists.Where(o => o.House_Grade <= grade).ToList();
            }
            if (!string.IsNullOrEmpty(itype))
            {
                lists = lists.Where(o => o.House_Type == itype).ToList();
            }
            if (minP > 0 || maxP > 0)
            {
                if (maxP > minP)
                    lists = lists.Where(o => o.House_Price >= minP && o.House_Price <= maxP).ToList();
                else if (minP > 0 && maxP == 0)
                    lists = lists.Where(o => o.House_Price >= minP).ToList();
                else if (maxP > 0 && minP == 0)
                    lists = lists.Where(o => o.House_Price <= maxP).ToList();
            }
            if (!string.IsNullOrEmpty(keywords))
            {
                lists = lists.Where(o => o.House_Title.Contains(keywords)).ToList();
            }
            switch (orderindex)
            {
                case "price":
                    lists = lists.OrderBy(o => o.House_Price).ToList();
                    break;
                case "price_desc":
                    lists = lists.OrderByDescending(o => o.House_Price).ToList();
                    break;
                case "grade":
                    lists = lists.OrderBy(o => o.House_Grade).ToList();
                    break;
                default:
                    lists = lists.OrderByDescending(o => o.House_Grade).ToList();
                    break;
            }

            int total = lists.Count;
            int pages = 0;
            if (pageSize > 0)
            {
                pages = (total + pageSize - 1) / pageSize;
                int startIndex = pageSize * (page - 1);
                lists = lists.Skip(startIndex).Take(pageSize).ToList();
            }

            result.datas = Utility.MapTo<List<HouseDto>>(lists);
            result.pages = pages;
            result.total = total;

            return result;
        }

        public ErrorMsg CreateOrUpdate(HouseDto model)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var obj = dbr.FCK_Houses.Where(o => o.ID == model.ID).FirstOrDefault();
                if (obj != null)
                {
                    obj = Common.Utility.MapTo<FCK_Houses>(model);
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.message = "UPDATE_SUCCESS";
                }
                else
                {
                    model.House_UpdateTimes = DateTime.Now;
                    model.House_Code = Utility.CreateRandNum("");
                    FCK_Houses item = Utility.MapTo<FCK_Houses>(model);
                    db.FCK_Houses.Add(item);
                    db.SaveChanges();
                    result.code = 100;
                    result.id = item.ID;
                    result.message = "ADD_SUCCESS";
                }

                #region 插入房源数据成功后给房源添加照片
                try
                {
                    List<HouseImageDto> images = Utility.MapTo<List<HouseImageDto>>(model.House_Image);
                    if (images != null)
                    {
                        AddImage(images, model.House_Code);
                    }
                }
                catch (Exception err)
                {
                    result.code = 100;
                    result.message += "HOUSE_IMAGE_ADD_ERROR: " + err.Message;
                }
                #endregion

            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }

        public ErrorMsg UpdateStatus(int id, int status)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var item = dbr.FCK_Houses.Where(o => o.ID == id).FirstOrDefault();
                if (item != null)
                {
                    item.House_Status = status;
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

        public ReturnData<HouseDto> GetModel(int id)
        {
            ReturnData<HouseDto> result = new ReturnData<HouseDto>();
            try
            {
                var item = dbr.FCK_Houses.Where(o => o.ID == id).FirstOrDefault();
                if (item != null)
                {
                    result.code = 100;
                    result.message = "SUCCESS";
                    result.datas = Utility.MapTo<HouseDto>(item);
                }
                else
                    result.code = 101;
            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }

        public ReturnData<HouseDto> GetModelByCode(string code)
        {
            ReturnData<HouseDto> result = new ReturnData<HouseDto>();
            try
            {
                var item = dbr.FCK_Houses.Where(o => o.House_Code == code).FirstOrDefault();
                if (item != null)
                {
                    result.code = 100;
                    result.message = "SUCCESS";
                    result.datas = Utility.MapTo<HouseDto>(item);
                }
                else
                    result.code = 101;
            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }

        public ErrorMsg AddImage(List<HouseImageDto> models, string code)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var lists = dbr.FCK_HouseImages.Where(o => o.House_Code == code).ToList();
                db.Entry(lists).State = EntityState.Deleted;
                db.SaveChanges();

                List<FCK_HouseImages> items = Utility.MapTo<List<FCK_HouseImages>>(models);
                foreach (var item in items)
                {
                    item.House_Code = code;
                    db.FCK_HouseImages.Add(item);
                    db.SaveChanges();
                }

                #region 更新房源表中的图片地址
                var house = dbr.FCK_Houses.Where(o => o.House_Code == code).FirstOrDefault();
                if (house != null)
                {
                    house.House_Image = items.FirstOrDefault().Url;
                    db.Entry(house).State = EntityState.Modified;
                    db.SaveChanges();
                }
                #endregion

                result.code = 100;
                result.message = "ADD_SUCCESS";
            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }

        public ReturnData<List<HouseImageDto>> GetImageByCode(string code)
        {
            ReturnData<List<HouseImageDto>> result = new ReturnData<List<HouseImageDto>>();
            try
            {
                var item = dbr.FCK_HouseImages.Where(o => o.House_Code == code).ToList();
                if (item != null)
                {
                    result.code = 100;
                    result.message = "SUCCESS";
                    result.datas = Utility.MapTo<List<HouseImageDto>>(item);
                }
                else
                    result.code = 101;
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
