using AspnetcoreEcommercedemo.Models.ViewModels;
using AspnetcoreEcommercedemo.Services;
using AspnetcoreEcommercedemo.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Areas.UI.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentApiController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string loginUserId;
        private readonly string role;


        public AppointmentApiController(IAppointmentService appointmentService,
            IHttpContextAccessor httpContextAccessor)
        {
            _appointmentService = appointmentService;
            _httpContextAccessor = httpContextAccessor;
            loginUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }

        [HttpPost]
        [Route("SaveCalendarData")]
        public IActionResult SaveCalendarData(AppointmentVM data)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.status = _appointmentService.AddUpdate(data).Result;
                if (commonResponse.status == 1)
                {
                    commonResponse.message = SD.appointmentUpdated;
                }
                if (commonResponse.status == 2)
                {
                    commonResponse.message = SD.appointmentAdded;
                }
            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = SD.failure_code;
            }

            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetCalendarData")]
        public IActionResult GetCalendarData(string ownerId)
        {
            CommonResponse<List<AppointmentVM>> commonResponse = new CommonResponse<List<AppointmentVM>>();
            try
            {
                if (role == SD.Role_Employee)
                {
                    commonResponse.dataenum = _appointmentService.OwnersEventsById(loginUserId);
                    commonResponse.status = SD.success_code;
                }
                else if (role == SD.Role_Owner)
                {
                    commonResponse.dataenum = _appointmentService.OwnersEventsById(loginUserId);
                    commonResponse.status =SD.success_code;
                }
                else
                {
                    commonResponse.dataenum = _appointmentService.OwnersEventsById(ownerId);
                    commonResponse.status = SD.success_code;
                }
            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = SD.failure_code;
            }
            return Ok(commonResponse);
        }

    }
}
