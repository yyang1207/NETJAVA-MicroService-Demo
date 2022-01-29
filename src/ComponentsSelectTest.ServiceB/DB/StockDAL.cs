using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelectTest.ServiceB.DB
{
    public class StockDAL
    {
        private readonly string _ConnectionString = "Server=119.3.134.203;Database=db_stock;User ID=postgres;Password=yyang@1207;";
        public bool Add(StockTable stock)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(_ConnectionString))
            {
                string cmdText = $"insert into tb_stock(ProductId,Stock,ReserveStock) values({stock.ProductId},{stock.Stock},{stock.ReverseStock})";
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

        public bool PhysicalDelete(StockTable stock)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(_ConnectionString))
            {
                string cmdText = $"delete from tb_stock where ProductId={stock.ProductId}";
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
