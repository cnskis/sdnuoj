﻿using System;
using System.Web.Mvc;

using SDNUOJ.Controllers.Core;
using SDNUOJ.Entity;
using SDNUOJ.Utilities;

namespace SDNUOJ.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdministrator")]
    public class UserController : AdminBaseController
    {
        /// <summary>
        /// 用户列表页面
        /// </summary>
        /// <param name="id">页面索引</param>
        /// <param name="order">排序类型</param>
        /// <param name="names">用户名包含</param>
        /// <param name="nickname">昵称包含</param>
        /// <param name="email">邮箱包含</param>
        /// <param name="school">学校包含</param>
        /// <param name="lastip">最后登录IP包含</param>
        /// <param name="islocked">是否锁定</param>
        /// <param name="regstartdate">注册时间范围起始</param>
        /// <param name="regenddate">注册时间范围结束</param>
        /// <param name="loginstartdate">最后登陆时间起始</param>
        /// <param name="loginenddate">最后登陆时间结束</param>
        /// <returns>操作后的结果</returns>
        public ActionResult List(Int32 id = 1, String order = "",
            String names = "", String nickname = "", String email = "", String school = "", String lastip = "", String islocked = "",
            String regstartdate = "", String regenddate = "", String loginstartdate = "", String loginenddate = "")
        {
            PagedList<UserEntity> list = UserManager.AdminGetUserList(id,
                names, nickname, email, school, lastip, islocked,
                regstartdate, regenddate, loginstartdate, loginenddate, order);

            ViewBag.Order = order;
            ViewBag.Names = names;
            ViewBag.NickName = nickname;
            ViewBag.Email = email;
            ViewBag.School = school;
            ViewBag.LastIP = lastip;
            ViewBag.IsLocked = islocked;
            ViewBag.RegStartDate = regstartdate;
            ViewBag.RegEndDate = regenddate;
            ViewBag.LoginStartDate = loginstartdate;
            ViewBag.LoginEndDate = loginenddate;

            return ViewWithPager(list, id);
        }

        /// <summary>
        /// 用户详细信息页面
        /// </summary>
        /// <param name="id">用户名</param>
        /// <returns>操作后的结果</returns>
        public ActionResult Detail(String id)
        {
            return ResultToView(UserManager.AdminGetUserInfo, id);
        }

        /// <summary>
        /// 用户锁定
        /// </summary>
        /// <param name="ids">用户名</param>
        /// <returns>操作后的结果</returns>
        public ActionResult Lock(String ids)
        {
            return ResultToJson(UserManager.AdminUpdateUserIsLocked, ids, true);
        }

        /// <summary>
        /// 用户解锁
        /// </summary>
        /// <param name="ids">用户名</param>
        /// <returns>操作后的结果</returns>
        public ActionResult Unlock(String ids)
        {
            return ResultToJson(UserManager.AdminUpdateUserIsLocked, ids, false);
        }

        /// <summary>
        /// 用户统计计算
        /// </summary>
        /// <param name="id">用户名</param>
        /// <returns>操作后的结果</returns>
        public ActionResult Recalculate(String id)
        {
            return ResultToJson(
                UserManager.AdminUpdateSolvedCount, 
                UserManager.AdminUpdateSubmitCount, 
                id);
        }

        /// <summary>
        /// 当前在线用户页面
        /// </summary>
        /// <returns>操作后的结果</returns>
        public ActionResult Online()
        {
            return ResultToView(UserManager.AdminGetOnlineUserNames);
        }

        /// <summary>
        /// 用户密码修改页面
        /// </summary>
        /// <param name="id">用户名</param>
        /// <returns>操作后的结果</returns>
        public ActionResult ChangePassword(String id)
        {
            ViewBag.UserName = id;

            return View();
        }

        /// <summary>
        /// 用户密码修改
        /// </summary>
        /// <param name="form">Form集合</param>
        /// <returns>操作后的结果</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(FormCollection form)
        {
            return ResultToMessagePage(UserManager.AdminResetUserPassword, form["username"], form["newpassword"], "Your have updated user password successfully!");
        }

        /// <summary>
        /// 有特殊权限的用户列表页面
        /// </summary>
        /// <param name="id">页面索引</param>
        /// <returns>操作后的结果</returns>
        public ActionResult PermissionList(Int32 id = 1)
        {
            PagedList<UserEntity> list = UserManager.AdminGetPermissionUsers(id);

            return ViewWithPager(list, id);
        }

        /// <summary>
        /// 用户权限修改页面
        /// </summary>
        /// <param name="id">用户名</param>
        /// <returns>操作后的结果</returns>
        public ActionResult PermissionEdit(String id)
        {
            ViewBag.UserName = id;

            return View();
        }

        /// <summary>
        /// 用户权限修改
        /// </summary>
        /// <param name="form">Form集合</param>
        /// <returns>操作后的结果</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PermissionEdit(FormCollection form)
        {
            return ResultToMessagePage(UserManager.AdminUpdatePermision, form["username"], form["permission"], "Your have updated user permission successfully!");
        }
    }
}