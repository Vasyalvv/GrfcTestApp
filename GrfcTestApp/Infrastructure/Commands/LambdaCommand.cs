using GrfcTestApp.Infrastructure.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrfcTestApp.Infrastructure.Commands
{
    /// <summary>
    /// Универсальный шаблон команды
    /// </summary>
    internal class LambdaCommand : CommandBase
    {
        private readonly Action<object> _Execute;
        private readonly Func<object, bool> _CanExecute;

        public LambdaCommand(Action<object> execute,Func<object,bool> canExecute=null)
        {
            _Execute = execute?? throw new ArgumentNullException(nameof(execute));
            _CanExecute = canExecute;
        }
        public override bool CanExecute(object parameter)
        {
           return _CanExecute?.Invoke(parameter) ?? true;
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
                return;
            _Execute(parameter);
        }
    }
}
