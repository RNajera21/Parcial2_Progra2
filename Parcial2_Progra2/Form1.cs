using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Parcial2_Progra2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


    
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechadenacimiento = dateTimePicker1.Value;
            DateTime fechaactual = DateTime.Today;
            int edad = fechaactual.Year - fechadenacimiento.Year;

            if (edad >= 18 || edad <= 51) 
                label1.Text = "Continúa";      

            else
                label1.Text = "La encuesta es para mayores de 18 años, y menores de 50. Gracias";
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int numero_encuesta = random.Next();
            label1.Text = ("Su numero de encuesta es: " + random);
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            bool car = radioButton1.Checked;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string name = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string lastname = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string email = textBox3.Text;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=LAPTOP\\SQLEXPRESS;Initial Catalog=Encuesta_Parcial2;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Abrir la conexión
                    connection.Open();
                    Console.WriteLine("Conexión abierta exitosamente.");
                    string query = "INSERT INTO Encuesta_Parcial2 (Nombre, Apellido, email, num_encuesta) VALUES (@Nombre, @Apellido, @Email, @num_encuesta)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Parámetros para evitar inyección de SQL
                        command.Parameters.AddWithValue("@Nombre", textBox1.Text);
                        command.Parameters.AddWithValue("@Apellido", textBox2.Text);
                        command.Parameters.AddWithValue("@Car", radioButton1.Checked); // Si radioButton1 está activado, tiene carro
                        command.Parameters.AddWithValue("@NumEncuesta", label1.Text);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            bool car = !radioButton2.Checked;
        }

    }
}
