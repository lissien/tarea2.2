using SignaturePad.Forms;
using tarea2.Model;
using tarea2.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace tarea2
{
    public partial class MainPage : ContentPage
    {
        string valueBase64;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            Stream image = await PadView.GetImageStreamAsync(SignatureImageFormat.Png);
            var mStream = (MemoryStream)image;
            byte[] data = mStream.ToArray();
            valueBase64 = Convert.ToBase64String(data);


            if (String.IsNullOrWhiteSpace(name.Text) || String.IsNullOrWhiteSpace(description.Text))
            {
                await DisplayAlert("ERROR", "SE DEBEN DE LLENAR LOS DATOS DE NOMBRE Y DESCRIPCION PARA GUARDAR", "OK");
            }

            var signatureToSave = new Signatures
            {
                imageCode = valueBase64,
                name = name.Text,
                description = description.Text
            };

            var result = await App.BaseDatos.saveSignature(signatureToSave);

            if (result != 1)
            {
                await DisplayAlert("ERROR", "OCURRIO UN ERROR, INTENTE DE NUEVO", "OK");
            }

            await DisplayAlert("AVISO", "GUARDADO CORRECTAMENTE", "OK");
        }

        private async void ViewSignaturesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignaturesList());
        }

        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            PadView.Clear();
        }
    }
}

