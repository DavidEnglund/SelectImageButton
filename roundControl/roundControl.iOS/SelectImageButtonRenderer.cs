using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using MonoTouch.Foundation;
//using MonoTouch.UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using SelectableControls.iOS;
using System.ComponentModel;
using UIKit;
using roundControl;
using CoreGraphics;
using CoreAnimation;

[assembly: ExportRenderer(typeof(SelectableControls.SelectImageButton), typeof(SelectImageButtonRenderer))]
namespace SelectableControls.iOS
{
    class SelectImageButtonRenderer : ViewRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            SelectImageButton formControl = (SelectImageButton)sender;

            Xamarin.Forms.Color bgcolor = formControl.BackgroundColor;

            //Layer.CornerRadius = 1000;
            // all 20 rect that are not putting baby in a corner - layer.bounds layer.contentsrect frame bounds
            // culprit - layer.frame now lets use it

            //  this.Bounds = new CGRect(20, 20, 20, 20);
            //  Layer.Bounds = new CGRect(20, 20, 20, 20);
            //Layer.MasksToBounds = true;
            //  Layer.ContentsCenter = new CGRect(20, 20, 20, 20);
            //Layer.ContentsRect = new CGRect(20, 20, 20, 20);
            // Layer.ContentsScale = 20F;
            CALayer teslay = new CALayer();
            CGRect tesrec = new CGRect();
            teslay.Frame = Layer.Frame;
            teslay.Bounds = Layer.Bounds;
            teslay.ContentsRect = Layer.ContentsRect;
            // shape layer test
            CAShapeLayer betes = new CAShapeLayer();
            betes.Frame = Frame;
            betes.FillRule = CAShapeLayer.FillRuleEvenOdd;
            // add path test
            CGPath pathy = new CGPath();
            pathy.AddEllipseInRect(new CGRect(0,0,formControl.Width,formControl.Height));
            betes.Path = pathy;
            Layer.Mask = betes;
            
           
            tesrec.Width = Bounds.Width - 30F;
            tesrec.Height = Bounds.Height - 30;
            tesrec.X = (float)Bounds.X;
            tesrec.Y = (float)Bounds.Y;
            //teslay.Bounds = tesrec;
           // Layer.Mask = teslay;
            // Layer.Frame = tesrec;
            // Console.WriteLine(tesrec.ToString() + " testrec----------------------------------------------------------");
            // Console.WriteLine(Layer.Frame.ToString() + " layer.frame----------------------------------------------------------");
           
            // Frame = new CGRect(20, 20, 20, 20); ;
            // this.AutosizesSubviews = true;
            // testing bounds
            Console.WriteLine(Layer.Frame.ToString() + " bounds before ----------------------------------------------------------");
           // Layer.Bounds = tesrec;
            
           // Layer.MasksToBounds = true;
            
            Console.WriteLine(Layer.Frame.ToString() + " bounds after ----------------------------------------------------------");
            Console.WriteLine(formControl.X.ToString() + "control X -------------");
            Console.WriteLine(formControl.Width.ToString() + "control wdith -------------");
            Layer.BackgroundColor = bgcolor.ToCGColor();
            // lets test adding if  not null
            Layer.BorderColor = formControl.BorderColor.ToCGColor();
           // Layer.BorderWidth = formControl.BorderWidth;
            Layer.CornerRadius = (float)formControl.Width/2;
            //this.LayoutMargins = new UIEdgeInsets(20, 20, 20, 20);
            


        }
        /*
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
           
        }


        /*
        // testing the on element changed
        protected override void OnElementChanged(ElementChangedEventArgs<SelectImageButton> e)
        {
            base.OnElementChanged(e);


            SelectImageButton formControl = e.NewElement;

            Xamarin.Forms.Color bgcolor = formControl.BackgroundColor;

            Layer.CornerRadius = 1000;
            Layer.BackgroundColor = bgcolor.ToCGColor();
            // lets test adding if  not null
            this.BackgroundColor = bgcolor.ToUIColor();
            if (Control != null)
            {
                Control.BackgroundColor = bgcolor.ToUIColor();
            }
            // testing creating a new UIview 
            UIView BackgroundView = new UIView();
            BackgroundView.Layer.BorderColor = bgcolor.ToCGColor();
            SetNativeControl(BackgroundView);
            if(formControl != null)
            {
                var shadowView = new UIKit.UIView();

                UIKit.UIView childView = new UIKit.UIView()
                {
                    BackgroundColor = formControl.BackgroundColor.ToUIColor(),
                    AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight
                };

                childView.Layer.BorderColor = Color.Black.ToCGColor();
                childView.Layer.BorderWidth = 1;
                childView.Layer.MasksToBounds = true;

                shadowView.Add(childView);
                shadowView.Layer.ShadowColor = Color.Black.ToCGColor();
                shadowView.Layer.ShadowOffset = new CoreGraphics.CGSize(10, 10);
                shadowView.Layer.ShadowOpacity = 0.4f;

                SetNativeControl(shadowView);
            }
        }
        /**/

    }

}
