using System;
using System.ComponentModel;

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

        public MainPage()
        {
            InitializeComponent();
            Letras.AddRange("qwertyuiopasdfghjklzxcvbnm".ToCharArray());
            BindingContext = this;

            string adivinar = ElegirPalabra();
            EnmascararPalabra(adivinar, ListaLetrasUsuario);
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


    }

}
