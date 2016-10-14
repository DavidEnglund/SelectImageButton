using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SelectableControls
{    
    public class SelectButtonGroup : INotifyPropertyChanged
    {
        // the data binding event change handler
        public event PropertyChangedEventHandler PropertyChanged;
        // the list of buttons in the group
        private ObservableCollection<SelectImageButton> group;

        // the public interface for the list of button in the group
        public ObservableCollection<SelectImageButton> Buttons
        {
            get { return group; }
            set { group = value; }
        }
        // the selected index's private variable
        private int selectedIndex = 0;

        // the public interface for setting the selected control
        public SelectImageButton Selected
        {
            //get { return (SelectImageButton)GetValue(SelectedProperty); }
           // set { SetValue(SelectedProperty, value); }
            
            get { return group[selectedIndex]; }
            set
            {
                // set the requested buttton to be selected then deselect the rest
                if (group.Contains(value))
                {
                    selectedIndex = group.IndexOf(value);
                    value.selected = true;
                    foreach (SelectImageButton checkForSelected in group)
                    {
                        // the button wont let itself be set to false if the group has it as selected
                        checkForSelected.selected = false;
                        // and update for the binding
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Selected"));
                    }
                }else
                {
                    Debug.WriteLine("you dont belong here");
                }            
            }
            /**/
        }
        // function to chnage selecet item
        /*
        static void SelectedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // get then new button
            var newButton = (SelectImageButton)newValue;
            // set the requested buttton to be selected then deselect the rest
            if (group.Contains(newButton))
            {
                ((SelectButtonGroup)bindable).selectedIndex = group.IndexOf(newButton);
                newButton.selected = true;
                foreach (SelectImageButton checkForSelected in group)
                {
                    // the button wont let itself be set to false if the group has it as selected
                    checkForSelected.selected = false;
                }
            }
            else
            {
                Debug.WriteLine("you dont belong here");
            }
        }
        */
        // another interface that is a function
        public void SetSelected(SelectImageButton selectMe)
        {
        Selected = selectMe;
        }
        // and a function version for the get as well
        public SelectImageButton getSelected()
        {
            return Selected;
        }
        // interfaces for the selected index
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if(value >= 0 && value < group.Count)
                {
                    Selected = group[value];
                    selectedIndex = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedIndex"));
                }
            }
        }
        // bindable change index function
        // bindalbe change button function

        public SelectButtonGroup()
        {
            group = new ObservableCollection<SelectImageButton>();
        }
        // adding a button to the group
        public void addButton(SelectImageButton addedButton)
        {
            if (!group.Contains(addedButton))
            {
                group.Add(addedButton);
                addedButton.buttonGroup = this;
            }
        }
        // a second init for adding an array of buttons
        public SelectButtonGroup(SelectImageButton[] buttonArray)
        {
            foreach(SelectImageButton addedButton in buttonArray)
            {
                group.Add(addedButton);
            }
        }
        // removing a button from the group
        public void removeButton(SelectImageButton addedButton)
        {
            if (group.Contains(addedButton))
            {
                group.Remove(addedButton);
                addedButton.buttonGroup = null;
            }
        }
    }
}
