namespace Domain.Aggregates.Memberships;

public sealed class Membership
{
    private Membership(string id, string title, string description, List<string> benefits, decimal price, int monthlyClasses)
    {
        Id = Required(id, nameof(id));
        Title = Required(title,nameof(title));
        Description = Required(description, nameof(description));
        Benefits = benefits;
        Price = CheckPriceValue(price, nameof(price));
        MonthlyClasses = monthlyClasses;
    }

    public string Id { get; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public List<string> Benefits { get; private set; } = [];
    public decimal Price { get; private set; }
    public int MonthlyClasses { get; private set; }

    private static string Required(string value, string propertyName) 
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"'{propertyName}' is required.", propertyName);
        return value.Trim();
    }

    private static decimal CheckPriceValue(decimal value, string propertyName) 
    {
        if (value < 0)
            throw new ArgumentException($"{propertyName} can not be negative");
        return value;
    }

    public static Membership Create(string id, string title, string description, List<string> benefits, decimal price = 0, int monthlyClasses = 20) =>
        new(Guid.NewGuid().ToString(), title, description, benefits, price, monthlyClasses);

    public static Membership CreateWithId(string id, string title, string description, List<string> benefits, decimal price = 0, int monthlyClasses = 20) =>
        new (id, title, description, benefits, price, monthlyClasses);

}
