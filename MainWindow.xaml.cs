using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections;
using System.Windows;
using System;
using System.Collections.Generic;

//Миронова Анастасия 22-ИСП-2/1

namespace lab6; 

public partial class MainWindow
{
    private readonly ArrayList _array = new();
    private readonly List<double> _list = new();
    private readonly Random _random = new();

    private readonly Stack.Stack<double> _intermediateStack = new();


    public MainWindow()
    {
        InitializeComponent();
        OutputArrayList.ItemsSource = _array;
    }

    private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            PushToArray(sender, e);
        }
    }


    private void ShowCount()
    {
        var arrayCopy = (double[])_array.ToArray(typeof(double));

        int count = 0;
        foreach (var number in arrayCopy)
        {
            if (number > 0) count++;
        }


        Count.Text = $"В записанном массиве {count} положительных чисел";
    }

    private void UpdateStack()
    {
        OutputStack.Items.Clear();
        Stack.Stack<double> outputStack = new();

        foreach (var item in _intermediateStack)
            outputStack.Enqueue(item);
        foreach (var item in outputStack)
        {
            ListBoxItem listItem = new ListBoxItem { Content = item };
            OutputStack.Items.Add(listItem);
        }
    }

    private void PushToArray(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(TextBoxInput.Text))
        {
            SetError("Необходимо ввести число");
        }
        else
        {
            var inputStringArray = TextBoxInput.Text.Split(";");

            if (inputStringArray.Length != 12)
            {
                SetError("Необходимо ввести 12 вещественных чисел");
                return;
            }

            foreach (var inputString in inputStringArray)
            {
                var success = double.TryParse(inputString, out var value);

                if (!success)
                {
                    SetError("Данные некорректны");
                    return;
                }

                if (Math.Abs(value) <= 10)
                    _array.Add(value);
            }

            TextBoxInput.Clear();
            DisableArrayAddAndFillButton();

            OutputArrayList.Items.Refresh();
            ShowCount();   
        }
    }

    private void NumberInputGotFocus(object sender, RoutedEventArgs e)
    {
        ErrorOut.Text = "";
        TextBoxInput.BorderBrush = Brushes.Black;
        ErrorOut.Margin = new Thickness();
        ErrorOut.Padding = new Thickness();
        ErrorOut.Visibility = Visibility.Hidden;
    }

    private void SetError(string message)
    {
        ErrorOut.Text = message;
        TextBoxInput.BorderBrush = Brushes.DarkRed;
        ErrorOut.Margin = new Thickness(0, 0, 0, 10);
        ErrorOut.Padding = new Thickness(5);
        ErrorOut.Visibility = Visibility.Visible;
    }

    private void PushToStack(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(TextBoxStackInput.Text))
        {
            TextBoxStackInput.BorderBrush = Brushes.DarkRed;
            TextBoxStackInput.Clear();
            return;
        }

        if (!double.TryParse(TextBoxStackInput.Text, out var number))
        {
            TextBoxStackInput.BorderBrush = Brushes.DarkRed;
            TextBoxStackInput.Clear();
            return;
        }

        _intermediateStack.Enqueue(number);
        UpdateStack();
        TextBoxStackInput.Clear();
    }

    private void PopToStack(object sender, RoutedEventArgs e)
    {
        _intermediateStack.Dequeue();
        UpdateStack();
    }

    private void InsertToList(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(TextBoxListInput.Text))
        {
            TextBoxListInput.BorderBrush = Brushes.DarkRed;
            TextBoxListInput.Clear();
            return;
        }

        if (!double.TryParse(TextBoxListInput.Text, out var number))
        {
            TextBoxListInput.BorderBrush = Brushes.DarkRed;
            TextBoxListInput.Clear();
            return;
        }

        _list.Add(number);
        UpdateListTextBox();
        TextBoxListInput.Clear();
        UpdateListSum();
    }

    private void UpdateListSum()
    {
        double sum = 0;
        foreach (var number in _list)
        {
            if (number >= 15)
                sum += number;
        }

        ListSum.Text = $"Сумма чисел больше или равных 15: {sum}";
    }

    private void UpdateListTextBox()
    {
        OutputList.Items.Clear();
        foreach (var item in _list)
        {
            var listItem = new ListBoxItem
            {
                Content = item
            };

            OutputList.Items.Add(listItem);
        }
    }

    private void FillArray(object sender, RoutedEventArgs e)
    {
        for (var _ = 0; _ < 12; _++)
        {
            var number = Math.Round(_random.NextDouble() * 10, 3);

            if (Math.Abs(number) <= 10)
            {
                _array.Add(_random.Next(2) == 1 ? number : -number);
            }
        }

        DisableArrayAddAndFillButton();

        ShowCount();
        OutputArrayList.Items.Refresh();
    }

    private void DisableArrayAddAndFillButton()
    {
        const string toolTip = "Массив уже был введен";
        FillArrayButton.IsEnabled = false;
        FillArrayButton.ToolTip = toolTip;

        AddButton.IsEnabled = false;
        AddButton.ToolTip = toolTip;
    }
}