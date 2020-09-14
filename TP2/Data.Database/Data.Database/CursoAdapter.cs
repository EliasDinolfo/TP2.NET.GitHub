using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Threading;

namespace Data.Database
{
    public class CursoAdapter:Adapter
    {
        public List<Curso> GetAll()
        {
            //instanciamos el objeto lista a retornar
            List<Curso> cursos = new List<Curso>();

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
                SqlCommand cmdCursos = new SqlCommand("select * from cursos", sqlConn);
                /* 
                 * instanciamos un objeto DataReader que será
                 * el que recuperará los datos de la BD
                */
                SqlDataReader drCursos = cmdCursos.ExecuteReader();
                /*
                 * Read() devuelve una fila de las devueltas por el comando sql
                 * carga los datos en drUsuarios para poder accederlos,
                 * devuelve verdadero mientras haya podido leer datos
                 * y avanza a la fila siguiente para el próximo read
                */
                while (drCursos.Read())
                {
                    /*
                     * creamos un objeto Curso de la capa entidades para copiar
                     * los datos de la fila del DataReader al objeto de entidades
                    */
                    Curso cur = new Curso();

                    //ahora copiamos los datos de la fila al objeto
                    cur.ID = (int)drCursos["id_curso"];
                    //cur.Descripcion = (string)drPlanes["desc_curso"]; no tiene columna descripcion
                    cur.IDComision = (int)drCursos["id_comision"];
                    cur.IDMateria = (int)drCursos["id_materia"];
                    cur.AnioCalendario = (int)drCursos["anio_calendario"];
                    cur.Cupo = (int)drCursos["cupo"];
                    cur.DatosCompletos = cur.AnioCalendario + " - ";
                    //agregamos el objeto con datos a la lista que devolveremos
                    cursos.Add(cur);
                }
                //cerramos el data reader y la conexion a la BD
                drCursos.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar lista de cursos", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            // seteamos la descripción de la materia al curso
            foreach (Curso curso in cursos)
            {
                try
                {
                    this.OpenConnection();
                    SqlCommand cmdMaterias = new SqlCommand("select desc_materia from materias where id_materia=@id_materia", sqlConn);
                    cmdMaterias.Parameters.Add("@id_materia", SqlDbType.Int).Value = curso.IDMateria;
                    SqlDataReader drMaterias = cmdMaterias.ExecuteReader();
                    while (drMaterias.Read())
                    {
                        curso.DatosCompletos += (string)drMaterias["desc_materia"];
                    }
                    drMaterias.Close();
                }
                catch (Exception Ex)
                {
                    Exception ExcepcionManejada =
                   new Exception("Error al recuperar materia del curso", Ex);
                    throw ExcepcionManejada;
                }
                finally
                {
                    this.CloseConnection();
                }
            }
            //seteamos la descripción de la comisión al curso
            foreach (Curso curso in cursos)
            {
                try
                {
                    this.OpenConnection();
                    SqlCommand cmdComisiones = new SqlCommand("select desc_comision from comisiones where id_comision=@id_comision", sqlConn);
                    cmdComisiones.Parameters.Add("@id_comision", SqlDbType.Int).Value = curso.IDComision;
                    SqlDataReader drComisiones = cmdComisiones.ExecuteReader();
                    while (drComisiones.Read())
                    {
                        curso.DatosCompletos += " "+(string)drComisiones["desc_comision"];
                    }
                    drComisiones.Close();
                }
                catch (Exception Ex)
                {
                    Exception ExcepcionManejada =
                   new Exception("Error al recuperar comisión del curso", Ex);
                    throw ExcepcionManejada;
                }
                finally
                {
                    this.CloseConnection();
                }
            }

            //devolvemos la lista
            return cursos;
        }

