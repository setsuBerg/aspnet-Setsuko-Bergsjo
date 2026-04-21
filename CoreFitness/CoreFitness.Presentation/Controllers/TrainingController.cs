using Infrastructure.Identity;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Entities.Bookings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Presentation.Controllers;

public class TrainingController : Controller
{
    private readonly DataContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public TrainingController(DataContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var classes = _context.TrainingClasses.ToList();
        return View(classes);
    }

    [HttpGet]
    public IActionResult Book(string id)
    {
        var trainingClass = _context.TrainingClasses.FirstOrDefault(x => x.Id == id);
        if (trainingClass is null)
            return NotFound();

        return View(trainingClass);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Book")]
    public async Task<IActionResult> BookPost(string id)
    {
        var userId = _userManager.GetUserId(User);

        if (userId is null)
            return Challenge();

        var trainingClassExists = _context.TrainingClasses.Any(x => x.Id == id);
        if (!trainingClassExists)
        {
            ModelState.AddModelError(string.Empty, "The selected class does not exist.");
            return RedirectToAction("Index");
        }

        var alreadyBooked = _context.Bookings.Any(x => x.UserId == userId && x.TrainingClassId == id);
        if (alreadyBooked)
        {
            TempData["BookingMessage"] = "You have already booked this class.";
            return RedirectToAction("Index");
        }

        var booking = new BookingEntity
        {
            Id = Guid.NewGuid().ToString(),
            UserId = userId,
            TrainingClassId = id,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
