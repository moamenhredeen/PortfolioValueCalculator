namespace Domain.ValueObjects;

/// <summary>
/// Represents a monetary value
/// this is a very simple representation of a monetary value to demonstrate the concept
/// of a value object.
/// In a real world scenario a money type should be currency-aware
/// and should handle all rounding edge cases.
/// </summary>
public readonly record struct Money(decimal Value)
{

    
    /// <summary>
    /// Calculates the given percentage of this money value.
    /// </summary>
    /// <param name="percent">The percentage as a decimal (e.g., 0.1 for 10%).</param>
    /// <returns>A new Money instance representing the percentage of the original value.</returns>
    public Money Percent(decimal percent)
    {
        return new Money(Value * percent);
    }
    
    public static Money operator +(Money lhs, Money rhs)
    {
        return new Money(lhs.Value + rhs.Value);
    }
    
    public static Money operator -(Money lhs, Money rhs)
    {
        return new Money(lhs.Value - rhs.Value);
    }
}