using CrudWithDapper.Context;
using CrudWithDapper.Models;
using CrudWithDapper.Models.ConstantsAndEnums;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CrudWithDapper.Controllers
{
    public class DapperSPController : Controller
    {
        // GET: DapperSP
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetEmployees()
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                var list = DbContext.ExecuteReader<Employee>(Procedures.SP_USP_Select_Employee);
                jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
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
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FirstName", employee.FirstName);
                parameters.Add("@LastName", employee.LastName);
                parameters.Add("@Address", employee.Address);
                parameters.Add("@City", employee.City);

                int rowsEffected = DbContext.ExecuteNonQuery(Procedures.SP_USP_Insert_Employee, parameters);
                jsonResult = Json(rowsEffected, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
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
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);
                var employee = DbContext.ExecuteReader<Employee>(Procedures.SP_USP_Select_Id_Employee, parameters).FirstOrDefault();
                jsonResult = Json(employee, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
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
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", employee.id);
                parameters.Add("@FirstName", employee.FirstName);
                parameters.Add("@LastName", employee.LastName);
                parameters.Add("@Address", employee.Address);
                parameters.Add("@City", employee.City);

                int rowsEffected = DbContext.ExecuteNonQuery(Procedures.SP_USP_Update_Employee, parameters);
                jsonResult = Json(rowsEffected, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
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
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);
                int rowsEffected = DbContext.ExecuteNonQuery(Procedures.SP_USP_Delete_Id_Employee, parameters);
                jsonResult = Json(rowsEffected, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                jsonResult = Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            return jsonResult;
        }
    }
}