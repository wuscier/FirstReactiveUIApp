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
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView :IViewFor<LoginViewModel>
    {
        public LoginView()
        {
            InitializeComponent();

            ViewModel = new LoginViewModel();

            this.WhenActivated(d =>
            {
                d(this.Bind(ViewModel, x => x.UserName, x => x.UserNameTextBox.Text));
                d(this.Bind(ViewModel, x => x.Password, x => x.PasswordTextBox.Text));
                d(this.BindCommand(ViewModel, x => x.LoginCommand, x => x.LoginButton, nameof(LoginButton.Click)));
                d(this.BindCommand(ViewModel, x => x.ResetCommand, x => x.ResetButton, nameof(ResetButton.Click)));
                d(this.BindCommand(ViewModel, x => x.LoadCommand, x => x.LoginWindow, nameof(LoginWindow.Loaded)));
            });
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }

            set { ViewModel = (LoginViewModel) value; }
        }



        public LoginViewModel ViewModel
        {
            get { return (LoginViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(LoginViewModel), typeof(LoginView));


    }
}
