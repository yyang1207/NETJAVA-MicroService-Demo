using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace ComponentsSelectTest.ServiceA.DB
{
    public class OrderDAL
    {
        private readonly string _ConnectionString = "Server=119.3.134.203;Database=db_order;User ID=postgres;Password=yyang@1207;";
        public bool Add(Order order)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(_ConnectionString))
            {
                string cmdText = $"insert into tb_order(Id,UserName,Amount) values({order.Id},'{order.UserName}',{order.Amount})";
                try
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn))
                    {
                        if (cmd.ExecuteNonQuery() > 0) return true;
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }

            return false;
        }

        public bool PhysicalDelete(Order order)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(_ConnectionString))
            {
                string cmdText = $"delete from tb_order where Id={order.Id}";
                try
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn))
                    {
                        if (cmd.ExecuteNonQuery() > 0) return true;
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }

            return false;
        }
    }
}
