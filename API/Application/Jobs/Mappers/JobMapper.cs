using Application.Jobs.Command;
using Domain.Entities;
using Domain.Enums;

namespace Application.Jobs.Mappers;

public static class JobMapper
{
    public static JobDetailDto MapToDetailDto(this Job job)
    {
        return new()
        {
            Id = job.Id,
            JobCode = job.JobCode,
            JobTypeId = job.JobTypeId,
            PONumber = job.PONumber,
            Address = job.Address,
            Start = job.Start,
            End = job.End,
            CustomerId = job.CustomerId,
            Status = job.Status,
            Created = job.Created,
            CreatedBy = job.CreatedBy,
            Updated = job.Updated,
            UpdatedBy = job.UpdatedBy,
            Description = job.Description,
        };
    }

    public static JobListDto MapToListDto(this Job job)
    {
        return new()
        {
            Id = job.Id,
            JobCode = job.JobCode,
            JobType = job.JobType?.Name?.ToString(),
            PONumber = job.PONumber,
            Address = job.Address,
            Start = job.Start,
            End = job.End,
            Customer = job.Customer?.Name,
            Status = job.Status,
            Created = job.Created,
            CreatedBy = job.CreatedBy
        };
    }

    public static Job MapToModel(this CreateJobCommand job)
    {
        string id = Guid.NewGuid().ToString();

        return new()
        {
            Id = Guid.Parse(id),
            JobCode = id.Replace("-", "").Substring(1, 6).ToUpper(),
            CompanyId = job.CompanyId,
            JobTypeId = job.JobType,
            Description = job.Description,
            Address = job.Address,
            Created = DateTime.UtcNow,
            CreatedBy = job.CreatedBy,
            CustomerId = job.CustomerId,
            PONumber = job.PONumber,
            Start = DateTime.SpecifyKind(job.Start, DateTimeKind.Utc),
            End = DateTime.SpecifyKind(job.End, DateTimeKind.Utc),
            Status = JobStatus.Draft,
        };
    }
}
