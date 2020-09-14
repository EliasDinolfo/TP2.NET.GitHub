using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class EspecialidadLogic : BusinessLogic
    {
        private EspecialidadAdapter _EspecialidadAdapter;
        public EspecialidadLogic()
        {
            EspecialidadAdapter = new EspecialidadAdapter();
        }
        public EspecialidadAdapter EspecialidadAdapter
        {
            get => _EspecialidadAdapter;
            set { _EspecialidadAdapter = value; }
        }
        public List<Especialidad> GetAll()
        {
            return _EspecialidadAdapter.GetAll();
        }
        public Especialidad GetOne(int ID)
        {
            return _EspecialidadAdapter.GetOne(ID);
        }
        public void Delete(int ID)
        {
            _EspecialidadAdapter.Delete(ID);
        }
        public void Save(Especialidad EspecialidadDeEntities)
        {
            _EspecialidadAdapter.Save(EspecialidadDeEntities);
        }
    }
}
