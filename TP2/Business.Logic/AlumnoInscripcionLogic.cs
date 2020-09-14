using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class AlumnoInscripcionLogic : BusinessLogic
    {
        private AlumnoInscripcionAdapter _AlumnoInscripcionAdapter;
        public AlumnoInscripcionLogic()
        {
            AlumnoInscripcionAdapter = new AlumnoInscripcionAdapter();
        }
        public AlumnoInscripcionAdapter AlumnoInscripcionAdapter
        {
            get => _AlumnoInscripcionAdapter;
            set { _AlumnoInscripcionAdapter = value; }
        }
        public List<AlumnoInscripcion> GetAll()
        {
            return _AlumnoInscripcionAdapter.GetAll();
        }
        public AlumnoInscripcion GetOne(int ID)
        {
            return _AlumnoInscripcionAdapter.GetOne(ID);
        }
        public List<AlumnoInscripcion> GetPorPersona(int ID)
        {
            return _AlumnoInscripcionAdapter.GetPorPersona(ID);
        }
        public List<AlumnoInscripcion> GetPorCurso(int ID)
        {
            return _AlumnoInscripcionAdapter.GetPorCurso(ID);
        }
        public void Delete(int ID)
        {
            _AlumnoInscripcionAdapter.Delete(ID);
        }
        public void Save(AlumnoInscripcion AlumnoInscripcionDeEntities, int nuevoCupo)
        {
            _AlumnoInscripcionAdapter.Save(AlumnoInscripcionDeEntities);
            _AlumnoInscripcionAdapter.UpdateCupo(AlumnoInscripcionDeEntities, nuevoCupo);
        }
        public void Save(AlumnoInscripcion AlumnoInscripcionDeEntities, AlumnoInscripcion alumnoInscripcionAnterior, int nuevoCupo, int cupoAnterior)
        {
            _AlumnoInscripcionAdapter.Save(AlumnoInscripcionDeEntities);
            _AlumnoInscripcionAdapter.UpdateCupo(AlumnoInscripcionDeEntities, nuevoCupo);
            _AlumnoInscripcionAdapter.UpdateCupo(alumnoInscripcionAnterior, cupoAnterior);
        }
    }
}
