using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using roundControl;
using roundControl.Droid;
using System.ComponentModel;
using Android.Graphics;
using SelectableControls;
using SelectableControls.Droid;

[assembly: ExportRenderer(typeof(SelectableControls.SelectImageButton), typeof(SelectImageButtonRenderer))]
namespace SelectableControls.Droid
{
    class SelectImageButtonRenderer : ViewRenderer
    {
        /*
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);
            Xamarin.Forms.AbsoluteLayout aStack = (Xamarin.Forms.AbsoluteLayout)e.NewElement;
            var rad = new global::Android.Graphics.Drawables.GradientDrawable();
            rad.SetCornerRadius(1000.00F);
            //rad.SetColor(Android.Graphics.Color.Cyan);
            Android.Graphics.Color backColor = new Android.Graphics.Color();
            backColor.Equals(aStack.BackgroundColor);
            rad.SetColor(backColor);
            rad.SetStroke(5, Android.Graphics.Color.Silver);
            this.Background = rad;
            // Foreground = rad;   
            
            
        }
        /**/
        // testing setting a double here that I can set on property chnage with the border width and then use in the draw
        private float drarBorderWidth = 0;
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            // get a copy of the xamarin control we are changing
            SelectImageButton formControl = (SelectImageButton)sender;
            // get the set background color
            Xamarin.Forms.Color bgcolor = formControl.BackgroundColor;
            // create a drawable color and set irs raduis, color, and border
            var rad = new global::Android.Graphics.Drawables.GradientDrawable();
            rad.SetCornerRadius(1000.00F);
            rad.SetColor(bgcolor.ToAndroid());
            rad.SetStroke(formControl.BorderWidth,formControl.BorderColor.ToAndroid());
            // apply the drawable to the background
            this.Background = rad;
            // get the base padding by calling the contral as a absolute layout 
            Xamarin.Forms.AbsoluteLayout getBasePadding = (Xamarin.Forms.AbsoluteLayout)sender;
            // take the padding form the xamarin control and convert it the the android padding also adding the border width so the border displays outside the image
            int ctrlPaddingLeft = Convert.ToInt32(getBasePadding.Padding.Left);// + formControl.BorderWidth;
            int ctrlPaddingTop = Convert.ToInt32(getBasePadding.Padding.Top);// + formControl.BorderWidth;
            int ctrlPaddingRight = Convert.ToInt32(getBasePadding.Padding.Right);// + formControl.BorderWidth;
            int ctrlPaddingBottom = Convert.ToInt32(getBasePadding.Padding.Bottom);// + formControl.BorderWidth;
             this.SetPadding(ctrlPaddingLeft,ctrlPaddingTop,ctrlPaddingRight,ctrlPaddingBottom);
            // set the draw border
            drarBorderWidth = (float)formControl.BorderWidth;
        }
        // this will clip the image to the circle inside the border and the padding using the padding set above
        protected override void OnDraw(Canvas canvas)
        {
            
            base.OnDraw(canvas);
            Path clipPath = new Path();
            //RectF converterRect = new RectF(0+(PaddingLeft*2),0+(PaddingTop*2),this.Width-(PaddingRight*2),this.Height-(PaddingBottom*2));
            RectF converterRect = new RectF(drarBorderWidth,drarBorderWidth, this.Width - drarBorderWidth, this.Height - drarBorderWidth);
            float[] rounding = new float[] { 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000 };
           // Path.Direction clipDir = new Path.Direction();
            clipPath.AddRoundRect(converterRect,rounding,Path.Direction.Ccw );
            canvas.ClipPath(clipPath);
        }
    }
}