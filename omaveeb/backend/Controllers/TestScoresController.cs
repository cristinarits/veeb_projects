using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using orm.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace orm.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestScoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Konstruktor süstib ApplicationDbContexti
        public TestScoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TestScores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestScore>>> GetTestScores()
        {
            // Tagastab kõik testitulemused
            return await _context.TestScores.ToListAsync();
        }

        // GET: TestScores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestScore>> GetTestScore(int id)
        {
            var testScore = await _context.TestScores.FindAsync(id);

            // Kui testitulemust ei leitud, tagastab 404
            if (testScore == null)
            {
                return NotFound("Puudub");
            }

            // Tagastab leitud testitulemuse
            return testScore;
        }

        // POST: TestScores
        [HttpPost]
        public async Task<ActionResult<TestScore>> PostTestScore(TestScore testScore)
        {
            // Kontrollib mudeli valideerimist
            var context = new ValidationContext(testScore, null, null);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(testScore, context, results, true))
            {
                // Tagastab valideerimisvead
                return BadRequest(results);
            }

            _context.TestScores.Add(testScore);
            await _context.SaveChangesAsync();

            // Tagastab loodud testitulemuse
            return CreatedAtAction(nameof(GetTestScore), new { id = testScore.Id }, testScore);
        }

        // PUT: TestScores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestScore(int id, TestScore testScore)
        {
            // Kontrollib, kas ID-d ühtivad
            if (id != testScore.Id)
            {
                return BadRequest("ID mismatch");
            }

            // Kontrollib mudeli valideerimist
            var context = new ValidationContext(testScore, null, null);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(testScore, context, results, true))
            {
                // Tagastab valideerimisvead
                return BadRequest(results);
            }

            _context.Entry(testScore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Kui testitulemust ei leitud, tagastab 404
                if (!TestScoreExists(id))
                {
                    return NotFound("Puudub");
                }
                else
                {
                    throw;
                }
            }

            // Tagastab 204 (No Content)
            return NoContent();
        }

        // DELETE: TestScores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestScore(int id)
        {
            var testScore = await _context.TestScores.FindAsync(id);
            if (testScore == null)
            {
                // Kui testitulemust ei leitud, tagastab 404
                return NotFound("Puudub");
            }

            _context.TestScores.Remove(testScore);
            await _context.SaveChangesAsync();

            // Tagastab 204 (No Content)
            return NoContent();
        }

        // Kontrollib, kas testitulemus eksisteerib
        private bool TestScoreExists(int id)
        {
            return _context.TestScores.Any(e => e.Id == id);
        }
    }
}