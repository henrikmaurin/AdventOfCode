using System.Numerics;

namespace Common
{
    public static class CircularExtensions
    {
        public static T CircularWrap<T>(this T value, T min, T max)
            where T : INumber<T>
        {
            T range = max - min + T.One;
            T shifted = value - min;
            T wrapped = ((shifted % range) + range) % range;
            return wrapped + min;
        }

        public static (T finalValue, int hitCount) CircularWrapWithHits<T>(
            this T start,
            T delta,
            T min,
            T max,
            T target
            ) where T : INumber<T>
        {
            T range = max - min + T.One;

            // Hur många fulla varv passeras?
            T absDelta = T.Abs(delta);
            T fullTurns = absDelta / range;

            int hits = 0;

            // Varje helt varv inkluderar ETT stopp på varje värde
            hits += int.CreateChecked(fullTurns);

            // Beräkna var vi hamnar efter fulla varv
            T remainder = delta % range;
            T finalValue = (start + remainder).CircularWrap(min, max);

            // Räknas endast om finalValue == target
            if (finalValue == target )
                hits++;

            return (finalValue, hits);
        }

        public static (T finalValue, int passCount) CircularWrapWithPassages<T>(
       this T start,
       T delta,
       T min,
       T max,
       T target
   ) where T : INumber<T>
        {
            T range = max - min + T.One;

            // Fulla varv och resterande steg
            T absDelta = T.Abs(delta);
            T fullTurns = absDelta / range;
            T remainder = absDelta % range;

            int passes = 0;

            // Om target != start: varje helt varv passerar target exakt en gång
            if (target != start)
                passes += int.CreateChecked(fullTurns);

            // Bestäm riktning
            T step = delta > T.Zero ? T.One : -T.One;

            // Nu simulerar vi återstående steg (men bara remainder, inte hela delta)
            T current = start;

            for (T i = T.Zero; i < remainder; i++)
            {
                current += step;

                // Wrap
                if (current > max) current = min;
                else if (current < min) current = max;

                if (current == target)
                    passes++;
            }

            return (current, passes);
        }
    }
}
