﻿using SqlSugar;
using System;

namespace sdglsys.DbHelper
{
    public class DbContext:IDisposable
    {
        public SqlSugarClient Db;
        public static string dbtype = null;
        public static string connectstring = null;
        public DbContext()
        {
            var dbType = new SqlSugar.DbType();
            if (dbtype == null)
            {
                dbtype = (string)Utils.GetAppSetting("DBType", typeof(string));
            }
            switch (dbtype)
            {
                case "mysql": dbType = DbType.MySql; break;
                case "sqlserver": dbType = DbType.SqlServer; break;
                case "postgresql": dbType = DbType.PostgreSQL; break;
                case "sqlite": dbType = DbType.Sqlite; break;
                case "oracle": dbType = DbType.Oracle; break;
            }
            if (connectstring == null)
                connectstring = (string)DbHelper.Utils.GetAppSetting("DBConnectionString", typeof(string));

            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectstring,
                DbType = dbType,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
        }

        /// <summary>
        /// 自定义连接
        /// </summary>
        /// <param name="ConnectionString">数据库连接字符串</param>
        /// <param name="DBType">mysql,sqlserver,postgresql,oracle,sqlite</param>
        public DbContext(string ConnectionString, string DBType)
        {
            connectstring = ConnectionString;
            dbtype = DBType;

            var dbType = new SqlSugar.DbType();
            switch (dbtype)
            {
                case "mysql": dbType = DbType.MySql; break;
                case "sqlserver": dbType = DbType.SqlServer; break;
                case "postgresql": dbType = DbType.PostgreSQL; break;
                case "sqlite": dbType = DbType.Sqlite; break;
                case "oracle": dbType = DbType.Oracle; break;
            }
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectstring,
                DbType = dbType,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
        }

        public SimpleClient<Entity.TUser> UserDb { get { return new SimpleClient<Entity.TUser>(Db); } }
        public SimpleClient<Entity.TDorm> DormDb { get { return new SimpleClient<Entity.TDorm>(Db); } }
        public SimpleClient<Entity.TLog> LogDb { get { return new SimpleClient<Entity.TLog>(Db); } }
        public SimpleClient<Entity.TBuilding> BuildingDb { get { return new SimpleClient<Entity.TBuilding>(Db); } }
        public SimpleClient<Entity.TRoom> RoomDb { get { return new SimpleClient<Entity.TRoom>(Db); } }
        public SimpleClient<Entity.TNotice> NoticeDb { get { return new SimpleClient<Entity.TNotice>(Db); } }
        public SimpleClient<Entity.TBill> BillDb { get { return new SimpleClient<Entity.TBill>(Db); } }
        public SimpleClient<Entity.TUsed> UsedDb { get { return new SimpleClient<Entity.TUsed>(Db); } }
        public SimpleClient<Entity.TUsed_total> Used_totalDb { get { return new SimpleClient<Entity.TUsed_total>(Db); } }

        
        /// <summary>
        /// 解决MSBUILD : warning CA1001: Microsoft.Design 报错提示
        /// fork from https://blog.csdn.net/unopenmycode/article/details/38311797?hp.com
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                /// 释放前提交更改
                Db.Ado.CommitTran();
                Db.Ado.Close();
                /// 释放各个对象
                Db.Dispose();
            }
        }
        //弥补方法
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // 调用GC释放对象资源
        }

    }
}