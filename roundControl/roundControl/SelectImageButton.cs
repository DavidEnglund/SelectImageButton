using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SelectableControls
{
    public class SelectImageButton : AbsoluteLayout
    {
        // all of the selected and unselected values - selected first
        // these will all need at least simple getter/setter function to be used with xaml
        private int _SelectedBorderWidth;
        private Color _SelectedBorderColor;
        private Color _SelectedBackgroundColor;

        private int _UnselectedBorderWidth;
        private Color _UnselectedBorderColor;
        private Color _UnselectedBackgroundColor;
        // the public get and set for the selected/unselected colors and widths
        public int SelectedBorderWidth
        {
            get { return _SelectedBorderWidth; }
            set { _SelectedBorderWidth = value; }
        }
        public Color SelectedBorderColor
        {
            get { return _SelectedBorderColor; }
            set { _SelectedBorderColor = value; }
        }
        public Color SelectedBackgroundColor
        {
            get { return _SelectedBackgroundColor; }
            set { _SelectedBackgroundColor = value; }
        }
        
        public int UnselectedBorderWidth
        {
            get { return _UnselectedBorderWidth; }
            set { _UnselectedBorderWidth = value; }
        }
        public Color UnselectedBorderColor
        {
            get { return _UnselectedBorderColor; }
            set { _UnselectedBorderColor = value; }
        }
        public Color UnselectedBackgroundColor
        {
            get { return _UnselectedBackgroundColor; }
            set { _UnselectedBackgroundColor = value; }
        }

        // a private image and a public image source that maps to the images source
        private Image _SelectedImage = new Image();
        private Image _UnselectedImage = new Image();

        public ImageSource SelectedImage
        {
            get
            {
                return _SelectedImage.Source;
            }
            set
            {
                _SelectedImage.Source = value;
            }
        }
        public ImageSource UnselectedImage
        {
            get
            {
                return _UnselectedImage.Source;
            }
            set
            {
                _UnselectedImage.Source = value;
            }
        }
        // the  public get functions for the current modes looks
        public int BorderWidth
        {
            get
            {
                if (_selected)
                {
                    return _SelectedBorderWidth;
                }
                else
                {
                    return _UnselectedBorderWidth;
                }
            }
        }
        public Color BorderColor
        {
            get
            {
                if (_selected)
                {
                    return _SelectedBorderColor;
                }
                else
                {
                    return _UnselectedBorderColor;
                }
            }
        }
       new public Color BackgroundColor
        {
            get
            {
                if (_selected)
                {
                    return _SelectedBackgroundColor;
                }
                else
                {
                    return _UnselectedBackgroundColor;
                }
            }
        }
      
        // gets the currently displayed image
        public Image CurrentImage
        {
            get
            {
                if (_selected)
                {
                    return _SelectedImage;
                }
                else
                {
                    return _UnselectedImage;
                }
            }
        }
      
        // a selected bool
        private bool _selected = false;
        // public interface for getting the status of the selected bool
        public bool selected
        {
            get { return _selected; }
            set
            {
                if(_buttonGroup != null)
                {
                    if(!value)
                    {
                        if (_buttonGroup.selected != this)
                        {
                            setAsUnselected();
                        }// no else - if the group wants me selected I stay selected
                    }
                    else
                    {
                        if(_buttonGroup.selected == this)
                        {
                            setAsSelected();
                        }else
                        {
                            // dont need to run set as selected - the group will set itself to me and then tell me to be selected running the true value of this if
                            _buttonGroup.selected = this;
                        }
                    }
                }
                else
                {
                    // I belong to nobody and do as I please
                    if (value)
                    {
                        setAsSelected();
                    }
                    else
                    {
                        setAsUnselected();
                    }
                }
            }     
        }
        // A function to set this button as selected
        private void setAsSelected()
        {
            _selected = true;

            _SelectedImage.IsVisible = true;
            _UnselectedImage.IsVisible = false;
          
            // force a redraw to make it change the colors and stuff - could proably also use forceLayout but I want to change the background anyway.
            base.BackgroundColor = _SelectedBackgroundColor;            
        }
        //A function to set this button as unselected
        private void setAsUnselected()
        {
            _selected = false;

            _SelectedImage.IsVisible = false;
            _UnselectedImage.IsVisible = true;

            // force a redraw to make it change colors,images and border widths
            base.BackgroundColor = _UnselectedBackgroundColor;
        }
        // a select button group for this control to belong to
        private SelectButtonGroup _buttonGroup;
        // public interface for the button group this control belongs to
        public SelectButtonGroup buttonGroup
        {
            get
            {
                return _buttonGroup;
            }
            set
            {
                if (value != _buttonGroup)
                {
                    if (_buttonGroup != null)
                    {
                        _buttonGroup.removeButton(this);
                    }
                    _buttonGroup = value;
                    value.addButton(this);
                }              
            }
        }
        // a click function
        private TapGestureRecognizer tapRecognizer = new TapGestureRecognizer();
        public event EventHandler Clicked
        {
            remove { tapRecognizer.Tapped -= value; }
            add { tapRecognizer.Tapped += value; }
        }
        // the tapped does not work for iOS so lets try this command stuff and see
        
        void OnTapped(object s)
        {
            if (_buttonGroup != null)
            {
                selected = true;
            }
            else
            {
                if (_selected)
                {
                    selected = false;
                }
                else
                {
                    selected = true;
                }
            }
        }

        // the init class
        public SelectImageButton()
        {
            // init a tap(click) object to react to presses
            tapRecognizer.Tapped += Click_Tapped;
            
            this.GestureRecognizers.Add(tapRecognizer);
            // testing tapping for ios
           // tapRecognizer.Command = new Command(OnTapped);
           // tapRecognizer.Tapped += Click_Tapped;
          //  _SelectedImage.GestureRecognizers.Add(tapRecognizer);
           // _UnselectedImage.GestureRecognizers.Add(tapRecognizer);
           // this.InputTransparent = false;
           // this.IsEnabled = true;
            // image view setup
             _SelectedImage.IsVisible = false;

            this.Children.Add(_SelectedImage);
            this.Children.Add(_UnselectedImage);

            AbsoluteLayout.SetLayoutBounds(_SelectedImage, new Rectangle(.5, .5, 1, 1));
            AbsoluteLayout.SetLayoutFlags(_SelectedImage, AbsoluteLayoutFlags.All);

            AbsoluteLayout.SetLayoutBounds(_UnselectedImage, new Rectangle(.5, .5, 1, 1));
            AbsoluteLayout.SetLayoutFlags(_UnselectedImage, AbsoluteLayoutFlags.All);
       
           
            // also need a default to set the stacklayout to not fill
            this.VerticalOptions = LayoutOptions.Center;
            this.HorizontalOptions = LayoutOptions.Center;

            // some defaults
            _UnselectedBackgroundColor = Color.Silver;
            _UnselectedBorderColor = Color.Gray;
            _UnselectedBorderWidth = 5;

            _SelectedBackgroundColor = Color.White;
            _SelectedBorderColor = Color.Blue;
            _SelectedBorderWidth = 5;
            
            base.BackgroundColor = _UnselectedBackgroundColor;

           
        }

        private void Click_Tapped(object sender, EventArgs e)
        {
            // if the button is not part of a group we need to set it to toggle on/off
            if (_buttonGroup != null)
            {
                selected = true;
            }
            else
            {
                if (_selected)
                {
                    selected = false;
                }
                else
                {
                    selected = true;
                }
            }
        }
        // an override of the padding - if its set  in xaml it sets te base so ill just check its more than 0 and set it when I size the thing
        // otherwise we will set the base padding to the largest border plus _Padding so the image is inside the largest border
        private Double _Padding = -1;
        new public Thickness Padding
        {
            get { return new Thickness(_Padding); }
            set
            {
                _Padding = value.Bottom;
            }
        }
        // an override of the size request to get the width and height square
        protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint)
        {
            // messing with the padding to get the image inside the border
            if(_Padding < 0)
            {
                _Padding = base.Padding.Bottom;
            }
            //base.Padding = _Padding + _UnselectedBorderWidth;
            double fullsize = 0;
            // first we want to get the largest padding then the largest border then the largest of width or height
            /*
            if (base.Padding.HorizontalThickness < base.Padding.VerticalThickness)
            {
                fullsize += base.Padding.VerticalThickness;
            }
            else
            {
                fullsize += base.Padding.HorizontalThickness;
            }
            /**/
           // fullsize += _Padding*4;
            if(_SelectedBorderWidth < _UnselectedBorderWidth)
            {
                fullsize += _UnselectedBorderWidth*2;
                base.Padding = (_Padding*0.5)+ (_UnselectedBorderWidth*1);
            }
            else
            {
                fullsize += _SelectedBorderWidth*2;
                base.Padding = _Padding + _SelectedBorderWidth;
            }
            SizeRequest mysize =  base.OnSizeRequest(widthConstraint, heightConstraint);
            if(mysize.Request.Height > mysize.Request.Width || Double.IsInfinity(mysize.Request.Width))
            {
                fullsize += mysize.Request.Height;
            }
            else
            {
                fullsize += mysize.Request.Width;
            }
            // and now for a default size if there is no images
            if (SelectedImage == null && UnselectedImage == null)
            {
                fullsize += 20;
            }
       
            // everythings been added up now to apply it
            return new SizeRequest(new Size(fullsize,fullsize));
           

        }
    }
}
