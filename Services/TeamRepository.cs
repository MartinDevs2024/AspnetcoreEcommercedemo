using AspnetcoreEcommercedemo.Data;
using AspnetcoreEcommercedemo.Interfaces;
using AspnetcoreEcommercedemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Services
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _context;

        public TeamRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddTeam(Team team)
        {
            _context.Teams.Add(team);
        }

        public List<Team> GetAllTeams()
        {
            return _context.Teams.ToList();
        }

        public Team GetTeam(int id)
        {
            return _context.Teams.Find(id);
        }

        public void RemoveTeam(int id)
        {
            _context.Teams.Remove(GetTeam(id));
        }


        public void UpdateTeam(Team team)
        {
            _context.Teams.Update(team);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
