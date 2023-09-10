using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrgOffering.Data;
using OrgOffering.Models;
using OrgOffering.Repository;

namespace OrgOffering.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceRepository _serviceRepository;


        public ServicesController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            var results = _serviceRepository.GetAll();

            return View(results);
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            

            var service = _serviceRepository.GetById(id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceId,ServiceName,ServiceDescription,CreatedDate")] Service service)
        {
            if (ModelState.IsValid)
            {
                Service existingService;
                do
                {
                    service.ServiceId = Guid.NewGuid();
                    existingService = _serviceRepository.GetById(service.ServiceId);
                }
                while (existingService != null);
                service.CreatedDate = DateTime.Now;

                _serviceRepository.Add(service);
                _serviceRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = _serviceRepository.GetById(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ServiceId,ServiceName,ServiceDescription,CreatedDate")] Service service)
        {
            if (id != service.ServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _serviceRepository.Update(service);
                    _serviceRepository.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.ServiceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = _serviceRepository.GetById(id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var service = _serviceRepository.GetById(id);
            _serviceRepository.Remove(service);
            _serviceRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(Guid? id)
        {
            if (id == null)
            {
                return false; // Handle the case when id is null (service doesn't exist)
            }

            // Create an expression to filter services by their ID
            Expression<Func<Service, bool>> filter = service => service.ServiceId == id;

            // Use the filter expression in your repository's Find method
            return _serviceRepository.Find(filter).Any();
        }

    }
}
