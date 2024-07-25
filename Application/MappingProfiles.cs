﻿namespace Application;
public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		CreateMap<Brewery, BreweryDto>();
		CreateMap<Beer, BeerDto>();
		CreateMap<Brewer, BrewerDto>();
		CreateMap<Wholesaler, WholesalerDto>();

		CreateMap<BeerCreateDto, Beer>();
	}
}
