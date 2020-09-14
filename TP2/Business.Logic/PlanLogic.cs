using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class PlanLogic : BusinessLogic
    {
        private PlanAdapter _PlanAdapter;
        public PlanLogic()
        {
            PlanAdapter = new PlanAdapter();
        }
        public PlanAdapter PlanAdapter
        {
            get => _PlanAdapter;
            set { _PlanAdapter = value; }
        }
        public List<Plan> GetAll()
        {
            return _PlanAdapter.GetAll();
        }
        public Plan GetOne(int ID)
        {
            return _PlanAdapter.GetOne(ID);
        }
        public void Delete(int ID)
        {
            _PlanAdapter.Delete(ID);
        }
        public List<Plan> GetPorEspecialidad(int ID)
        {
            return _PlanAdapter.GetPorEspecialidad(ID);
        }
        public void Save(Plan PlanDeEntities)
        {
            _PlanAdapter.Save(PlanDeEntities);
        }
    }
}
