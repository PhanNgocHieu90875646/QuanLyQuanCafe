using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Bill
    {
        public Bill(int id, DateTime? dataCheckIn, DateTime? dataCheckOnt,int status, int discount=0) {
            this.ID = id;
            this.DateCheckIn = dataCheckIn;
            this.DateCheckOnt = dataCheckOnt;
            this.Status = status;
            this.Discount=discount;
        }
        public Bill(DataRow row)
        {
            this.ID = (int)row["id"];
            this.DateCheckIn = (DateTime?)row["DataCheckIn"];
            var DateCheckOntTemp= row["DataCheckOnt"];
            if(DateCheckOntTemp.ToString()!="") 
            this.DateCheckOnt = (DateTime?)DateCheckOntTemp;
            this.Status = (int)row["status"];
            if(row["discount"].ToString()!="")
                this.Discount = (int)row["discount"];
        }

      
        private int discount;
        public int Discount
        {
            get { return discount; }
            set { discount = value; }
        }
        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        private DateTime? dataCheckOnt;
        public DateTime? DateCheckOnt
        {
            get { return dataCheckOnt; }
            set { dataCheckOnt = value; }
        }
        private  DateTime? dataCheckIn;
        public DateTime? DateCheckIn
        {
            get { return dataCheckIn; }
            set { dataCheckIn = value; }
        }
        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
