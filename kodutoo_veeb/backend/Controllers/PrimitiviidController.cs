using Microsoft.AspNetCore.Mvc;

namespace veeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PrimitiividController : ControllerBase
    {
        private static readonly Random rand = new Random();

        // GET: primitiivid/hello-world
        [HttpGet("hello-world")]
        public string HelloWorld()
        {
            return "Hello world at " + DateTime.Now;
        }

        // GET: primitiivid/hello-variable/mari
        [HttpGet("hello-variable/{nimi}")]
        public string HelloVariable(string nimi)
        {
            return "Hello " + nimi;
        }

        // GET: primitiivid/add/5/6
        [HttpGet("add/{nr1}/{nr2}")]
        public int AddNumbers(int nr1, int nr2)
        {
            return nr1 + nr2;
        }

        // GET: primitiivid/multiply/5/6
        [HttpGet("multiply/{nr1}/{nr2}")]
        public int Multiply(int nr1, int nr2)
        {
            return nr1 * nr2;
        }

        // GET: primitiivid/do-logs/5
        [HttpGet("do-logs/{arv}")]
        public void DoLogs(int arv)
        {
            for (int i = 0; i < arv; i++)
            {
                Console.WriteLine("See on logi nr " + i);
            }
        }
        
        // GET: primitiivid/random-number?min=1&max=100
        [HttpGet("random-number")]
        public int GetRandomNumber(int min, int max)
        {
            return rand.Next(min, max + 1);
        }
        
        // GET: primitiivid/age?birthYear=1990&birthMonth=5&birthDay=20
        [HttpGet("age")]
        public string GetAge(int birthYear, int birthMonth, int birthDay)
        {
            DateTime today = DateTime.Today;
            DateTime birthdayThisYear = new DateTime(today.Year, birthMonth, birthDay);
            DateTime nextBirthday = birthdayThisYear > today ? birthdayThisYear : birthdayThisYear.AddYears(1);
            
            int age = today.Year - birthYear;
            if (today < birthdayThisYear)
            {
                age--;
            }

            int daysUntilNextBirthday = (nextBirthday - today).Days;

            return $"Oled {age} aastat vana ja järgmise sünnipäevani on {daysUntilNextBirthday} päeva.";
        }
    }
}