using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Session;

public class UserSessionDto
{
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Expires { get; set; } = DateTime.UtcNow.AddDays(1).ToString("O");
}