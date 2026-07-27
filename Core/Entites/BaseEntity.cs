namespace Core.Contracts;

public class BaseEntity<T> : IEntity<T>
{
    public T Id { get; set; }
}