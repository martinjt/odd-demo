using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using honeycomb_odd.Data;
using honeycomb_odd.Models;

namespace honeycomb_odd.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly honeycomb_odd.Data.SchoolContext _context;

        public IndexModel(honeycomb_odd.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; }

        public async Task OnGetAsync()
        {
            using var span = ActivityHelper.Source.StartActivity("Get Students");
            Student = await _context.Students.ToListAsync();
            span?.AddTag("no_of_students", Student.Count);
        }
    }
}
