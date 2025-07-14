using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class FoodView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string CategoryName { get; set; }

        public FoodView(DataRow row)
        {
            this.ID = (int)row["ID"];
            this.Name = row["Name"].ToString();
            this.Price = (float)Convert.ToDouble(row["Price"]);
            this.CategoryName = row["CategoryName"].ToString();
        }
    }
}
