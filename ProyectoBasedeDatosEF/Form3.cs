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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        VentasIDATEntities db = new VentasIDATEntities();
        private void Form3_Load(object sender, EventArgs e)
        { //consulta agrupada
            var linquery=from p in db.Pedidos join d in db.Detalles_de_pedidos
            on p.IdPedido equals d.IdPedido group new {p,d} 
            by new {p.IdPedido,p.FechaPedido,p.Cargo,p.IdCliente,p.IdEmpleado} into grupoidat
            orderby grupoidat.Sum(det =>det.d.PrecioUnidad*det.d.Cantidad) descending
            select new
            {
                grupoidat.Key.IdPedido,
                grupoidat.Key.FechaPedido,
                grupoidat.Key.Cargo,
                grupoidat.Key.IdCliente,
                grupoidat.Key.IdEmpleado,
                NroProdComprados=grupoidat.Select(det => det.d.IdProducto).Count(),
                Venta= grupoidat.Sum(det => det.d.PrecioUnidad*det.d.Cantidad)
            };
            dataGridView1.DataSource = linquery.ToList();
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //sumar la columna cantidad
            txtTotalVenta.Text = linquery.Select(idat => idat.Venta).Sum().ToString();
        }
    }
}
