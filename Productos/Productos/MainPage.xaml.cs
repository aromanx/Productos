using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace Productos
{
	public partial class MainPage : ContentPage
	{
        public class Productos
        {
            [PrimaryKey]
            [AutoIncrement]
            public int Id { get; set; }
            public string Nombre { get; set; }
            public double PreciodeVenta { get; set; }
            public int Cantidad { get; set; }
            public double PreciodeCosto { get; set; }
            public string Descripcion { get; set; }
            public string Foto { get; set; }
        }

        public void AbrirBase()
        {
            //Crear la ruta donde se almacenara la base de datos
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio.db");

            //Abrir la base de datos y en caso de que no exista se creara
            var db = new SQLiteConnection(rutaDb);

            //Abrir la tabla de productos y en cso de que no exista se creara
            db.CreateTable<Productos>();

            //Cargar la lista de todos los productos a una arreglo
            var todoslosproductos = db.Table<Productos>().ToList();
            
            //mostrarla en el lisview perzonalizado para ver los datos de los productos
            lst.ItemsSource = null;
            lst.ItemsSource = todoslosproductos;
        }

        //Metodo que permite refrescar la vista principal de los productos cada bes que aparesca la MainPage
        private void MainPage_Appearing(object sender, EventArgs e)
        {

            AbrirBase();
        }

        //Metodo que llama a la pagina de AgregarProducto
        async private void MenuItem1_Clicked(object sender, EventArgs e)
        {
            var detailPage = new AgregarProducto();
            await Navigation.PushAsync(detailPage);
            AbrirBase();
        }

        //Metodo que prmite mostrar el detalle de un producto cada vez que se de click en uno en la vista principal
        private async Task ItemSeleccionado(object sender, ItemTappedEventArgs e)
        {
            var elemento = e.Item as Productos;
            var detailPage = new DetalleProducto();
            detailPage.BindingContext = elemento;
            await Navigation.PushAsync(detailPage);
            
        }


        public MainPage()
		{
			InitializeComponent();

            

            this.Appearing += MainPage_Appearing; 
        }


	}
}
