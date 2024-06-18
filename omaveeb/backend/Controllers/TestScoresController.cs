using orm.Models;
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

        public TestScoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestScore>>> GetTestScores()
        {
            return await _context.TestScores.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TestScore>> GetTestScore(int id)
        {
            var testScore = await _context.TestScores.FindAsync(id);

            if (testScore == null)
            {
                return NotFound("Puudub");
            }

            return testScore;
        }

        [HttpPost]
        public async Task<ActionResult<TestScore>> PostTestScore(TestScore testScore)
        {
            var context = new ValidationContext(testScore, null, null);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(testScore, context, results, true))
            {
                return BadRequest(results);
            }

            _context.TestScores.Add(testScore);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTestScore), new { id = testScore.Id }, testScore);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestScore(int id, TestScore testScore)
        {
            if (id != testScore.Id)
            {
                return BadRequest("ID mismatch");
            }

            var context = new ValidationContext(testScore, null, null);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(testScore, context, results, true))
            {
                return BadRequest(results);
            }

            _context.Entry(testScore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestScoreExists(id))
                {
                    return NotFound("Puudub");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestScore(int id)
        {
            var testScore = await _context.TestScores.FindAsync(id);
            if (testScore == null)
            {
                return NotFound("Puudub");
            }

            _context.TestScores.Remove(testScore);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TestScoreExists(int id)
        {
            return _context.TestScores.Any(e => e.Id == id);
        }
    }
}