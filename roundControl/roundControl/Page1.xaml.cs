using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SelectableControls;

using Xamarin.Forms;

namespace roundControl
{
    public partial class Page1 : ContentPage
    {
        SelectButtonGroup myButtonGroup = new SelectButtonGroup();

        public Page1()
        {
            InitializeComponent();

            myButtonGroup.addButton(firstButton);
            myButtonGroup.addButton(secondButton);
            myButtonGroup.addButton(thirdButton);

        }

        private void button_clicked(object sender, EventArgs e)
        {
            SelectImageButton clickedButton = (SelectImageButton)sender;
            DisplayAlert("buttons index in Group", "index: " + clickedButton.buttonGroup.SelectedIndex, "ok");
            //Navigation.PopModalAsync();
        }
        private void three_clicked(object sender, EventArgs e)
        {
            SelectImageButton clickedButton = (SelectImageButton)sender;
            DisplayAlert("buttons index in Group", "index: " + clickedButton.buttonGroup.SelectedIndex, "ok");
            Navigation.PopModalAsync();
        }

    }
}
