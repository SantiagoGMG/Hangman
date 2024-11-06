//using Android.Print;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Hangman
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        List<string> ListaPalabra = new List<string>()
        {
        "python",
        "java",
        "csharp",
        "javascript",
        "html",
        "css",
        "ruby",
        "swift",
        "kotlin",
        "go",
        "typescript",
        "php",
        "cobol",
        "rust",
        "dart"
        };

        Random random = new Random();

        List<char> ListaLetrasUsuario = new List<char>();

        private string spotlight = "";
        public string Spotlight
        {
            get => spotlight;
            set{ 
                spotlight = value;
                OnPropertyChanged();
            }
        }

        private List<char> letras = new List<char>();

        public List<char> Letras
        {
            get => letras;
            set
            {
                letras = value;
                OnPropertyChanged();
            }
        }

        //private string mensaje = "";

        public string Mensaje
        {
            get => lblPerdiste.Text;
            set
            {
                lblPerdiste.Text = value;
                OnPropertyChanged();
            }
        }

        public string Estado
        {
            get => lblErrores.Text;
            set
            {
                lblErrores.Text = value;
                OnPropertyChanged();
            }
        }

        private int errores;
        private ImageSource Imagen
        {
            get => imgHangman.Source;
            set
            {
                imgHangman.Source = value;
                OnPropertyChanged();
            }
        }



        private string adivinar;

        public MainPage()
        {
            InitializeComponent();
            Letras.AddRange("qwertyuiopasdfghjklzxcvbnm".ToCharArray());
            BindingContext = this;

            adivinar = ElegirPalabra();
            EnmascararPalabra(adivinar, ListaLetrasUsuario);
            lblPerdiste.IsVisible = false;
        }

        private string ElegirPalabra()
        {   // entre el 0 y el 15
            var value = random.Next(0, ListaPalabra.Count);
            String palabra = ListaPalabra[value];
            //lblPalabraAdivinar.Text = palabra;
            return palabra;
        }

        private void EnmascararPalabra (String palabra, List<char> letras)
        {
            var mascara = palabra.Select(x => letras.IndexOf(x) >= 0 ? x : '_').ToArray();
            Spotlight = string.Join(" ", mascara);
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Button boton = (Button)sender;
            //se obtiene el strign del boton pero es necesario convertirlo a char, y usamos el método toCharArray y se agarra la posición 0
            char letraBoton = boton.Text.ToCharArray()[0];
            boton.IsEnabled = false;
            //imgHangman.Source = "img2.jpg";
            //Debug.WriteLine($"Letra presionada: {letraBoton}");
            ManejadorLetra(letraBoton);
            
        }

        private void ManejadorLetra(char letraClickeada)
        {
            if(ListaLetrasUsuario.IndexOf(letraClickeada) == -1)
            {
                ListaLetrasUsuario.Add(letraClickeada);
            }
            if(adivinar.IndexOf(letraClickeada) >=0)
            {
                EnmascararPalabra(adivinar, ListaLetrasUsuario);
                RevisarSiGano();
            }
            else
            {
                errores = errores + 1;
                ActualizarEstado();
            }
        }

        private void RevisarSiGano()
        {
            if(Spotlight.Replace(" ", "") == adivinar)
            {
                Mensaje = "Ganador!";
                lblPerdiste.IsVisible = true;
            }
        }

        private void ActualizarEstado()
        { 

            Estado = $"Errores: {errores} de 6";
            Imagen = $"img{errores}.jpg";
            if(errores == 6)
            {
                Mensaje = "Perdiste!";
                lblPerdiste.IsVisible = true;
                Spotlight = adivinar;
            }
        }

        private void btnReiniciar_Clicked(object sender, EventArgs e)
        {
            Mensaje = "Perdiste!";
            lblPerdiste.IsVisible = false;
            lblErrores.Text = "Errores: 0 de 6";
            errores = 0;
            imgHangman.Source = "img0.jpg";
            Spotlight = "";
            ListaLetrasUsuario.Clear();
            adivinar = ElegirPalabra();
            EnmascararPalabra(adivinar, ListaLetrasUsuario);

            //sirve para reactivar los botones
            foreach (var view in contenedorBotones.Children.OfType<Button>())
            {
                if (view is Button button)
                {
                    button.IsEnabled = true;
                }
            }
        }
    }

}
