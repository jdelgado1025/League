using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using League.Data;
using League.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace League.Pages.Teams
{
    public class TeamModel : PageModel
    {
        private readonly LeagueContext _context;

        public TeamModel(LeagueContext context)
        {
            _context = context;
        }

        public Team Team { get; set; }
        public Division Division { get; set; }

        public async Task OnGetAsync(string id)
        {
            Team = await _context.Teams
                .Include(t => t.Division)
                .Include(t => t.Players)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TeamId == id);
        }
    }
}
