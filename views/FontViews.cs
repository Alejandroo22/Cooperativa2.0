using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace sistema_modular_cafe_majada.views
{
    class FontViews : Form
    {
        #region Generales
        //estilo para label con texto seminegrita
        public static void LabelStyle(Label[] labels )
        {
            foreach (Label label in labels)
            {
                label.Font = new Font("Segoe UI Semibold", 9f);
            }
        }

        //estilo para label con texto seminegrita para titulos
        public static void LabelStyleTitle(Label[] labelTitle)
        {
            foreach (Label label in labelTitle)
            {
                label.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            }
        }

        //estilo para textbox
        public static void TextBoxStyle(TextBox[] textboxs)
        {
            foreach (TextBox textBox in textboxs)
            {
                textBox.Font = new Font("Segoe UI", 10f,FontStyle.Regular);
            }
        }

        //estilo para combobox
        public static void ComboBoxStyle(ComboBox[] comboBoxes)
        {
            foreach (ComboBox comboBox in comboBoxes)
            {
                comboBox.Font = new Font("Segoe UI", 10f, FontStyle.Regular);
            }
        }

        //estilo para fechas
        public static void DateStyle(DateTimePicker[] dateTimePickers)
        {
            foreach (DateTimePicker date in dateTimePickers)
            {
                date.Font = new Font("Segoe UI", 10f, FontStyle.Regular);
            }
        }

        //estilo de botones Guardar y cancelar
        public static void ButtonStyleGC(Button[] buttons)
        {
            foreach (Button button in buttons)
            {
                button.Font = new Font("Segoe UI Semibold", 9f);
                button.Width = 85;
                button.Height = 30;
                button.TextAlign = ContentAlignment.MiddleRight;
            }
        }
        #endregion

        #region Login
        // Configuraciones para el login
        public static void ButtonStyleLogin(Button[] buttons)
        {
            foreach (Button button in buttons)
            {
                button.Font = new Font("Segoe UI Semibold", 11f);
            }
        }
        #endregion

        #region Administracion
        //estilo de botones en administracion
        public static void ButtonStyle(Button[] buttons)
        {
            foreach (Button button in buttons)
            {
                button.Font = new Font("Segoe UI Semibold", 9f);
            }
        }
        #endregion

        #region MAIN
        public static void ButtonStyleMain(Button[] buttons)
        {
            foreach (Button button in buttons)
            {
                button.Font = new Font("Segoe UI Semibold", 11f);
            }
        }

        public static void LabelStyleEncabezado(Label[] labels)
        {
            foreach (Label label in labels)
            {
                label.Font = new Font("Segoe UI Semibold", 8f);
            }
        }

        public static void LabelStyleInfo(Label[] labels)
        {
            foreach (Label label in labels)
            {
                label.Font = new Font("Segoe UI Semibold", 11f);
            }
        }
        #endregion

        #region PanelPrincipal

        public static void LabelStylePanelEncabezado(Label[] labels)
        {
            foreach (Label label in labels)
            {
                label.Font = new Font("Segoe UI Semibold", 10f);
            }
        }

        public static void LabelStylePanelInfo(Label[] labels)
        {
            foreach (Label label in labels)
            {
                label.Font = new Font("Segoe UI", 10f ,FontStyle.Regular);
            }
        }
        #endregion
    }
}
