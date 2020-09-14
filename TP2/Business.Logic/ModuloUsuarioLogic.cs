using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class ModuloUsuarioLogic : BusinessLogic
    {
        private ModuloUsuarioAdapter _ModuloUsuarioAdapter;
        public ModuloUsuarioLogic()
        {
            ModuloUsuarioAdapter = new ModuloUsuarioAdapter();
        }
        public ModuloUsuarioAdapter ModuloUsuarioAdapter
        {
            get => _ModuloUsuarioAdapter;
            set { _ModuloUsuarioAdapter = value; }
        }
        public List<ModuloUsuario> GetAll()
        {
            return _ModuloUsuarioAdapter.GetAll();
        }
        public ModuloUsuario GetOne(int ID)
        {
            return _ModuloUsuarioAdapter.GetOne(ID);
        }
        public List<ModuloUsuario> GetPorUsuario(int ID)
        {
            return _ModuloUsuarioAdapter.GetPorUsuario(ID);
        }
        public List<ModuloUsuario> GetPorModulo(int ID)
        {
            return _ModuloUsuarioAdapter.GetPorModulo(ID);
        }
        public void Delete(int ID)
        {
            _ModuloUsuarioAdapter.Delete(ID);
        }
        public void Save(ModuloUsuario ModuloUsuarioDeEntities)
        {
            _ModuloUsuarioAdapter.Save(ModuloUsuarioDeEntities);
        }
    }
}
