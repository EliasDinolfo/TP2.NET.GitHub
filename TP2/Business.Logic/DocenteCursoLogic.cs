using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class DocenteCursoLogic : BusinessLogic
    {
        private DocenteCursoAdapter _DocenteCursoAdapter;
        public DocenteCursoLogic()
        {
            DocenteCursoAdapter = new DocenteCursoAdapter();
        }
        public DocenteCursoAdapter DocenteCursoAdapter
        {
            get => _DocenteCursoAdapter;
            set { _DocenteCursoAdapter = value; }
        }
        public List<DocenteCurso> GetAll()
        {
            return _DocenteCursoAdapter.GetAll();
        }
        public DocenteCurso GetOne(int ID)
        {
            return _DocenteCursoAdapter.GetOne(ID);
        }
        public List<DocenteCurso> GetPorPersona(int ID)
        {
            return _DocenteCursoAdapter.GetPorPersona(ID);
        }
        public List<DocenteCurso> GetPorCurso(int ID)
        {
            return _DocenteCursoAdapter.GetPorCurso(ID);
        }
        public void Delete(int ID)
        {
            _DocenteCursoAdapter.Delete(ID);
        }
        public void Save(DocenteCurso DocenteCursoDeEntities)
        {
            _DocenteCursoAdapter.Save(DocenteCursoDeEntities);
        }
    }
}
