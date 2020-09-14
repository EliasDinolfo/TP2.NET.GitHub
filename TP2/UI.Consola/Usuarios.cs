using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Business.Logic;

namespace UI.Consola
{
    public class Usuarios
    {
        private UsuarioLogic _UsuarioNegocio;

        public Usuarios()
        {
            UsuarioNegocio = new UsuarioLogic(); 
        }

        public UsuarioLogic UsuarioNegocio
        {
            get => _UsuarioNegocio;
            set { _UsuarioNegocio = value; }
        }

        public void Menu()
        {
            int opc;
            do
            {
                Console.Clear();
                Console.WriteLine("1- Listado general");
                Console.WriteLine("2- Consulta");
                Console.WriteLine("3- Agregar");
                Console.WriteLine("4- Modificar");
                Console.WriteLine("5- Eliminar");
                Console.WriteLine("0- Salir");
                Console.WriteLine("Elija una opción.");
                opc = int.Parse(Console.ReadLine());
                switch (opc)
                {
                    case 1:
                        {
                            ListadoGeneral();
                            break;
                        }
                    case 2:
                        {
                            Consultar();
                            break;
                        }
                    case 3:
                        {
                            Agregar();
                            break;
                        }
                    case 4:
                        {
                            Modificar();
                            break;
                        }
                    case 5:
                        {
                            Eliminar();
                            break;
                        }
                }
            } while (opc != 0);
        }
        public void ListadoGeneral()
        {
            Console.Clear();
            foreach (Usuario usr in UsuarioNegocio.GetAll())
            {
                MostrarDatos(usr);
            }
            Console.WriteLine("Presione una tecla para regresar");
            Console.ReadKey();
        }
        public void MostrarDatos(Usuario usr)
        {
            Console.WriteLine("Usuario: "+ usr.ID);
            Console.WriteLine("\t\tNombre: "+ usr.Nombre);
            Console.WriteLine("\t\tApellido: "+ usr.Apellido);
            Console.WriteLine("\t\tNombre de Usuario: "+ usr.NombreUsuario);
            Console.WriteLine("\t\tClave: "+ usr.Clave);
            Console.WriteLine("\t\tEmail: "+ usr.Email);
            Console.WriteLine("\t\tHabilitado: "+ usr.Habilitado);
            Console.WriteLine();
        }
        public void Consultar()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Ingrese el ID del usuario a consultar: ");
                int ID = int.Parse(Console.ReadLine());
                this.MostrarDatos(UsuarioNegocio.GetOne(ID));
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("La ID ingresada no está registrada");
            }
            catch (FormatException)
            {
                Console.WriteLine("La ID ingresada debe ser un número entero.");
            }
            
            finally
            {
                Console.WriteLine("Ingrese una tecla para continuar...");
                Console.ReadKey();
            }
        }
        public void Modificar()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Ingrese la ID del usuario a modificar: ");
                int ID = int.Parse(Console.ReadLine());
                Usuario usuario = UsuarioNegocio.GetOne(ID);
                Console.WriteLine("Ingrese Nombre");
                usuario.Nombre = Console.ReadLine();               
                Console.WriteLine("Ingrese Apellido");
                usuario.Apellido = Console.ReadLine();
                Console.WriteLine("Ingrese Nombre de Usuario");
                usuario.NombreUsuario = Console.ReadLine();                
                Console.WriteLine("Ingrese Clave");
                usuario.Clave = Console.ReadLine(); 
                Console.WriteLine("Ingrese Email");
                usuario.Email = Console.ReadLine();
                Console.WriteLine("Ingrese Habilitación del usuario (1- Si/ Otra tecla- No");
                usuario.Habilitado = Console.ReadLine()=="1";
                usuario.State = BusinessEntity.States.Modified;
                UsuarioNegocio.Save(usuario);
            }
            catch (FormatException)
            {
                Console.WriteLine("La ID ingresada debe ser un número entero.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Ingrese una tecla para continuar...");
                Console.ReadKey();
            }
        }
        public void Agregar()
        {
            Usuario usuario = new Usuario();
            Console.Clear();
            Console.WriteLine("Ingrese Nombre");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese Apellido");
            usuario.Apellido = Console.ReadLine();
            Console.WriteLine("Ingrese Nombre de Usuario");
            usuario.NombreUsuario = Console.ReadLine();
            Console.WriteLine("Ingrese Clave");
            usuario.Clave = Console.ReadLine();
            Console.WriteLine("Ingrese Email");
            usuario.Email = Console.ReadLine();
            Console.WriteLine("Ingrese Habilitación del usuario (1- Si/ Otra tecla- No");
            usuario.Habilitado = Console.ReadLine() == "1";
            usuario.State = BusinessEntity.States.New;
            UsuarioNegocio.Save(usuario);
            Console.WriteLine();
            Console.WriteLine("ID: (0)", usuario.ID);
        }
        public void Eliminar()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Ingrese la ID del usuario a eliminar:");
                int ID = int.Parse(Console.ReadLine());
                UsuarioNegocio.Delete(ID);
            }
            catch (FormatException)
            {
                Console.WriteLine("La ID ingresada debe ser un número entero.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Ingrese una tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}
