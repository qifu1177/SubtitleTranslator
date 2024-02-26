

namespace App.UI.Infrastructure.Controls;

public partial class CheckBoxView : ContentView
{

    public CheckBoxView()
    {
        InitializeComponent();
        Checkbox.CheckedChanged += Checkbox_CheckedChanged;
    }

    private void Checkbox_CheckedChanged(object? sender, CheckedChangedEventArgs e)
    {
        UpdateValue();
    }

    public static readonly BindableProperty IsCheckedProperty =
        BindableProperty.Create("IsChecked", typeof(bool), typeof(CheckBoxView), false
            , BindingMode.OneWay, propertyChanged: OnIsCheckedChanged);

    private static object CreateDefaultValue(BindableObject bindable)
    {
        CheckBoxView obj = (CheckBoxView)bindable;
        obj.Checkbox.IsChecked= obj.IsChecked;
        return obj.IsChecked;
    }

    private static object GetIsChecked(BindableObject bindable, object value)
    {
        CheckBoxView obj = (CheckBoxView)bindable;
        return obj.Checkbox.IsChecked;
    }

    private static void OnIsCheckedChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (oldValue.Equals(newValue))
        {
            return;
        }
        CheckBoxView obj = (CheckBoxView)bindable;
        obj.Checkbox.IsChecked = (bool)newValue;
    }

    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set
        {
            SetValue(IsCheckedProperty, value);
        }
    }

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create("Text", typeof(string), typeof(CheckBoxView), "Checkbox View", BindingMode.OneWay, propertyChanged: OnTextChanged);

    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        CheckBoxView obj = (CheckBoxView)bindable;
        obj.CheckboLable.Text = (string)newValue;
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set
        {
            SetValue(TextProperty, value);
        }
    }

    private void LabelClicked(object sender, TappedEventArgs e)
    {
        Checkbox.IsChecked = !Checkbox.IsChecked;
        UpdateValue();
    }
    private void UpdateValue()
    {
        if (Checkbox.IsChecked == IsChecked)
        {
            return;
        }
        IsChecked = Checkbox.IsChecked;
        if (IsCheckedChanged != null)
        {
            IsCheckedChanged(this, Checkbox.IsChecked);
        }
    }
    public event Action<CheckBoxView, bool> IsCheckedChanged;
}