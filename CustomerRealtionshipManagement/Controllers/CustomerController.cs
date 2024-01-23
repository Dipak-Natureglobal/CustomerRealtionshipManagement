using CustomerRealtionshipManagement.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CustomerRealtionshipManagement.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View(DapperORM.ReturnList<Customer>("Customerviewall"));
        }

        // ...Employee/AddorEdit/id=0
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View();
            else
            {
                DynamicParameters para = new DynamicParameters();
                para.Add("@CustomerID", id);
                return View(DapperORM.ReturnList<Customer>("CustomerViewByID", para).FirstOrDefault<Customer>());
            }
        }
        [HttpPost]
        public ActionResult AddorEdit(Customer cust)
        {

            DynamicParameters para = new DynamicParameters();
            para.Add("CustomerID", cust.CustomerID);
            para.Add("Name", cust.Name);
            para.Add("PhoneNumber", cust.PhoneNumber);
            para.Add("Question", cust.Question);
            DapperORM.ExecuteWithoutReturn("CustomerAddOrEdit", para);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            DynamicParameters para = new DynamicParameters();
            para.Add("@CustomerID", id);
            DapperORM.ExecuteWithoutReturn("CustomerDeleteByID", para);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult AddQuestion(int id = 0)
        {
            if (id == 0)
                return View();
            else
            {
                DynamicParameters para = new DynamicParameters();
                para.Add("@CustomerID", id);
                var customer = DapperORM.ReturnList<Customer>("CustomerViewByID", para).FirstOrDefault<Customer>();
                if (customer != null)
                {
                    customer.Question = " ";
                }
                return View(customer);
            }
        }

        [HttpPost]
        public ActionResult AddQuestion(Customer cust)
        {
            DynamicParameters para = new DynamicParameters();
            para.Add("CustomerID", cust.CustomerID);
            para.Add("Question", cust.Question);
            DapperORM.ExecuteWithoutReturn("CustomerAddQuestion", para);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Checking1(Customer cust)
        {
            DynamicParameters para = new DynamicParameters();
            para.Add("@PhoneNumber", cust.PhoneNumber);
            para.Add("@Result", 0,DbType.Int32,ParameterDirection.Output);

            int Result = DapperORM.ExecuteReturnScalarInt("CheckPhoneNumber", para);
            bool isPhoneNumberPresent;
            if (Result > 0)
            {
                isPhoneNumberPresent = true;
            }
            else
            {
                isPhoneNumberPresent = false;
            }
            ViewData["IsPhoneNumberPresent"] = isPhoneNumberPresent;
            ViewData["PhoneNumber"] = cust.PhoneNumber;
            return View("Checking");
        }
        [HttpGet]
        public ActionResult Checking2(Customer cust)
        {
            DynamicParameters para = new DynamicParameters();
            para.Add("@Name", cust.Name);
            para.Add("@Result", 0, DbType.Int32, ParameterDirection.Output);

            int Result = DapperORM.ExecuteReturnScalarInt("CustomerCheckName", para);
            bool isName;
            if (Result > 0)
            {
                isName = true;
            }
            else
            {
                isName = false;
            }
            ViewData["IsName"] = isName;
            ViewData["Name"] = cust.Name;
            return View("CheckingName");
        }
        [HttpGet]
        public  ActionResult PhoneView(string PhoneNumber)
        {
            DynamicParameters para = new DynamicParameters();
            para.Add("@PhoneNumber", PhoneNumber);
         
            return View(DapperORM.ReturnList<Customer>("CustomerViewByPhoneNumber", para));
        }

        [HttpGet]
        public ActionResult NameView(string Name)
        {
            DynamicParameters para = new DynamicParameters();
            para.Add("@Name", Name);

            return View(DapperORM.ReturnList<Customer>("CustomerViewByName", para));
        }

    }
}
