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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        VentasIDATEntities db = new VentasIDATEntities();
        private void Form2_Load(object sender, EventArgs e)
        { //LINQ
            var linqTablas = from p in db.Productos
                             join pr in db.Proveedores on p.IdProveedor equals pr.IdProveedor
                             join c in db.Categorias on p.IdCategoria equals c.IdCategoria
                             select new
                             {
                                 p.IdProducto,
                                 p.NombreProducto,
                                 pr.NombreCompañia,
                                 c.NombreCategoria
                             };
            dataGridView1.DataSource = linqTablas.ToList();
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            txtNProductos.Text = linqTablas.Count().ToString();
            //txtNProductos.Text = dataGridView1.RowCount.ToString();
        }
    }
}
