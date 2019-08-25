using System.Linq;
using System.Windows;
using System.Windows.Interactivity;

namespace Emando.Vantage.Windows.Controls
{
    public class DropTarget : Behavior<UIElement>
    {
        public static readonly DependencyProperty AllowedEffectsProperty = DependencyProperty.Register("AllowedEffects", typeof(DragDropEffects), typeof(DropTarget),
            new PropertyMetadata(DragDropEffects.Move));

        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(IDropTarget), typeof(DropTarget),
            new PropertyMetadata(null));

        public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(DropTarget), new PropertyMetadata(null));

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public IDropTarget Target
        {
            get { return (IDropTarget)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        public DragDropEffects AllowedEffects
        {
            get { return (DragDropEffects)GetValue(AllowedEffectsProperty); }
            set { SetValue(AllowedEffectsProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AllowDrop = true;
            AssociatedObject.PreviewDragEnter += OnPreviewDragEnter;
            AssociatedObject.PreviewDragOver += OnPreviewDragEnter;
            AssociatedObject.Drop += OnDrop;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.AllowDrop = false;
            AssociatedObject.PreviewDragEnter -= OnPreviewDragEnter;
            AssociatedObject.PreviewDragOver -= OnPreviewDragEnter;
            AssociatedObject.Drop -= OnDrop;
            base.OnDetaching();
        }

        private void OnPreviewDragEnter(object sender, DragEventArgs e)
        {
            if ((from f in e.Data.GetFormats()
                 let data = e.Data.GetData(f)
                 where e.Data.GetDataPresent(f) && Target != null && Target.CanDrop(Name, f, data)
                 select f).Any())
            {
                e.Effects = e.AllowedEffects & AllowedEffects;
                e.Handled = true;
                return;
            }

            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            if (Target == null)
                return;

            var position = e.GetPosition(AssociatedObject);
            var relativePosition = new Point(position.X / AssociatedObject.RenderSize.Width, position.Y / AssociatedObject.RenderSize.Height);

            foreach (var format in e.Data.GetFormats())
                if (e.Data.GetDataPresent(format))
                    Target.Drop(Name, format, e.Data.GetData(format), relativePosition);
        }
    }
}