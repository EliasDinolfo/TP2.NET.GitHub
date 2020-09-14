using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Data.Database
{
    public class ComisionAdapter:Adapter
    {
        public List<Comision> GetAll()
        {
            //instanciamos el objeto lista a retornar
            List<Comision> comisiones = new List<Comision>();

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
                SqlCommand cmdComisiones = new SqlCommand("select * from comisiones", sqlConn);
                /* 
                 * instanciamos un objeto DataReader que será
                 * el que recuperará los datos de la BD
                */
                SqlDataReader drComisiones = cmdComisiones.ExecuteReader();
                /*
                 * Read() devuelve una fila de las devueltas por el comando sql
                 * carga los datos en drComisiones para poder accederlos,
                 * devuelve verdadero mientras haya podido leer datos
                 * y avanza a la fila siguiente para el próximo read
                */
                while (drComisiones.Read())
                {
                    /*
                     * creamos un objeto Comision de la capa entidades para copiar
                     * los datos de la fila del DataReader al objeto de entidades
                    */
                    Comision com = new Comision();

                    //ahora copiamos los datos de la fila al objeto
                    com.ID = (int)drComisiones["id_comision"];
                    com.Descripcion = (string)drComisiones["desc_comision"];
                    com.IDPlan = (int)drComisiones["id_plan"];
                    com.AnioEspecialidad = (int)drComisiones["anio_especialidad"];
                    com.DataFull = com.Descripcion + " - AñoEsp: " + com.AnioEspecialidad;

                    //agregamos el objeto con datos a la lista que devolveremos
                    comisiones.Add(com);
                }
                //cerramos el data reader y la conexion a la BD
                drComisiones.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar lista de comisiones", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            //devolvemos el objeto
            return comisiones;
        }
        public Business.Entities.Comision GetOne(int ID)
        {
            Comision com = new Comision();

            try
            {
                this.OpenConnection();
                SqlCommand cmdComisiones = new SqlCommand("select * from comisiones where id_comision=@id", sqlConn);
                cmdComisiones.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drComisiones = cmdComisiones.ExecuteReader();
                while (drComisiones.Read())
                {
                    com.ID = (int)drComisiones["id_comision"];
                    com.Descripcion = (string)drComisiones["desc_comision"];
                    com.IDPlan = (int)drComisiones["id_plan"];
                    com.AnioEspecialidad = (int)drComisiones["anio_especialidad"];
                    com.DataFull = com.Descripcion + " - AñoEsp: " + com.AnioEspecialidad;
                }
                drComisiones.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar datos de comisiones", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            return com;
        }

        public void Delete(int ID)
        {
            try
            {
                //abrimos la conexión
                this.OpenConnection();

                //creamos la setencia SQL y asignamos un valor al parámetro
                SqlCommand cmdDelete = new SqlCommand("delete comisiones where id_comision=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                //ejecutamos la setencia SQL
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar comision", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public List<Comision> GetPorPlan(int ID)
        {
            List<Comision> comisiones = new List<Comision>();
            try
            {
                //abrimos la conexión
                this.OpenConnection();

                //creamos la setencia SQL y asignamos un valor al parámetro
                SqlCommand cmdDelete = new SqlCommand("select * from comisiones where id_plan=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                //ejecutamos la setencia SQL
                SqlDataReader drComisiones = cmdDelete.ExecuteReader();
                while (drComisiones.Read())
                {
                    Comision com = new Comision();
                    com.ID = (int)drComisiones["id_comision"];
                    com.IDPlan = (int)drComisiones["id_plan"];
                    com.AnioEspecialidad = (int)drComisiones["anio_especialidad"];
                    com.Descripcion = (string)drComisiones["desc_comision"];
                    com.DataFull = com.Descripcion + " - AñoEsp: " + com.AnioEspecialidad;
                    comisiones.Add(com);
                }
                //cerramos el data reader y la conexion a la BD
                drComisiones.Close();

            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al obtener comision", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return comisiones;
        }

        public void Update(Comision comision)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("UPDATE comisiones SET desc_comision=@descripcion, id_plan=@id_plan," +
                    "anio_especialidad=@anio WHERE id_comision=@id", sqlConn);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = comision.ID;
                cmdSave.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = comision.Descripcion;
                cmdSave.Parameters.Add("@id_plan", SqlDbType.Int).Value = comision.IDPlan;
                cmdSave.Parameters.Add("@anio", SqlDbType.Int).Value = comision.AnioEspecialidad;
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos de la comisión", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Insert(Comision comision)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("insert into comisiones(desc_comision, id_plan, anio_especialidad" +
                    ") values(@descripcion,@id_plan,@anio)" +
                    "select @@identity", // esta línea es para recuperar el id que asignó sql automáticamente
                    sqlConn);

                cmdSave.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = comision.Descripcion;
                cmdSave.Parameters.Add("@id_plan", SqlDbType.Int).Value = comision.IDPlan;
                cmdSave.Parameters.Add("@anio", SqlDbType.Int).Value = comision.AnioEspecialidad;
                comision.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar());
                // así se obtiene la ID asignada automáticamente
                //cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear comisión", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public void Save(Comision comision)
        {
            if (comision.State == BusinessEntity.States.New)
            {
                this.Insert(comision);
                comision.State = BusinessEntity.States.Unmodified;
            }
            else if (comision.State == BusinessEntity.States.Deleted)
            {
                this.Delete(comision.ID);
            }
            else if (comision.State == BusinessEntity.States.Modified)
            {
                this.Update(comision);
            }
        }
    }
}
