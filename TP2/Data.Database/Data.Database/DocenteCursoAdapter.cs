using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Data.Database
{
    public class DocenteCursoAdapter:Adapter
    {
        public List<DocenteCurso> GetAll()
        {
            //instanciamos el objeto lista a retornar
            List<DocenteCurso> docentesCursos = new List<DocenteCurso>();

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
                SqlCommand cmdDocentesCursos = new SqlCommand("select * from docentes_cursos", sqlConn);
                /* 
                 * instanciamos un objeto DataReader que será
                 * el que recuperará los datos de la BD
                */
                SqlDataReader drDocentesCursos = cmdDocentesCursos.ExecuteReader();
                /*
                 * Read() devuelve una fila de las devueltas por el comando sql
                 * carga los datos en drUsuarios para poder accederlos,
                 * devuelve verdadero mientras haya podido leer datos
                 * y avanza a la fila siguiente para el próximo read
                */
                while (drDocentesCursos.Read())
                {
                    /*
                     * creamos un objeto DocenteCurso de la capa entidades para copiar
                     * los datos de la fila del DataReader al objeto de entidades
                    */
                    DocenteCurso dc = new DocenteCurso();

                    //ahora copiamos los datos de la fila al objeto
                    dc.ID = (int)drDocentesCursos["id_dictado"];
                    dc.IDCurso = (int)drDocentesCursos["id_curso"];
                    dc.IDDocente = (int)drDocentesCursos["id_docente"];
                    dc.Cargo = (DocenteCurso.TiposCargos)drDocentesCursos["cargo"];

                    //agregamos el objeto con datos a la lista que devolveremos
                    docentesCursos.Add(dc);
                }
                //cerramos el data reader y la conexion a la BD
                drDocentesCursos.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar lista de docentes cursos", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            //devolvemos el objeto
            return docentesCursos;
        }

        public Business.Entities.DocenteCurso GetOne(int ID)
        {
            DocenteCurso dc = new DocenteCurso();

            try
            {
                this.OpenConnection();
                SqlCommand cmdDocentesCursos = new SqlCommand("select * from docentes_cursos where id_dictado=@id", sqlConn);
                cmdDocentesCursos.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drDocentesCursos = cmdDocentesCursos.ExecuteReader();
                while (drDocentesCursos.Read())
                {
                    dc.ID = (int)drDocentesCursos["id_dictado"];
                    dc.IDCurso = (int)drDocentesCursos["id_curso"];
                    dc.IDDocente = (int)drDocentesCursos["id_docente"];
                    dc.Cargo = (DocenteCurso.TiposCargos)drDocentesCursos["cargo"];
                }
                drDocentesCursos.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar datos de docentes cursos", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            return dc;
        }
        public List<DocenteCurso> GetPorPersona(int ID)
        {
            List<DocenteCurso> docentesCursos = new List<DocenteCurso>();
            try
            {
                //abrimos la conexión
                this.OpenConnection();

                //creamos la setencia SQL y asignamos un valor al parámetro
                SqlCommand cmdDelete = new SqlCommand("select * from docentes_cursos where id_docente=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                //ejecutamos la setencia SQL
                SqlDataReader drDocentesCursos = cmdDelete.ExecuteReader();
                while (drDocentesCursos.Read())
                {
                    DocenteCurso dc = new DocenteCurso();
                    dc.ID = (int)drDocentesCursos["id_dictado"];
                    dc.IDDocente = (int)drDocentesCursos["id_docente"];
                    dc.IDCurso = (int)drDocentesCursos["id_curso"];
                    dc.Cargo = (DocenteCurso.TiposCargos)drDocentesCursos["cargo"];
                    docentesCursos.Add(dc);
                }
                //cerramos el data reader y la conexion a la BD
                drDocentesCursos.Close();

            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al obtener docentes_cursos", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();

            }
            return docentesCursos;
        }
        public List<DocenteCurso> GetPorCurso(int ID)
        {
            List<DocenteCurso> docentesCursos = new List<DocenteCurso>();
            try
            {
                //abrimos la conexión
                this.OpenConnection();

                //creamos la setencia SQL y asignamos un valor al parámetro
                SqlCommand cmdDelete = new SqlCommand("select id_dictado from docentes_cursos where id_curso=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                //ejecutamos la setencia SQL
                SqlDataReader drDocentesCursos = cmdDelete.ExecuteReader();
                while (drDocentesCursos.Read())
                {
                    DocenteCurso dc = new DocenteCurso();
                    dc.ID = (int)drDocentesCursos["id_dictado"];
                    docentesCursos.Add(dc);
                }
                //cerramos el data reader y la conexion a la BD
                drDocentesCursos.Close();

            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al obtener docentes_cursos", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();

            }
            return docentesCursos;
        }
        public void Delete(int ID)
        {
            try
            {
                //abrimos la conexión
                this.OpenConnection();

                //creamos la setencia SQL y asignamos un valor al parámetro
                SqlCommand cmdDelete = new SqlCommand("delete docentes_cursos where id_dictado=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                //ejecutamos la setencia SQL
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar docente curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public void Update(DocenteCurso docenteCurso)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("UPDATE docentes_cursos SET id_docente=@id_docente, id_curso=@id_curso," +
                    "cargo=@cargo WHERE id_dictado=@id", sqlConn);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = docenteCurso.ID;
                cmdSave.Parameters.Add("@id_docente", SqlDbType.Int).Value = docenteCurso.IDDocente;
                cmdSave.Parameters.Add("@id_curso", SqlDbType.Int).Value = docenteCurso.IDCurso;
                cmdSave.Parameters.Add("@cargo", SqlDbType.Bit).Value = docenteCurso.Cargo;
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos del docente curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Insert(DocenteCurso docenteCurso)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("insert into docentes_cursos(id_docente, id_curso," +
                    "cargo) values(@id_docente,@id_curso,@cargo) select @@identity", // esta línea es para recuperar el id que asignó sql automáticamente
                    sqlConn);

                cmdSave.Parameters.Add("@id_docente", SqlDbType.Int).Value = docenteCurso.IDDocente;
                cmdSave.Parameters.Add("@id_curso", SqlDbType.Int).Value = docenteCurso.IDCurso;
                cmdSave.Parameters.Add("@cargo", SqlDbType.Bit).Value = docenteCurso.Cargo;
                docenteCurso.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar());
                // así se obtiene la ID asignada automáticamente
                //cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear docente curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public void Save(DocenteCurso docenteCurso)
        {
            if (docenteCurso.State == BusinessEntity.States.New)
            {
                this.Insert(docenteCurso);
                docenteCurso.State = BusinessEntity.States.Unmodified;
            }
            else if (docenteCurso.State == BusinessEntity.States.Deleted)
            {
                this.Delete(docenteCurso.ID);
            }
            else if (docenteCurso.State == BusinessEntity.States.Modified)
            {
                this.Update(docenteCurso);
            }
        }
    }
}
