namespace Application;
public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		CreateMap<Brewery, BreweryDto>();
	}
}
