namespace WebApplication1.ViewModel
{
    public class MonthOfExpenseVM
    {
        public int Id { get; set; }
        public int num_month { get; set; }
        public IReadOnlyList<ExpenseVM> Expenses { get; set; }
        public string User_Id { get; set; }
        public int TotalAmountMoney { get; set; } = 0;
    }
}