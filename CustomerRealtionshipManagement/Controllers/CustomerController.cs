using CustomerRealtionshipManagement.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace CustomerRealtionshipManagement.Controllers
{
    public class CustomerController : Controller
    {

        List<string> list = new List<string>();
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
            list.Add(cust.PhoneNumber);
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
        public IActionResult Checking1(string phoneNumber)
        {
            bool isPhoneNumberPresent = !string.IsNullOrWhiteSpace(phoneNumber) && CheckPhoneNumberPresence(phoneNumber);
            ViewData["IsPhoneNumberPresent"] = isPhoneNumberPresent;

            return View("Checking");
        }

        private bool CheckPhoneNumberPresence(string phoneNumber)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                return list.Contains(phoneNumber);
            }
            return false;
        }
    }
}
