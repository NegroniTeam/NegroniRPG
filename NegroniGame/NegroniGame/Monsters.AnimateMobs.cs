namespace NegroniGame.Monsters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;

    public class AnimateMobs
    {
        private static float elapsed;
        private static float delay = 200f;
        private static int frames = 0;
        private static Random randomGenerator = new Random();

        public static Rectangle Animate(GameTime gameTime)
        {
            //int direction = randomGenerator.Next(1, 4);
            //int positions = randomGenerator.Next(10, 400);

            //if (direction == 1)
            //{
            //    // right
            //}

            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (frames >= 2)
                {
                    frames = 0;
                }
                else
                {
                    frames++;
                }
                elapsed = 0;
            }

            // if on frame 0 - top up position 0
            return new Rectangle(64 * frames, 0, 64, 64);
        }
    }
}
