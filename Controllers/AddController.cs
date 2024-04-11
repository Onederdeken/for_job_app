using Microsoft.AspNetCore.Mvc;
using app_test_job.Models;
using app_test_job.bd;
using Microsoft.EntityFrameworkCore;
namespace app_test_job.Controllers;
[ApiController]
[Route("[controller]")]
public class AddController : Controller{
    private readonly ILogger<EmploeeController> _logger;
    private readonly Context db;

    public AddController(ILogger<EmploeeController> logger, Context db)
    {
        _logger = logger;
        this.db = db;
    }
    [Route("add/{status}&{company}")]
    [Route("add/{status}")]
    [HttpGet]
    public async Task<IActionResult> add(int status, String? company){

        switch(status){
            case 0:
                ViewData["status"] = status;
                ViewData["action"] = "AddEmploeepost";
                ViewData["Controller"] = "Emploee";
                ViewData["name"] = "Сотрудника";
                ViewData["Company"] = company;
                return View();

            case 1:
                ViewData["status"] = status;
                ViewData["action"] = "AddOrg";
                ViewData["Controller"] = "Home";
                ViewData["name"] = "Организации";
                return View();
            default:
                return RedirectToRoute(new{COntroller="Home", Action="Index"});
        }

    }


    

}