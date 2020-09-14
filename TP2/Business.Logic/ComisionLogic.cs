using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class ComisionLogic : BusinessLogic
    {
        private ComisionAdapter _ComisionAdapter;
        public ComisionLogic()
        {
            ComisionAdapter = new ComisionAdapter();
        }
        public ComisionAdapter ComisionAdapter
        {
            get => _ComisionAdapter;
            set { _ComisionAdapter = value; }
        }
        public List<Comision> GetAll()
        {
            return _ComisionAdapter.GetAll();
        }
        public Comision GetOne(int ID)
        {
            return _ComisionAdapter.GetOne(ID);
        }
        public void Delete(int ID)
        {
            _ComisionAdapter.Delete(ID);
        }
        public List<Comision> GetPorPlan(int ID)
        {
           return _ComisionAdapter.GetPorPlan(ID);
        }
        public void Save(Comision ComisionDeEntities)
        {
            _ComisionAdapter.Save(ComisionDeEntities);
        }
    }
}
