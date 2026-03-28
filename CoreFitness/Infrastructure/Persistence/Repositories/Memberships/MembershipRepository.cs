using Domain.Abstractions.Repositories;
using Domain.Aggregates.Memberships;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Entities.Memberships;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Memberships;

public sealed class MembershipRepository(DataContext context) : RepositoryBase<Membership, string, MembershipEntity, DataContext>(context), IMembershipRepository
{
    protected override void ApplyPropertyUpdates(MembershipEntity entity, Membership model)
    {
        entity.Title = model.Title;
        entity.Description = model.Description;
        entity.Price = model.Price;
        entity.MonthlyClasses = model.MonthlyClasses;
    }

    
    protected override string GetId(Membership model)
    {
        return model.Id;
    }

    protected override Membership ToDomainModel(MembershipEntity entity)
    {
        var benefits = new List<string>();
        foreach (var benefit in entity.Benefits) 
        { 
            benefits.Add(benefit.Benefit);
        }

        var model = Membership.CreateWithId(
            entity.Id,
            entity.Title,
            entity.Description,
            benefits,
            entity.Price,
            entity.MonthlyClasses
        );

        return model;
    }

    protected override MembershipEntity GetEntity(Membership model)
    {
        var entity = new MembershipEntity 
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            Price = model.Price,
            MonthlyClasses = model.MonthlyClasses
        };

        foreach (var benefit in model.Benefits)
            entity.Benefits.Add(new MembershipBenefitEntity { Benefit = benefit });

        return entity;
    }

    public override async Task<IReadOnlyList<Membership>> GetAllAsync(CancellationToken ct = default)
    {

        var entities = await _context.Memberships
            .Include(x => x.Benefits)
            .AsNoTracking()
            .ToListAsync(ct);

        return [.. entities.Select(ToDomainModel)];
    }   
}
