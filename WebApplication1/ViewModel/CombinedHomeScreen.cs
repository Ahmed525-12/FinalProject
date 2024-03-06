namespace WebApplication1.ViewModel
{
    public class CombinedHomeScreen
    {
        public UserVM UserVM { get; set; }
        public IEnumerable<SaveGoalVM> SaveGoalVM { get; set; }
        public IEnumerable<MonthOfExpenseVM> MonthOfExpenseVM { get; set; }
    }
}