namespace Impart
{
    public class Pixels : Measurement
    {
        private static int Value;

        /// <summary>Creates a Pixels instance with <paramref name="pixels"/> as the value.</summary>
        /// <returns>A Pixels instance.</returns>
        /// <param name="pixels">The pixels value.</param>
        public Pixels(int pixels)
        {
            if (pixels < 0)
            {
                throw new ImpartError("Pixel number must be positive!");
            }
            Value = pixels;
        }

        /// <summary>Convert the Pixels instance to a String.</summary>
        /// <returns>A String instance.</returns>
        public override string ToString()
        {
            return Value.ToString();
        }

        /// <summary>Convert the Pixels instance to an Int.</summary>
        /// <returns>An Int instance.</returns>
        /// <param name="p">The Pixels to convert.</param>
        public static implicit operator int(Pixels p) => Value;

        /// <summary>Convert the Int instance to Pixels.</summary>
        /// <returns>A Pixels instance.</returns>
        /// <param name="i">The Int to convert.</param>
        public static implicit operator Pixels(int i) => new Pixels(i);
    }
}