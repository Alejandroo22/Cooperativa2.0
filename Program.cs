using sistema_modular_cafe_majada.model.Connection;
using sistema_modular_cafe_majada.views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace sistema_modular_cafe_majada
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*
            //el siguiente bloque es una Prueba de conexion
            ConnectionDB conexionBD = new ConnectionDB();
            conexionBD.Conectar();
            // Aquí puedes realizar las operaciones que necesites en la base de datos
            conexionBD.Desconectar();*/

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new form_splash());
        }
    }
}
