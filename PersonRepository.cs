using PPadillaT5.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPadillaT5
{
    internal class PersonRepository
    {
        string dbPAth;
        public string statusMessage { get; set; }
        private SQLiteConnection conn;

        public void Init()
        {
            if (conn is not null)
                return;
            conn = new(dbPAth);
            conn.CreateTable<Persona>();


        }


        public PersonRepository(string dbPath1)
        {
            dbPAth = dbPath1;
        }

        public void AddNewPerson(string nombre)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(nombre))
                {
                    throw new Exception("Nombre Requerido");
                }
                Persona persona = new Persona() { Name = nombre };
                result = conn.Insert(persona);
                statusMessage = string.Format("Filas Agregadas", result, nombre);
            }
            catch (Exception ex)
            {

                statusMessage = string.Format("Error al insertar", nombre, ex.Message);
            }



        }
        public List<Persona> GetAllPeople()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {

                statusMessage = string.Format("Error al mostrar los datos", ex.Message);
            }
            return new List<Persona>();
        }


        public void DeletePerson(int personId)
        {
            try
            {
                Init();
                if (personId <= 0)
                {
                    throw new ArgumentException("ID de persona no válido");
                }

                int result = conn.Delete<Persona>(personId);
                statusMessage = result > 0 ? "Persona eliminada correctamente" : "No se encontró ninguna persona con ese ID";
            }
            catch (Exception ex)
            {
                statusMessage = string.Format("Error al eliminar persona: {0}", ex.Message);
            }
        }

        public void UpdatePerson(int personId, string newNombre)
        {
            try
            {
                Init();
                if (personId <= 0)
                {
                    throw new ArgumentException("ID de persona no válido");
                }

                Persona existingPerson = conn.Find<Persona>(personId);
                if (existingPerson != null)
                {
                    existingPerson.Name = newNombre;
                    int result = conn.Update(existingPerson);
                    statusMessage = result > 0 ? "Persona actualizada correctamente" : "No se encontró ninguna persona con ese ID";
                }
                else
                {
                    statusMessage = "No se encontró ninguna persona con ese ID";
                }
            }
            catch (Exception ex)
            {
                statusMessage = string.Format("Error al actualizar persona: {0}", ex.Message);
            }
        }

        internal void AddNewPerson(object text)
        {
            throw new NotImplementedException();
        }
    }


}
