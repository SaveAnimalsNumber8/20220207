using PracticeFive.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracticeFive.ViewModel
{
    public class BlogDetailViewModel
    {
        [DisplayName("發文人")]
        public int BlogMemberID { get; set; }
        [DisplayName("發文人")]
        public string BlogMemberName { get; set; }
        [DisplayName("種類"), Required]
        public string BlogCategory { get; set; }
        [DisplayName("標題"), Required, StringLength(50)]
        public string BlogTitle { get; set; }
        [DisplayName("內容"), Required, StringLength(4000)]
        public string BlogContent { get; set; }
        [DisplayName("張貼日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Created_At { get; set; }
    }
}