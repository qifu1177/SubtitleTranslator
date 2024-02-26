using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Common
{    
    public class Factory<TC> where TC : class
    {
        private static TC _instance;
        static Factory()
        {
            _instance = (TC)Activator.CreateInstance(typeof(TC));
        }
        public static TC Instance { get { return _instance; } }
        public static TC Init(Action<TC> initAction)
        {
            initAction.Invoke(_instance);
            return _instance;
        }
    }
    public class InstanceFactory<TC> where TC : class
    {
        public static TC Create()
        {
            return (TC)Activator.CreateInstance(typeof(TC));
        }
    }
    public class InstanceMap<I>
    {
        public static I Instance { get; set; }
    }
}
