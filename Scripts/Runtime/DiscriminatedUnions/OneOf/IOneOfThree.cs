namespace GUtilsUnity.DiscriminatedUnions
{
    public interface IOneOf<TFirst, TSecond, TThird>
    {
        bool TryGetFirst(out TFirst value);
        bool TryGetSecond(out TSecond value);
        bool TryGetThird(out TThird value);
        TFirst GetFirstUnsafe();
        TSecond GetSecondUnsafe();
        TThird GetThirdUnsafe();
    }
}
