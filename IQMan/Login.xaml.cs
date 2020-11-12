
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using IQManClient.DAL;
using EManifestClient.Core;
using IQMan.Helpers;
using System.Net.Http;
using IQManClient.DAL.Enums;
using System.IO;

namespace IQMan
{
    public partial class Login : Window
    {
        DataDownloader<Vessels, Users, VoyageDetails, CountryCodes, LocationCodes, UNHSCodes, PackageCodes, Lines, ContainerIsoCodes, UNHazardousCodes> downloader;
        LoginInfoHelper loginHelper;
        public Login()
        {
            InitializeComponent();
            downloader = new DataDownloader<Vessels, Users, VoyageDetails, CountryCodes, LocationCodes, UNHSCodes, PackageCodes, Lines, ContainerIsoCodes, UNHazardousCodes>();

            Sources source = new Sources();

        }

        private async void btnlogin_Click(object sender, RoutedEventArgs e)
        {

            if (txtname.Text == "" || txtpassword.Password == "")
            {
                RadWindow.Alert(new DialogParameters { Owner = this, Content = "Enter username and password" });
                return;
            }
            using (EmanifestContext db = new EmanifestContext())
            {
                try
                {
                    btnlogin.IsEnabled = btnClose.IsEnabled = false;
                    loginHelper = new LoginInfoHelper(db);
                    var onlineUser = await downloader.GetUser(txtname.Text, txtpassword.Password);
                    if (onlineUser != null)
                    {
                        loginHelper.ProcessLoginInfo(onlineUser);
                        if (onlineUser.IsActive != false)
                        {
                            Settings.User = onlineUser;
                            winMain window = new winMain();
                            window.Show();
                            Close();
                        }
                        else
                        {
                            RadWindow.Alert(new DialogParameters { Owner = this, Content = "User is stopped contact admin for support." });
                        }
                    }
                    else
                    {
                        RadWindow.Alert(new DialogParameters { Owner = this, Content = "User not found." });
                    }
                }
                catch (HttpRequestException)
                {
                    try
                    {
                        var dbUser = db.Users.Include("Carriers.ApiClients").FirstOrDefault(u => u.UserName == txtname.Text && u.Password == txtpassword.Password);
                        if (dbUser != null)
                        {
                            if (dbUser.IsActive == false)
                            {
                                RadWindow.Alert(new DialogParameters { Owner = this, Content = "User is stopped contact admin for support." });
                                return;
                            }
                            Settings.User = dbUser;
                            winMain window = new winMain();
                            window.Show();
                            Close();
                        }
                        else
                        {
                            RadWindow.Alert(new DialogParameters { Owner = this, Content = "User not found." });
                        }
                    }
                    catch (Exception ex)
                    {
                        RadWindow.Alert(new DialogParameters
                        {
                            Owner = this,
                            Content = new TextBlock
                            {
                                Width = 200,
                                Text = ex.Message,
                                TextWrapping = TextWrapping.WrapWithOverflow
                            }
                        });
                    }

                }
                catch (Exception ex)
                {
                    RadWindow.Alert(new DialogParameters
                    {
                        Owner = this,
                        Content = new TextBlock
                        {
                            Width = 200,
                            Text = ex.Message,
                            TextWrapping = TextWrapping.WrapWithOverflow
                        }
                    });
                }
                finally
                {
                    btnlogin.IsEnabled = btnClose.IsEnabled = true;
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtname.Focus();
        }

        private void txtpassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnlogin.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}
