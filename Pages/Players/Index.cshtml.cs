using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using League.Data;
using League.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace League.Pages.Players
{
    public class IndexModel : PageModel
    {
        private readonly LeagueContext _context;

        public IndexModel(LeagueContext context)
        {
            _context = context;
        }

        public const string SessionKeyFavorite = "_Favorite";
        public List<Player> Players { get; set; }

        [BindProperty (SupportsGet = true)]
        public string SelectedTeam { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SelectedPosition { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortField { get; set; }

        public SelectList AllTeams { get; set; }
        public SelectList AllPositions { get; set; }
        public SelectList SortBy { get; set; }

        public string Favorite { get; set; }

        public async Task OnGetAsync()
        {
            Players = await _context.Players.ToListAsync();

            var teamIds = from tms in _context.Teams
                          orderby tms.TeamId
                          select tms.TeamId;

            AllTeams = new SelectList(await teamIds.ToListAsync());

            var playerPos = (from p in _context.Players
                             select p.Position).Distinct();
                            

            AllPositions = new SelectList(await playerPos.ToListAsync());

            //string favoriteCookieSession = HttpContext.Session.GetString(SessionKeyFavorite);

            //if (!string.IsNullOrEmpty(favoriteCookieSession))
            //{
            //    Favorite = favoriteCookieSession;
            //}
                            
        }
    }
}
