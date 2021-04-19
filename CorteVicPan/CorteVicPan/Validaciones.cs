using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing;
using System.Drawing.Printing;

namespace CorteVicPan
{
    class Validaciones
    {
        public Boolean Vacio(TextBox Campo)
        {
            Boolean verificacion;
            if (Campo.Text == "")
            {
                verificacion = true;
            }
            else
            {
                verificacion = false;
            }
            return verificacion;
        }

        public void Letras(KeyPressEventArgs tecla)
        {
            if (char.IsLetter(tecla.KeyChar))
                tecla.Handled = false;
            else if (char.IsSeparator(tecla.KeyChar))
                tecla.Handled = false;
            else if (char.IsControl(tecla.KeyChar))
                tecla.Handled = false;
            else
                tecla.Handled = true;
        }

        public void Numeros(KeyPressEventArgs tecla)
        {
            if (char.IsNumber(tecla.KeyChar))
                tecla.Handled = false;
            else if (char.IsSeparator(tecla.KeyChar))
                tecla.Handled = false;
            else if (char.IsControl(tecla.KeyChar))
                tecla.Handled = false;
            else
                tecla.Handled = true;
        }

        public void cantidad(KeyPressEventArgs tecla)
        {

            if (char.IsNumber(tecla.KeyChar))
                tecla.Handled = false;
            else if (char.IsSeparator(tecla.KeyChar))
                tecla.Handled = false;
            else if (char.IsControl(tecla.KeyChar))
                tecla.Handled = false;
            else if (char.IsPunctuation(tecla.KeyChar))
                tecla.Handled = false;
            else
                tecla.Handled = true;
        }

        public int valbombobox(ComboBox combo)
        {
            int valor=combo.SelectedIndex;
            return valor;
        }
    }
}
