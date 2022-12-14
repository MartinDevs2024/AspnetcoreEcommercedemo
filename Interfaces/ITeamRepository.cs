using AspnetcoreEcommercedemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Interfaces
{
    public interface ITeamRepository
    {
        Team GetTeam(int id);
        List<Team> GetAllTeams();
        void AddTeam(Team team);
        void UpdateTeam(Team team);
        void RemoveTeam(int id);
        Task<bool> SaveChangesAsync();
    }
}
