using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCK.Studio.Web.Models
{
    public class ParaArticle
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int cateid { get; set; }
        public string catename { get; set; }
        public string keywords { get; set; }
        public string tag { get; set; }
        public string orderindex { get; set; }
        public int isrec { get; set; }
        public int memberid { get; set; }
        public int ispage { get; set; }
    }

    public class MailSendDto
    {
        public string inputEmail { get; set; }
        public string inputTel { get; set; }
        public string inputName { get; set; }
        public string inputContent { get; set; }
        public string inputTitle { get; set; }
        public string inputMailto { get; set; }
    }
}