using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Data.Database
{
    public class ModuloUsuarioAdapter:Adapter
    {
        public List<ModuloUsuario> GetAll()
        {
            //instanciamos el objeto lista a retornar
            List<ModuloUsuario> modulosUsuarios = new List<ModuloUsuario>();

            //abrimos la conexión a la base de datos que creamos antes
            try
            {
                this.OpenConnection();
                /*
                 * creamos un objeto SqlCommand que será la sentencia SQL
                 * que vamos a ejecutar contra la base de datos
                 * (los datos de la BD usuario, contraseña, servidor, etc. 
                 * están en el connection String)
                */
                SqlCommand cmdModulosUsuarios = new SqlCommand("select * from modulos_usuarios", sqlConn);
                /* 
                 * instanciamos un objeto DataReader que será
                 * el que recuperará los datos de la BD
                */
                SqlDataReader drModulosUsuarios = cmdModulosUsuarios.ExecuteReader();
                /*
                 * Read() devuelve una fila de las devueltas por el comando sql
                 * carga los datos en drUsuarios para poder accederlos,
                 * devuelve verdadero mientras haya podido leer datos
                 * y avanza a la fila siguiente para el próximo read
                */
                while (drModulosUsuarios.Read())
                {
                    /*
                     * creamos un objeto ModuloUsuario de la capa entidades para copiar
                     * los datos de la fila del DataReader al objeto de entidades
                    */
                    ModuloUsuario mu = new ModuloUsuario();

                    //ahora copiamos los datos de la fila al objeto
                    mu.ID = (int)drModulosUsuarios["id_modulo_usuario"];
                    mu.IdModulo = (int)drModulosUsuarios["id_modulo"];
                    mu.IdUsuario = (int)drModulosUsuarios["id_usuario"];
                    mu.PermiteAlta = (bool)drModulosUsuarios["alta"];
                    mu.PermiteBaja = (bool)drModulosUsuarios["baja"];
                    mu.PermiteModificacion = (bool)drModulosUsuarios["modificacion"];
                    mu.PermiteConsulta = (bool)drModulosUsuarios["consulta"];

                    //agregamos el objeto con datos a la lista que devolveremos
                    modulosUsuarios.Add(mu);
                }
                //cerramos el data reader y la conexion a la BD
                drModulosUsuarios.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar lista de modulos de usuarios", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            //devolvemos el objeto
            return modulosUsuarios;
        }

        public ModuloUsuario GetOne(int ID)
        {
            ModuloUsuario mu = new ModuloUsuario();

            try
            {
                this.OpenConnection();
                SqlCommand cmdModulosUsuarios = new SqlCommand("select * from modulos_usuarios where id_modulo_usuario=@id", sqlConn);
                cmdModulosUsuarios.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drModulosUsuarios = cmdModulosUsuarios.ExecuteReader();
                while (drModulosUsuarios.Read())
                {
                    mu.ID = (int)drModulosUsuarios["id_modulo_usuario"];
                    mu.IdModulo = (int)drModulosUsuarios["id_modulo"];
                    mu.IdUsuario = (int)drModulosUsuarios["id_usuario"];
                    mu.PermiteAlta = (bool)drModulosUsuarios["alta"];
                    mu.PermiteBaja = (bool)drModulosUsuarios["baja"];
                    mu.PermiteConsulta = (bool)drModulosUsuarios["consulta"];
                    mu.PermiteModificacion = (bool)drModulosUsuarios["modificacion"];
                }
                drModulosUsuarios.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar datos de modulos de usuarios", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            return mu;
        }

        public List<ModuloUsuario> GetPorUsuario(int ID)
        {
            List<ModuloUsuario> modulosUsuarios = new List<ModuloUsuario>();

            try
            {
                this.OpenConnection();
                SqlCommand cmdModulosUsuarios = new SqlCommand("select * from modulos_usuarios where id_usuario=@id", sqlConn);
                cmdModulosUsuarios.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drModulosUsuarios = cmdModulosUsuarios.ExecuteReader();
                while (drModulosUsuarios.Read())
                {
                    ModuloUsuario mu = new ModuloUsuario();
                    mu.ID = (int)drModulosUsuarios["id_modulo_usuario"];
                    mu.IdModulo = (int)drModulosUsuarios["id_modulo"];
                    mu.IdUsuario = (int)drModulosUsuarios["id_usuario"];
                    mu.PermiteAlta = (bool)drModulosUsuarios["alta"];
                    mu.PermiteBaja = (bool)drModulosUsuarios["baja"];
                    mu.PermiteConsulta = (bool)drModulosUsuarios["consulta"];
                    mu.PermiteModificacion = (bool)drModulosUsuarios["modificacion"];
                    modulosUsuarios.Add(mu);
                }
                drModulosUsuarios.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar datos de modulos de usuarios", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            return modulosUsuarios;
        }
        public List<ModuloUsuario> GetPorModulo(int ID)
        {
            List<ModuloUsuario> modulosUsuarios = new List<ModuloUsuario>();

            try
            {
                this.OpenConnection();
                SqlCommand cmdModulosUsuarios = new SqlCommand("select * from modulos_usuarios where id_modulo=@id", sqlConn);
                cmdModulosUsuarios.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drModulosUsuarios = cmdModulosUsuarios.ExecuteReader();
                while (drModulosUsuarios.Read())
                {
                    ModuloUsuario mu = new ModuloUsuario();
                    mu.ID = (int)drModulosUsuarios["id_modulo_usuario"];
                    mu.IdModulo = (int)drModulosUsuarios["id_modulo"];
                    mu.IdUsuario = (int)drModulosUsuarios["id_usuario"];
                    mu.PermiteAlta = (bool)drModulosUsuarios["alta"];
                    mu.PermiteBaja = (bool)drModulosUsuarios["baja"];
                    mu.PermiteConsulta = (bool)drModulosUsuarios["consulta"];
                    mu.PermiteModificacion = (bool)drModulosUsuarios["modificacion"];
                    modulosUsuarios.Add(mu);
                }
                drModulosUsuarios.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar datos de modulos de usuarios", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            return modulosUsuarios;
        }

        public void Delete(int ID)
        {
            try
            {
                //abrimos la conexión
                this.OpenConnection();

                //creamos la setencia SQL y asignamos un valor al parámetro
                SqlCommand cmdDelete = new SqlCommand("delete modulos_usuarios where id_modulo_usuario=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                //ejecutamos la setencia SQL
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar modulo de usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public void Update(ModuloUsuario moduloUsuario)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("UPDATE modulos_usuarios SET id_modulo=@id_modulo, id_usuario=@id_usuario," +
                    "alta=@alta, baja=@baja, modificacion=@modificacion, consulta=@consulta WHERE id_modulo_usuario=@id", sqlConn);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = moduloUsuario.ID;
                cmdSave.Parameters.Add("@id_modulo", SqlDbType.Int).Value = moduloUsuario.IdModulo;
                cmdSave.Parameters.Add("@id_usuario", SqlDbType.Int).Value = moduloUsuario.IdUsuario;
                cmdSave.Parameters.Add("@alta", SqlDbType.Bit).Value = moduloUsuario.PermiteAlta;
                cmdSave.Parameters.Add("@baja", SqlDbType.Bit).Value = moduloUsuario.PermiteBaja;
                cmdSave.Parameters.Add("@modificacion", SqlDbType.Bit).Value = moduloUsuario.PermiteModificacion;
                cmdSave.Parameters.Add("@consulta", SqlDbType.Bit).Value = moduloUsuario.PermiteConsulta;
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos del modulo de usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Insert(ModuloUsuario moduloUsuario)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("insert into modulos_usuarios(id_modulo, id_usuario," +
                    "alta,baja,modificacion,consulta) values(@id_modulo,@id_usuario,@alta,@baja,@modificacion," +
                    "@consulta) select @@identity", // esta línea es para recuperar el id que asignó sql automáticamente
                    sqlConn);

                cmdSave.Parameters.Add("@id_modulo", SqlDbType.Int).Value = moduloUsuario.IdModulo;
                cmdSave.Parameters.Add("@id_usuario", SqlDbType.Int).Value = moduloUsuario.IdUsuario;
                cmdSave.Parameters.Add("@alta", SqlDbType.Bit).Value = moduloUsuario.PermiteAlta;
                cmdSave.Parameters.Add("@baja", SqlDbType.Bit).Value = moduloUsuario.PermiteBaja;
                cmdSave.Parameters.Add("@modificacion", SqlDbType.Bit).Value = moduloUsuario.PermiteModificacion;
                cmdSave.Parameters.Add("@consulta", SqlDbType.Bit).Value = moduloUsuario.PermiteConsulta;
                moduloUsuario.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar());
                // así se obtiene la ID asignada automáticamente
                //cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear modulo de usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public void Save(ModuloUsuario moduloUsuario)
        {
            if (moduloUsuario.State == BusinessEntity.States.New)
            {
                this.Insert(moduloUsuario);
                moduloUsuario.State = BusinessEntity.States.Unmodified;
            }
            else if (moduloUsuario.State == BusinessEntity.States.Deleted)
            {
                this.Delete(moduloUsuario.ID);
            }
            else if (moduloUsuario.State == BusinessEntity.States.Modified)
            {
                this.Update(moduloUsuario);
            }
        }
    }
}
