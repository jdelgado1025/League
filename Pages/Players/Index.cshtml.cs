using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using League.Data;
using League.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace League.Pages.Players
{
    public class IndexModel : PageModel
    {
        private readonly LeagueContext _context;

        public IndexModel(LeagueContext context)
        {
            _context = context;
        }

        public List<Player> Players { get; set; }

        public SelectList AllTeams { get; set; }
        public SelectList AllPositions { get; set; }
        public SelectList SortBy { get; set; }

        public async Task OnGetAsync()
        {
        }
    }
}
