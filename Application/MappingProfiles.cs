namespace Application;
public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		CreateMap<Brewery, BreweryDto>();
		CreateMap<Beer, BeerDto>();
		CreateMap<Brewer, BrewerDto>();
		CreateMap<Wholesaler, WholesalerDto>();
		CreateMap<Order, OrderDto>();

		CreateMap<WholesalerInventory, WholesalerInventoryDto>()
			.ForMember(f => f.FixedPrice,
			opt => opt.MapFrom(m => m.Beer.Price));

		CreateMap<BeerCreateDto, Beer>();
		CreateMap<OrderCreateDto, Order>();
	}
}
