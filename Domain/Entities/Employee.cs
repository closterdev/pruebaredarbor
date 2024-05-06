using Shared.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Employee
    {
        [NotZero(ErrorMessage = "El CompanyId es obligatorio.")]
        [Required(ErrorMessage = "El CompanyId es obligatorio.")]
        public int? CompanyId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        [Required(ErrorMessage = "El correo electronico es obligatorio.")]
        public string Email { get; set; }
        public int EmployeeId { get; set; }
        public string Fax { get; set; }
        public string Name { get; set; }
        public DateTime LastLogin { get; set; }

        [Required(ErrorMessage = "El password es obligatorio.")]
        public string Password { get; set; }

        [NotZero(ErrorMessage = "El PortalId es obligatorio.")]
        [Required(ErrorMessage = "El PortalId es obligatorio.")]
        public int? PortalId { get; set; }

        [NotZero(ErrorMessage = "El RoleId es obligatorio.")]
        [Required(ErrorMessage = "El RoleId es obligatorio.")]
        public int? RoleId { get; set; }

        [NotZero(ErrorMessage = "El StatusId es obligatorio.")]
        [Required(ErrorMessage = "El StatusId es obligatorio.")]
        public int? StatusId { get; set; }
        public string Telephone { get; set; }
        public DateTime? UpdatedOn { get; set; }

        [Required(ErrorMessage = "El Username es obligatorio.")]
        public string Username { get; set;}
        public bool IsDeleted { get; set; } = false;
    }
}