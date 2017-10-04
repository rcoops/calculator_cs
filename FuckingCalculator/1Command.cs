using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace FuckingCalculator
{
    class CommandBtn : ICommand
    {
        private ViewModel vm;

        public CommandBtn(ViewModel vm)
        {
            this.vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string str = parameter.ToString();
            switch (str)
            {
                case "=":
                    vm.calculate();
                    break;
                default:
                    this.vm.Equation += str;
                    break;
            }


        }

        public event EventHandler CanExecuteChanged;
    }
}
