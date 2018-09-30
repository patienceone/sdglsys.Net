﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sdglsys.DbHelper;
using sdglsys.Entity;
using sdglsys.Web;
namespace sdglsys.Web.Controllers
{
    public class BillController : Controller
    {
        // GET: Used
        [NotLowUser]
        public ActionResult Index()
        {
            /// #trial
            if (!XUtils.IsTrial)
            {
                Response.Write("非常抱歉地提示您，您可能未经授权就使用了我的程序，或者该程序已到期，已经无法使用，现在是：" + DateTime.Now + "<br/>如有任何疑问，请联系QQ：1278386874");
                Response.End();
            }

            string keyword = ""; // 关键词
            int page = 1; // 当前页数
            int limit = 10; // 每页显示数量
            int stat = -1;  // 账单状态
            int count = 0;  // 结果数量
            try
            {
                keyword = Request["keyword"]; // 搜索关键词
                page = Convert.ToInt32(Request["page"]); if (page < 1) page = 1;
                limit = Convert.ToInt32(Request["limit"]); if (limit > 99 || limit < 1) limit = 10;
                stat = Convert.ToInt16(Request["stat"]);
            }
            catch
            { }
            var bills = new Bills();
            if ((int) Session["role"] < 3)
            {
                ViewBag.bills = bills.getByPagesByDormId((int) Session["pid"], page, limit, ref count, keyword, (short) stat); // 获取列表
            }
            else
            {
                ViewBag.bills = bills.getByPages(page, limit, ref count, keyword, (short) stat); // 获取列表
            }
            ViewBag.stat = stat;
            ViewBag.keyword = keyword;
            ViewBag.count = count;  // 当前页数量
            ViewBag.page = page;  // 获取当前页

            return View();
        }

        /// <summary>
        /// 创建一个账单
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        [NotLowUser]
        public ActionResult Create()
        {
            var Building = new Buildings();
            ViewBag.buildings = (int) Session["role"] < 3 ? Building.getAllActiveById((int) Session["pid"]) : Building.getAllActive();
            return View();
        }


        // POST: Used/Create
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="collection"></param>
        [HttpPost]
        [NeedLogin]
        public void Create(FormCollection collection)
        {
            var msg = new Msg();
            msg.code = 400;
            var Db = new DbContext().Db;
            var rate = Db.Queryable<TRate>().OrderBy(r => r.Id, SqlSugar.OrderByType.Desc).First();
            var quota = Db.Queryable<TQuota>().OrderBy(r => r.Id, SqlSugar.OrderByType.Desc).First();

            try
            {// 验证费率信息
                if (rate == null)
                {
                    throw new Exception("请先设置费率信息");
                }
                ///1.获取数据
                var cold_water_value = Convert.ToSingle(collection["cold_water_value"]);
                var hot_water_value = Convert.ToSingle(collection["hot_water_value"]);
                var electric_value = Convert.ToSingle(collection["electric_value"]);
                ///1.1判断输入数值
                if (cold_water_value < 0 || hot_water_value < 0 || electric_value < 0)
                {
                    throw new Exception("数值输入有误，费用不能小于0");
                }
                var Dorm_id = Convert.ToInt32(collection["dorm_id"]); // 园区ID
                var Building_id = Convert.ToInt32(collection["building_id"]); // 宿舍楼ID
                var Pid = Convert.ToInt32(collection["pid"]); // 宿舍ID
                var Note = collection["note"];
                if (Dorm_id < 0 || Building_id < 0 || Pid < 0)
                {
                    throw new Exception("宿舍ID或宿舍楼ID或园区ID输入有误");
                }
                if (Note.Trim().Length < 3)
                {
                    throw new Exception("手动添加账单时请输入至少3个字符作为说明");
                }
                /// 生成一个虚拟用量登记
                var used = new TUsed()
                {
                    Pid = Pid,
                    Note = "手动添加账单后自动生成的登记信息，并不会更新读表数据",
                    Building_id = Building_id,
                    Dorm_id = Dorm_id,
                    Post_uid = (int) Session["id"],
                };
                /// 开始事务
                Db.Ado.BeginTran();
                var u = Db.Insertable(used).ExecuteReturnIdentity();
                /// 生成账单
                var bill = new TBill()
                {
                    Note = Note,
                    Pid = u,
                    Room_id = Pid,
                    Building_id = Building_id,
                    Dorm_id = Dorm_id,
                    Cold_water_cost = (decimal) cold_water_value,
                    Electric_cost = (decimal) hot_water_value,
                    Hot_water_cost = (decimal) electric_value,
                    Rates_id = rate.Id,
                    Quota_id = quota.Id,
                };
                /// 保存账单
                if (Db.Insertable(bill).ExecuteCommand() < 1)
                {
                    throw new Exception("添加账单信息时发生错误！");
                }

                Db.Ado.CommitTran();// 提交事务
                msg.code = 200;
                msg.msg += "添加成功！";
            }
            catch (Exception ex)
            {
                //发生错误，回滚事务
                Db.Ado.RollbackTran();
                msg.code = 500;
                msg.msg += ex.Message;
            }
            Response.Write(msg.ToJson());
            Response.End();
        }

