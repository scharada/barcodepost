using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.IO;

	/// <summary>
	/// Copyright (C) 2004-2008 LiTianPing 
	/// 数据访问基础类(基于SQLServer)
	/// 用户可以修改满足自己项目的需要。
	/// </summary>
	public abstract class DbHelperSQL
	{
		//数据库连接字符串(web.config来配置)
		//<add key="ConnectionString" value="server=127.0.0.1;database=DATABASE;uid=sa;pwd=" />		
        protected static string connectionString = "";
		public DbHelperSQL()
		{
            InitDB();
		}
		
        public static void InitDB()
        {
            string TIPdatasource = ConfigurationManager.AppSettings["DatabaseServer"];
            string TIPdatauser = ConfigurationManager.AppSettings["DatabaseUser"];
            string TIPdatapass = ConfigurationManager.AppSettings["DatabasePwd"];
            string TIPdata = ConfigurationManager.AppSettings["DatabaseName"];

            string TIPSourceString = "Data Source=" + TIPdatasource + ";Initial Catalog=" + TIPdata + ";User ID=" + TIPdatauser + ";Password=" + TIPdatapass;

            connectionString = TIPSourceString;
        }

		#region  执行简单SQL语句

		/// <summary>
		/// 执行SQL语句，返回影响的记录数
		/// </summary>
		/// <param name="SQLString">SQL语句</param>
		/// <returns>影响的记录数</returns>
		public static int ExecuteSql(string SQLString)
		{
            InitDB();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{				
				using (SqlCommand cmd = new SqlCommand(SQLString,connection))
				{
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
				}				
			}
		}
		
		/// <summary>
		/// 执行多条SQL语句，实现数据库事务。
		/// </summary>
		/// <param name="SQLStringList">多条SQL语句</param>		
		public static void ExecuteSqlTran(ArrayList SQLStringList)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand();
				cmd.Connection=conn;				
				SqlTransaction tx=conn.BeginTransaction();			
				cmd.Transaction=tx;				
				try
				{   		
					for(int n=0;n<SQLStringList.Count;n++)
					{
						string strsql=SQLStringList[n].ToString();
						if (strsql.Trim().Length>1)
						{
							cmd.CommandText=strsql;
							cmd.ExecuteNonQuery();
						}
					}										
					tx.Commit();					
				}
				catch(System.Data.SqlClient.SqlException E)
				{		
					tx.Rollback();
					throw new Exception(E.Message);
				}
			}
		}
		/// <summary>
		/// 执行带一个存储过程参数的的SQL语句。
		/// </summary>
		/// <param name="SQLString">SQL语句</param>
		/// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
		/// <returns>影响的记录数</returns>
		public static int ExecuteSql(string SQLString,string content)
		{
            InitDB();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand(SQLString,connection);		
				System.Data.SqlClient.SqlParameter  myParameter = new System.Data.SqlClient.SqlParameter ( "@content", SqlDbType.NText);
				myParameter.Value = content ;
				cmd.Parameters.Add(myParameter);
				try
				{
					connection.Open();
					int rows=cmd.ExecuteNonQuery();
					return rows;
				}
				catch(System.Data.SqlClient.SqlException E)
				{				
					throw new Exception(E.Message);
				}
				finally
				{
					cmd.Dispose();
					connection.Close();
				}	
			}
		}		
		/// <summary>
		/// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
		/// </summary>
		/// <param name="strSQL">SQL语句</param>
		/// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
		/// <returns>影响的记录数</returns>
		public static int ExecuteSqlInsertImg(string strSQL,byte[] fs)
		{
            InitDB();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand(strSQL,connection);	
				System.Data.SqlClient.SqlParameter  myParameter = new System.Data.SqlClient.SqlParameter ( "@fs", SqlDbType.Image);
				myParameter.Value = fs ;
				cmd.Parameters.Add(myParameter);
				try
				{
					connection.Open();
					int rows=cmd.ExecuteNonQuery();
					return rows;
				}
				catch(System.Data.SqlClient.SqlException E)
				{				
					throw new Exception(E.Message);
				}
				finally
				{
					cmd.Dispose();
					connection.Close();
				}				
			}
		}
		
		/// <summary>
		/// 执行一条计算查询结果语句，返回查询结果（object）。
		/// </summary>
		/// <param name="SQLString">计算查询结果语句</param>
		/// <returns>查询结果（object）</returns>
		public static object GetSingle(string SQLString)
		{
            InitDB();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				using(SqlCommand cmd = new SqlCommand(SQLString,connection))
				{
					try
					{
						connection.Open();
						object obj = cmd.ExecuteScalar();
						if((Object.Equals(obj,null))||(Object.Equals(obj,System.DBNull.Value)))
						{					
							return null;
						}
						else
						{
							return obj;
						}				
					}
					catch(System.Data.SqlClient.SqlException e)
					{						
						connection.Close();
						throw new Exception(e.Message);
					}	
				}
			}
		}
		/// <summary>
		/// 执行查询语句，返回SqlDataReader
		/// </summary>
		/// <param name="strSQL">查询语句</param>
		/// <returns>SqlDataReader</returns>
		public static SqlDataReader ExecuteReader(string strSQL)
		{
			SqlConnection connection = new SqlConnection(connectionString);			
			SqlCommand cmd = new SqlCommand(strSQL,connection);				
			try
			{
				connection.Open();	
				SqlDataReader myReader = cmd.ExecuteReader();
				return myReader;
			}
			catch(System.Data.SqlClient.SqlException e)
			{								
				throw new Exception(e.Message);
			}			
			
		}		
		/// <summary>
		/// 执行查询语句，返回DataSet
		/// </summary>
		/// <param name="SQLString">查询语句</param>
		/// <returns>DataSet</returns>
		public static DataSet Query(string SQLString)
		{
            InitDB();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				DataSet ds = new DataSet();
				try
				{
					connection.Open();
					SqlDataAdapter command = new SqlDataAdapter(SQLString,connection);				
					command.Fill(ds,"ds");
				}
				catch(System.Data.SqlClient.SqlException ex)
				{				
					throw new Exception(ex.Message);
				}			
				return ds;
			}			
		}

        private static IDataReader InnerReader;
        /// <summary>
        /// 保存与table对应的Adapter
        /// </summary>
        private static SortedList sList = new SortedList();
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static IDbCommand ActiveCommand;
        /// <summary>
        /// 公用数据库连接
        /// </summary>
        public static IDbConnection ActiveConnection;

        public static DataSet ActiveDataSet;

        /// <summary>
        /// 初始化数据库连接
        /// </summary>
        private static void InitialConnection()
        {
            if (ActiveConnection ==null)
            ActiveConnection = new SqlConnection(connectionString);

            if(ActiveCommand==null )
            ActiveCommand = new SqlCommand();

            ActiveCommand.Connection = ActiveConnection;
            if (ActiveConnection.State == ConnectionState.Closed)
                ActiveConnection.Open();
        }

        public static DataTable DoQueryEx(string tbName, string queryString, bool bRelease)
        {
            DataTable dt = new DataTable(tbName);
            //DataTable dt2 = new DataTable();

            //CloseReader();
            //if (connectionString != string.Empty)
                InitDB();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(queryString, connection))
                {
                    //sqlAdapter.SelectCommand.Transaction = (SqlTransaction)ActiveCommand.Transaction;
                    // SqlCommandBuilder myDataRowsCommandBuilder = new SqlCommandBuilder(sqlAdapter);
                    //if (ActiveDataSet ==null)
                    //DataSet ds = new DataSet();
                    try
                    {
                        //sqlAdapter.Fill(ds, tbName);
                        sqlAdapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                    finally
                    {
                        //DisConnectDB();
                    }
                }
            }



            return dt;
        }

        public static void DisConnectDB()
        {
            try
            {
                ActiveConnection.Close();
            }
            catch
            {
            }
            finally
            {
                ActiveConnection.Close();
            }
        }

        public static int GetMaxID(string tbName, string sfName)
        {
            string sql = "SELECT MAX(" + sfName + ") as ID FROM " + tbName;
            DataTable dt = DoQueryEx(tbName, sql, true);
            if (dt == null || dt.Rows.Count == 0)
                return 1;
            else
                return (Convert.IsDBNull(dt.Rows[0][0])?1:Convert.ToInt32(dt.Rows[0][0]) + 1);
        }

        public static void CloseReader()
        {
            if (InnerReader != null)
            {
                if (InnerReader.IsClosed == false)
                {
                    InnerReader.Close();
                }
            }

        }
        /// <summary>
        /// 清除dataset
        /// </summary>
        void Clear()
        {
            if (ActiveDataSet != null)
            {
                ActiveDataSet.Tables.Clear();
            }

            if (sList != null)
            {
                sList.Clear();
            }
        }

		#endregion

		#region 执行带参数的SQL语句

		/// <summary>
		/// 执行SQL语句，返回影响的记录数
		/// </summary>
		/// <param name="SQLString">SQL语句</param>
		/// <returns>影响的记录数</returns>
		public static int ExecuteSql(string SQLString,params SqlParameter[] cmdParms)
		{
            InitDB();            
			using (SqlConnection connection = new SqlConnection(connectionString))
			{				
				using (SqlCommand cmd = new SqlCommand())
				{
					try
					{		
						PrepareCommand(cmd, connection, null,SQLString, cmdParms);
						int rows=cmd.ExecuteNonQuery();
						cmd.Parameters.Clear();
						return rows;
					}
					catch(System.Data.SqlClient.SqlException E)
					{				
						throw new Exception(E.Message);
					}
				}				
			}
		}
		
			
		/// <summary>
		/// 执行多条SQL语句，实现数据库事务。
		/// </summary>
		/// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
		public static void ExecuteSqlTran(Hashtable SQLStringList)
		{			
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();
				using (SqlTransaction trans = conn.BeginTransaction()) 
				{
					SqlCommand cmd = new SqlCommand();
					try 
					{
						//循环
						foreach (DictionaryEntry myDE in SQLStringList)
						{	
							string 	cmdText=myDE.Key.ToString();
							SqlParameter[] cmdParms=(SqlParameter[])myDE.Value;
							PrepareCommand(cmd,conn,trans,cmdText, cmdParms);
							int val = cmd.ExecuteNonQuery();
							cmd.Parameters.Clear();

							trans.Commit();
						}					
					}
					catch 
					{
						trans.Rollback();
						throw;
					}
				}				
			}
		}
	
				
		/// <summary>
		/// 执行一条计算查询结果语句，返回查询结果（object）。
		/// </summary>
		/// <param name="SQLString">计算查询结果语句</param>
		/// <returns>查询结果（object）</returns>
		public static object GetSingle(string SQLString,params SqlParameter[] cmdParms)
		{
            InitDB();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand())
				{
					try
					{
						PrepareCommand(cmd, connection, null,SQLString, cmdParms);
						object obj = cmd.ExecuteScalar();
						cmd.Parameters.Clear();
						if((Object.Equals(obj,null))||(Object.Equals(obj,System.DBNull.Value)))
						{					
							return null;
						}
						else
						{
							return obj;
						}				
					}
					catch(System.Data.SqlClient.SqlException e)
					{				
						throw new Exception(e.Message);
					}					
				}
			}
		}
		
		/// <summary>
		/// 执行查询语句，返回SqlDataReader
		/// </summary>
		/// <param name="strSQL">查询语句</param>
		/// <returns>SqlDataReader</returns>
		public static SqlDataReader ExecuteReader(string SQLString,params SqlParameter[] cmdParms)
		{		
			SqlConnection connection = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand();				
			try
			{
				PrepareCommand(cmd, connection, null,SQLString, cmdParms);
				SqlDataReader myReader = cmd.ExecuteReader();
				cmd.Parameters.Clear();
				return myReader;
			}
			catch(System.Data.SqlClient.SqlException e)
			{								
				throw new Exception(e.Message);
			}					
			
		}		
		
		/// <summary>
		/// 执行查询语句，返回DataSet
		/// </summary>
		/// <param name="SQLString">查询语句</param>
		/// <returns>DataSet</returns>
		public static DataSet Query(string SQLString,params SqlParameter[] cmdParms)
		{
            InitDB();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand();
				PrepareCommand(cmd, connection, null,SQLString, cmdParms);
				using( SqlDataAdapter da = new SqlDataAdapter(cmd) )
				{
					DataSet ds = new DataSet();	
					try
					{												
						da.Fill(ds,"ds");
						cmd.Parameters.Clear();
					}
					catch(System.Data.SqlClient.SqlException ex)
					{				
						throw new Exception(ex.Message);
					}			
					return ds;
				}				
			}			
		}


		private static void PrepareCommand(SqlCommand cmd,SqlConnection conn,SqlTransaction trans, string cmdText, SqlParameter[] cmdParms) 
		{            
			if (conn.State != ConnectionState.Open)
				conn.Open();
			cmd.Connection = conn;
			cmd.CommandText = cmdText;
			if (trans != null)
				cmd.Transaction = trans;
			cmd.CommandType = CommandType.Text;//cmdType;
			if (cmdParms != null) 
			{
				foreach (SqlParameter parm in cmdParms)
					cmd.Parameters.Add(parm);
			}
		}

		#endregion

		#region 存储过程操作

		/// <summary>
		/// 执行存储过程
		/// </summary>
		/// <param name="storedProcName">存储过程名</param>
		/// <param name="parameters">存储过程参数</param>
		/// <returns>SqlDataReader</returns>
		public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters )
		{
			SqlConnection connection = new SqlConnection(connectionString);
			SqlDataReader returnReader;
			connection.Open();
			SqlCommand command = BuildQueryCommand( connection,storedProcName, parameters );
			command.CommandType = CommandType.StoredProcedure;
			returnReader = command.ExecuteReader();				
			return returnReader;			
		}
		
		
		/// <summary>
		/// 执行存储过程
		/// </summary>
		/// <param name="storedProcName">存储过程名</param>
		/// <param name="parameters">存储过程参数</param>
		/// <param name="tableName">DataSet结果中的表名</param>
		/// <returns>DataSet</returns>
		public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName )
		{
            InitDB();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				DataSet dataSet = new DataSet();
				connection.Open();
				SqlDataAdapter sqlDA = new SqlDataAdapter();
				sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters );
				sqlDA.Fill( dataSet, tableName );
				connection.Close();
				return dataSet;
			}
		}

		
		/// <summary>
		/// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
		/// </summary>
		/// <param name="connection">数据库连接</param>
		/// <param name="storedProcName">存储过程名</param>
		/// <param name="parameters">存储过程参数</param>
		/// <returns>SqlCommand</returns>
		private static SqlCommand BuildQueryCommand(SqlConnection connection,string storedProcName, IDataParameter[] parameters)
		{			
			SqlCommand command = new SqlCommand( storedProcName, connection );
			command.CommandType = CommandType.StoredProcedure;
			foreach (SqlParameter parameter in parameters)
			{
				command.Parameters.Add( parameter );
			}
			return command;			
		}
		
		/// <summary>
		/// 执行存储过程，返回影响的行数		
		/// </summary>
		/// <param name="storedProcName">存储过程名</param>
		/// <param name="parameters">存储过程参数</param>
		/// <param name="rowsAffected">影响的行数</param>
		/// <returns></returns>
		public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected )
		{
            InitDB();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				int result;
				connection.Open();
				SqlCommand command = BuildIntCommand(connection,storedProcName, parameters );
				rowsAffected = command.ExecuteNonQuery();
				result = (int)command.Parameters["ReturnValue"].Value;
				//Connection.Close();
				return result;
			}
		}
		
		/// <summary>
		/// 创建 SqlCommand 对象实例(用来返回一个整数值)	
		/// </summary>
		/// <param name="storedProcName">存储过程名</param>
		/// <param name="parameters">存储过程参数</param>
		/// <returns>SqlCommand 对象实例</returns>
		private static SqlCommand BuildIntCommand(SqlConnection connection,string storedProcName, IDataParameter[] parameters)
		{
			SqlCommand command = BuildQueryCommand(connection,storedProcName, parameters );
			command.Parameters.Add( new SqlParameter ( "ReturnValue",
				SqlDbType.Int,4,ParameterDirection.ReturnValue,
				false,0,0,string.Empty,DataRowVersion.Default,null ));
			return command;
		}

        //public static SqlDatabaseOperator DBCon = new SqlDatabaseOperator();
		#endregion	

       

        public static int ExecuteSqlEx(string SQLString, string content)
        {
            InitDB();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(SQLString, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
     
	}
