

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace app_test_job.Models;



public class adres{
    [Required]
    [Key]
    [ForeignKey(nameof(org))]
    public int id{get;set;}
    [UIHint("int")]
    public int PIndex{get;set;}
    [UIHint("String")]
    public String? city {get;set;}
    [UIHint("String")]
    public String? street{get;set;}
    [UIHint("int")]
    public int? Nhouse{get;set;}
    [UIHint("int")]
    public int? NApartament{get;set;}
    [UIHint("int")]
    public int? NStructure{get;set;}

    public Org? org{get;set;}
    }

