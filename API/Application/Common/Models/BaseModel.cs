using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models;

public class BaseModel
{
	public Guid Id { get; set; }

	public DateTime Created { get; set; }
	public string CreatedBy { get; set; } = string.Empty;

	public DateTime? Updated { get; set; }
	public string? UpdatedBy { get; set; }
}
