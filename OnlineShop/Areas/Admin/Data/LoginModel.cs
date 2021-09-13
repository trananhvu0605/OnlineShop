using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Data
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Mời nhập username")]
        public string UserName { set; get; }
        [Required(ErrorMessage = "Mời nhập PassWord")]
        public string PassWord { set; get; }
        public bool RememberMe { set; get; }
    }
}