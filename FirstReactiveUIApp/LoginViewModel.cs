using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ReactiveUI;

namespace FirstReactiveUIApp
{
    public class LoginViewModel : ReactiveObject
    {
        private readonly ReactiveCommand<Unit, Unit> _loadCommand;
         private readonly ReactiveCommand<Unit, Unit> _loginCommand;
        private readonly ReactiveCommand<Unit, Unit> _resetCommand;

        private string _userName;
        private string _password;

        public LoginViewModel()
        {
            var canLogin = this.WhenAnyValue(
                x => x.UserName,
                x => x.Password,
                (username, password) => !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password));

            _loginCommand = ReactiveCommand.CreateFromObservable(
                this.LoginAsync, canLogin);

            _resetCommand = ReactiveCommand.Create(() =>
            {
                UserName = null;
                Password = null;
            });

            _loadCommand = ReactiveCommand.CreateFromTask(LoadAsync);
        }

        private async Task<Unit> LoadAsync()
        {
            using (WebClient webClient = new WebClient())
            {
                string result = await webClient.DownloadStringTaskAsync("https://github.com/wuscier");

                UserName = result;

                return Unit.Default;
            }
        }

        public ReactiveCommand<Unit, Unit> LoginCommand => _loginCommand;
        public ReactiveCommand<Unit, Unit> ResetCommand => _resetCommand;
        public ReactiveCommand<Unit, Unit> LoadCommand => _loadCommand;

        public string UserName
        {
            get { return _userName; }
            set { this.RaiseAndSetIfChanged(ref _userName, value); }
        }


        public string Password
        {
            get { return _password; }
            set { this.RaiseAndSetIfChanged(ref _password, value); }
        }

        private IObservable<Unit> LoginAsync() => Observable.Return(new Random().Next(0, 2) == 1)
            .Delay(TimeSpan.FromSeconds(1))
            .Do(success =>
            {
                if (!success)
                {
                    MessageBox.Show("登录失败！");
                }
                else
                {
                    MessageBox.Show("登录成功！");
                }
            })
            .Select(_ => Unit.Default);
    }
}
