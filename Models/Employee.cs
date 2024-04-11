

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app_test_job.Models;

public class Employee{
    [Required]
    [Key]
    public int id{get;set;}

    [UIHint("String")]
    public String? NameE{get;set;}

    [UIHint("String")]
    public String? Secondname {get;set;}

    [UIHint("String")]
    public String? otche{get;set;}
    [UIHint("date")]
    public DateTime Dates{get;set;}
    [UIHint("int")]
    public String? Pser{get;set;}
    [UIHint("int")]
    public String? Pnumb{get;set;}

    public int CompanyId{get;set;}

    [ForeignKey(nameof(CompanyId))]
    public  Org? org{get;set;}
    }

