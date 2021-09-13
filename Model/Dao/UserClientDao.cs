using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserClientDao
    {
        OnlineShopDbContext db = null;
        public UserClientDao()
        {
            db = new OnlineShopDbContext();
        }
        public string Insert(TaiKhoan entity)
        {
            db.TaiKhoans.Add(entity);
            db.SaveChanges();
            return entity.TenTK;
        }
        public int Login(string userName, string matKhau)
        {
            var result = db.TaiKhoans.SingleOrDefault(x => x.TenTK == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Khoa == true)
                {
                    return -1;
                }
                else
                {
                    if (result.Mat_Khau == matKhau)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
        }
        public TaiKhoan GetByUserName(string username)
        {
            return db.TaiKhoans.SingleOrDefault(x => x.TenTK == username);
        }
        public bool Update(TaiKhoan entity)
        {
            try
            {
                var user = db.TaiKhoans.SingleOrDefault(x => x.TenTK == entity.TenTK);
                user.HoTen = entity.HoTen;
                user.SDT = entity.SDT;
                user.Dia_Chi = entity.Dia_Chi;
                user.Email = entity.Email;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
