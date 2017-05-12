using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ReactiveUI;

namespace FirstReactiveUIApp
{
    /// <summary>
    /// TheView.xaml 的交互逻辑
    /// </summary>
    public partial class TheView : IViewFor<TheViewModel>
    {
        public TheView()
        {
            InitializeComponent();

            ViewModel = new TheViewModel();

            this.WhenActivated(d =>
            {
                d(this.Bind(this.ViewModel, x => x.TheText, x => x.TheTextBox.Text));
                d(this.OneWayBind(this.ViewModel, x => x.TheText, x => x.TheTextBlock.Text));
            });
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }

            set { ViewModel = (TheViewModel) value; }
        }



        public TheViewModel ViewModel
        {
            get { return (TheViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(TheViewModel), typeof(TheView));


    }
}
