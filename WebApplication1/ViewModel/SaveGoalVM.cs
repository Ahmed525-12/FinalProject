using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModel
{
    public class SaveGoalVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string TitleGoal { get; set; }

        [Required(ErrorMessage = "TargetAmount is required")]
        public int TargetAmount { get; set; }

        public string User_Id { get; set; }
    }
}