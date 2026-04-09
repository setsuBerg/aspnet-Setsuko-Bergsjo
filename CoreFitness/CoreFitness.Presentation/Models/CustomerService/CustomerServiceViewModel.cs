using CoreFitness.Presentation.Models.Memberships;

namespace CoreFitness.Presentation.Models.CustomerService;

public class CustomerServiceViewModel
{
    public ContactFormModel FormData { get; set; } = new();

    public IEnumerable<FaqItemViewModel> Faqs { get; set; } = [];
}