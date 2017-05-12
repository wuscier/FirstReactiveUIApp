using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace FirstReactiveUIApp
{
    public class TheViewModel:ReactiveObject
    {
        private string _theText;

        public string TheText
        {
            get { return _theText; }
            set { this.RaiseAndSetIfChanged(ref _theText, value); }
        }

    }
}
