namespace CompanyManager.Domain.Models.Abstractions
{
    /// <summary>
    /// Базовый класс для всех сущностей
    /// </summary>
    public abstract class Entity : IEquatable<Entity>
    {
        public readonly Guid Id;

        protected Entity(Guid id = default)
        {
            Id = id == Guid.Empty ? id : Guid.CreateVersion7();
        }

        public bool Equals(Entity? other)
        {
            return Id.Equals(other?.Id);
        }

        public override bool Equals(object? obj)
        {
            return obj is Entity other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity? left, Entity? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity? left, Entity? right)
        {
            return !Equals(left, right);
        }
    }
}