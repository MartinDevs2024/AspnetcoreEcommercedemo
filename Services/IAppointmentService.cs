using AspnetcoreEcommercedemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Services
{
    public interface IAppointmentService
    {
        public List<OwnerVM> GetOwnerList();
        public List<CustomerVM> GetCustomerList();

        public Task<int> AddUpdate(AppointmentVM model);

        public List<AppointmentVM> OwnersEventsById(string ownerId);

        public List<AppointmentVM> CustomerEventsById(string customerId);

        public AppointmentVM GetById(int id);

        public Task<int> Delete(int id);
        public Task<int> ConfirmEvent(int id);
    }
}
