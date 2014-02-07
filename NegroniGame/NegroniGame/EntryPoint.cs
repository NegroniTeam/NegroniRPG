namespace NegroniGame
{
    using System;

    static class EntryPoint
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            using (Screens.GameScreen game = new Screens.GameScreen())
            {
                game.Run();
            }

            //using (Screens.StartScreen StartMenu = new Screens.StartScreen())
            //{
            //    StartMenu.Run();
            //}
        }
    }
}

