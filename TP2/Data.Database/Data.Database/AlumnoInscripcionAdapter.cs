using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Data.Database
{
    public class AlumnoInscripcionAdapter:Adapter
    {
        public List<AlumnoInscripcion> GetAll()
        {
            //instanciamos el objeto lista a retornar
            List<AlumnoInscripcion> alumnosIncripciones = new List<AlumnoInscripcion>();

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
                SqlCommand cmdAlumnosIncripciones = new SqlCommand("select * from alumnos_inscripciones", sqlConn);
                /* 
                 * instanciamos un objeto DataReader que será
                 * el que recuperará los datos de la BD
                */
                SqlDataReader drAlumnosIncripciones = cmdAlumnosIncripciones.ExecuteReader();
                /*
                 * Read() devuelve una fila de las devueltas por el comando sql
                 * carga los datos en drUsuarios para poder accederlos,
                 * devuelve verdadero mientras haya podido leer datos
                 * y avanza a la fila siguiente para el próximo read
                */
                while (drAlumnosIncripciones.Read())
                {
                    /*
                     * creamos un objeto AlumnoInscripcion de la capa entidades para copiar
                     * los datos de la fila del DataReader al objeto de entidades
                    */
                    AlumnoInscripcion ai = new AlumnoInscripcion();

                    //ahora copiamos los datos de la fila al objeto
                    ai.ID = (int)drAlumnosIncripciones["id_inscripcion"];
                    ai.IDCurso = (int)drAlumnosIncripciones["id_curso"];
                    ai.IDAlumno = (int)drAlumnosIncripciones["id_alumno"];
                    ai.Condicion= (string)drAlumnosIncripciones["condicion"];
                    ai.Nota = (int)drAlumnosIncripciones["nota"];

                    //agregamos el objeto con datos a la lista que devolveremos
                    alumnosIncripciones.Add(ai);
                }
                //cerramos el data reader y la conexion a la BD
                drAlumnosIncripciones.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar lista de alumnos inscripciones", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            //devolvemos el objeto
            return alumnosIncripciones;
        }

        public Business.Entities.AlumnoInscripcion GetOne(int ID)
        {
            AlumnoInscripcion ai = new AlumnoInscripcion();

            try
            {
                this.OpenConnection();
                SqlCommand cmdAlumnosIncripciones = new SqlCommand("select * from alumnos_inscripciones where id_inscripcion=@id", sqlConn);
                cmdAlumnosIncripciones.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drAlumnosIncripciones = cmdAlumnosIncripciones.ExecuteReader();
                while (drAlumnosIncripciones.Read())
                {
                    ai.ID = (int)drAlumnosIncripciones["id_inscripcion"];
                    ai.IDCurso = (int)drAlumnosIncripciones["id_curso"];
                    ai.IDAlumno = (int)drAlumnosIncripciones["id_alumno"];
                    ai.Condicion = (string)drAlumnosIncripciones["condicion"];
                    ai.Nota = (int)drAlumnosIncripciones["nota"];
                }
                drAlumnosIncripciones.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar datos de alumnos inscripciones", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            return ai;
        }
        public List<AlumnoInscripcion> GetPorPersona(int ID)
        {
            List<AlumnoInscripcion> alumnosInscripciones = new List<AlumnoInscripcion>();
            try
            {
                //abrimos la conexión
                this.OpenConnection();

                //creamos la setencia SQL y asignamos un valor al parámetro
                SqlCommand cmdDelete = new SqlCommand("select * from alumnos_inscripciones where id_alumno=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                //ejecutamos la setencia SQL
                SqlDataReader drAlumnosInscripciones = cmdDelete.ExecuteReader();
                while (drAlumnosInscripciones.Read())
                {
                    AlumnoInscripcion ai = new AlumnoInscripcion();
                    ai.ID = (int)drAlumnosInscripciones["id_inscripcion"];
                    ai.IDCurso = (int)drAlumnosInscripciones["id_curso"];
                    ai.Nota = (int)drAlumnosInscripciones["nota"];
                    ai.IDAlumno = (int)drAlumnosInscripciones["id_alumno"];
                    ai.Condicion = (string)drAlumnosInscripciones["condicion"];

                    alumnosInscripciones.Add(ai);
                }
                //cerramos el data reader y la conexion a la BD
                drAlumnosInscripciones.Close();

            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al obtener alumnos_inscripciones", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();

            }
            return alumnosInscripciones;
        }
        public List<AlumnoInscripcion> GetPorCurso(int ID)
        {
            List<AlumnoInscripcion> alumnosInscripciones = new List<AlumnoInscripcion>();
            try
            {
                //abrimos la conexión
                this.OpenConnection();

                //creamos la setencia SQL y asignamos un valor al parámetro
                SqlCommand cmdDelete = new SqlCommand("select id_inscripcion from alumnos_inscripciones where id_curso=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                //ejecutamos la setencia SQL
                SqlDataReader drAlumnosInscripciones = cmdDelete.ExecuteReader();
                while (drAlumnosInscripciones.Read())
                {
                    AlumnoInscripcion ai = new AlumnoInscripcion();
                    ai.ID = (int)drAlumnosInscripciones["id_inscripcion"];
                    alumnosInscripciones.Add(ai);
                }
                //cerramos el data reader y la conexion a la BD
                drAlumnosInscripciones.Close();

            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al obtener alumnos_inscripciones", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();

            }
            return alumnosInscripciones;
        }
        public void Delete(int ID)
        {
            try
            {
                //abrimos la conexión
                this.OpenConnection();

                //creamos la setencia SQL y asignamos un valor al parámetro
                SqlCommand cmdDelete = new SqlCommand("delete alumnos_inscripciones where id_inscripcion=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                //ejecutamos la setencia SQL
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar alumno inscripcion", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public void Update(AlumnoInscripcion alumnoInscripcion)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("UPDATE alumnos_inscripciones SET id_alumno=@id_alumno, id_curso=@id_curso," +
                    "condicion=@condicion, nota=@nota WHERE id_inscripcion=@id", sqlConn);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = alumnoInscripcion.ID;
                cmdSave.Parameters.Add("@id_alumno", SqlDbType.Int).Value = alumnoInscripcion.IDAlumno;
                cmdSave.Parameters.Add("@id_curso", SqlDbType.Int).Value = alumnoInscripcion.IDCurso;
                cmdSave.Parameters.Add("@condicion", SqlDbType.VarChar,50).Value = alumnoInscripcion.Condicion;
                cmdSave.Parameters.Add("@nota", SqlDbType.Int).Value = alumnoInscripcion.Nota;
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos del alumno inscripcion", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public void UpdateCupo(AlumnoInscripcion alumnoInscripcion, int nuevoCupo)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("UPDATE cursos SET cupo=@cupo WHERE id_curso=@id_curso", sqlConn);

                cmdSave.Parameters.Add("@id_curso", SqlDbType.Int).Value = alumnoInscripcion.IDCurso;
                cmdSave.Parameters.Add("@cupo", SqlDbType.Int).Value = nuevoCupo;
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al actualizar cupo del curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Insert(AlumnoInscripcion alumnoInscripcion)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("insert into alumnos_inscripciones(id_alumno, id_curso," +
                    "condicion,nota) values(@id_alumno,@id_curso,@condicion,@nota) select @@identity", // esta línea es para recuperar el id que asignó sql automáticamente
                    sqlConn);

                cmdSave.Parameters.Add("@id_alumno", SqlDbType.Int).Value = alumnoInscripcion.IDAlumno;
                cmdSave.Parameters.Add("@id_curso", SqlDbType.Int).Value = alumnoInscripcion.IDCurso;
                cmdSave.Parameters.Add("@condicion", SqlDbType.VarChar,50).Value = alumnoInscripcion.Condicion;
                cmdSave.Parameters.Add("@nota", SqlDbType.Int).Value = alumnoInscripcion.Nota;
                alumnoInscripcion.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar());
                // así se obtiene la ID asignada automáticamente
                //cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear alumno inscripcion", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public void Save(AlumnoInscripcion alumnoInscripcion)
        {
            if (alumnoInscripcion.State == BusinessEntity.States.New)
            {
                this.Insert(alumnoInscripcion);
                alumnoInscripcion.State = BusinessEntity.States.Unmodified;
            }
            else if (alumnoInscripcion.State == BusinessEntity.States.Deleted)
            {
                this.Delete(alumnoInscripcion.ID);
            }
            else if (alumnoInscripcion.State == BusinessEntity.States.Modified)
            {
                this.Update(alumnoInscripcion);
            }
        }
    }
}
