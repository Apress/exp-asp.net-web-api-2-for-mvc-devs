using Microsoft.Owin.Hosting;
using System;

namespace SelfHost {

    class Program {

        static void Main(string[] args) {
            WebApp.Start<Startup>("http://localhost:5000/");
            Console.ReadLine();
        }
    }
}
