﻿using System.Configuration;
using Microsoft.Owin.Hosting;

namespace Funkmap.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = ConfigurationManager.AppSettings["serverAddress"];

            using (WebApp.Start<Startup>(baseAddress))
            {
                System.Console.WriteLine($"Основной сервер запущен по адерсу {baseAddress}");
                System.Console.ReadLine();
            }
        }
    }
}
