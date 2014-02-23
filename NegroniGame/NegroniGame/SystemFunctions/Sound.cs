namespace NegroniGame.SystemFunctions
{
    using System;
    using System.Media;

    public static class Sound
    {
        private readonly static SoundPlayer inGameMusic = new SoundPlayer(@"..\..\..\..\NegroniGameContent\media\DST-Exanos.wav");
        private readonly static SoundPlayer gameOverMusic = new SoundPlayer(@"..\..\..\..\NegroniGameContent\media\DST-GameOver.wav");

        public static void PlayIngameMusic()
        {
            inGameMusic.PlayLooping();
        }

        public static void StopIngameMusic()
        {
            inGameMusic.Stop();
        }
        public static void PlayGameOverMusic()
        {
            gameOverMusic.PlayLooping();
        }
    }
}