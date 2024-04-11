

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app_test_job.Models;

public class Org{
    [Required]
    [Key]
    public int id{get;set;}
    [UIHint("String")]
    public String? name {get;set;}
    [UIHint("String")]
    public String? Inn{get;set;}
  
    public adres? adres{get;set;}

    public List<Employee>? employees = new List<Employee>();
    
    }

