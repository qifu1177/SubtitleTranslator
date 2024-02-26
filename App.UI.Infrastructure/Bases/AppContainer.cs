using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.UI.Infrastructure.Bases
{
    public class AppContainer
    {
        private IServiceProvider _serviceProvider;
        public void SetProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public T? GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
