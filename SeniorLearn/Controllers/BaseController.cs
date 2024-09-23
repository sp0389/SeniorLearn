using Microsoft.AspNetCore.Mvc;
using SeniorLearn.Data.Core;

namespace SeniorLearn.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ApplicationDbContext _context;
        protected readonly ILogger<BaseController> _logger;
        public BaseController (ApplicationDbContext context, ILogger<BaseController> logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}