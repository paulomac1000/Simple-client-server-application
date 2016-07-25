using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Klient
{
    class Program
    {

        static void Main(string[] args)
        {
            Socket klient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.Write("Podaj adres IP serwera: ");
            string IP = Console.ReadLine();
            
            try
            {
                klient.Connect(IPAddress.Parse(IP), 31230);
            }
            catch
            {
                Console.WriteLine("Nie można nawiązać połączenia.");
                Main(args);
            }
            Console.WriteLine("Połączono!");


            //odbieram numer od serwera

            byte[] bufor = new byte[4];
            klient.Receive(bufor);
            

            int n = BitConverter.ToInt32(bufor, 0);
            Console.WriteLine("Odebrano od serwera liczbę n = {0}",n);

            for (int i=1; i<= n; i++)
            {
                try
                {
                    klient.Send(BitConverter.GetBytes(i));
                }
                catch
                {
                    Console.WriteLine("Nie wysłano liczby: {0}",i);
                    Console.WriteLine(" Połączenie zostało przerwane.");
                    break;
                }
                
                Console.WriteLine("Wysłano liczbę: {0}",i);
                System.Threading.Thread.Sleep(1000);
            }

            Console.WriteLine("Program zakoczył wysyłanie. Wciśnij dowolny klawisz aby zakończyć");
            klient.Close();

            Console.ReadKey();
        }
    }
}
