using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class CursoLogic : BusinessLogic
    {
        private CursoAdapter _CursoAdapter;
        public CursoLogic()
        {
            CursoAdapter = new CursoAdapter();
        }
        public CursoAdapter CursoAdapter
        {
            get => _CursoAdapter;
            set { _CursoAdapter = value; }
        }
        public List<Curso> GetAll()
        {
            return _CursoAdapter.GetAll();
        }
        public Curso GetOne(int ID)
        {
            return _CursoAdapter.GetOne(ID);
        }
        public List<Curso> GetPorComision(int ID)
        {
            return _CursoAdapter.GetPorComision(ID);
        }
        public List<Curso> GetPorMateria(int ID)
        {
            return _CursoAdapter.GetPorMateria(ID);
        }
        public List<Curso> GetPorMaterias(List<Materia> materias)
        {
            return _CursoAdapter.GetPorMaterias(materias);
        }
        public List<Curso> GetPorComisiones(List<Comision> comisiones)
        {
            return _CursoAdapter.GetPorComisiones(comisiones);
        }
        public void Delete(int ID)
        {
            _CursoAdapter.Delete(ID);
        }
        public void Save(Curso CursoDeEntities)
        {
            _CursoAdapter.Save(CursoDeEntities);
        }
    }
}