        public Business.Entities.Curso GetOne(int ID)
        {
            Curso cur = new Curso();

            try
            {
                this.OpenConnection();
                SqlCommand cmdCursos = new SqlCommand("select * from cursos where id_curso=@id", sqlConn);
                cmdCursos.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drCursos = cmdCursos.ExecuteReader();
                while (drCursos.Read())
                {
                    cur.ID = (int)drCursos["id_curso"];
                    cur.AnioCalendario = (int)drCursos["anio_calendario"];
                    cur.Cupo = (int)drCursos["cupo"];
                    //cur.Descripcion = (string)drPlanes["desc_plan"];
                    cur.IDComision = (int)drCursos["id_comision"];
                    cur.IDMateria = (int)drCursos["id_materia"];
                }
                drCursos.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar datos de cursos", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            return cur;
        }
        public List<Curso> GetPorComision(int ID)
        {
            List<Curso> cursos = new List<Curso>();
            try
            {
                //abrimos la conexión
                this.OpenConnection();

                //creamos la setencia SQL y asignamos un valor al parámetro
                SqlCommand cmdDelete = new SqlCommand("select id_curso from cursos where id_comision=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                //ejecutamos la setencia SQL
                SqlDataReader drCursos = cmdDelete.ExecuteReader();
                while (drCursos.Read())
                {
                    Curso cur = new Curso();
                    cur.ID = (int)drCursos["id_curso"];
                    cursos.Add(cur);
                }
                //cerramos el data reader y la conexion a la BD
                drCursos.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al obtener curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return cursos;
        }
        public List<Curso> GetPorMateria(int ID)
        {
            List<Curso> cursos = new List<Curso>();
            try
            {
                //abrimos la conexión
                this.OpenConnection();

                //creamos la setencia SQL y asignamos un valor al parámetro
                SqlCommand cmdDelete = new SqlCommand("select id_curso from cursos where id_materia=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                //ejecutamos la setencia SQL
                SqlDataReader drCursos = cmdDelete.ExecuteReader();
                while (drCursos.Read())
                {
                    Curso cur = new Curso();
                    cur.ID = (int)drCursos["id_curso"];
                    cursos.Add(cur);
                }
                //cerramos el data reader y la conexion a la BD
                drCursos.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al obtener curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return cursos;
        }
        public List<Curso> GetPorMaterias(List<Materia> materias)
        {
            List<Curso> cursos = new List<Curso>();
            foreach (Materia materia in materias)
            {
                try
                {
                    //abrimos la conexión
                    this.OpenConnection();

                    //creamos la setencia SQL y asignamos un valor al parámetro
                    SqlCommand cmdDelete = new SqlCommand("select * from cursos where id_materia=@id_materia", sqlConn);
                    cmdDelete.Parameters.Add("@id_materia", SqlDbType.Int).Value = materia.ID;

                    //ejecutamos la setencia SQL
                    SqlDataReader drCursos = cmdDelete.ExecuteReader();
                    while (drCursos.Read())
                    {
                        Curso cur = new Curso();
                        cur.ID = (int)drCursos["id_curso"];
                        cur.IDComision = (int)drCursos["id_comision"];
                        cur.IDMateria = (int)drCursos["id_materia"];
                        cur.Cupo = (int)drCursos["cupo"];
                        cur.AnioCalendario = (int)drCursos["anio_calendario"];
                        cur.DatosCompletos = cur.AnioCalendario + " - ";
                        cursos.Add(cur);
                    }
                    //cerramos el data reader y la conexion a la BD
                    drCursos.Close();
                }
                catch (Exception Ex)
                {
                    Exception ExcepcionManejada = new Exception("Error al obtener curso", Ex);
                    throw ExcepcionManejada;
                }
                finally
                {
                    this.CloseConnection();
                }
            }
            // seteamos la descripción de la materia al curso
            foreach (Curso curso in cursos)
            {
                try
                {
                    this.OpenConnection();
                    SqlCommand cmdMaterias = new SqlCommand("select desc_materia from materias where id_materia=@id_materia", sqlConn);
                    cmdMaterias.Parameters.Add("@id_materia", SqlDbType.Int).Value = curso.IDMateria;
                    SqlDataReader drMaterias = cmdMaterias.ExecuteReader();
                    while (drMaterias.Read())
                    {
                        curso.DatosCompletos += (string)drMaterias["desc_materia"];
                    }
                    drMaterias.Close();
                }
                catch (Exception Ex)
                {
                    Exception ExcepcionManejada =
                   new Exception("Error al recuperar materia del curso", Ex);
                    throw ExcepcionManejada;
                }
                finally
                {
                    this.CloseConnection();
                }
            }
            //seteamos la descripción de la comisión al curso
            foreach (Curso curso in cursos)
            {
                try
                {
                    this.OpenConnection();
                    SqlCommand cmdComisiones = new SqlCommand("select desc_comision from comisiones where id_comision=@id_comision", sqlConn);
                    cmdComisiones.Parameters.Add("@id_comision", SqlDbType.Int).Value = curso.IDComision;
                    SqlDataReader drComisiones = cmdComisiones.ExecuteReader();
                    while (drComisiones.Read())
                    {
                        curso.DatosCompletos += " " + (string)drComisiones["desc_comision"];
                    }
                    drComisiones.Close();
                }
                catch (Exception Ex)
                {
                    Exception ExcepcionManejada =
                   new Exception("Error al recuperar comisión del curso", Ex);
                    throw ExcepcionManejada;
                }
                finally
                {
                    this.CloseConnection();
                }
            }
            return cursos;
        }
        public List<Curso> GetPorComisiones(List<Comision> comisiones)
        {
            List<Curso> cursos = new List<Curso>();
            foreach (Comision comision in comisiones)
            {
                try
                {
                    //abrimos la conexión
                    this.OpenConnection();

                    //creamos la setencia SQL y asignamos un valor al parámetro
                    SqlCommand cmdDelete = new SqlCommand("select * from cursos where id_comision=@id_comision", sqlConn);
                    cmdDelete.Parameters.Add("@id_comision", SqlDbType.Int).Value = comision.ID;

                    //ejecutamos la setencia SQL
                    SqlDataReader drCursos = cmdDelete.ExecuteReader();
                    while (drCursos.Read())
                    {
                        Curso cur = new Curso();
                        cur.ID = (int)drCursos["id_curso"];
                        cur.IDComision = (int)drCursos["id_comision"];
                        cur.IDMateria = (int)drCursos["id_materia"];
                        cur.Cupo = (int)drCursos["cupo"];
                        cur.AnioCalendario = (int)drCursos["anio_calendario"];
                        cursos.Add(cur);
                    }
                    //cerramos el data reader y la conexion a la BD
                    drCursos.Close();
                }
                catch (Exception Ex)
                {
                    Exception ExcepcionManejada = new Exception("Error al obtener curso", Ex);
                    throw ExcepcionManejada;
                }
                finally
                {
                    this.CloseConnection();
                }
            }
            return cursos;
        }
        public void Delete(int ID)
        {
            try
            {
                //abrimos la conexión
                this.OpenConnection();

                //creamos la setencia SQL y asignamos un valor al parámetro
                SqlCommand cmdDelete = new SqlCommand("delete cursos where id_curso=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                //ejecutamos la setencia SQL
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public void Update(Curso curso)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("UPDATE cursos SET id_comision=@id_comision, id_materia=@id_materia, " +
                    "anio_calendario=@anio, cupo=@cupo WHERE id_curso=@id", sqlConn);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = curso.ID;
                cmdSave.Parameters.Add("@anio", SqlDbType.Int).Value = curso.AnioCalendario;
                cmdSave.Parameters.Add("@cupo", SqlDbType.Int).Value = curso.Cupo;
                //cmdSave.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = curso.Descripcion;
                cmdSave.Parameters.Add("@id_comision", SqlDbType.Int).Value = curso.IDComision;
                cmdSave.Parameters.Add("@id_materia", SqlDbType.Int).Value = curso.IDMateria;
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos del curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Insert(Curso curso)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("insert into cursos(id_comision, id_materia, anio_calendario," +
                    "cupo) values(@id_comision,@id_materia,@anio,@cupo)" +
                    "select @@identity", // esta línea es para recuperar el id que asignó sql automáticamente
                    sqlConn);

                //cmdSave.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = plan.Descripcion;
                cmdSave.Parameters.Add("@id_comision", SqlDbType.Int).Value = curso.IDComision;
                cmdSave.Parameters.Add("@id_materia", SqlDbType.Int).Value = curso.IDMateria;
                cmdSave.Parameters.Add("@anio", SqlDbType.Int).Value = curso.AnioCalendario;
                cmdSave.Parameters.Add("@cupo", SqlDbType.Int).Value = curso.Cupo;
                curso.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar());
                // así se obtiene la ID asignada automáticamente
                //cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public void Save(Curso curso)
        {
            if (curso.State == BusinessEntity.States.New)
            {
                this.Insert(curso);
                curso.State = BusinessEntity.States.Unmodified;
            }
            else if (curso.State == BusinessEntity.States.Deleted)
            {
                this.Delete(curso.ID);
            }
            else if (curso.State == BusinessEntity.States.Modified)
            {
                this.Update(curso);
            }
        }
    }
}
