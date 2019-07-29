using CompoundInterest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Compound_Interest_Calculator
{
   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<CompoundingDisplayModel> YearlyAmounts = new List<CompoundingDisplayModel>();

        public MainWindow()
        {
            InitializeComponent();
        }

        //Lead's people to my GitHub Account
        private void Author_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/bsm52");
        }

        //Only allows Numeric values to be entered into the fields
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("$[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
           
            //allows for the compounding functions
            Compounding compound = new Compounding();

            //clear every time new submission is made
            YearlyAmounts = new List<CompoundingDisplayModel>();
            YearlyAmountsDataGrid.ItemsSource = null;

            bool IsValid = ValidateValues();

            //If all of the fields have been entered. If not, nothing will happen
            if (IsValid == true)
            {
                CalculatorModel CalcMod = new CalculatorModel();
                CalcMod.InitialAmount = Convert.ToDouble(InitialAmountTextBox.Text); //can't keep it as a string
                CalcMod.InterestRate = Convert.ToDouble(InterstRateTextBox.Text);
                CalcMod.CalcPeriod = Convert.ToDouble(CompoundingPeriodTextBox.Text);
                CalcMod.CompoundingTimes = Convert.ToDouble(CompoundingPerYearTextBox.Text);
                if(AmountAddedPerMonTextBox != null && AmountAddedPerMonTextBox.Text  != "")
                {
                    CalcMod.MonthlyDeposit = Convert.ToDouble(AmountAddedPerMonTextBox.Text);
                }
                     

                int years = Convert.ToInt32(CalcMod.CalcPeriod);

                double TotalInterest = 0;

                double YearInterst = 0;

                double PrevYearBalance;
               
                PrevYearBalance = CalcMod.InitialAmount;
                
               

                for (int i = 1; i <= years; i++)
                {
                    CalcMod.CalcPeriod = i;

                    double balance;

                    if (CalcMod.MonthlyDeposit == null)
                    {
                        balance = compound.GetEndingBalance(CalcMod);
                        TotalInterest = balance - CalcMod.InitialAmount;
                    }
                    else
                    {
                        balance = compound.GetEndingBalanceWithMonthly(CalcMod);
                        TotalInterest = balance - (CalcMod.InitialAmount + (double)CalcMod?.MonthlyDeposit * 12 * i);
                    }
                        


                   

                    YearInterst = PrevYearBalance * CalcMod.InterestRate/100;

                    PrevYearBalance = balance;

                    YearlyAmounts.Add(new CompoundingDisplayModel()
                    {
                        Year = i,
                        Total_Balance = balance,
                        Total_Interest = TotalInterest,
                        Year_Interest = YearInterst
                    });
                    
                }

                YearlyAmountsDataGrid.Visibility = Visibility.Visible;

                YearlyAmountsDataGrid.ItemsSource = YearlyAmounts;

                


            }  
            
        }

        /// <summary>
        /// Validates all of the fields of the calculator
        /// </summary>
        /// <returns></returns>
        private bool ValidateValues()
        {
            if(InitialAmountTextBox.Text == "")
            {
                MessageBox.Show("Please enter an Initial Amount");
                return false;
            }
            if(InterstRateTextBox.Text == "")
            {
                MessageBox.Show("Please enter an Interest Rate");
                return false;
            }
            if(CompoundingPeriodTextBox.Text == "")
            {
                MessageBox.Show("Please enter a Compounding Period");
                return false;
            }
            if(CompoundingPerYearTextBox.Text == "")
            {
                MessageBox.Show("Please enter a Compounding Amount Per Year");
                return false;
            }
            return true;
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Info InfoWindow = new Info();
            InfoWindow.ShowDialog();
        }
    }
}
 