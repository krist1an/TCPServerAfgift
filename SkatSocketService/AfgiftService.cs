using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using RegistreringsAfgift;

namespace TcpSkat
{
    class AfgiftService
    {
        private TcpClient connectionSocket;


        // Laver en reference til afgift class library
        private Afgift afgift;

        public AfgiftService(TcpClient connectionSocket)
        {
            this.connectionSocket = connectionSocket;
            afgift = new Afgift();
        }


        
        internal void DoIt()
        {
            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns) {AutoFlush = true};

            string message = sr.ReadLine();
            string answer;

            // Undersøger om beskeden er tom eller null før der fortsættes
            while (!string.IsNullOrEmpty(message))
            {
                try
                {
                    // Kunne eventuelt bruge decimal.TryParse(message, out var pris)

                    decimal pris = decimal.Parse(message);

                    if (pris >= 0)
                    {
                        sw.WriteLine("Hvilken type bil drejer det sig om?");
                        string type = sr.ReadLine();
                        switch (type.ToLower())
                        {
                            case "normal":
                                sw.WriteLine($"Biler af typen {type} med en pris på {pris} vil have en afgift på {afgift.BilAfgift(pris)}");
                                break;
                            case "el":
                                sw.WriteLine($"Biler af typen {type} med en pris på {pris} vil have en afgift på {afgift.ElBilAfgift(pris)}");
                                break;
                            default:
                                sw.WriteLine("Brug venligst 'normal' eller 'el'");
                                break;
                        }
                    }
                    else
                    {
                        throw new Exception("Systemet kan ikke håndtere negative tal");
                    }

                }
                catch (Exception e)
                {
                    answer = $"Brug venligst tal | {e.Message}";
                    sw.WriteLine(answer);
                }

                // Venter på en ny pris

                
                message = sr.ReadLine();
            }

            sw.WriteLine("Stopping ..");
            ns.Close();
            connectionSocket.Close();
        }

  



    }
}
