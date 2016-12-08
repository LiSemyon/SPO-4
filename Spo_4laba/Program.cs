using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spo_4laba {
    class Program {
        static void Main(string[] args) {
            Console.CancelKeyPress += delegate { Environment.Exit(0); };
            while (true) {
                Console.Write(Directory.GetCurrentDirectory() + ": ");
                String s = Console.ReadLine().ToString();
                if (s.Contains(Commands.FIND)) {
                    Commands.parseCommand(s);
                }
                else {
                    Console.WriteLine("Command " + "\'" + s + "\' " + "not found");
                }
                
                /*if (s.Length > 3) {
                    if (s.Substring(0, 2).Equals("cd")) {
                        
                    }
                }*/
            } 
        }
    }
}
