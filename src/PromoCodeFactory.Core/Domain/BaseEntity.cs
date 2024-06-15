using System;

namespace PromoCodeFactory.Core.Domain
{
    public abstract  class BaseEntity
    {
        public Guid Id { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is BaseEntity entity &&
                   Id.Equals(entity.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}