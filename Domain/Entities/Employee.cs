using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Employee
    {
        [Required]
        public int CompanyId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime DeletedOn { get; set; }

        [Required]
        public string Email { get; set; }
        public int EmployeeId { get; set; }
        public string Fax { get; set; }
        public string Name { get; set; }
        public DateTime LastLogin { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int PortalId { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public int StatusId { get; set; }
        public string Telephone { get; set; }
        public DateTime UpdateOn { get; set; }
        
        [Required]
        public string Username { get; set;}
    }
}