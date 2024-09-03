using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TemplateExampleUsers.Queries.TemplateGetAllUsers
{
    public class TemplateUserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;



        public static TemplateUserDTO MapToDTO(User source) => new()
        {
            Id = source.Id,
            Email = source.Email,
            FirstName = source.FirstName,
            LastName = source.LastName,
            RoleName = source.Role.Name ?? string.Empty,
        };
    }
}
