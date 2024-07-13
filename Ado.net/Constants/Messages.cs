using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Ado.net.Constants
{
    internal static class Messages
    {
        public static void InvalidInputMessage(string title) => Console.WriteLine($"{title} is invalid try again");
      public static void InputMessage(string title) => Console.WriteLine($"Input{title}");

        public static void SuccessAddMessage(string title,string value) => Console.WriteLine($"{title} - {value} Elave oldu");
        public static void SuccessUpdateMessage(string title, string value) => Console.WriteLine($"{title} - {value} Update oldu");
        public static void SuccessDeleteMessage(string title, string value) => Console.WriteLine($"{title} - {value} Silindi");

        public static void ErrorOccuredMessage() => Console.WriteLine($"Error");
        public static void AlreadyExistMessage(string title, string value) => Console.WriteLine($"{title} - {value} already exists ");

        public static void PrintMessage(string title, string value) => Console.WriteLine($"{title} - {value}");
        public static void NotFoundMessage(string title, string value) => Console.WriteLine($"{title} - {value} Not Found");
        public static void PrintWantToChangeMessage(string title) => Console.WriteLine($"Want to change {title}? Y or No");


    }
}
