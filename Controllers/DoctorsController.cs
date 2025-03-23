using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toothcare_Appointment_System.Data;
using Toothcare_Appointment_System.Models;


namespace Toothcare_Appointment_System.Controllers
{
    [Route("api/Doctors")]
    [ApiController]
    public class APIDoctorsController : ControllerBase
    {
        private readonly ToothcareAppointmentContext _context;

        public APIDoctorsController(ToothcareAppointmentContext context)
        {
            _context = context;
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctors>>> GetDoctors()
        {
            return await _context.Doctors.ToListAsync();
        }

        // GET: api/Doctors/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctors>> GetDoctors(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }


        // POST: api/Doctors
        [HttpPost]
        public async Task<ActionResult<Doctors>> PostDoctors(Doctors doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctors", new { id = doctor.DoctorID }, doctor);
        }

        // PUT: api/Doctors/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctors(int id, Doctors doctor)
        {
            if (id != doctor.DoctorID)
            {
                return BadRequest();
            }

            _context.Entry(doctor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Doctors/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctors(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }

    [Route("Doctors")]
    public class DoctorsController : Controller
    {
        private readonly ToothcareAppointmentContext _context;
        public DoctorsController(ToothcareAppointmentContext context)
        {
            _context = context;
        }

        // GET: Doctors
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Doctors.ToListAsync());
        }

        // GET: Doctors/Details/1
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var doctors = await _context.Doctors
                .FirstOrDefaultAsync(m => m.DoctorID == id);
            if (doctors == null)
            {
                return NotFound();
            }
            return View(doctors);
        }

        // GET: Doctors/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Doctors doctors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctors);
        }

        // GET: Doctors/Edit/1
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var doctors = await _context.Doctors.FindAsync(id);
            if (doctors == null)
            {
                return NotFound();
            }
            return View(doctors);
        }

        // POST: Doctors/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Doctors doctors)
        {
            if (id != doctors.DoctorID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorsExists(doctors.DoctorID))
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
            return View(doctors);
        }
        // GET: Doctors/Delete/1
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var doctors = await _context.Doctors
                .FirstOrDefaultAsync(m => m.DoctorID == id);
            if (doctors == null)
            {
                return NotFound();
            }
            return View(doctors);
        }

        // POST: Doctors/Delete/1
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctors = await _context.Doctors.FindAsync(id);
            _context.Doctors.Remove(doctors);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorsExists(int id)
        {
            return _context.Doctors.Any(e => e.DoctorID == id);
        }
    }
}
