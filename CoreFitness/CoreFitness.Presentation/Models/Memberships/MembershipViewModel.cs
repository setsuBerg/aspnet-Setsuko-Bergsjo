using Domain.Aggregates.Memberships;

namespace CoreFitness.Presentation.Models.Memberships;

public class MembershipViewModel
{
    public IEnumerable<Membership> Memberships { get; set; } = [];

}
