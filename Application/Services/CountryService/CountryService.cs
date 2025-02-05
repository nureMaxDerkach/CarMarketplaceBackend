using Application.DTOs.Country;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.CountryService;

public class CountryService : ICountryService
{
    private readonly DataContext _dataContext;

    public CountryService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<CountryDto>> GetCountriesAsync() =>
        await _dataContext.Countries
            .Select(c => new CountryDto
            {
                Id = c.Id, 
                Name = c.Name
            })
            .ToListAsync();

}