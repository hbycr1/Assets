using Application.Common.Interfaces;
using Application.Common.Models;
using Mediator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Session.Query;
public class GetUserSession : IRequest<Result<UserSessionDto>>
{
	public Guid UserId { get; set; }
}

public class GetUserSessionHandler : IRequestHandler<GetUserSession, Result<UserSessionDto>>
{
	private readonly IDbContext _db;

	public GetUserSessionHandler(IDbContext db)
	{
		_db=db;
	}

	public async ValueTask<Result<UserSessionDto>> Handle(GetUserSession request, CancellationToken cancellationToken)
	{
		var user = await _db.User.Where(x => x.Id == request.UserId)
								 .Select(x => new UserSessionDto
								 {
									 FirstName = x.FirstName,
									 LastName = x.LastName,
									 Email = x.Email
								 })
								 .FirstOrDefaultAsync(cancellationToken);

		return new Result<UserSessionDto>
		{
			Error = user == null ? "User not found" : null,
			Model = user
		};
	}
}