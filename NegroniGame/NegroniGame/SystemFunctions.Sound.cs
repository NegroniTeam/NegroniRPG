namespace NegroniGame.SystemFunctions
{
    using System;
    using System.Media;

    public static class Sound
    {
        private static SoundPlayer inGameMusic = new SoundPlayer(@"..\..\..\..\NegroniGameContent\media\DST-Exanos.wav");

        public static void PlayIngameMusic()
        {
            inGameMusic.PlayLooping();
        }
        public static void StopIngameMusic()
        {
            inGameMusic.Stop();
        }

        //public static bool isPlayingMusic = true;
        //public static bool isPlayingMusicMenu = true;
        //public static bool isStartedMusicMenu = false;

        // private static SoundPlayer menuMusic = new SoundPlayer(@"..\..\ExternalFiles\DST-XToFly.wav");

        //public static void StopMenuMusic()
        //{
        //    menuMusic.Stop();
        //    isPlayingMusicMenu = false;
        //}

        //public static void StopIngameMusic()
        //{
        //    inGameMusic.Stop();
        //}
        //public static void PauseIngameMusic()
        //{
        //    inGameMusic.Stop();
        //    isPlayingMusic = false;
        //}
        //public static void PlayMenuMusic()
        //{
        //    menuMusic.PlayLooping();

        //}

        //private static SoundPlayer go = new SoundPlayer(@"..\..\ExternalFiles\Go.wav");
        //private static SoundPlayer one = new SoundPlayer(@"..\..\ExternalFiles\One.wav");
        //private static SoundPlayer two = new SoundPlayer(@"..\..\ExternalFiles\Two.wav");
        //private static SoundPlayer three = new SoundPlayer(@"..\..\ExternalFiles\Three.wav");

        //public static void PlayOne()
        //{
        //    one.Play();
        //}
        //public static void PlayTwo()
        //{
        //    two.Play();
        //}
        //public static void PlayThree()
        //{
        //    three.Play();
        //}
        //public static void PlayGo()
        //{
        //    go.Play();
        //}

    }
}