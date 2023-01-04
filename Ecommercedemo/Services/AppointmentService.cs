using AspnetcoreEcommercedemo.DataAccess.Data;
using AspnetcoreEcommercedemo.Models;
using AspnetcoreEcommercedemo.Models.ViewModels;
using AspnetcoreEcommercedemo.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;

        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddUpdate(AppointmentVM model)
        {
            var startDate = DateTime.Parse(model.StartDate);
            var endDate = DateTime.Parse(model.StartDate).AddMinutes(Convert.ToDouble(model.Duration));

            if (model != null && model.Id > 0)
            {
                //update
                var appointment = _context.Appointments.FirstOrDefault(x => x.Id == model.Id);
                appointment.Title = model.Title;
                appointment.Description = model.Description;
                appointment.StartDate = startDate;
                appointment.EndDate = endDate;
                appointment.Duration = model.Duration;
                appointment.OwnerId = model.OnwerId;
                appointment.CustomerId = model.CustomerId;
                appointment.IsOwnerApproved = false;
                appointment.AdminId = model.AdminId;
                await _context.SaveChangesAsync();
                return 1;
            }
            else
            {
                //create
                Appointment appointment = new Appointment()
                {
                    Title = model.Title,
                    Description = model.Description,
                    StartDate = startDate,
                    EndDate = endDate,
                    Duration = model.Duration,
                    OwnerId = model.OnwerId,
                    CustomerId = model.CustomerId,
                    IsOwnerApproved = false,
                    AdminId = model.AdminId
                };

                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();
                return 2;
            }
        }

        public async Task<int> ConfirmEvent(int id)
        {
            var appointment = _context.Appointments.FirstOrDefault(x => x.Id == id);
            if (appointment != null)
            {
                appointment.IsOwnerApproved = true;
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Delete(int id)
        {
            var appointment = _context.Appointments.FirstOrDefault(x => x.Id == id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public List<AppointmentVM> OwnersEventsById(string ownerId)
        {
            return _context.Appointments.Where(x => x.OwnerId == ownerId).ToList().Select(c => new AppointmentVM()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsOwnerApproved = c.IsOwnerApproved
            }).ToList();
        }

        public AppointmentVM GetById(int id)
        {
            return _context.Appointments.Where(x => x.Id == id).ToList().Select(c => new AppointmentVM()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsOwnerApproved = c.IsOwnerApproved,
                CustomerId = c.CustomerId,
                OnwerId = c.OwnerId,
                CustomerName = _context.Users.Where(x => x.Id == c.CustomerId).Select(x => x.UserName).FirstOrDefault(),
                OwnerName = _context.Users.Where(x => x.Id == c.OwnerId).Select(x => x.UserName).FirstOrDefault(),
            }).SingleOrDefault();
        }

        public List<OwnerVM> GetOwnerList()
        {
            var owners = (from user in _context.Users
                          join userRoles in _context.UserRoles on user.Id equals userRoles.UserId
                          join roles in _context.Roles.Where(x => x.Name == SD.Role_Owner) on userRoles.RoleId equals roles.Id
                          select new OwnerVM
                          {
                              Id = user.Id,
                              Name = user.UserName
                          }).ToList();
            return owners;
        }

        public List<CustomerVM> GetCustomerList()
        {
            var customers = (from user in _context.Users
                             join userRoles in _context.UserRoles on user.Id equals userRoles.UserId
                             join roles in _context.Roles.Where(x => x.Name == SD.Role_User) on userRoles.RoleId equals roles.Id
                             select new CustomerVM
                             {
                                 Id = user.Id,
                                 Name = user.UserName
                             }
                             ).ToList();
            return customers;
        }


        public List<AppointmentVM> CustomerEventsById(string customerId)
        {
            return _context.Appointments.Where(x => x.CustomerId == customerId).ToList().Select(c => new AppointmentVM()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsOwnerApproved = c.IsOwnerApproved
            }).ToList();
        }



    }
}
