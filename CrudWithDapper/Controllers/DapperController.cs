using CrudWithDapper.Context;
using CrudWithDapper.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudWithDapper.Controllers
{
    public class DapperController : Controller
    {
        // GET: Dapper
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetEmployees()
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                List<Employee> list = DbContext.db.Query<Employee>("SELECT * from Employee").ToList();
                jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                jsonResult = Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            return jsonResult;
        }

        [HttpPost]
        public ActionResult SaveEmployee(Employee employee)
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                var query = "Insert into Employee(FirstName, LastName, Address, City) values(@FirstName, @LastName, @Address, @City) ";
                int rowsEffected = DbContext.db.Execute(query, employee);
                jsonResult = Json(rowsEffected, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                jsonResult = Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            return jsonResult;
        }

        public ActionResult GetEmployee(int? id)
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                var employee = DbContext.db.Query<Employee>("select * from Employee where id =" + id, new { id }).FirstOrDefault();
                jsonResult = Json(employee, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                jsonResult = Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            return jsonResult;
        }

        [HttpPost]
        public ActionResult UpdateEmployee(Employee employee)
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                var query = "update Employee set FirstName = @FirstName, LastName = @LastName, Address = @Address, City = @City where id = @id";
                int rowsEffected = DbContext.db.Execute(query, employee);
                jsonResult = Json(rowsEffected, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                jsonResult = Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            return jsonResult;
        }

        [HttpPost]
        public ActionResult DeleteEmployee(int? id)
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                var query = "Delete from employee where id = " + id;
                int rowsEffected = DbContext.db.Execute(query, id);
                jsonResult = Json(rowsEffected, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                jsonResult = Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            return jsonResult;
        }
    }
}