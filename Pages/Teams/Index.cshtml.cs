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

namespace League.Pages.Teams
{
    public class IndexModel : PageModel
    {
        private readonly LeagueContext _context;

        public IndexModel(LeagueContext context)
        {
            _context = context;
        }

        public List<Conference> Conferences { get; set; }
        public List<Division> Divisions { get; set; }
        public List<Team> Teams { get; set; }
        public const string SessionKeyFavorite = "_Favorite";

        public SelectList AllTeams { get; set; }
        [BindProperty (SupportsGet = true)]
        public string FavoriteTeam { get; set; }

        public async Task OnGetAsync()
        {
            //Get a list of all Conferences
            Conferences = await _context.Conferences.ToListAsync();
            //List of all Divisions
            Divisions = await _context.Divisions.ToListAsync();
            //List of all Teams
            Teams = await _context.Teams.ToListAsync();

            var teamIds = from tm in _context.Teams
                          orderby tm.TeamId
                          select tm.TeamId;

            //Populate from all Team ID's
            AllTeams = new SelectList(await teamIds.ToListAsync());

            //Checking for new favorite on each page load and saving to the session cookie
            if (!string.IsNullOrEmpty(FavoriteTeam))
            {
                HttpContext.Session.SetString(SessionKeyFavorite, FavoriteTeam);
            }
            //Otherwise get the old favorite or null
            else
            {
                FavoriteTeam = HttpContext.Session.GetString(SessionKeyFavorite);
            }
        }

        //All Divisions by Conference sorted by Name
        public List<Division> GetDivisionsByConference(string ConferenceId)
        {
            return Divisions.Where(div => div.ConferenceId.Equals(ConferenceId)).OrderBy(div => div.Name).ToList();
        }

        //All Teams by Division and sorted by Win and Loss
        public List<Team> GetTeamsByDivision(string DivisionId)
        {
            return Teams.Where(t => t.DivisionId.Equals(DivisionId)).OrderByDescending(t => t.Win).ToList();
        }
    }
}
