using FCK.Studio.API.Filter;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System.Collections.Generic;
using System.Web.Http;

namespace FCK.Studio.API.Controllers
{
    [BasicAuthentication]
    public class OrderController : ApiController
    {
        FCKOrders core = new FCKOrders();

        public OrdersJson GetPageList(int page, int pageSize, string OrderNumber = "", int MemberID = 0, string stime = "", string etime = "", int status = -1, string OrderIndex = "")
        {
            return core.GetPageList(page, pageSize, OrderNumber, MemberID, stime, etime, status, OrderIndex);
        }

        public List<OrderDetailData> GetDetails(string OrderNumber)
        {
            return core.GetDetails(OrderNumber);
        }

        public ErrorMsg UpdateStatus(string OrderNumber, int status)
        {
            return core.UpdateStatus(OrderNumber, status);
        }
    }
}
