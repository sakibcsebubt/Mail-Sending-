using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
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

namespace WPFMAILSENDING_APPS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnAttach_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                this.txtAttach.Text = openFileDialog.FileName;
            }
        }
        private void btnSendMail_Click(object sender, RoutedEventArgs e)
        {
            string sendFromMail = txtSender.Text.Trim() + cmbRateUnit.Text.Trim();//"rubel.gm@pirthe.com";
            string sendPassword = txtpass.Password.Trim();// "rubel12345";
            string senderhost = ((ComboBoxItem)this.cmbRateUnit.SelectedItem).Tag.ToString(); //"shared122.accountservergroup.com"; // "smtp.gmail.com"; for gmail
            int senderport = Convert.ToInt32(((ComboBoxItem)this.cmbRateUnit.SelectedItem).Uid);// 587;


            string sendToMail = txtTo.Text.Trim();//"hafiz6906@gmail.com"; // "hafiz@asitsl.com";//
            MailMessage mm = new MailMessage(sendFromMail, sendToMail);
            mm.Subject = "Test Subject";
            mm.Body = "This is a test description";
            mm.IsBodyHtml = false;
            mm.Attachments.Add(new Attachment(txtAttach.Text));// "LandSurvey1.ico"); ;//);
            //mm.Attachments.Add(new Attachment(eTest.Attachments.InputStream, fileName));

            SmtpClient smtp = new SmtpClient();
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Host = senderhost;// "shared122.accountservergroup.com";
            //smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            if (senderport == 8889) { smtp.EnableSsl = false; }
            NetworkCredential NetworkCred = new NetworkCredential(sendFromMail, sendPassword);
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = senderport;// 587;
            smtp.Send(mm);

            //msgBox.Visibility = Visibility.Visible.;
            msgBox.Text = "Mail Sent Successfully !! Thank You.";
        }

    }
}
