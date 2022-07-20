using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace conexion20
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static String conexion = "server=127.0.0.1;database=pruebac#;UID=root;password=;port=3306;";

        MySqlConnection DataBaseConexion = new MySqlConnection(conexion);

        public DataTable llenar()
        {
            DataBaseConexion.Open();

            DataTable table = new DataTable();

            String llenar = "select * from usuario";

            MySqlCommand comando = new MySqlCommand(llenar, DataBaseConexion);

            MySqlDataAdapter datos = new MySqlDataAdapter(comando);

            datos.Fill(table);

            DataBaseConexion.Close();

            return table;
        }

        private void mostrar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = llenar();
        }

        private void agregar_Click(object sender, EventArgs e)
        {
            DataBaseConexion.Open();

            String agregar = "insert into usuario(ID, Nombre , Apelido, Edad) values(@id, @nombre, @apelido, @edad)";

            MySqlCommand comandos = new MySqlCommand(agregar, DataBaseConexion);

            comandos.Parameters.AddWithValue("@id", textBoxID.Text);
            comandos.Parameters.AddWithValue("@nombre", textBoxNombre.Text);
            comandos.Parameters.AddWithValue("@apelido", textBoxApellido.Text);
            comandos.Parameters.AddWithValue("@edad", textBoxEdad.Text);

            comandos.ExecuteNonQuery();

            DataBaseConexion.Close();

            MessageBox.Show("DATOS AGREGADOS CORRECTAMENTE");

            dataGridView1.DataSource = llenar();
        }

        private void modificar_Click(object sender, EventArgs e)
        {
            DataBaseConexion.Open();

            String modificar = "update usuario set ID=@id, Nombre=@nombre, Apelido=@apelido, Edad=@edad where ID=@id";

            MySqlCommand comandos = new MySqlCommand(modificar, DataBaseConexion);

            comandos.Parameters.AddWithValue("@id", textBoxID.Text);
            comandos.Parameters.AddWithValue("@nombre", textBoxNombre.Text);
            comandos.Parameters.AddWithValue("@apelido", textBoxApellido.Text);
            comandos.Parameters.AddWithValue("@edad", textBoxEdad.Text);

            comandos.ExecuteNonQuery();

            DataBaseConexion.Close();

            MessageBox.Show("DATOS MODIFICADOS CORRECTAMENTE");

            dataGridView1.DataSource = llenar();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBoxNombre.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBoxApellido.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBoxEdad.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }
            catch
            {

            }
        }

        private void eliminar_Click(object sender, EventArgs e)
        {
            DataBaseConexion.Open();

            String eliminar = "delete from usuario where ID=@id";

            MySqlCommand comando = new MySqlCommand(eliminar, DataBaseConexion);

            comando.Parameters.AddWithValue("@id", textBoxID.Text);

            comando.ExecuteNonQuery();

            DataBaseConexion.Close();

            MessageBox.Show("DATOS ELIMINADOS");

            dataGridView1.DataSource = llenar();
        }

        private void nuevo_Click(object sender, EventArgs e)
        {
            textBoxID.Clear();
            textBoxNombre.Clear();
            textBoxApellido.Clear();
            textBoxEdad.Clear();

        }
    }
}
