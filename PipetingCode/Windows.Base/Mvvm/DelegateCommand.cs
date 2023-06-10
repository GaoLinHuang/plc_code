#nullable enable

using System;
using System.Windows.Input;

namespace Windows.Base
{
    public class DelegateCommand : ICommand
    {
        public DelegateCommand()
        { }

        public DelegateCommand(Action<object>? execute)
        {
            this.ExecuteAction = execute;
        }

        // 事件
        public event EventHandler? CanExecuteChanged;

        // 使用lambda表达式简写，??说明，当前面的值为空的时候，返回预设的值
        public bool CanExecute(object? parameter) => true;

        //parameter != null && (CanExecuteFunc?.Invoke(parameter) ?? true);

        public void Execute(object? parameter)
        {
            ExecuteAction?.Invoke(parameter);
        }

        public Action<object>? ExecuteAction { get; set; }
        public Func<object, bool>? CanExecuteFunc { get; set; }
    }
}