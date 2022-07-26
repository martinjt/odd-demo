using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using honeycomb_odd.Data;
using honeycomb_odd.Models;

namespace honeycomb_odd.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly honeycomb_odd.Data.SchoolContext _context;

        public CreateModel(honeycomb_odd.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Student = new Student { EnrollmentDate = DateTime.Now, FirstMidName = "Joe", LastName = "Smith" };
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("OnPost");
            foreach (var error in ModelState)
            {
                if (error.Value.Errors != null && error.Value.Errors.Any())
                    Console.WriteLine($"{error.Key}:{error.Value.Errors?[0].ErrorMessage}");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Students.Add(Student);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
