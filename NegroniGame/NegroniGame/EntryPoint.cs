namespace NegroniGame
{
    using System;

    public static class EntryPoint
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        static void Main()
        {

            using (GameScreen game = GameScreen.Instance)
            {
                game.Run();
            }
        }
    }
}

