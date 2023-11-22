using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.SecurityData
{
    class PasswordManager
    {
        // Función para cifrar una contraseña
        public static string EncryptPassword(string password)
        {
            // Generar un "salt" aleatorio
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            // Cifrar la contraseña utilizando el "salt" generado
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }

        // Función para comparar una contraseña cifrada con una contraseña ingresada
        public static bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            // Verificar si la contraseña ingresada coincide con la contraseña cifrada
            bool isMatch = BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);

            return isMatch;
        }
    }
}
