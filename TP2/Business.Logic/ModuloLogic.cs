using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class ModuloLogic : BusinessLogic
    {
        private ModuloAdapter _ModuloAdapter;
        public ModuloLogic()
        {
            ModuloAdapter = new ModuloAdapter();
        }
        public ModuloAdapter ModuloAdapter
        {
            get => _ModuloAdapter;
            set { _ModuloAdapter = value; }
        }
        public List<Modulo> GetAll()
        {
            return _ModuloAdapter.GetAll();
        }
        public Modulo GetOne(int ID)
        {
            return _ModuloAdapter.GetOne(ID);
        }
        public void Delete(int ID)
        {
            _ModuloAdapter.Delete(ID);
        }
        public void Save(Modulo moduloDeEntities)
        {
            _ModuloAdapter.Save(moduloDeEntities);
        }
    }
}

