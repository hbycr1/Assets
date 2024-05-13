﻿namespace Domain.Entities;

public class Customer : BaseEntity
{
	public Guid CompanyId { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Phone { get; set; } = string.Empty;

	public virtual Company? Company { get; set; }
}
