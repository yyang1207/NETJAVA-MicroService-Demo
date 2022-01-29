using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace swmsweb.Service
{
    public class TestOrderService
    {

        public int UpdateTestOrder(string orderId,string storeId,string version) {

            try
            {

                string sql = $"update testOrder set status=status+1,version=@version where orderId=@orderId and storeId=@storeId  ";

                List<MySql.Data.MySqlClient.MySqlParameter> paramList = new List<MySql.Data.MySqlClient.MySqlParameter>() {
                    new MySql.Data.MySqlClient.MySqlParameter("@version", version),
                    new MySql.Data.MySqlClient.MySqlParameter("@orderId", orderId),
                    new MySql.Data.MySqlClient.MySqlParameter("@storeId", storeId)
                };


                return MySqlHelper.SetDataBySql(sql, paramList);

            }
            catch (Exception ex)
            {
                
                Log.WriteLogNew(LogType.Default,ex);

                return 0;

            }
        }

        public TestOrder GetOrderInfo(string orderId, string storeId)
        {
            string sql = $"SELECT * FROM testOrder WHERE orderid=@orderid AND storeid=@storeid";

            List<MySql.Data.MySqlClient.MySqlParameter> paramList = new List<MySql.Data.MySqlClient.MySqlParameter>() {
                    new MySql.Data.MySqlClient.MySqlParameter("@orderId", orderId),
                    new MySql.Data.MySqlClient.MySqlParameter("@storeId", storeId)
            };

            var res = MySqlHelper.GetDataTableBySql(sql,paramList);

            if (res != null && res.Rows.Count > 0)
            {
                TestOrder order = new TestOrder()
                {
                    OrderID = res.Rows[0]["OrderID"].ToString(),
                    StoreID = res.Rows[0]["storeID"].ToString(),
                    Status=Convert.ToInt32(res.Rows[0]["status"].ToString()),
                    Version= res.Rows[0]["version"].ToString(),
                };
                return order;
            }
            else
            {
                return null;
            }

        }

    }

    public class TestOrder 
    {

        public string OrderID { get; set; }

        public string StoreID { get; set; }


        public int Status { get; set; }

        public string Version { get; set; }
    }
}
