using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBasedeDatosEF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //conexion al modelo de objetos de EF
        VentasIDATEntities db = new VentasIDATEntities();
        private void Form1_Load(object sender, EventArgs e)
        { //consultar el modelo usando LINQ            
            var query = from p in db.Productos
                        orderby p.PrecioUnidad ascending
                        select new
                        {
                            p.IdProducto,        
                            p.NombreProducto,
                            p.PrecioUnidad,
                            Stock = p.UnidadesEnExistencia
                        };
                        //select p;  //muestra todas las columnas
            dataGridView1.DataSource = query.ToList();
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            txtTotalizar.Text = db.Productos.Select(p => p.PrecioUnidad).Sum().ToString();
        }
    }
}
