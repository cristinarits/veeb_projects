using Microsoft.AspNetCore.Mvc;
using veeb.models;
using veeb.Models;

namespace veeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ToodeController : ControllerBase
    {
        private static Toode _toode = new Toode(1, "Dr. Pepper", 1.5, true);

        // GET: toode
        [HttpGet]
        public Toode GetToode()
        {
            return _toode;
        }

        // GET: toode/suurenda-hinda
        [HttpGet("suurenda-hinda")]
        public Toode SuurendaHinda()
        {
            _toode.Price = _toode.Price + 1;
            return _toode;
        }

        // GET: toode/muuda-aktiivsust
        [HttpGet("muuda-aktiivsust")]
        public Toode MuudaAktiivsust()
        {
            _toode.IsActive = !_toode.IsActive;
            return _toode;
        }

        // GET: toode/muuda-nime/{uusNimi}
        [HttpGet("muuda-nime/{uusNimi}")]
        public Toode MuudaNime(string uusNimi)
        {
            _toode.Name = uusNimi;
            return _toode;
        }

        // GET: toode/korruta-hinda/{kordaja}
        [HttpGet("korruta-hinda/{kordaja}")]
        public Toode KorrutaHinda(double kordaja)
        {
            _toode.Price *= kordaja;
            return _toode;
        }
    }
}
