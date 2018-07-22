﻿using BCrypt.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace sdglsys.DbHelper
{
    /// <summary>
    /// 常用工具集
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// 密码验证
        /// </summary>
        /// <param name="pwd">密码明文</param>
        /// <param name="hashpwd">密码hash</param>
        /// <returns></returns>
        public static bool checkpw(string pwd, string hashpwd)
        {
            return BCrypt.Net.BCrypt.Verify(pwd, hashpwd);
        }

        /// <summary>
        /// 不可逆加密密码或字符串
        /// </summary>
        /// <param name="pwd">密码或字符串</param>
        /// <returns>加密后的密码或字符串hash</returns>
        public static string hashpwd(string pwd)
        {
            return BCrypt.Net.BCrypt.HashPassword(pwd, 4);
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="login_name">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public static Entity.TUser Login(string login_name, string pwd)
        {
            Users u = new Users();
            Entity.TUser user = u.findByLoginName(login_name);
            return (user == null || user.Is_active == false || checkpw(pwd, user.Pwd) == false) ? null : user;
        }

        /// <summary>
        /// 获取App配置信息
        /// </summary>
        /// <param name="key">App配置节点名称(Key)</param>
        /// <param name="type">类型：typeof(string,bool,int)</param>
        /// <returns></returns>
        public static object GetAppSetting(string key, Type type)
        {
            var setting = new AppSettingsReader();
            return setting.GetValue(key, type);
        }

        /// <summary>
        /// Json化对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        /// <summary>
        /// 检查是否为敏感操作
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>false</returns>
        public static bool NeedAudit(string url)
        {
            url.ToLower();
            // 敏感操作列表
            string[] permit_list = {
                "create","edit","delete","pay","createusedinfo","editusedinfo","deleteusedinfo","reset"
            };
            foreach (var item in permit_list)
            {
                if (url.Contains(item))
                    return true;
            }
            return false;
        }

        public static bool OutTrial()
        {
            DateTime trial_end_date;
            DateTime.TryParse("2019-12-30 20:51", out trial_end_date);
            return DateTime.Now >= trial_end_date;
        }

        public static bool IsTrial {
            get {
                return OutTrial()?false:true;
            }
        }
    }
}
