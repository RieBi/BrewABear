using Application.Exceptions;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.BrewerQueries;
internal class GetBrewerDetailsQueryHandler(DataContext context, IMapper mapper) : IRequestHandler<GetBrewerDetailsQuery, BrewerDto>
{
    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<BrewerDto> Handle(GetBrewerDetailsQuery request, CancellationToken cancellationToken)
    {
        var brewer = await _context.Brewers
            .Where(f => f.Id == request.brewerId)
            .ProjectTo<BrewerDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new BrewerNotFoundException(request.brewerId);

        return _mapper.Map<BrewerDto>(brewer);
    }
}
