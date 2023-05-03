using Avalonia.Controls;
using Avalonia.Controls.Templates;
using sks_toolkit.ViewModels;
using System;

namespace sks_toolkit
{
    public class ViewLocator : IDataTemplate
    {
        public IControl Build(object data)
        {
            string User=Environment.UserName;
            Console.WriteLine(User);
            var name = data.GetType().FullName!.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type)!;
            }

            return new TextBlock { Text = "Not Found: " + name };
        }

        public bool Match(object data)
        {
            return data is ViewModelBase;
        }
    }
}