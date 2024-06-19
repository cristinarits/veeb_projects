using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace orm.Models
{
    public class TestScore : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        public string Subject { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Saadud punktid peavad olema mitte-negatiivsed.")]
        public int SaadudPunktid { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Maksimum punktid peavad olema suuremad kui null.")]
        public int MaksPunktid { get; set; }

        public double Protsent
        {
            get { return (double)SaadudPunktid / MaksPunktid * 100; }
        }

        public string Hinne
        {
            get
            {
                var protsent = Protsent;
                if (protsent >= 91) return "A";
                else if (protsent >= 81) return "B";
                else if (protsent >= 71) return "C";
                else if (protsent >= 61) return "D";
                else if (protsent >= 51) return "E";
                else return "F";
            }
        }

        public string Comment { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Protsent > 100)
            {
                yield return new ValidationResult("Protsent ei saa olla suurem kui 100.",
                    new[] { nameof(SaadudPunktid), nameof(MaksPunktid) });
            }
        }
    }
}
