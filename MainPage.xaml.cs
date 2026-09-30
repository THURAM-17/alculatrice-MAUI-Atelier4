using System;
namespace CalculatriceMAUI;

public partial class MainPage : ContentPage
{
    string currentEntry = "";
    string selectedOperator = "";
    double firstNumber = 0;
    bool isOperatorClicked = false;

    public MainPage()
    {
        InitializeComponent();
    }

    void OnNumberClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string pressed = button.Text;

        if (CurrentInputLabel.Text == "0" || isOperatorClicked)
        {
            currentEntry = pressed;
            isOperatorClicked = false;
        }
        else
        {
            currentEntry += pressed;
        }

        CurrentInputLabel.Text = currentEntry;
    }

    void OnDecimalClicked(object sender, EventArgs e)
    {
        // Protection : Blocage de la double virgule
        if (!currentEntry.Contains(","))
        {
            if (string.IsNullOrEmpty(currentEntry))
                currentEntry = "0,";
            else
                currentEntry += ",";

            CurrentInputLabel.Text = currentEntry;
        }
    }

    void OnOperatorClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        selectedOperator = button.Text;
        
        string val = CurrentInputLabel.Text.Replace(",", ".");
        if (double.TryParse(val, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double res))
        {
            firstNumber = res;
        }
        isOperatorClicked = true;
    }

    void OnClearClicked(object sender, EventArgs e)
    {
        currentEntry = "";
        firstNumber = 0;
        selectedOperator = "";
        CurrentInputLabel.Text = "0";
    }

    void OnCalculateClicked(object sender, EventArgs e)
    {
        Button button = (sender as Button);
        string btnText = button?.Text;
        
        string val = CurrentInputLabel.Text.Replace(",", ".");
        if (!double.TryParse(val, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double secondNumber))
        {
            return;
        }

        if (btnText == "+/-")
        {
            double r = secondNumber * -1;
            CurrentInputLabel.Text = r.ToString().Replace(".", ",");
            currentEntry = CurrentInputLabel.Text;
            return;
        }
        
        if (btnText == "%")
        {
            double r = secondNumber / 100.0;
            CurrentInputLabel.Text = r.ToString().Replace(".", ",");
            currentEntry = CurrentInputLabel.Text;
            return;
        }

        double result = 0;
        switch (selectedOperator)
        {
            case "+": result = firstNumber + secondNumber; break;
            case "-": result = firstNumber - secondNumber; break;
            case "x": result = firstNumber * secondNumber; break;
            case "/":
                // Protection : Gestion de la division par zéro requise
                if (secondNumber == 0)
                {
                    CurrentInputLabel.Text = "Erreur : Div/0";
                    currentEntry = "";
                    return;
                }
                result = firstNumber / secondNumber;
                break;
        }

        CurrentInputLabel.Text = result.ToString().Replace(".", ",");
        currentEntry = CurrentInputLabel.Text;
    }
}