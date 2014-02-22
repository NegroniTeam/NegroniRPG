using System;
namespace NegroniGame.SystemFunctions.Exceptions
{
    public class GameException : ApplicationException
    {
        
        private string exMessage;

        public string ExMessage
        {
            get { return exMessage; }
            private set { exMessage = value; }
        }
        

        public GameException(string exMessage)
            : base(exMessage)
        {

        }

        public GameException(string exMessage, Exception innerEx)
            : base(exMessage, innerEx)
        {

        }
    }
}