namespace NegroniGame.SystemFunctions
{
    using System;

    public class GameException : ApplicationException
    {
        public string ExMessage { get; set; }

        public GameException(string ExMessage)
            : base(ExMessage)
        {

        }

        public GameException(string ExMessage, Exception innerEx)
            : base(ExMessage, innerEx)
        {

        }
    }
}