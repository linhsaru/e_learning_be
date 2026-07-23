using System.Reflection;

namespace SharedKernel.Common;

public abstract class Enumeration : IComparable, IEquatable<Enumeration>
{
    public int Id { get; }
    public string Name { get; }

    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString() => Name;

    public static IEnumerable<TEnum> GetAll<TEnum>() where TEnum : Enumeration =>
        typeof(TEnum)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Select(f => f.GetValue(null))
            .OfType<TEnum>();

    public override bool Equals(object? obj) =>
        obj is Enumeration other && Equals(other);

    public bool Equals(Enumeration? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return GetType() == other.GetType() && Id == other.Id;
    }

    public override int GetHashCode() => Id.GetHashCode();

    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;
        if (obj is Enumeration other) return Id.CompareTo(other.Id);
        throw new ArgumentException("Object is not an Enumeration", nameof(obj));
    }

    public static bool operator ==(Enumeration? left, Enumeration? right) =>
        Equals(left, right);

    public static bool operator !=(Enumeration? left, Enumeration? right) =>
        !Equals(left, right);

    public static TEnum? FromValue<TEnum>(int value) where TEnum : Enumeration =>
        GetAll<TEnum>().FirstOrDefault(item => item.Id == value);

    public static TEnum? FromName<TEnum>(string name) where TEnum : Enumeration =>
        GetAll<TEnum>().FirstOrDefault(item => string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase));
}