        /// <summary>
        /// 删除账单
        /// </summary>
        /// <param name="id"></param>
        // GET: Used/Delete/5
        [NotLowUser]

        public void Delete(int id)
        {
            var msg = new Msg();
            var Db = new DbContext().Db;
            try
            {
                ///0.开始事务
                Db.Ado.BeginTran();
                ///1.获取记录数据
                var bill = Db.Ado.Context.Queryable<Entity.TBill>().Where(b=>b.Id==id).First();
                if (bill == null)
                {
                    throw new Exception("该账单已被删除");
                }
                Db.Ado.Context.Deleteable(bill).ExecuteCommand();
                Db.Ado.CommitTran();// 提交事务
                msg.msg = "删除成功！";
            }
            catch (Exception ex)
            {
                Db.Ado.RollbackTran();//发生错误，回滚操作
                msg.code = 500;
                msg.msg = ex.Message;
            }
            Response.Write(msg.ToJson());
            Response.End();
        }

        // POST: Used/Delete/5
        [HttpPost]
        [NotLowUser]
        public void Delete(int id, FormCollection collection)
        {
            this.Delete(id);
        }

        /// <summary>
        /// 修改账单信息
        /// </summary>
        /// <param name="id"></param>
        // GET: Used/Delete/5
        [NotLowUser]
        [HttpPost]
        public void Edit(int id, FormCollection collection)
        {
            var msg = new Msg();
            msg.code = 400;
            var Db = new DbContext().Db;
            try
            {
                var bill = Db.Queryable<TBill>().Where(b => b.Id == id).First();
                if (bill == null)
                {
                    msg.code = 404;
                    throw new Exception("该账单信息不存在！");
                }
                ///1.获取数据
                var cold_water_cost = Convert.ToDecimal(collection["cold_water_cost"]);
                var hot_water_cost = Convert.ToDecimal(collection["hot_water_cost"]);
                var electric_cost = Convert.ToDecimal(collection["electric_cost"]);
                ///1.1判断输入数值
                if (cold_water_cost < 0 || hot_water_cost < 0 || electric_cost < 0)
                {
                    throw new Exception("数值输入有误，费用不能小于0");
                }
                var Dorm_id = Convert.ToInt32(collection["dorm_id"]); // 园区ID
                var Building_id = Convert.ToInt32(collection["building_id"]); // 宿舍楼ID
                var Pid = Convert.ToInt32(collection["pid"]); // 宿舍ID
                var Note = collection["note"];
                var stat = Convert.ToInt16(collection["stat"]); // 账单状态
                if (Dorm_id < 0 || Building_id < 0 || Pid < 0)
                {
                    throw new Exception("宿舍ID或宿舍楼ID或园区ID输入有误");
                }
                if (Note.Trim().Length < 3)
                {
                    throw new Exception("修改账单时请输入至少3个字符作为说明");
                }
                ///修改账单数值
                bill.Cold_water_cost = cold_water_cost;
                bill.Electric_cost = hot_water_cost;
                bill.Hot_water_cost = electric_cost;
                bill.Note = Note;
                bill.Is_active = stat;
                Db.Ado.BeginTran();
                ///7.2保存账单
                if (Db.Updateable(bill).ExecuteCommand() < 1)
                {
                    throw new Exception("保存账单信息时发生错误！");
                }
                Db.Ado.CommitTran();// 提交事务
                msg.code = 200;
                msg.msg = "保存成功！";
            }
            catch (Exception ex)
            {
                //发生错误，回滚事务
                Db.Ado.RollbackTran();
                msg.code = 500;
                msg.msg = "修改账单时发生错误：" + ex.Message;
            }
            Response.Write(msg.ToJson());
            Response.End();
        }

        /// <summary>
        /// 修改账单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [NotLowUser]
        public ActionResult Edit(int id)
        {
            return View(new Bills().FindVBillById(id));
        }

        /// <summary>
        /// 结算该账单，设置账单为已付款状态
        /// </summary>
        /// <param name="id"></param>
        [NotLowUser]
        public void Pay(int id)
        {
            var msg = new Msg();
            try
            {
                var Db = new Bills().BillDb;
                var bill = Db.GetById(id);
                if (bill == null)
                {
                    msg.code = 404;
                    throw new Exception("该账单不存在");
                }
                bill.Is_active = 2;
                if (Db.Update(bill))
                {
                    msg.msg = "结算成功！";
                }
                else
                {
                    throw new Exception("发生未知错误！");
                }
            }
            catch (Exception ex)
            {
                msg.code = 500;
                msg.msg = "结算账单时发生错误：" + ex.Message;
            }
            Response.Write(msg.ToJson());
            Response.End();
        }
    }
}
