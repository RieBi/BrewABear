namespace Application;
public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		CreateMap<Brewery, BreweryDto>();
		CreateMap<Beer, BeerDto>();
	}
}
