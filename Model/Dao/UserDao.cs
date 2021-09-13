using HomeBi.Libraries.PagedList;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserDao
    {
        OnlineShopDbContext db = null;
        public UserDao()
        {
            db = new OnlineShopDbContext();
        }
        public string Insert(TaiKhoan entity)
        {
            db.TaiKhoans.Add(entity);
            db.SaveChanges();
            return entity.TenTK;
        }
        public TaiKhoan GetByID(string tenTK)
        {
            return db.TaiKhoans.FirstOrDefault(x => x.TenTK == tenTK );
        }
        public IEnumerable<TaiKhoan> GetAllTaiKhoan()
        {
            return db.TaiKhoans.Select(x => x);
        }
        public int Login(string tenTK, string matKhau)
        {
            var result = db.TaiKhoans.SingleOrDefault(x => x.TenTK == tenTK);
            if(result == null)
            {
                return 0;
            }
            else
            {
                if(result.Quyen == null)
                {
                    return -3;
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
        }
    }
}
