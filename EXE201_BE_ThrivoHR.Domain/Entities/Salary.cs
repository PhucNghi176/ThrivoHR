using EXE201_BE_ThrivoHR.Domain.Entities.Base;
using EXE201_BE_ThrivoHR.Domain.Entities.Contracts;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE201_BE_ThrivoHR.Domain.Entities;

public class Salary : AuditableEntity
{
    public new string Id { get; set; } = Guid.NewGuid().ToString();
    public string? EmployeeId { get; set; }
    public virtual AppUser? Employee { get; set; }
    public string? ContractId { get; set; }
    public virtual EmployeeContract? Contract { get; set; }
    public DateOnly Date { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal BasicSalary { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAllowance { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Bonus { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalIncome { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal SocialInsurance { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal HealthInsurance { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnemploymentInsurance { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalIncomeBeforeTax { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal PersonalDeduction { get; set; }
    public int NumberOfDependants { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal TaxableIncome { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal PersonalIncomeTax { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal? OtherDeductions { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal NetIncome { get; set; }




}
