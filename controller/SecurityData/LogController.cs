using sistema_modular_cafe_majada.model.DAO.SecurityDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.SecurityData
{
    class LogController
    {
        private LogDAO logDAO;

        public LogController()
        {
            logDAO = new LogDAO();
        }

        public void RegistrarLog(int idUsuario, string descripcion, string modulo, string tipoAccion, string datosAdicionales)
        {
            try
            {
                // Llamada al método del DAO para registrar el log
                logDAO.RegistrarLog(idUsuario, descripcion, modulo, tipoAccion, datosAdicionales);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al registrar el log: " + ex.Message);
            }
        }

    }
}
