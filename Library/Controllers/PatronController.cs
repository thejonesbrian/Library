using Library.Models.Patron;
using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class PatronController : Controller
    {
        private IPatron _patron;
        public PatronController(IPatron patron)
        {
            _patron = patron;
        }

        public IActionResult Index()
        {

            var allPatrons = _patron.GetAll();

            var patronModels = allPatrons.Select(p => new PatronDetailModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                LibraryCardId = p.LibraryCard.Id,
                HomeLibraryBranch = p.HomeLibraryBranch.Name,

            }).ToList();
            var model = new PatronIndexModel()
            {
                Patrons = patronModels
            };
            return View(model);
        }
        public IActionResult Detail(int Id)
        {
            var patron = _patron.Get(Id);

            var model = new PatronDetailModel
            {
                LastName = patron.LastName,
                FirstName = patron.FirstName,
                Address = patron.Address,
                HomeLibraryBranch = patron.HomeLibraryBranch.Name,
                MemberSince = patron.LibraryCard.Created,
                OverdueFees = patron.LibraryCard.Fees,
                LibraryCardId = patron.LibraryCard.Id,
                Telephone = patron.TelephoneNumber,
                AssetsCheckedOut = _patron.GetCheckouts(Id).ToList() ?? new List<Checkouts>(),
                CheckoutHistory = _patron.GetCheckoutHistory(Id),
                Holds = _patron.GetHolds(Id)
            };
            return View(model);
        }
    }
}
