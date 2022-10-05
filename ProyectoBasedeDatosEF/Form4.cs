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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        VentasIDATEntities db = new VentasIDATEntities();
        private void Form4_Load(object sender, EventArgs e)
        {
            //consulta agrupada
            var linquery=from p in db.Pedidos join c in db.Clientes
            on p.IdCliente equals c.IdCliente group new {p,c} 
            by new {c.IdCliente,c.NombreContacto,c.Pais} into grupoidat
            orderby grupoidat.Select(det =>det.p.IdPedido).Count() descending
            select new
            {
                grupoidat.Key.IdCliente,
                grupoidat.Key.NombreContacto,
                grupoidat.Key.Pais,
                CantPedidos=grupoidat.Select(det => det.p.IdPedido).Count(),                
            };
            dataGridView1.DataSource = linquery.ToList();
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //sumar la columna cantidad
            txtTotalVenta.Text = linquery.Select(idat => idat.CantPedidos).Sum().ToString();
        }
    }
}
