namespace Domain.ValueObjects;

/// <summary>
/// Represents an International Securities Identification Number (ISIN).
/// </summary>
/// <remarks>
/// This is a simple representation of an ISIN as a string wrapper.
/// In a real-world scenario, this class would include comprehensive validation logic,
/// such as format checks and length validation to ensure ISIN correctness.
/// </remarks>
public readonly record struct ISIN(string Value)
{
}