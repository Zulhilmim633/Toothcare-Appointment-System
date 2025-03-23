using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toothcare_Appointment_System.Data;
using Toothcare_Appointment_System.Models;

namespace Toothcare_Appointment_System.Controllers
{
    [Route("api/Appointmnets")]
    [ApiController]
    public class APIAppointmentsController : ControllerBase
    {
        private readonly ToothcareAppointmentContext _context;

        public APIAppointmentsController(ToothcareAppointmentContext context)
        {
            _context = context;
        }

        // GET: api/Appoinments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppoinment()
        {
            return await _context.Appointment.ToListAsync();
        }

        // GET: api/Appoinments/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppoinment(int id)
        {
            var appoinment = await _context.Appointment.FindAsync(id);
            if (appoinment == null)
            {
                return NotFound();
            }
            return appoinment;
        }

        // POST: api/Appoinments
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppoinment(Appointment appoinment)
        {
            _context.Appointment.Add(appoinment);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetAppoinment", new { id = appoinment.AppointmentID }, appoinment);
        }

        // PUT: api/Appoinments/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppoinment(int id, Appointment appoinment)
        {
            if (id != appoinment.AppointmentID)
            {
                return BadRequest();
            }
            _context.Entry(appoinment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Appoinments/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<Appointment>> DeleteAppoinment(int id)
        {
            var appoinment = await _context.Appointment.FindAsync(id);
            if (appoinment == null)
            {
                return NotFound();
            }
            _context.Appointment.Remove(appoinment);
            await _context.SaveChangesAsync();
            return appoinment;
        }
    }

    [Route("Appointments")]
    public class AppointmentsController : Controller
    {
        private readonly ToothcareAppointmentContext _context;
        public AppointmentsController(ToothcareAppointmentContext context)
        {
            _context = context;
        }

        // GET: Appointments
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Appointment.ToListAsync());
        }

        // GET: Appointments/Details/1
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var appointment = await _context.Appointment
                .FirstOrDefaultAsync(m => m.AppointmentID == id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Appointments/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Appointments/Edit/1
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/1
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Appointment appointment)
        {
            if (id != appointment.AppointmentID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentID))
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
            return View(appointment);
        }

        // GET: Appointments/Delete/1
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var appointment = await _context.Appointment
                .FirstOrDefaultAsync(m => m.AppointmentID == id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/1
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.AppointmentID == id);
        }
    }
}
