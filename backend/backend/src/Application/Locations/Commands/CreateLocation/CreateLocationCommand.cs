using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Application.Common.Interfaces;
using backend.Domain.Entities;
using MediatR;

namespace backend.Application.Locations.Commands.CreateLocation;
public class CreateLocationCommand: IRequest
{
    public string city { get; set; }
    public string streetname { get; set; }
    public int housenumber { get; set; }
    public string zipcode { get; set; }
}

public class CreateLocationCommandHandler : AsyncRequestHandler<CreateLocationCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ICurrentUserService _currentUser;
    public CreateLocationCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    protected override async Task Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        var entityTobeAdded = new Location() { 
            City = request.city, 
            StreetName = request.streetname, 
            HouseNumber = request.housenumber, 
            ZipCode = request.zipcode,
            CreatorId=int.Parse(_currentUser.UserId)
        };

        await _dbContext.Locations.AddAsync(entityTobeAdded); 
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
