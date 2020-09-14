using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class PersonaLogic : BusinessLogic
    {
        private PersonaAdapter _PersonaAdapter;
        public PersonaLogic()
        {
            PersonaAdapter = new PersonaAdapter();
        }
        public PersonaAdapter PersonaAdapter
        {
            get => _PersonaAdapter;
            set { _PersonaAdapter = value; }
        }
        public List<Persona> GetAll()
        {
            return _PersonaAdapter.GetAll();
        }
        public List<Persona> GetAllDocentes()
        {
            return _PersonaAdapter.GetAllDocentes();
        }
        public List<Persona> GetAllAlumnos()
        {
            return _PersonaAdapter.GetAllAlumnos();
        }
        public Persona GetOne(int ID)
        {
            return _PersonaAdapter.GetOne(ID);
        }
        public List<Persona> GetPorPlan(int ID)
        {
            return _PersonaAdapter.GetPorPlan(ID);
        }
        public void Delete(int ID)
        {
            _PersonaAdapter.Delete(ID);
        }
        public void Save(Persona PersonaDeEntities)
        {
            _PersonaAdapter.Save(PersonaDeEntities);
        }
    }
}
