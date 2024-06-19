using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using orm.Data;
using kontrolltoo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace orm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Konstruktor, mis seab andmebaasi konteksti
        public LessonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Meetod, mis tagastab kõik õppetunnid või filtreeritud ja sorteeritud õppetunnid
        // GET: api/Lessons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons(
            [FromQuery] string subject = null,
            [FromQuery] DateTime? date = null)
        {
            // Teen päringu andmebaasile kõigi õppetundide saamiseks
            var query = _context.Lessons.AsQueryable();

            // Kui aine on määratud, filtreerin õppetunnid aine järgi
            if (!string.IsNullOrEmpty(subject))
            {
                query = query.Where(l => l.Subject.Contains(subject));
            }

            // Kui kuupäev on määratud, filtreerin õppetunnid kuupäeva järgi
            if (date.HasValue)
            {
                query = query.Where(l => l.Date.Date == date.Value.Date);
            }

            // Tagastan filtreeritud õppetunnid
            return await query.ToListAsync();
        }

        // Meetod, mis arvutab igas kuus igas aines olevate tundide arvu
        // GET: api/Lessons/MonthlyReport
        [HttpGet("MonthlyReport")]
        public async Task<ActionResult<IEnumerable<MonthlyReportDto>>> GetMonthlyReport()
        {
            // Laen kõik õppetunnid andmebaasist
            var lessons = await _context.Lessons.ToListAsync();

            // Grupeerin õppetunnid aasta, kuu ja aine järgi ning loendan õppetundide arvu
            var report = lessons
                .GroupBy(l => new { l.Date.Year, l.Date.Month, l.Subject })
                .Select(g => new MonthlyReportDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Subject = g.Key.Subject,
                    LessonCount = g.Count()
                })
                .ToList();

            // Tagastan kuuaruande
            return Ok(report);
        }

        // Meetod, mis lisab uue õppetunni
        // POST: api/Lessons
        [HttpPost]
        public async Task<ActionResult<Lesson>> PostLesson(Lesson lesson)
        {
            // Lisan uue õppetunni andmebaasi
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();

            // Tagastan loodud õppetunni koos asukoha URL-iga
            return CreatedAtAction(nameof(GetLessons), new { id = lesson.LessonId }, lesson);
        }

        // Meetod, mis uuendab olemasolevat õppetundi
        // PUT: api/Lessons/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLesson(int id, Lesson lesson)
        {
            // Kontrollin, kas antud ID ja õppetunni ID kattuvad
            if (id != lesson.LessonId)
            {
                return BadRequest();
            }

            // Märgin õppetunni muutunuks
            _context.Entry(lesson).State = EntityState.Modified;

            try
            {
                // Salvesta muudatused andmebaasi
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Kontrollin, kas õppetund eksisteerib
                if (!LessonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Tagastan vastuse ilma sisuta, mis tähendab, et uuendamine õnnestus
            return NoContent();
        }

        // Meetod, mis kustutab õppetunni
        // DELETE: api/Lessons/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            // Otsin õppetunni andmebaasist
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                // Kui õppetundi ei leita, tagastan 404 staatuse
                return NotFound();
            }

            // Eemaldan õppetunni andmebaasist
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();

            // Tagastan vastuse ilma sisuta, mis tähendab, et kustutamine õnnestus
            return NoContent();
        }

        // Meetod, mis kontrollib, kas õppetund eksisteerib
        private bool LessonExists(int id)
        {
            return _context.Lessons.Any(e => e.LessonId == id);
        }
    }

    // DTO (Data Transfer Object) klass, mis esindab kuuaruande andmeid
    public class MonthlyReportDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string Subject { get; set; }
        public int LessonCount { get; set; }
    }
}
