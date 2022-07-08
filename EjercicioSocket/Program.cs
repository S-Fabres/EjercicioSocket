using EjercicioSocket.Comunicaciones;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            string server = ConfigurationManager.AppSettings["servidor"];
            Console.WriteLine("Iniciando Servidor en puerto {0}", puerto);
            ServerSocket servidor = new ServerSocket(puerto);

            if (servidor.Iniciar())
            {
                Console.WriteLine("Servidor iniciado");
                while (true)
                {
                    Console.WriteLine("Esperando Cliente...");
                    Socket socketCliente = servidor.ObtenerCliente();
                    ClienteCom cliente = new ClienteCom(socketCliente);
                    cliente.Escribir("Ingrese puerto del servidor:");
                    string puertoEntrante = cliente.Leer();
                    if (puertoEntrante.Length != 0)
                    {
                        int puertoInt = Convert.ToInt32(puertoEntrante);
                        if (puertoInt == puerto)
                        {
                            cliente.Escribir("Ingrese IP del servidor:");
                            string servidorEntrante = cliente.Leer();
                            if (servidorEntrante.Length != 0)
                            {
                                if (servidorEntrante == server)
                                {
                                    string mensaje = "";
                                    while (mensaje != "chao")
                                    {
                                        cliente.Escribir("Ingrese un mensaje:");
                                        mensaje = cliente.Leer();
                                        cliente.Escribir(mensaje);
                                    }
                                    cliente.Escribir("chao");
                                    cliente.Desconectar();
                                }
                                else
                                {
                                    cliente.Escribir("IP del servidor incorrecta");
                                    cliente.Escribir("chao");
                                    cliente.Desconectar();
                                }
                            }
                            else
                            {
                                cliente.Escribir("Campo vacío");
                                cliente.Escribir("chao");
                                cliente.Desconectar();
                            }
                        }
                        else
                        {
                            cliente.Escribir("Puerto incorrecto");
                            cliente.Escribir("chao");
                            cliente.Desconectar();
                        }
                    }
                    else
                    {
                        cliente.Escribir("Campo vacío");
                        cliente.Escribir("chao");
                        cliente.Desconectar();
                    }
                }
            }
            else
            {
                Console.WriteLine("Error, el puerto {0} esta en uso...", puerto);
            }
        }
    }
}
