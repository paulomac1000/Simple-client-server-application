using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Serwer
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket serwer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.Write("Podaj adres IP: ");
            string serwer_ip = Console.ReadLine();
            serwer.Bind(new IPEndPoint(IPAddress.Parse(serwer_ip),31230)); // przypisuje gniazdku podany adres nasłuchiwania na porcie 31230
            serwer.Listen(1); //uruchamiam nasłuchiwanie i czekam na 1 klienta

            Console.WriteLine("Adres IP serwera to: {0}", serwer_ip);
            Console.WriteLine("Oczekuję na klienta...");
            Socket połączony = serwer.Accept();
            
            Console.WriteLine("Połączono!");
            
            Console.Write("Podaj liczbę \"n\": ");
            int n = Convert.ToInt32(Console.ReadLine());

            try
            {   //wysyłamy do klienta  liczbę n
                połączony.Send(BitConverter.GetBytes(n));

            }
            catch
            {
                Console.WriteLine("Nie udało się wysłać liczby {0}",n);
                Main(args);
            }
            Console.WriteLine("Wysłano liczbę n = {0}",n);

            byte[] bufor = new byte[4];
            
            for (int i=1; i<= n; i++)
            {
                połączony.Receive(bufor);
                Console.WriteLine("Odebrano liczbę: {0}", BitConverter.ToInt32(bufor, 0));
                //Console.WriteLine(BitConverter.ToInt32(bufor, 0));
            }

            Console.WriteLine("Program zakończył odbieranie danych. Wciśnij dowolny klawisz aby zakończyć");

            serwer.Close();
            połączony.Close();

            Console.ReadKey();    
        }
    }
}
