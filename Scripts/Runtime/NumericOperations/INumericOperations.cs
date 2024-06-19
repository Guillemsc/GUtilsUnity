namespace GUtilsUnity.NumericOperations
{
    public interface INumericOperations<T>
    {
        T Zero { get; }
        T One { get; }
        T Addition(T first, T second);
        T Subtraction(T first, T second);
        T Multiplication(T first, T second);
        T Division(T first, T second);
        T Modulo(T first, T second);
        T Negation(T value);
        T Absolute(T value);
        T Min(T first, T second);
        T Max(T first, T second);
        bool IsZero(T value);
        bool IsGreaterThan(T first, T second);
        bool IsGreaterThanOrEqual(T first, T second);
        bool IsLessThan(T first, T second);
        bool IsLessThanOrEqual(T first, T second);
        bool AreEqual(T first, T second);
    }
}
