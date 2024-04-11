using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using app_test_job.Models;
using app_test_job.bd;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace app_test_job.Controllers;
[ApiController]
[Route("[controller]")]
public class HomeController : Controller
{
    private readonly IWebHostEnvironment _appEnvironment;
    private readonly ILogger<HomeController> _logger;
    private readonly Context db;

    public HomeController(ILogger<HomeController> logger, Context db, IWebHostEnvironment webHostEnvironment)
    {
        _logger = logger;
        this.db = db;
        this._appEnvironment = webHostEnvironment;
    }
    [Route("/")]
    [Route("Index")]
    
    public async Task<IActionResult> Index()
    {
        List<Org> org = db.org
        .Include(u=>u.adres)
        .ToList();
        return View(org);
    }

    public IActionResult Privacy()
    {
        return View();
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    [Route("AddOrg")]
    [HttpGet]
    public async Task<IActionResult> AddOrg(){

        return RedirectToRoute(new{Controller="Add", Action = "add", status = "1"})  ;
    }
    [Route("AddOrg")]
    [HttpPost]
    public async Task<IActionResult> AddOrg([FromForm] AddModel model){
        Org org = model.org;
        adres adres = model.adres;
        org.adres = adres;
        adres.org = org;
        db.Add(org);
        adres.id = org.id;
        db.Add(adres);
        db.SaveChanges();


        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("download")]
    public async Task<IActionResult> download(){
        String path = "wwwroot/csv/exportOrg.csv"; 
        var items = await db.org
            .Include(u=>u.adres).Where(u=>u.id == u.adres.id).ToListAsync();
        using (StreamWriter streamWriter= new StreamWriter(path))
            {
                using (CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(items);
                    csvWriter.Flush();
                }
                
            }
        String file_type = "application/csv";
        String name = "ExportFromOrg.csv";
        return File("csv/exportOrg.csv",file_type,name);
    }
    [HttpPost]
    [Route("download")]
    public async Task<IActionResult> download([FromForm] IFormFile filevbase){

        using(var reader =  new StreamReader(filevbase.OpenReadStream()) ){
            using(var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture)){
                var orgs = csvReader.GetRecords<Org>();
                await db.AddRangeAsync(orgs);
                db.SaveChanges();
            }
        }
        
        return RedirectToAction("Index");
    }
}
