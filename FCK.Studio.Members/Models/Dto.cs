using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCK.Studio.Members.Models
{
    public class OrderPageDto
    {
        public List<OrderDetailEdit> Products { get; set; }
        public MemberDto Member { get; set; }
        public string OrderDetail { get; set; }
        public int Register_ID { get; set; }
        public string Share_Code { get; set; }
    }

    public class PayResultDto
    {
        public OrdersData Orders { get; set; }
        public List<OrderDetailData> OrdersDetails{ get; set; }
    }
}