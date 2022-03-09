using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using League.Data;
using League.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace League.Pages.Teams
{
    public class IndexModel : PageModel
    {
        private readonly LeagueContext _context;

        public IndexModel(LeagueContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            //Get a list of all Conferences
            var Conferences = GetConferences();
            var AFC = Conferences[0];
            var NFC = Conferences[1];

            //Get all Divisions by Conference and sort them by name
            var afcDivisions = GetDivisionsByConference(AFC.ConferenceId);
            var nfcDivisions = GetDivisionsByConference(NFC.ConferenceId);
        }

        private List<Conference> GetConferences()
        {
            return _context.Conferences.ToList();
        }

        private List<Division> GetDivisionsByConference(string conferenceId)
        {
            var Divisions = _context.Divisions.ToList();

            return Divisions.Where(div => div.ConferenceId.Equals(conferenceId)).OrderBy(div => div.Name).ToList();
        }

        //private List<Team> GetTeamsByDivision(string divisionId)
        //{
        //    var Teams = _context.Teams.ToList();

        //    return Teams.Where
        //}
    }
}
