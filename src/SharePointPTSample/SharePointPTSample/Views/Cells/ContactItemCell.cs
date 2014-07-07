using Xamarin.Forms;

namespace SharePointPTSample.Views.Cells
{
    public class ContactItemCell : ViewCell
    {
        public ContactItemCell()
        {
            var name = new Label
            {
                YAlign = TextAlignment.Center
            };
            name.SetBinding(Label.TextProperty, "Name");

            var email = new Label
            {
                YAlign = TextAlignment.Center
            };
            email.SetBinding(Label.TextProperty, "Email");
            name.Font = Font.SystemFontOfSize(22);
            email.Font = Font.SystemFontOfSize(16);
           
            var layout = new StackLayout
            {
                Padding = new Thickness(20, 0, 0, 0),
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children = { name, email }
            };
            View = layout;
        }

        protected override void OnBindingContextChanged()
        {
            View.BindingContext = BindingContext;
            base.OnBindingContextChanged();
        }
    }
}
