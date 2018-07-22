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
    public class DormController : Controller
    {
        // GET: Dorm
        [IsAdmin]
        public ActionResult Index()
        {
            string keyword = "";
            var d = new Dorms();
            ViewBag.dorms = d.getAll(); // 获取所有园区
            int pageIndex = 1;
            int pageSize = 10;
            try
            {
                keyword = Request["keyword"]; // 搜索关键词
                pageIndex = Convert.ToInt32(Request["pageIndex"]); if (pageIndex < 1) pageIndex = 1;
                pageSize = Convert.ToInt32(Request["pageSize"]); if (pageSize > 99 || pageSize < 1) pageSize = 10;
            }
            finally
            {
            }
            
            int count = 0;
            ViewBag.keyword = keyword;
            ViewBag.dorms = d.getByPages(pageIndex, pageSize, ref count, keyword); // 获取列表
            ViewBag.count = count;  // 获取当前页数量
            ViewBag.pageIndex = pageIndex;  // 获取当前页
            return View();
        }

        // GET: Dorm/Details/5
        [IsAdmin]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dorm/Create
        [NeedLogin]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dorm/Create
        /// <summary>
        /// 添加园区
        /// </summary>
        /// <param name="collection"></param>
        [HttpPost]
        [IsAdmin]
        public void Create(FormCollection collection)
        {
            var msg = new Msg();
            try
            {
                // 初始化对象
                Entity.TDorm dorm = new Entity.TDorm()
                {
                    Nickname = collection["name"],
                    Note = collection["note"],
                    Type = Convert.ToBoolean(Convert.ToInt32(collection["type"])),
                };
                var Dorm = new Dorms();
                if (Dorm.Add(dorm))
                {
                    msg.msg = "添加成功！";
                }
                else
                {
                    msg.msg = "发生未知错误，添加失败！";
                    msg.code = 500;
                }
            }
            catch (Exception ex)
            {
                msg.code = 500;
                msg.msg = ex.Message;
            }
            finally
            {
                Response.Write(msg.ToJson());
                Response.End();
            }
        }

        // GET: Dorm/Edit/5
        [IsAdmin]
        public ActionResult Edit(int id)
        {
            var Dorm = new Dorms();
            return View(Dorm.findById(id));
        }

        // POST: Dorm/Edit/5
        [HttpPost]
        [IsAdmin]
        public void Edit(int id, FormCollection collection)
        {
            var msg = new Msg();
            var Dorm = new Dorms();
            var dorm = Dorm.findById(id);
            if (dorm == null)
            {
                msg.msg = "该园区不存在";
                msg.code = 404;
            }
            else
            {
                try
                {
                    dorm.Nickname = collection["name"];
                    dorm.Note = collection["note"];
                    dorm.Is_active = Convert.ToBoolean(collection["is_active"]);
                    dorm.Type = Convert.ToBoolean(Convert.ToInt32(collection["type"]));
                    if (Dorm.Update(dorm))
                    {
                        msg.msg = "保存成功！";
                    }
                    else
                    {
                        msg.msg = "发生未知错误，保存失败！";
                        msg.code = 500;
                    }
                }
                catch (Exception ex)
                {
                    msg.code = 500;
                    msg.msg = "发生错误："+ex.Message;
                }
                finally
                {
                    Response.Write(msg.ToJson());
                    Response.End();
                }
            }
        }

        // GET: Dorm/Delete/5
        [IsAdmin]
        public void Delete(int id)
        {
            var msg = new Msg();
            var User = new Dorms();
            var user = User.findById(id);
            if (user == null)
            {
                msg.msg = "该园区不存在！";
                msg.code = 404;
            }
            else
            {
                if (User.Delete(id))
                {
                    msg.msg = "删除成功！";
                }
                else
                {
                    msg.msg = "发生未知错误，删除失败！";
                }
            }
            Response.Write(msg.ToJson());
            Response.End();
        }

        // POST: Dorm/Delete/5
        [HttpPost]
        [IsAdmin]
        public void Delete(int id, FormCollection collection)
        {
            Delete(id);
        }

        public void List() {
            var db = new Dorms().Db;
            Response.Write(db.Queryable<TDorm>().Where(d=>d.Is_active==true).ToJson());
        }
    }
}
