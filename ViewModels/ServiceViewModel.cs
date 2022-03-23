using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RegistrationClient.ViewModels
{
    public class ServiceViewModel
    {
        public string HashValue(string text, string salt = "")
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }

            // Создаём хеш в кодировке SHA256
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                // Конвертируем в байты и обрабатываем получаемый поток
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text + salt);
                byte[] hashBytes = sha.ComputeHash(textBytes);
                // Конвертирем обратно в строк и получаем готовый хеш
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                return hash;
            }
        }

        public void SendData(string data)
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 6967);

            Socket sender = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            sender.Connect(remoteEP);
            sender.Send(Encoding.UTF8.GetBytes(data));
        }
    }
}
