using System;
using System.Collections.Generic;
using System.Text;

namespace Memory.Shared
{
    public class BaseEntity<TEntityKey> where TEntityKey : struct
    {
        public virtual TEntityKey Id { get; set; }
        public override bool Equals(object obj)
        {
            if (!(obj is BaseEntity<TEntityKey>))
            {
                return false;
            }
            return Equals((BaseEntity<TEntityKey>)obj);
        }
        private static bool IsTransient(BaseEntity<TEntityKey> obj)
        {
            return obj != null && Equals(obj.Id, default(int));
        }

        public virtual bool Equals(BaseEntity<TEntityKey> other)
        {
            if (other == null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (!IsTransient(this) &&
                !IsTransient(other) &&
                Equals(Id, other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                var isAssignable = thisType.IsAssignableFrom(otherType) || otherType.IsAssignableFrom(thisType);
                return isAssignable;
            }
            return false;
        }

        public static bool operator ==(BaseEntity<TEntityKey> x, BaseEntity<TEntityKey> y)
        {
            if (Equals(x, null))
            {
                return (Equals(y, null));
            }
            else if (Equals(y, null))
            {
                return false;
            }
            return Equals(x.Id, y.Id);
        }
        public static bool operator !=(BaseEntity<TEntityKey> x, BaseEntity<TEntityKey> y)
        {
            return !(x == y);
        }
        public Type GetUnproxiedType()
        {
            return GetType();
        }
    }
}
