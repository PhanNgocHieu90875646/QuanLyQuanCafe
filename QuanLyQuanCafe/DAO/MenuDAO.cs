using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;
        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO(); return MenuDAO.instance; }
            private set { MenuDAO.instance = value; }


        }
        private MenuDAO() { }
        public List<Menu> GetListMenuByTable(int id)
        {
            List<Menu> listmenus = new List<Menu>();
            string query = $@"
            SELECT 
                f.name AS TenMon,
                s.Size AS SizeMon,
                bi.count AS SoLuong,
                (f.price + s.Gia) AS DonGia,
                ((f.price * bi.count)+s.Gia) AS ThanhTien
            FROM dbo.ThongTinHoaDon AS bi
            JOIN dbo.HoaDon AS b ON bi.idBill = b.id
            JOIN dbo.MonAn AS f ON bi.idFood = f.id
            LEFT JOIN dbo.SizeMonAn AS s ON bi.IdSizeMonAn = s.Id
            WHERE b.status = 0 AND b.inttable ={id}";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                listmenus.Add(menu);
            }
            return listmenus;
        }
    }
}