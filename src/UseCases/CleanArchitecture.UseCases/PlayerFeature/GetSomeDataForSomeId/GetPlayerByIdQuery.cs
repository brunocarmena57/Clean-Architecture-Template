using CleanArchitecture.Shared.Query;
using CleanArchitecture.UseCases.Dtos;
using Bruno57.ResultPattern.Foundations;

namespace CleanArchitecture.UseCases.PlayerFeature.GetSomeDataForSomeId;

/// <summary>
/// Query for fetching player details
/// </summary>
/// <param name="PlayerId"></param>
public record GetPlayerByIdQuery(int PlayerId) : IQuery<Result<FilteredPlayerDto>>;
