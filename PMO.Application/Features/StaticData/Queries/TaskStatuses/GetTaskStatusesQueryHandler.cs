namespace PMO.Application.Features.StaticData.Queries.TaskStatuses;

public class GetTaskStatusesQueryHandler : IRequestHandler<GetTaskStatusesQuery, Result<IReadOnlyList<StaticDataResponse>>>
{
    public async Task<Result<IReadOnlyList<StaticDataResponse>>> Handle(GetTaskStatusesQuery request, CancellationToken cancellationToken)
    {
        var taskStatuses = Enum.GetValues(typeof(ETaskStatus))
            .Cast<ETaskStatus>()
            .Select(status => new StaticDataResponse((int)status, status.ToString()))
            .ToList();
        return Result<IReadOnlyList<StaticDataResponse>>.Success(taskStatuses);
    }
}