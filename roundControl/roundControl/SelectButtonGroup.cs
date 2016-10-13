using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectableControls
{    
    public class SelectButtonGroup
    {
        // the list of buttons in the group
        private List<SelectImageButton> group;
        // the selected index's private variable
        private int _selectedIndex = 0;
       
        // the public interface for setting the selected control
        public SelectImageButton selected
        {
            get { return group[_selectedIndex]; }
            set
            {
                // set the requested buttton to be selected then deselect the rest
                _selectedIndex = group.IndexOf(value);
                value.selected = true;
                foreach (SelectImageButton checkForSelected in group)
                {
                    // the button wont let itself be set to false if the group has it as selected
                    checkForSelected.selected = false;
                }               
            }
        }
        // another interface that is a function
        public void setSelected(SelectImageButton selectMe)
        {
            selected = selectMe;
        }
        // and a function version for the get as well
        public SelectImageButton getSelected()
        {
            return selected;
        }
        // interfaces for the selected index
        public int selectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if(value > 0 && value < group.Count)
                {
                    selected = group[value];
                    _selectedIndex = value;
                }
            }
        }
        public SelectButtonGroup()
        {
            group = new List<SelectImageButton>();
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
