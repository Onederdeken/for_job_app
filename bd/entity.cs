
using app_test_job.Models;
using Microsoft.EntityFrameworkCore;

namespace app_test_job.bd;

public class Context:DbContext{
    public DbSet<Org> org{get;set;}
    public DbSet<Employee> Employees{get;set;}
    public Context(DbContextOptions<Context> options):base(options){
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        optionsBuilder.UseMySql("server=localhost;user=root;password=fylhtq9049791;database=forjob", ServerVersion.AutoDetect("server=localhost;user=root;password=fylhtq9049791;database=forjob;"));
    }

}