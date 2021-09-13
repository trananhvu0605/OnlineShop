using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời nhập tên tài khoản")]
        public string userName { get; set; }
        public string Name { get; set; }
        public string PhoneNumbers { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Mời nhập password")]
        public string Password { get; set; }
    }
}