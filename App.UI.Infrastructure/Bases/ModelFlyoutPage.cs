﻿using App.Infrastructure.Common;
using App.UI.Infrastructure.ViewModels.Abstractions;

namespace App.UI.Infrastructure.Bases
{
    public class ModelFlyoutPage<T> : FlyoutPage where T : ViewModelAbstract
    {
        public T ViewModel { get; private set; }
        public ModelFlyoutPage()
        {
            ViewModel = InstanceMap<AppContainer>.Instance.GetService<T>();
            BindingContext = ViewModel;
        }
    }
}
