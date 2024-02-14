using UDM.Models.ViewModels;

namespace UDM.Services.Interfaces
{
    public interface IStaffService
    {
        Task<List<TableStaffVM>> GetPersonsWithFilters(PersonsVM personsVM);

        Task<List<TableSalaryVM>> GetSalaryWithFilters(PersonsVM personsVM);

        Task<byte[]> SaveSalaryTable(List<TableSalaryVM> salaryTable);
    }
}
