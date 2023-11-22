namespace sistema_modular_cafe_majada.Settings
{
    // Clase que almacena los datos cifrados relacionados con la configuración
    class text
    {
        private string key;

        // Propiedades públicas para almacenar los valores cifrados
        public string servidorCifrado { get; }
        public string usuarioCifrado { get; }
        public string contrasenaCifrada { get; }
        public string baseDeDatosCifrada { get; }

        // Constructor de la clase, recibe la clave de cifrado
        public text(string key)
        {
            this.key = key;

            // Valores originales sin cifrar
            string servidor = "localhost"; // Cambia esto a tu servidor MySQL
            string usuario = "root"; // Cambia esto a tu nombre de usuario MySQL
            string contrasena = "admin"; // Cambia esto a tu contraseña MySQL
            string baseDeDatos = "DBMAJADADEMO"; // Cambia esto a tu nombre de base de datos MySQL

            // Cifra los datos individualmente usando EncryptString y la misma clave
            servidorCifrado = EncryptionUtility.EncryptString(servidor, key);
            usuarioCifrado = EncryptionUtility.EncryptString(usuario, key);
            contrasenaCifrada = EncryptionUtility.EncryptString(contrasena, key);
            baseDeDatosCifrada = EncryptionUtility.EncryptString(baseDeDatos, key);
        }
    }
}






