using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Interfaces.Services.Abstracts
{
    public abstract class AbstractService
    {
        protected virtual void ActionWithExceptionHandler(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {

            }
        }
        protected virtual T FuncWithExceptionHandler<T>(Func<T> func) where T : class
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
