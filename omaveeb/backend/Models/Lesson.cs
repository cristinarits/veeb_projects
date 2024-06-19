using System;
using System.ComponentModel.DataAnnotations;

namespace kontrolltoo.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }

        [Required(ErrorMessage = "Aine peab olema määratud.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Kuupäev peab olema määratud.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Tunni teema peab olema määratud.")]
        public string Topic { get; set; }
    }
}