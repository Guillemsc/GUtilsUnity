namespace GUtilsUnity.Tweening.Enums
{
    public enum SequenceAdditionMode
    {
        /// <summary>
        /// Adds the given tween to the end of the Sequence.
        /// </summary>
        Append = 0,

        /// <summary>
        /// Inserts the given tween at the same time position of the last tween, callback or interval added to the Sequence.
        /// </summary>
        Join = 1,
    }
}
