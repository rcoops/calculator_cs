using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FuckingCalculator.Annotations;

namespace FuckingCalculator
{
    class ViewModel : INotifyPropertyChanged
    {
        private string equation;
        private CommandBtn cmd1;

        public CommandBtn Cmd1 { get; set; }

        public string Equation
        {
            get { return this.equation; }
            set
            {
                equation = value;
                RaisePropertyChanged("Equation");
            }
        }

        public ViewModel()
        {
            this.Cmd1 = new CommandBtn(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void calculate()
        {
            string[] numbers = Regex.Split(equation, "[\\\\+\\-\\*]"); 
            string[] operators = Regex.Split(equation,"[0-9]");
            Stack<double> numbersStack = new Stack<double>();
            foreach (var s in numbers.Reverse())
            {
                numbersStack.Push(double.Parse(s));
            }
            foreach (var op in operators)
            {
                if (numbersStack.Count == 1)
                {
                    break;
                }
                double secondNumber = numbersStack.Pop();
                double firstNumber = numbersStack.Pop();
                numbersStack.Push(calculate(op, firstNumber, secondNumber));
            }
            Equation = numbersStack.Pop().ToString();
        }

        protected double calculate(string op, double firstNumber, double secondNumber)
        {
            double result = 0.0d;
            switch (op)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "-":
                    result = firstNumber - secondNumber;
                    break;
                case "*":
                    result = firstNumber * secondNumber;
                    break;
                case "/":
                    result = firstNumber / secondNumber;
                    break;
            }
            return result;
        }
    }
}
