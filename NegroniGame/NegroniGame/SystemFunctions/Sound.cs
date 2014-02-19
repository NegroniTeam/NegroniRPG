namespace NegroniGame.SystemFunctions
{
    using System;
    using System.Media;

    public static class Sound
    {
        private readonly static SoundPlayer inGameMusic = new SoundPlayer(@"..\..\..\..\NegroniGameContent\media\DST-Exanos.wav");

        public static void PlayIngameMusic()
        {
            inGameMusic.PlayLooping();
        }

        public static void StopIngameMusic()
        {
            inGameMusic.Stop();
        }
    }
}