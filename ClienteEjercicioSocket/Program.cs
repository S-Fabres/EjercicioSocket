using ClienteEjercicioSocket.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteEjercicioSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            string servidor = ConfigurationManager.AppSettings["servidor"];
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Conectado a servidor");
            ClienteSocket clienteSocket = new ClienteSocket(servidor, puerto);

            if (clienteSocket.Conectar())
            {
                Console.WriteLine("Ingrese puerto del servidor:");
                string puertoEntrante = clienteSocket.Leer();
                int puertoInt = Convert.ToInt32(puertoEntrante);
                if (puertoInt == puerto)
                {
                    Console.WriteLine("Ingrese IP del servidor:");
                    string servidorEntrante = Convert.ToString(clienteSocket.Leer());

                    if (servidorEntrante == servidor)
                    {
                        string mensaje="";
                        while (mensaje != "chao")
                        {
                            Console.WriteLine("Ingrese un mensaje");
                            mensaje = clienteSocket.Leer();
                            Console.WriteLine(mensaje);
                        }
                        Console.WriteLine("chao");
                        clienteSocket.Desconectar();
                    }
                    else
                    {
                        Console.WriteLine("IP incorrecta");
                        Console.WriteLine("chao");
                        clienteSocket.Desconectar();
                    }
                }
                else
                {
                    Console.WriteLine("Puerto incorrecto");
                    Console.WriteLine("chao");
                    clienteSocket.Desconectar();
                }
            }
            else
            {
                Console.WriteLine("Error de comunicación");
                Console.WriteLine("chao");
            }
            Console.ReadKey();
        }
    }
}
