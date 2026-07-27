using Core.Contracts;

namespace Core.Dtos;

public class BaseDto<T> : IEntity<T>
{
    public T Id { get; set; }
}