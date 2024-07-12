using furni1.DataAccesLayer;
using furni1.Models;
using furni1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace furni1.ViewComponents
{
    public class FooterViewComponent: ViewComponent
    {
        private readonly AppDbContext _db;
        public FooterViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            FooterVM footerVM = new FooterVM
            {
                Bio = await _db.Bios.FirstOrDefaultAsync(),
                Socials = await _db.Socials.ToListAsync(),
            };
            return View(footerVM);
        }
    }
}
