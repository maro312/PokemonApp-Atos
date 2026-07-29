namespace Application.Contracts.Shared;

/// <summary>
/// Interface for a CRUD (Create, Read, Update, Delete) application service.
/// </summary>
/// <typeparam name="TInput">The input type, which must be a class.</typeparam>
/// <typeparam name="TOutput">The output type, which must be a class.</typeparam>
/// <typeparam name="TId">The identifier type, which must be a class.</typeparam>
public interface ICrudAppService<TInput, TOutput, TId, TOutputList>
    : IUpdateAppService<TInput, TOutput, TId> // inherits update functionality
    , IDeleteAppService<TOutput, TId> // inherits delete functionality
    , IGetByIdAppService<TOutput, TId> // inherits get by id functionality
    , IGetListAppService<TOutputList> // inherits get list functionality
    , ICreateAppService<TInput, TOutput> // inherits create functionality

    where TOutput : class // constraint for output type
    where TInput : class // constraint for input type
    where TOutputList : class // constraint for input type list
{

}

/// <summary>
/// Interface for a CRUD (Create, Read, Update, Delete) application service.
/// </summary>
/// <typeparam name="TCreateDto">The create dto type, which must be a class.</typeparam>
/// <typeparam name="TUpdateDto">The update dto type, which must be a class.</typeparam>
/// <typeparam name="TOutput">The output type, which must be a class.</typeparam>
/// <typeparam name="TId">The identifier type, which must be a class.</typeparam>
public interface ICrudAppService<TCreateDto, TUpdateDto, TOutput, TId, TOutputList>
    : IUpdateAppService<TUpdateDto, TOutput, TId> // inherits update functionality
    , IDeleteAppService<TOutput, TId> // inherits delete functionality
    , IGetByIdAppService<TOutput, TId> // inherits get by id functionality
    , IGetListAppService<TOutputList> // inherits get list functionality
    , ICreateAppService<TCreateDto, TOutput> // inherits create functionality

    where TOutput : class // constraint for output type
    where TUpdateDto : class // constraint for update dto type
    where TCreateDto : class // constraint for create dto type
    where TOutputList : class // constraint for input type list
{

}
