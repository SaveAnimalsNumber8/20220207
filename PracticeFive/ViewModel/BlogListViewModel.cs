using PracticeFive.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracticeFive.ViewModel
{
    public class BlogListViewModel
    {
        public int BlogID { get; set; }
        [DisplayName("發文人")]
        public int BlogMemberID { get; set; }
        [DisplayName("種類")]
        public string BlogCategory { get; set; }
        [DisplayName("標題")]
        public string BlogTitle { get; set; }
        [DisplayName("內容")]
        public string BlogContent { get; set; }
        [DisplayName("張貼日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Created_At { get; set; }
        public bool likes { get; set; }
        public string keyword { get; set; }

        public virtual tMember tMember { get; set; }
    }
}