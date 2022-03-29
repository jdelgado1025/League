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
        public string SortField { get; set; } = "Name";

        public SelectList AllTeams { get; set; }
        public SelectList AllPositions { get; set; }

        public string Favorite { get; set; }

        public async Task OnGetAsync()
        {
            // Generic retrieve all players
            var players = from p in _context.Players
                          select p;

            // Modify query based on filters
            if (!string.IsNullOrEmpty(SelectedTeam))
            {
                players = players.Where(p => p.TeamId == SelectedTeam);
            }

            if (!string.IsNullOrEmpty(SelectedPosition))
            {
                players = players.Where(p => p.Position == SelectedPosition);
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                players = players.Where(p => p.Name.Contains(SearchString));
            }

            switch (SortField)
            {
                case "Number":
                    players = players.OrderBy(p => p.Number);
                    break;
                case "Name":
                    players = players.OrderBy(p => p.Name);
                    break;
                case "Position":
                    players = players .OrderBy(p => p.Position);
                    break;
            }

            //Populate the Teams Select List
            var teamIds = from tms in _context.Teams
                          orderby tms.TeamId
                          select tms.TeamId;

            AllTeams = new SelectList(await teamIds.ToListAsync());

            //Populate the Position Select List using distinct values
            var playerPos = (from p in _context.Players
                             select p.Position).Distinct();
                            

            AllPositions = new SelectList(await playerPos.ToListAsync());

            //Set favorite if cookie exists
            string favoriteCookieSession = HttpContext.Session.GetString(SessionKeyFavorite);

            if (!string.IsNullOrEmpty(favoriteCookieSession))
            {
                Favorite = favoriteCookieSession;
            }

            Players = await players
                .Include(p => p.Team)
                .ToListAsync();

        }
    }
}
