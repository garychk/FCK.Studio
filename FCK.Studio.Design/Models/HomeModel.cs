using FCK.Studio.Core;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCK.Studio.Design.Models
{
    public class HomeModel
    {
        public List<ArticleDto> Item_Banner { get; set; }
        public List<ArticleDto> Item_Service { get; set; }
        public List<ArticleDto> Item_Partner { get; set; }
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