using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using League.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace League.Pages.Players
{
    public class IndexModel : PageModel
    {
        private readonly LeagueContext _context;

        public IndexModel(LeagueContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }
    }
}
