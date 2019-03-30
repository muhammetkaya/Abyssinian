using Abyssinian.Utilities.Serializations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abyssinian.Messaging.Extensions
{
    public static class MessageExtensions
    {
        public static Message AddContent(this Message message, object content)
        {
            if (message != null)
                message.Content = Serializer.JSON.Serialize(content);

            return message;
        }

        public static Message ConvertToMessage(this string message)
        {
            if (message != null)
                 return Serializer.JSON.Deserialize<Message>(message);

            return null;
        }
    }
}
