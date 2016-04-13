using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Power
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        double windspeed, AvgPowerDensity, AnnEnergyDelivered;

        public MainPage()
        {
            this.InitializeComponent();
        }

       // The method is used to calculate the Wind speed at the height given by the user.
        private void windSpeedCalculation () 
        {
            double a, b, c, d;
            int comboselection;
            double temp, temp1;

            a = double.Parse(txtAnemometerHeight.Text); //Ho
            b = double.Parse(txtReqHeight.Text);//H
            c = double.Parse(txtWindSpeed.Text);//Vo
            comboselection = cmbxTerrainChar.SelectedIndex;

            // Mapping the Friction Coefficient from terrain char combo box
            switch (comboselection)
            {
                case 0:
                    d = 0.10;
                    break;
                case 1:
                    d = 0.15;
                    break;
                case 2:
                    d = 0.20;
                    break;
                case 3:
                    d = 0.25;
                    break;
                case 4:
                    d = 0.30;
                    break;
                case 5:
                    d = 0.40;
                    break;
                default:
                    d = 0.15;
                    break;
            }

            //Wind Speed at the given Height
            temp = (b / a);
            temp1 = Math.Pow(temp, d);
            windspeed = temp1 * c;
            
            //Setting & enabling the required height with the user Input 
            bkHeight.Text = txtReqHeight.Text;
            bkHeightUnit.Visibility = Windows.UI.Xaml.Visibility.Visible;

            txtBlkWindSpeed.Text = windspeed.ToString(" #.### m/s");
            btnPowerDensity.IsEnabled = true;
        }

        private void btnWindSpeed_Click(object sender, RoutedEventArgs e)
        {
            if(chBoxDefault.IsChecked == true) // Checking whether the default value of Air density is chosen or not
            {
                if (txtAnemometerHeight.Text != "" && txtReqHeight.Text != "" && txtWindSpeed.Text != "" && cmbxTerrainChar.SelectedIndex != -1 
                                               && cmbxWindAltitude.SelectedIndex != -1 && cmbxWindTemperature.SelectedIndex != -1)
                {
                    try
                    {
                        windSpeedCalculation();
                    }
                    catch (Exception E)
                    {
                        MessageDialog dialog = new MessageDialog("Error: " + E.ToString(), "Error");
                        dialog.ShowAsync();
                    }
                }
                else
                {
                    MessageDialog dialog = new MessageDialog("No input fields on the left column can be empty. Enter a value", "Error");
                    dialog.ShowAsync();
                }
            }
            else
            {
                if (txtAnemometerHeight.Text != "" && txtReqHeight.Text != "" && txtWindSpeed.Text != "" 
                    && cmbxTerrainChar.SelectedIndex != -1)
                {
                    try
                    {
                        windSpeedCalculation();
                    }
                    catch (Exception E)
                    {
                        MessageDialog dialog = new MessageDialog("Error: " + E.ToString(), "Error");
                        dialog.ShowAsync();
                    }
                }
                else
                {
                    MessageDialog dialog = new MessageDialog("No input fields on the left column can be empty. Enter a value", "Error");
                    dialog.ShowAsync();
                }
            }
            
            
        }

        private void btnPowerDensity_Click(object sender, RoutedEventArgs e)
        {
            //Calulation for Average Power Density

                try
                {
                    if (chBoxDefault.IsChecked == true)
                    {
                        // The following calculations are done if the user chose to 
                        // enter the Wind Altitude and Wind Temperature
                        double a, b, c;
                        int tempSelec, windSelec;

                        a = windspeed;
                        tempSelec = cmbxWindTemperature.SelectedIndex;
                        windSelec = cmbxWindAltitude.SelectedIndex;

                        //Mapping the Density Ratio Kt from Temperature Combo Box
                        switch (tempSelec)
                        {
                            case 0:
                                b = 1.12;
                                break;
                            case 1:
                                b = 1.10;
                                break;
                            case 2:
                                b = 1.07;
                                break;
                            case 3:
                                b = 1.05;
                                break;
                            case 4:
                                b = 1.04;
                                break;
                            case 5:
                                b = 1.02;
                                break;
                            case 6:
                                b = 1.00;
                                break;
                            case 7:
                                b = 0.98;
                                break;
                            case 8:
                                b = 0.97;
                                break;
                            case 9:
                                b = 0.95;
                                break;
                            case 10:
                                b = 0.94;
                                break;
                            case 11:
                                b = 0.92;
                                break;
                            default:
                                b = 1.00;
                                break;
                        }

                        //Mapping the Density Ratio Kt from Temperature Combo Box
                        switch (windSelec)
                        {
                            case 0:
                                c = 1;
                                break;
                            case 1:
                                c = 0.977;
                                break;
                            case 2:
                                c = 0.954;
                                break;
                            case 3:
                                c = 0.931;
                                break;
                            case 4:
                                c = 0.910;
                                break;
                            case 5:
                                c = 0.888;
                                break;
                            case 6:
                                c = 0.868;
                                break;
                            case 7:
                                c = 0.847;
                                break;
                            case 8:
                                c = 0.827;
                                break;
                            case 9:
                                c = 0.808;
                                break;
                            case 10:
                                c = 0.789;
                                break;
                            case 11:
                                c = 0.771;
                                break;
                            default:
                                c = 1.00;
                                break;
                        }

                        AvgPowerDensity = 0.95 * 1.225 * b * c * Math.Pow(a, 3);

                        //Setting & enabling the required height with the user Input 
                        bkHeight1.Text = txtReqHeight.Text;
                        bkHeightUnit1.Visibility = Windows.UI.Xaml.Visibility.Visible;

                        txtBlkPowerDensity.Text = AvgPowerDensity.ToString(" #.### W/m2 ");
                        btnEnergyDelivered.IsEnabled = true;
                    }
                    else
                    {
                        // The following calculations are done if the user chose to 
                        // use the default values for Wind Temperature and Wind Altitude.
                        double a;

                        a = windspeed;

                        AvgPowerDensity = 0.95 * 1.225 * Math.Pow(a, 3);

                        //Setting & enabling the required height with the user Input 
                        bkHeight1.Text = txtReqHeight.Text;
                        bkHeightUnit1.Visibility = Windows.UI.Xaml.Visibility.Visible;

                        txtBlkPowerDensity.Text = AvgPowerDensity.ToString(" #.### W/m2 ");
                        btnEnergyDelivered.IsEnabled = true;
                    }
                }
                catch (Exception E)
                {
                    MessageDialog dialog = new MessageDialog("Error: " + E.ToString(), "Error");
                    dialog.ShowAsync();
                }
        }

        private void btnEnergyDelivered_Click(object sender, RoutedEventArgs e)
        {
            //Calculation for Annual Energy Delivered.

            if(txtRotorDia.Text != "" && txtTurbineEfficiency.Text != "")
            {
                try
                {
                    double a, b, c;

                    a = double.Parse(txtRotorDia.Text);
                    b = double.Parse(txtTurbineEfficiency.Text);
                    c = AvgPowerDensity;// Average Power Density

                    AnnEnergyDelivered = c * b * 8760 * (0.785 * Math.Pow(a, 2));
                    txtBlkEnergyDelivered.Text = AnnEnergyDelivered.ToString(" #.### J");

                }
                catch (Exception E)
                {
                    MessageDialog dialog = new MessageDialog("Error: " + E.ToString(), "Error");
                    dialog.ShowAsync();
                }
            }
            else
            {
                MessageDialog dialog = new MessageDialog("The input fields 'Blade Rotor Diameter' and 'Turbine Efficiency' cannot be empty. Enter a value", "Error");
                dialog.ShowAsync();
            } 
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            //Clearing all the Input Text Fields.
            txtAnemometerHeight.Text = "";
            txtReqHeight.Text = "";
            txtWindSpeed.Text = "";
            txtRotorDia.Text = "";
            txtTurbineEfficiency.Text = "";
           
            //Clearing all the Output Text Blocks
            txtBlkWindSpeed.Text = "";
            txtBlkPowerDensity.Text = "";
            txtBlkEnergyDelivered.Text = "";
            
            //Resetting the Buttons to their default state
            btnPowerDensity.IsEnabled = false;
            btnEnergyDelivered.IsEnabled = false;
            
            //Resetting the Combo Box selection and states.
            cmbxTerrainChar.SelectedIndex = -1;
            cmbxWindTemperature.IsEnabled = false;
            cmbxWindAltitude.IsEnabled = false;
            cmbxWindTemperature.SelectedIndex = -1;
            cmbxWindAltitude.SelectedIndex = -1;
            chBoxDefault.IsChecked = false;
            
            // Resetting the Unit Text Blocks in the results to their default state.
            bkHeight.Text = "";
            bkHeightUnit.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            bkHeight1.Text = "";
            bkHeight1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void chBoxDefault_Click(object sender, RoutedEventArgs e)
        {
            //Setting up the Combo boxes according to the state of Checkbox
            if(chBoxDefault.IsChecked == true)
            {
                cmbxWindTemperature.IsEnabled = true;
                cmbxWindAltitude.IsEnabled = true;
            }
            else
            {
                cmbxWindTemperature.IsEnabled = false;
                cmbxWindAltitude.IsEnabled = false;
                cmbxWindTemperature.SelectedIndex = -1;
                cmbxWindAltitude.SelectedIndex = -1;
            }
        }

    }
}
