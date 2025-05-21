using CleanArchitecture.Core.PlayerAggregate;
using CleanArchitecture.Shared.Extensions;
using CleanArchitecture.Shared.Query;
using CleanArchitecture.UseCases.Dtos;
using FluentValidation;
using Bruno57.Repository.Abstractions;
using Bruno57.ResultPattern.Foundations;
using Bruno57.ResultPattern.Foundations.Enums;
using Bruno57.ResultPattern.Foundations.Extensions;

namespace CleanArchitecture.UseCases.PlayerFeature.GetSomeDataForSomeId;

/// <summary>
/// Get Player by ID handler
/// </summary>
public class GetPlayerByIdHandler : IQueryHandler<GetPlayerByIdQuery, Result<FilteredPlayerDto>>
{
    private readonly IRepository<Player> _repository;
    private readonly IValidator<GetPlayerByIdQuery> _validator;

    /// <summary>
    /// Handler constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="validator"></param>
    public GetPlayerByIdHandler(IRepository<Player> repository, IValidator<GetPlayerByIdQuery> validator)
    {
        _repository = repository.CheckForNull();
        _validator = validator.CheckForNull();
    }

    /// <inheritdoc/>
    public async Task<Result<FilteredPlayerDto>> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        var errors = validationResult.PopulateValidationErrors();

        if (!validationResult.IsValid)
        {
            return Result.Invalid(errors);
        }

        var result = await _repository.GetByIdAsync(request.PlayerId, cancellationToken);

        if (result == null)
        {
            return Result<FilteredPlayerDto>.Error("Sample error message.");
        }

        return new FilteredPlayerDto
        {
            Id = result.Id,
            FullName = $"{result.FirstName} {result.LastName}",
            IsDeleted = result.IsDeleted
        };
    }
}
