using System.Security.Cryptography;
using AutoMapper;
using ThriftinessCore.Entites;
using ThriftinessCore.Entites.Identity;
using WebApplication1.ViewModel;

namespace Talabat.PL.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AppUser, UserVM>().ReverseMap();
            CreateMap<AppUser, UserSettingsVM>().ReverseMap();
            CreateMap<MonthOfExpense, MonthOfExpenseVM>().ReverseMap();
            CreateMap<Expense, ExpenseVM>().ReverseMap();
            CreateMap<SaveGoal, SaveGoalVM>().ReverseMap();
        }
    }
}