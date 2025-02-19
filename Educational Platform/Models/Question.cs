using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Educational_Platform.Models
{
    public class Question
    {
        [Required]
        [Key]
        public int QuestionId { get; set; }


        [Required(ErrorMessage = "Text is required")]
        [RegularExpression(@"^.{5,300}$",ErrorMessage ="Text must be 5-300 characters")]
        public string Text { get; set; }


        [Required(ErrorMessage = "Correct answer is required")]
        [Display(Name ="Correct Answer")]
        [RegularExpression(@"^.{1,300}$",ErrorMessage ="Correct answer must be 1-300 characters")]
        public string CorrectAnswer { get; set; }


        [Required(ErrorMessage = "Answer options are required")]
        [RegularExpression(@"^.{7,1000}$",ErrorMessage ="Answer options must be 7-1000 characters")]
        public string AnswerOptions { get; set; }

        [Required(ErrorMessage ="Quiz id is required")]
        [ForeignKey("Quiz")]
        public int QuizId { get; set; }

        public Quiz Quiz { get; set; }

    }
}
