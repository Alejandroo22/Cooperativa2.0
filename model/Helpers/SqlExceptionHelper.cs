using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Helpers
{
    public static class SqlExceptionHelper
    {
        public static int NumberException { get; set; }
        public static string MessageExceptionSql { get; set; }
        public static string HandleSqlException(Exception ex)
        {
            if (ex is MySqlException sqlException)
            {
                switch (sqlException.Number)
                {
                    case 2627: // Violación de clave única o restricción UNIQUE
                        return "Ya existe un registro con este valor único en la base de datos.";

                    case 547: // Violación de restricción de clave externa
                        return "No se puede eliminar o actualizar este registro debido a restricciones de clave externa.";
                        
                    case 1451: // Violación de tabla que tiene una restricción de clave externa
                        return "No se puede eliminar este registro debido a restricciones de clave externa, es decir el registro a eliminar tiene sub registros dependientes.";

                    // Puedes seguir agregando más casos según las necesidades.

                    default:
                        return "Se ha producido un error en la base de datos.";
                }
            }

            // Si no es una excepción SQL conocida, proporcionar un mensaje genérico.
            return "Se ha producido un error en la base de datos.";
        }
    }
}
