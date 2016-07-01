using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Xam;
using CustomControl.Android;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace CustomControl.Android
{
    class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.LightBlue);
                Control.SetTextColor(global::Android.Graphics.Color.Black);
                
            }
        }
    }
}
