using System;

namespace UsbHid.USB.Classes.Messaging
{
    public class CommandMessage : IMesage
    {
        private byte[] _parameters;
        public byte[] MessageData { get { return GetMessageData(); } }

        public bool isRead = true;
        private byte[] GetMessageData()
        {
            byte[] result = new byte[16];
            result[0] = isRead ? (byte)0x5B : (byte)0x5A;
            result[1] = Command;
            if (Parameters != null)
            {
                Array.Copy(Parameters, 0, result, 2, Parameters.Length);
            }

            byte sum = 0;
            for (int i = 0; i < result.Length - 1; i++)
            {
                sum += result[i];
            }
            result[15] = (byte)(sum & 0xFF);
            return result;
        }

        public byte Command { get; set; }

        public byte[] Parameters
        {
            get { return _parameters; }
            set
            {
                if (value != null && value.Length > 0)
                {
                    int length = value.Length > 13 ? 13 : value.Length;
                    _parameters = new byte[length];
                    Array.Copy(value, _parameters, length);
                }
            }
        }

        public CommandMessage(byte command, bool isRead = true)
        {
            Command = command;
            this.isRead = isRead;
        }

        public CommandMessage(byte command, bool isRead = true, params byte[] parameters)
            : this(command, isRead)
        {
            Parameters = parameters;
        }

    }
}
