using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Memoria
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Button> buttonList = new List<Button>();
        List<int> szamok = new List<int> { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
        int szamlalo_katt = 0;
        Button elso;
        Button masodik;

        public MainWindow()
        {
            InitializeComponent();
            GridBeallitas(4);
            CreateButtons(4);
        }
        public void GridBeallitas(int N)
        {
            for (int i = 0; i < N; i++)
            {
                racs.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < N; i++)
            {
                racs.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }
        public void CreateButtons(int N)
        {
            Random rnd = new Random();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Button button = new Button();
                    int randomnumber = rnd.Next(szamok.Count);
                    button.Tag = szamok[randomnumber];
                   // button.Content = szamok[randomnumber];
                    button.Click += Button_Click;
                    button.FontSize = 40;
                    szamok.RemoveAt(randomnumber);
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    racs.Children.Add(button);
                    buttonList.Add(button);
                }
            }
        }
        async private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var tagValue = button.Tag;
            if (szamlalo_katt == 0) 
            {
                elso = button;
                elso.Content = tagValue;
                szamlalo_katt++;
                elso.IsEnabled = false;
            }
            else if (szamlalo_katt == 1)
            {
                masodik = button;
                masodik.Content = tagValue;
                szamlalo_katt = 0;
                masodik.IsEnabled = false;
                buttonList.ForEach(x => x.IsEnabled = false);
                await Task.Delay(1000);
                if (elso.Content.ToString() == masodik.Content.ToString())
                {
                    buttonList.ForEach(x => x.IsEnabled = true);
                    elso.IsEnabled = false;
                    masodik.IsEnabled = false;
                    buttonList.Remove(elso);
                    buttonList.Remove(masodik);
                }
                else
                {
                    buttonList.ForEach(x => x.IsEnabled = true);
                    elso.Content = "";
                    masodik.Content = "";
                    elso.IsEnabled = true;
                    masodik.IsEnabled = true;
                }
            }
            if (buttonList.Count < 1)
            {
                MessageBox.Show("Gratulálok, nyertél!");
            }
        }
    }
}
