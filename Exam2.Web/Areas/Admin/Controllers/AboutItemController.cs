using AutoMapper;
using Exam2.Business.Models;
using Exam2.Business.Pagination;
using Exam2.Business.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Operations;

namespace Exam2.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class AboutItemController : Controller
    {
        private IAboutItemService _aboutItemService { get; }
        private IMapper _mapper { get; }

        public AboutItemController(IAboutItemService aboutItemService,IMapper mapper)
        {
            _aboutItemService = aboutItemService;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            var model = await _aboutItemService.GetAllPaginatedAsync(1,3);
            model.Current = Url.Action(nameof(PaginatedIndex), new {currentpage=1,perpage=3});
            model.Next = Url.Action(nameof(PaginatedIndex), new {currentpage=2,perpage=3});

            return View(model);
        }

        public async Task<IActionResult> PaginatedIndex(int currentpage,int perpage)
        {
            var model = await _aboutItemService.GetAllPaginatedAsync(currentpage, perpage);
            model.Next = Url.Action(nameof(PaginatedIndex), new { currentpage = currentpage+1, perpage = 3 });
            model.Prev = Url.Action(nameof(PaginatedIndex), new { currentpage = currentpage-1, perpage = 3 });
            model.Current = Url.Action(nameof(PaginatedIndex), new { currentpage = currentpage, perpage = 3 });

            return PartialView("PartialPaginated",model);
        }




        public IActionResult Create()
        {
            return View();
        }

        

        public async Task<IActionResult> Update(int id)
        {
            var model = await _aboutItemService.GetByIdAsync(id);
            if (model == null) return NotFound();
            var umodel = _mapper.Map<AboutItemUpdateVM>(model);
            return View(umodel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AboutItemCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);
            await _aboutItemService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public async Task<IActionResult> Update(int id, AboutItemUpdateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _aboutItemService.UpdateAsync(id,model);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _aboutItemService.SoftDeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> RevokeDelete(int id)
        {
            await _aboutItemService.RevokeDeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
