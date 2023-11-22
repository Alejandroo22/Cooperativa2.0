using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.Mapping.Acces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.AccesController
{
    class ChargeController
    {
        private ChargeDAO cargoDAO;

        public ChargeController()
        {
            // Se inicia la instancia de la clase CargoDAO
            cargoDAO = new ChargeDAO();
        }

        public bool InsertarCargo(Charge cargo)
        {
            try
            {
                //Se realiza el llamado al metodo DAO para insertar
                return cargoDAO.InsertarCargo(cargo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error durante la inserción del cargo en la base de datos: " + ex.Message);
                return false;
            }
        }

        public List<Charge> ObtenerCargos()
        {
            try
            {
                //se llama al metodo DAO para obtener las Cargoes
                return cargoDAO.ObtenerCargos();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al obtener la lista de Cargos: " + ex.Message);
                return new List<Charge>();
            }
        }

        public Charge ObtenerNombreCargo(string nomCargo)
        {
            Charge cargo = new Charge();
            try
            {
                //llamada al metodo DAO para obtener los datos
                cargo = cargoDAO.ObtenerNombreCargo(nomCargo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error la obtener los datos: " + ex.Message);
            }
            return cargo;
        }
        
        public Charge CountCargo()
        {
            Charge cargo = new Charge();
            try
            {
                //llamada al metodo DAO para obtener los datos
                cargo = cargoDAO.CountCargo();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error la obtener los datos: " + ex.Message);
            }
            return cargo;
        }
        
        public Charge ObtenrUltimoId()
        {
            Charge cargo = new Charge();
            try
            {
                //llamada al metodo DAO para obtener los datos
                cargo = cargoDAO.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error la obtener los datos: " + ex.Message);
            }
            return cargo;
        }

        public void EliminarCargos(int idCargos)
        {
            try
            {
                // se realiza el llamado el metodo DAO para eliminar
                cargoDAO.EliminarCargo(idCargos);
            }
            catch (Exception ex)
            {
                Console.WriteLine("OCurrio un error al eliminar un Cargo: " + ex.Message);
            }
        }

        public bool ActualizarCargos(int id, string Cargo, string descrip)
        {
            try
            {
                //llamada al metodo DAO para actualizar
                return cargoDAO.ActualizarCargos(id, Cargo, descrip);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al actualizar los datos: " + ex.Message);
                return false;
            }
        }

        //
        public bool ExisteId(int id)
        {
            try
            {
                // Llamada al método del DAO para insertar la Bodega
                return cargoDAO.ExisteId(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la verificacion del Cargo Personal en la base de datos: " + ex.Message);
                return false;
            }
        }

    }
}
