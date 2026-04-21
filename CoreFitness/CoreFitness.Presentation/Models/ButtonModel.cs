namespace CoreFitness.Presentation.Models;

public class ButtonModel
{
    public string Text { get; set; } = null!;
    public string Controller { get; set; } = null!;
    public string Action { get; set; } = null!;
    public string? RouteId { get; set; } = null;
}
