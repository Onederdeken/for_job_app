using Microsoft.AspNetCore.Mvc;
using app_test_job.Models;
using app_test_job.bd;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using CsvHelper.Configuration;

namespace app_test_job.Controllers;
[ApiController]
[Route("[controller]")]
public class EmploeeController : Controller{
    private readonly ILogger<EmploeeController> _logger;
    private readonly Context db;
    public EmploeeController(ILogger<EmploeeController> logger, Context db)
    {
        _logger = logger;
        this.db = db;
    }
    [HttpGet]
    [Route("emploee")]
    [Route("emploee/{name}")]
    public async Task<IActionResult> emploee([FromQuery]String? name)
    {
        ViewData["name"]=name;
        try{
            Org org = await db.org.FirstAsync(i=>i.name==name);
            int ID = org.id;
            List<Employee>? employees = db.Employees.Include(u=>u.org).Where(p=>p.CompanyId == ID).ToList();
            return View(employees);
        }
        catch(InvalidOperationException E){
            return RedirectToRoute(new{COntroller="Home", Action="Index"});
        }
        
        
        
    }

    [HttpGet]
    [Route("AddEmploe/{Company}")]
    public async Task<IActionResult> AddEmploee(string Company)
    {
        return RedirectToRoute(new{Controller="Add", Action = "add", status = "0", company = Company})  ;  
    }

    [HttpPost]
    [Route("AddEmploeepost")]
    public async Task<IActionResult> AddEmploeepost([FromForm] AddModel addmodel, [FromForm]String NameCompany)
    {
        var org = db.org.First(u=>u.name == NameCompany);
        addmodel.employee.CompanyId = org.id;
        addmodel.employee.org = org;
        org.employees.Add(addmodel.employee);
        db.Employees.Add(addmodel.employee);
        db.SaveChanges();
       
        return RedirectToAction("emploee", new{name = NameCompany})  ;  
    }
    [HttpGet]
    [Route("download")]
    public async Task<IActionResult> download(){
        String path = "wwwroot/csv/exportEmployee.csv"; 
        var items = await db.Employees.ToListAsync();
        using (StreamWriter streamWriter= new StreamWriter(path))
            {
                using (CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(items);
                    csvWriter.Flush();
                }
                
            }
        String file_type = "application/csv";
        String name = "ExportFromEmployee.csv";
        return File("csv/exportEmployee.csv",file_type,name);
    }
    [HttpPost]
    [Route("download")]
    public async Task<IActionResult> download([FromForm] IFormFile filebase, [FromForm] String Name){

        using(var reader =  new StreamReader(filebase.OpenReadStream()) ){
            CsvConfiguration configuration = new CsvConfiguration( CultureInfo.InvariantCulture);
            
            
            using(var csvReader = new CsvReader(reader, configuration)){
                var employees = csvReader.GetRecords<Employee>();
               
                foreach(var item in employees.ToList()){
                    Org org = db.org.Find(item.CompanyId);
                    item.org = org;
                    Console.WriteLine($"имя={item.NameE} , фамиия={item.Secondname}, отчество={item.otche} ");
                    org.employees.Add(item);
                    db.Add(item);
                }

                db.SaveChanges();
            }
            
            
        }
        return RedirectToAction("emploee", new{name = Name})  ;  
    }

   

}