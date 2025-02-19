using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Educational_Platform.ViewModels
{
    public class AddQuestionViewModel
    {
        [Required(ErrorMessage = "Text is required")]
        [RegularExpression(@"^.{5,300}$", ErrorMessage = "Text must be 5-300 characters")]
        public string Text { get; set; }


        [Required(ErrorMessage = "Correct answer is required")]
        [Display(Name = "Correct Answer")]
        [RegularExpression(@"^.{1,300}$", ErrorMessage = "Correct answer must be 1-300 characters")]
        public string CorrectAnswer { get; set; }


        [Display(Name = "Answer Options")]
        [Required(ErrorMessage = "Answer options are required")]
        [RegularExpression(@"^.{7,1000}$", ErrorMessage = "Answer options must be 7-1000 characters")]
        public string AnswerOptions { get; set; }

        [Required(ErrorMessage = "Quiz name is required")]
        [Display(Name ="Quiz Name")]
        public string QuizName { get; set; }

    }
}
