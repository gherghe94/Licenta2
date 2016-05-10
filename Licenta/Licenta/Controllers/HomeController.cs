using Licenta.Models.DTO;
using Licenta.Services.Implementation;
using Licenta.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licenta.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// This WORKED!
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetX(int x)
        {
            var x2 = x * x * 2;
            return Json(new { X = x2 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult TestPost(TestDto testDto)
        {
            testDto.Name = "Server filtered!";
            return Json(testDto);
        }

        public ActionResult Index()
        {
            return Json(new { Ceva = "c" });
        }

        [HttpGet]
        public ActionResult TestDb()
        {
            IStudentService studentService = new StudentService();
            var list = studentService.GetAll();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
