using System;

namespace WindowsFormsApp1
{
    public enum CompetitionUserEventArgs
    {
        ParticipantRegister
    }
    public class ChatUserEventArgs : EventArgs
    {
        private readonly CompetitionUserEventArgs userEvent;
        private readonly Object data;

        public ChatUserEventArgs(CompetitionUserEventArgs userEvent, object data)
        {
            this.userEvent = userEvent;
            this.data = data;
        }

        public CompetitionUserEventArgs UserEventType
        {
            get { return userEvent; }
        }

        public object Data
        {
            get { return data; }
        }
    }
}

