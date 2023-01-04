using AspnetcoreEcommercedemo.Services;
using AspnetcoreEcommercedemo.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Areas.UI.Controllers
{
    [Area("UI")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        public IActionResult Index()
        {
            ViewBag.Duration = SD.GetTimeDropDown();
            ViewBag.OwnerList = _appointmentService.GetOwnerList();
            ViewBag.CustomerList = _appointmentService.GetCustomerList();
            return View();
        }
    }
}
