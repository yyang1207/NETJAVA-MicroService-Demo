using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace swmsweb
{
    public class MySqlHelper
    {
        public static string dbConnection = string.Empty;

        public static string LogPath = string.Empty;
        public static DataTable GetDataTableBySql(string sql, string conStr, out int state)
        {
            MySqlConnection con = null;
            MySqlCommand cmd = null;
            MySqlDataAdapter dar = null;
            DataTable dt = null;

            conStr = conStr.Replace("\"", "");

            con = new MySqlConnection(conStr);
            cmd = new MySqlCommand();
            dar = new MySqlDataAdapter();
            dt = new DataTable();

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.CommandText = sql;
                dar.SelectCommand = cmd;
                dar.Fill(dt);
                con.Close();
                state = 1;
            }
            catch (Exception ex)
            {
                state = 0;
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                dar.Dispose();
                dar = null;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return dt;
        }
        public static DataTable GetDataTableBySql(string sql,List<MySql.Data.MySqlClient.MySqlParameter> parameters)
        {
            string conStr = null;
            MySqlConnection con = null;
            MySqlCommand cmd = null;
            MySqlDataAdapter dar = null;
            DataTable dt = null;

            conStr = dbConnection;
            conStr = conStr.Replace("metadata=res://*/SWMSModel.csdl|res://*/SWMSModel.ssdl|res://*/SWMSModel.msl;provider=MySql.Data.MySqlClient;provider connection string=\"", "");
            conStr = conStr.Replace("\"", "");
            conStr += ";characterset=utf8";
            con = new MySqlConnection(conStr);
            cmd = new MySqlCommand();
            cmd.CommandTimeout = 600;
            cmd.Connection = con;
            dar = new MySqlDataAdapter();
            dt = new DataTable();

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (var item in parameters)
                    {
                        cmd.Parameters.Add(item);
                    }
                }

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                dar.SelectCommand = cmd;
                dar.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                dar.Dispose();
                dar = null;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return dt;
        }

        public static int SetDataTranBySql(string sql)
        {
            int result = -1;
            string conStr = null;

            MySqlTransaction tra = null;
            MySqlCommand cmd = null;

            conStr = dbConnection;
            conStr = conStr.Replace("metadata=res://*/SWMSModel.csdl|res://*/SWMSModel.ssdl|res://*/SWMSModel.msl;provider=MySql.Data.MySqlClient;provider connection string=\"", "");
            conStr = conStr.Replace("\"", "");
            using (var con = new MySqlConnection(conStr))
            {
                cmd = new MySqlCommand();
                try
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    //MySqlTransaction对象并用MySqlConnection对象的BeginTransaction()方法开始事务
                    tra = con.BeginTransaction();
                    //创建保存SQL语句
                    cmd = con.CreateCommand();
                    //将Transaction属性设置为上面所生成的MySqlTransaction对象
                    cmd.Transaction = tra;
                    //将MySqlCommand对象的CommandText属性设置为第一个INSERT语句  第一个INSERT语句
                    cmd.CommandText = sql;
                    //执行第一个INSERT语句
                    cmd.ExecuteNonQuery();

                    //将MySqlCommand对象的CommandText属性设置为第二个INSERT语句  第二个INSERT语句
                    //cmd.CommandText = sql;
                    //执行第二个INSERT语句
                    //cmd.ExecuteNonQuery();

                    //提交事务, 使INSERT语句增加的两行在数据库中保存起来
                    tra.Commit();
                    con.Close();

                    result = 1;
                    return result;
                }
                catch (Exception)
                {
                    tra.Rollback();
                    return result;
                }
                finally
                {
                    cmd.Dispose();
                    cmd = null;
                    tra.Dispose();
                    tra = null;

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }


        public static int SetDataBySql(string sql)
        {
            int result = -1;
            string conStr = null;

           // MySqlTransaction tra = null;
            MySqlCommand cmd = null;

            conStr = dbConnection;
            conStr = conStr.Replace("metadata=res://*/SWMSModel.csdl|res://*/SWMSModel.ssdl|res://*/SWMSModel.msl;provider=MySql.Data.MySqlClient;provider connection string=\"", "");
            conStr = conStr.Replace("\"", "");
            using (var con = new MySqlConnection(conStr))
            {
                cmd = new MySqlCommand();
                try
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    //MySqlTransaction对象并用MySqlConnection对象的BeginTransaction()方法开始事务
                   // tra = con.BeginTransaction();
                    //创建保存SQL语句
                    cmd = con.CreateCommand();
                    //将Transaction属性设置为上面所生成的MySqlTransaction对象
                    //cmd.Transaction = tra;
                    //将MySqlCommand对象的CommandText属性设置为第一个INSERT语句  第一个INSERT语句
                    cmd.CommandText = sql;
                    //执行第一个INSERT语句
                    result=cmd.ExecuteNonQuery();

                    //将MySqlCommand对象的CommandText属性设置为第二个INSERT语句  第二个INSERT语句
                    //cmd.CommandText = sql;
                    //执行第二个INSERT语句
                    //cmd.ExecuteNonQuery();

                    //提交事务, 使INSERT语句增加的两行在数据库中保存起来
                    //tra.Commit();
                    con.Close();

                    //result = 1;
                    return result;
                }
                catch (Exception ex)
                {
                    // tra.Rollback();
                    Log.WriteLogNew(LogType.Error,ex);
                    return result;
                }
                finally
                {
                    cmd.Dispose();
                    cmd = null;
                   // tra.Dispose();
                    //tra = null;

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }

        public static int SetDataBySql(string sql,List<MySql.Data.MySqlClient.MySqlParameter> parameters)
        {
            int result = -1;
            string conStr = null;

            // MySqlTransaction tra = null;
            MySqlCommand cmd = null;

            conStr = dbConnection;
            conStr = conStr.Replace("metadata=res://*/SWMSModel.csdl|res://*/SWMSModel.ssdl|res://*/SWMSModel.msl;provider=MySql.Data.MySqlClient;provider connection string=\"", "");
            conStr = conStr.Replace("\"", "");
            using (var con = new MySqlConnection(conStr))
            {
                cmd = new MySqlCommand();
                try
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    //MySqlTransaction对象并用MySqlConnection对象的BeginTransaction()方法开始事务
                    // tra = con.BeginTransaction();
                    //创建保存SQL语句
                    cmd = con.CreateCommand();

                    if (parameters != null && parameters.Count > 0)
                    {
                        foreach (var item in parameters)
                        {
                            cmd.Parameters.Add(item);
                        }
                    }

                    //将Transaction属性设置为上面所生成的MySqlTransaction对象
                    //cmd.Transaction = tra;
                    //将MySqlCommand对象的CommandText属性设置为第一个INSERT语句  第一个INSERT语句
                    cmd.CommandText = sql;
                    //执行第一个INSERT语句
                    result = cmd.ExecuteNonQuery();

                    //将MySqlCommand对象的CommandText属性设置为第二个INSERT语句  第二个INSERT语句
                    //cmd.CommandText = sql;
                    //执行第二个INSERT语句
                    //cmd.ExecuteNonQuery();

                    //提交事务, 使INSERT语句增加的两行在数据库中保存起来
                    //tra.Commit();
                    con.Close();

                    //result = 1;
                    return result;
                }
                catch (Exception ex)
                {
                    // tra.Rollback();
                    Log.WriteLogNew(LogType.Error, ex);
                    return result;
                }
                finally
                {
                    cmd.Dispose();
                    cmd = null;
                    // tra.Dispose();
                    //tra = null;

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }

        public static int SetLogDataTranBySql(string sql)
        {
            int result = -1;
            string conStr = null;
            MySqlConnection con = null;
            MySqlTransaction tra = null;
            MySqlCommand cmd = null;

            conStr = dbConnection;
            conStr = conStr.Replace("metadata=res://*/SWMSModel.csdl|res://*/SWMSModel.ssdl|res://*/SWMSModel.msl;provider=MySql.Data.MySqlClient;provider connection string=\"", "");
            conStr = conStr.Replace("\"", "");

            con = new MySqlConnection(conStr);
            cmd = new MySqlCommand();

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                //MySqlTransaction对象并用MySqlConnection对象的BeginTransaction()方法开始事务
                tra = con.BeginTransaction();
                //创建保存SQL语句
                cmd = con.CreateCommand();
                //将Transaction属性设置为上面所生成的MySqlTransaction对象
                cmd.Transaction = tra;
                //将MySqlCommand对象的CommandText属性设置为第一个INSERT语句  第一个INSERT语句
                cmd.CommandText = sql;
                //执行第一个INSERT语句
                result = cmd.ExecuteNonQuery();

                //将MySqlCommand对象的CommandText属性设置为第二个INSERT语句  第二个INSERT语句
                //cmd.CommandText = sql;
                //执行第二个INSERT语句
                //cmd.ExecuteNonQuery();

                //提交事务, 使INSERT语句增加的两行在数据库中保存起来
                tra.Commit();
                con.Close();
                return result;
            }
            catch (Exception)
            {
                tra.Rollback();
                return result;
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                tra.Dispose();
                tra = null;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public static int SetDataTranBySql(string sql, int count)
        {
            int result = -1;
            string conStr = null;
            MySqlConnection con = null;
            MySqlTransaction tra = null;
            MySqlCommand cmd = null;

            conStr = dbConnection;
            conStr = conStr.Replace("metadata=res://*/SWMSModel.csdl|res://*/SWMSModel.ssdl|res://*/SWMSModel.msl;provider=MySql.Data.MySqlClient;provider connection string=\"", "");
            conStr = conStr.Replace("\"", "");

            con = new MySqlConnection(conStr);
            cmd = new MySqlCommand();

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                //MySqlTransaction对象并用MySqlConnection对象的BeginTransaction()方法开始事务
                tra = con.BeginTransaction();
                //创建保存SQL语句
                cmd = con.CreateCommand();
                //将Transaction属性设置为上面所生成的MySqlTransaction对象
                cmd.Transaction = tra;
                //将MySqlCommand对象的CommandText属性设置为第一个INSERT语句  第一个INSERT语句
                cmd.CommandText = sql;
                //执行第一个INSERT语句
                result = cmd.ExecuteNonQuery();

                //将MySqlCommand对象的CommandText属性设置为第二个INSERT语句  第二个INSERT语句
                //cmd.CommandText = sql;
                //执行第二个INSERT语句
                //cmd.ExecuteNonQuery();

                //提交事务, 使INSERT语句增加的两行在数据库中保存起来
                if (result == count)
                {
                    result = 1;
                    tra.Commit();
                }
                else
                {
                    result = 0;
                    tra.Rollback();
                }
                con.Close();

                return result;
            }
            catch (Exception)
            {
                tra.Rollback();
                result = -1;
                return result;
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                tra.Dispose();
                tra = null;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public static int SetDataTranBySqlList(List<string> sqlList)
        {
            int retCode = 0;
            string conStr = null;
            conStr = dbConnection;
            //sconStr = conStr.Replace("metadata=res://*/SWMSModel.csdl|res://*/SWMSModel.ssdl|res://*/SWMSModel.msl;provider=MySql.Data.MySqlClient;provider connection string=\"", "");
            conStr = conStr.Replace("\"", "");
            conStr += "; charset = 'utf8'";
            using (MySqlConnection con = new MySqlConnection(conStr))
            {

                MySqlTransaction tra = null;
                var cmd = new MySqlCommand();
                try
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    tra = con.BeginTransaction();
                    for (int i = 0; i < sqlList.Count; i++)
                    {
                        cmd.CommandText = sqlList[i];
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        cmd.CommandTimeout = 600;
                        Log.WriteLogNew(LogType.Default, "语句：" + cmd.CommandText);
                        var result = cmd.ExecuteNonQuery();


                        if (result <= 0)
                        {
                            tra.Rollback();
                            retCode = -1;
                            return retCode;
                        }
                        retCode += result;
                    }

                    tra.Commit();
                }
                catch (Exception ex)
                {
                    Log.WriteLogNew(LogType.Error,ex.Message);
                    tra.Rollback();
                    retCode = -2;
                    return retCode;
                }
                finally
                {
                    cmd.Dispose();
                    cmd = null;
                    tra.Dispose();
                    tra = null;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            return retCode;
        }

        public static int SetDataTranBySqlList(List<string> sqlList, out MySqlTransaction tra)
        {
            int retCode = 0;
            string conStr = null;
            conStr = dbConnection;
            conStr = conStr.Replace("metadata=res://*/SWMSModel.csdl|res://*/SWMSModel.ssdl|res://*/SWMSModel.msl;provider=MySql.Data.MySqlClient;provider connection string=\"", "");
            conStr = conStr.Replace("\"", "");
            conStr += "; charset = 'utf8'";
            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                tra = null;
                var cmd = new MySqlCommand();
                try
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    tra = con.BeginTransaction();
                    for (int i = 0; i < sqlList.Count; i++)
                    {
                        cmd.CommandText = sqlList[i];
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        var result = cmd.ExecuteNonQuery();
                        if (result <= 0)
                        {
                            tra.Rollback();
                            tra = null;
                            retCode = -1;
                            return retCode;
                        }
                    }
                    retCode = 1;
                    //tra.Commit();
                }
                catch (Exception ex)
                {
                    Log.WriteLogNew(LogType.Error, ex.Message);
                    tra.Rollback();
                    tra = null;
                    retCode = -2;
                    return retCode;
                }
                finally
                {
                    cmd.Dispose();
                    cmd = null;
                    tra.Dispose();
                    tra = null;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            return retCode;
        }

        public static DataTable GetDataTableBySqlUTF8(string sql)
        {
            string conStr = null;
            conStr = dbConnection;
            conStr = conStr.Replace("metadata=res://*/SWMSModel.csdl|res://*/SWMSModel.ssdl|res://*/SWMSModel.msl;provider=MySql.Data.MySqlClient;provider connection string=\"", "");
            conStr = conStr.Replace("\"", "");
            conStr += "; charset = 'utf8'";
            using (var con = new MySqlConnection(conStr))
            {
                MySqlCommand cmd = null;
                MySqlDataAdapter dar = null;
                DataTable dt = null;


                cmd = new MySqlCommand();
                dar = new MySqlDataAdapter();
                dt = new DataTable();

                try
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.CommandText = sql;
                    cmd.CommandTimeout = 600;
                    dar.SelectCommand = cmd;
                    dar.Fill(dt);
                    con.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Dispose();
                    cmd = null;
                    dar.Dispose();
                    dar = null;

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }

                return dt;
            }
        }

        public static DataTable GetUnitMailBySqlUTF8(string sql)
        {
            string conStr = null;
            MySqlConnection con = null;
            MySqlCommand cmd = null;
            MySqlDataAdapter dar = null;
            DataTable dt = null;

            conStr = dbConnection;
            conStr = conStr.Replace("metadata=res://*/SWMSModel.csdl|res://*/SWMSModel.ssdl|res://*/SWMSModel.msl;provider=MySql.Data.MySqlClient;provider connection string=\"", "");
            conStr = conStr.Replace("\"", "");
            conStr += "; charset = 'utf8'";
            con = new MySqlConnection(conStr);
            cmd = new MySqlCommand();
            dar = new MySqlDataAdapter();
            dt = new DataTable();

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.CommandText = sql;
                cmd.CommandTimeout = 600;
                dar.SelectCommand = cmd;
                dar.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                dar.Dispose();
                dar = null;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return dt;
        }
    }
}
