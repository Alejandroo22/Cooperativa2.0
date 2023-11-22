using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sistema_modular_cafe_majada.Settings;

namespace sistema_modular_cafe_majada.model.Connection
{
    class ConnectionDB
    {
        private MySqlConnection conexion;
        private MySqlCommand comando;

        public ConnectionDB()
        {
            string key = "CooperativaAdmin"; 

            text configuracion = new text(key);

            try
            {
                // Descifrar los valores cifrados
                string servidorDescifrado = EncryptionUtility.DecryptString(configuracion.servidorCifrado, key);
                string usuarioDescifrado = EncryptionUtility.DecryptString(configuracion.usuarioCifrado, key);
                string contrasenaDescifrada = EncryptionUtility.DecryptString(configuracion.contrasenaCifrada, key);
                string baseDeDatosDescifrada = EncryptionUtility.DecryptString(configuracion.baseDeDatosCifrada, key);

                // Construir la cadena de conexión con los valores descifrados
                string connectionString = $"Server={servidorDescifrado};Database={baseDeDatosDescifrada};User Id={usuarioDescifrado};Password={contrasenaDescifrada};";

                conexion = new MySqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al descifrar: " + ex.Message);
                // Manejar el error adecuadamente
            }
        }

        //private MySqlConnection connection;
        private string connectionString;

        public MySqlConnection Conectar()
        {
            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
            }
            return conexion;
        }

        public void Desconectar()
        {
            if (conexion.State != ConnectionState.Closed)
            {
                conexion.Close();
            }
        }

        // Aquí se añadiran más métodos para realizar consultas, inserciones, actualizaciones, etc.
        public void CrearComando(string consulta)
        {
            comando = new MySqlCommand(consulta, conexion);
            comando.CommandType = CommandType.Text;
        }

        public void AgregarParametro(string nombreParametro, object valorParametro)
        {
            comando.Parameters.AddWithValue(nombreParametro, valorParametro);
        }

        //
        public MySqlParameter ObtenerParametro(string nombreParametro)
        {
            return comando.Parameters[nombreParametro];
        }

        public object EjecutarConsultaEscalar()
        {
            return comando.ExecuteScalar();
        }

        public int EjecutarInstruccion()
        {
            return comando.ExecuteNonQuery();
        }
        public MySqlDataReader EjecutarConsultaReader(string consulta)
        {
            return comando.ExecuteReader();
        }



    }
}
