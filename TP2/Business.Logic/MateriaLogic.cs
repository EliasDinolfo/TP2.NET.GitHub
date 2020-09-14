using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class MateriaLogic : BusinessLogic
    {
        private MateriaAdapter _MateriaAdapter;
        public MateriaLogic()
        {
            MateriaAdapter = new MateriaAdapter();
        }
        public MateriaAdapter MateriaAdapter
        {
            get => _MateriaAdapter;
            set { _MateriaAdapter = value; }
        }
        public List<Materia> GetAll()
        {
            return _MateriaAdapter.GetAll();
        }
        public Materia GetOne(int ID)
        {
            return _MateriaAdapter.GetOne(ID);
        }
        public List<Materia> GetPorPlan(int ID)
        {
            return _MateriaAdapter.GetPorPlan(ID);
        }
        public void Delete(int ID)
        {
            _MateriaAdapter.Delete(ID);
        }
        public void Save(Materia MateriaDeEntities)
        {
            _MateriaAdapter.Save(MateriaDeEntities);
        }
    }
}
