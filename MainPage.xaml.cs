namespace Younique_Pro_Assistant;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net;

public partial class MainPage : ContentPage
{
    int count = 0;
    //To declare variables
    Frmdash dash;
    SmtpClient smtp; MailMessage mail;
    public string qp = "", usid, pword, fname, lev, btpe, busid, ubus, pic, phone, q2, q3, q4;
    Visibility hi, sh, col;
    int log = 1, pasw = 0, qstat = 0; Decimal bal = 0, pswd = 0;
    string constr;
    SqlDataAdapter da1;
    DataRowView dtr1;
    DataSet ds1, ds2;

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        //On load of the page
        var theme = Application.Current.RequestedTheme;
        if (theme is AppTheme.Light)
            lightThemeRadioButton.IsChecked = true;
        else if (theme is AppTheme.Dark)
            darkThemeRadioButton.IsChecked = true;
    }

    private void lightThemeRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Application.Current.UserAppTheme = AppTheme.Light;
    }

    private void darkThemeRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Application.Current.UserAppTheme = AppTheme.Dark;
    }

    DataTable dt1, dt2, dt3, dt4;
    SqlCommand cmd1, cmd2;
    SqlConnection conn = new SqlConnection();
    SqlDataReader qr1, qr2, qr3, qr4, qr5;
    string sql, q1, qry = "", tp = "test";
    mvcvalues mvc; string platf = "WinUI";
    Frmdash dashboard;
    Task task1, task2;

    public MainPage()
    {
        InitializeComponent();
        hi = Visibility.Hidden;
        sh = Visibility.Visible;
        col = Visibility.Collapsed;
        //constr = @"Data Source=sql.bsite.net\MSSQL2016;Encrypt=False;Connection Timeout = 30;Persist Security Info=True;TrustServerCertificate=True;Multiple Active Result Sets=False;MultiSubnetFailover=True;Initial Catalog=gawhanye_younique; User ID = gawhanye_younique; Password=Younique@2023;";
        //constr = @"Data Source= hnk201.truehost.cloud,1435;Encrypt=False;Connection Timeout = 30;Multiple Active Result Sets=False;TrustServerCertificate=true;MultiSubnetFailover=True;Initial Catalog=younique_ums; User ID = Younique_2023; Password=Younique@2023;";
        //constr = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=younique_ums.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;";
        //constr = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=younique_ums;Integrated Security=True";
        //constr = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=ytums;TrustServerCertificate=True;Integrated Security=True";
        //constr = @"workstation id=younique_ums.mssql.somee.com;Multiple Active Result Sets=False;TrustServerCertificate=true;MultiSubnetFailover=True;packet size=4096;user id=gawhanye_SQLLogin_1;pwd=Younique@2023;data source=younique_ums.mssql.somee.com;persist security info=False;initial catalog=younique_ums;";
        //constr = @"Data Source=younique_ums.mssql.somee.com;Encrypt=True;Connection Timeout = 30;Multiple Active Result Sets=False;TrustServerCertificate=True;packet size=4096;MultiSubnetFailover=True;Initial Catalog=younique_ums; User ID = gawhanye_SQLLogin_1; persist security info=False;Password=Younique@2023;";
        //constr = @"Data Source=np:\\.\pipe\LOCALDB#76B87831\tsql\query;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Database=younique_ums;";
        piclogload.IsVisible = false; gdsign.IsVisible = false;
        tp = "test"; platf = DeviceInfo.Current.Platform.ToString();
        if (platf == "WinUI")
        {
            constr = @"Data Source=localhost;Encrypt=False;Connection Timeout = 30;Persist Security Info=True;TrustServerCertificate=True;Initial Catalog=younique_ums; User ID = Younique; Password=Younique@2023;";
        }
        else
        {
            constr = @"Data Source=MICHAEL-BEZI;Encrypt=False;Connection Timeout = 30;Persist Security Info=True;TrustServerCertificate=True;Initial Catalog=younique_ums; User ID = Younique; Password=Younique@2023;";
        }
        if (tp == "test")
        {
            DisplayAlert("Younique Ng", "For testing purposes (demo) on - " + platf, "Ok");
        }
        conn.ConnectionString = constr;
        //DisplayAlert("Younique Ng", constr, "Okay");
    }

    //To run processes
    async Task sp_thread()
    {
        qstat = 0; msg = "";
        try
        {

            switch (qry)
            {
                //Statements for different functions 's' = SELECT ELSE (insert, update or delete)
                case "s":
                    conn.Open();
                    da1.Fill(dt1);
                    qr1 = cmd1.ExecuteReader();
                    if (qr1.Read())
                    {
                        switch (qp)
                        {
                            case "login":
                                fname = qr1.GetString(2); phone = qr1.GetString(3);
                                pic = qr1.GetString(5); lev = qr1.GetString(7);
                                bal = qr1.GetDecimal(6); 
                                conn.Close();
                                cmd1 = new SqlCommand(q2);
                                cmd1.Connection = conn;
                                cmd1.CommandType = CommandType.Text;
                                da1 = new SqlDataAdapter(cmd1);
                                da1.SelectCommand.CommandTimeout = 36000;
                                dt1 = new DataTable();
                                conn.Open();
                                qr1 = cmd1.ExecuteReader();
                                qstat = 1; msg = "active";
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (qp)
                        {
                            case "signup":
                                conn.Close();
                                smtp.Send(mail);
                                cmd1 = new SqlCommand(q2);
                                cmd1.Connection = conn;
                                cmd1.CommandType = CommandType.Text;
                                da1 = new SqlDataAdapter(cmd1);
                                da1.SelectCommand.CommandTimeout = 36000;
                                dt1 = new DataTable();
                                conn.Open();
                                qr1 = cmd1.ExecuteReader();
                                qstat = 1; msg = "active";
                                break;
                            default:
                                qstat = 0;
                                break;
                        }
                    }
                    break;
                default:
                    conn.Open();
                    da1.Fill(dt1);
                    qr1 = cmd1.ExecuteReader();
                    qstat = 1; msg = "active";
                    break;
            }


        }
        catch (Exception ex)
        {
            conn.Close();
            qstat = 0; msg = "";
            //SystemSounds.Exclamation.Play();
            if (tp == "test")
            {
                msg = ex.Message;
            }
            else
            {
                msg = "";
            }
        }

    }



    string msg;
    //To clear swap sign in and login boxes
    private void logclear(object sender, EventArgs e)
    {
        switch (log)
        {
            case 1:
                log = 0;
                //For signup
                gdsign.IsVisible = true;
                btnreg.Text = "Already have an account? Login...";
                btnlog.Text = lbltitle.Text = "Sign up";
                break;
            default:
                //For login
                gdsign.IsVisible = false;
                btnreg.Text = "Don't have an account? Signup...";
                btnlog.Text = lbltitle.Text = "Login";
                log = 1;
                break;
        }
    }

    private void btn_fly(object sender, EventArgs e)
    {
        //To open the fly page

    }



    private async void login_click(object sender, EventArgs e)
    {

        //login sequence
        piclogload.IsVisible = true;
        log = 1; qstat = 0;
        usid = txtuser.Text; pword = txtpword.Text;

        if (btnlog.Text == "Login")
        {
            qry = "login";
            q1 = "SELECT * FROM sqluser WHERE usid ='" +
                usid + "' AND pword = '" + pword + "';";
            q2 = "INSERT INTO tblog (usid, stat) " +
                    "VALUES ('" + usid + "', 'online');"
                    + " UPDATE tbluser SET stat = 'online' WHERE usid = '" + usid + "';";
            //To login
            try
            {
                qry = "s"; qp = "login";
                cmd1 = new SqlCommand(q1);
                cmd1.Connection = conn;
                cmd1.CommandType = CommandType.Text;
                da1 = new SqlDataAdapter(cmd1);
                da1.SelectCommand.CommandTimeout = 36000;
                dt1 = new DataTable();
                await Task.Run(() => sp_thread());
                if (qstat == 1)
                {
                    switch (pic)
                    {
                        case "def":
                            //To load default picture

                            break;
                        default:
                            //To load a profile picture
                            break;
                    }
                    /*gdlog.IsVisible = false; gdash.IsVisible = true;
                    lblfname.Text = fname;
                    lbldstitle.Text = "Welcome " + fname + "!";*/
                    mvcvalues.fname = fname; mvcvalues.pword = pword;
                    mvcvalues.uname = usid; mvcvalues.constr = constr; mvcvalues.lev = lev;
                    mvcvalues.bal = Convert.ToString(bal); mvcvalues.phone = phone;
                    dashboard = new Frmdash();
                    await Navigation.PushAsync(dashboard);

                }
                else
                {
                    await DisplayAlert("Login Error", "Invaid username or password please try again..." + msg, "Ok");
                }

                conn.Close();
            }
            catch (Exception ex)
            {

                await DisplayAlert("Connection Error", ex.Message, "Okay");
                conn.Close();
            }

            //sp_click(sender, e);
            //To show dashboard ui

        }
        else
        {
            if (txtpword.Text != txtcp.Text)
            {
                pword = "";
            }

            fname = txtlogfname.Text; phone = txtlogphone.Text;
            if (txtpword.Text == txtcp.Text)
            {
                pswd = 1;
            }
            else
            {
                pswd = 0;
            }
            //Signup sequence
            if (usid != "" || pword != ""
           || fname != "" || phone != "" || pswd > 0)
            {
                //Mail verification
                try
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                    ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
                    smtp = new SmtpClient();
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("youniqueums@gmail.com", "zals cnen bzqk nujj");
                    smtp.Host = "smtp.gmail.com";
                    mail = new MailMessage();
                    mail.From = new System.Net.Mail.MailAddress("youniqueums@gmail.com");
                    mail.To.Add(new MailAddress(usid));
                    mail.IsBodyHtml = true;
                    mail.Subject = "Younique Email Verification";
                    mail.Body = "Welcome to Younique Technology Management System. " +
                        "Your verification code is 9048. " +
                        "Do have a nice day!";

                    q1 = "SELECT * FROM sqluser WHERE usid ='" + usid + "'";
                    q2 = "INSERT INTO tbluser (usid, pword, fname, phone, stat, pic,lev) " +
                        "VALUES ('" + usid + "', '" + pword + "','" + fname + "','" + phone
                        + "','offline','def', 'user');"
                        + "INSERT INTO tblacct (usid, bal) VALUES('" + usid + "', 0);";
                    qry = "s"; qp = "signup";
                    cmd1 = new SqlCommand(q1);
                    cmd1.Connection = conn;
                    cmd1.CommandType = CommandType.Text;
                    da1 = new SqlDataAdapter(cmd1);
                    da1.SelectCommand.CommandTimeout = 36000;
                    dt1 = new DataTable();
                    await Task.Run(() => sp_thread());
                    if (qstat == 1)
                    {
                        //Login sequence starts here...
                        conn.Close();
                        logclear(sender, e); login_click(sender, e);
                    }
                    else
                    {
                        await DisplayAlert("Signup...",
                            "Email Address already exist or network issues, Server Response:" + msg,
                            "Ok");
                    }

                }
                catch (Exception exmsg)
                {

                    await DisplayAlert("Signup error", "Invalid email address, please try again..." + exmsg.Message.ToString(), "Okay");
                    txtuser.Text = "";
                }

            }
            else
            {
                await DisplayAlert("Sign up Error", "Please fill the form properly", "Okay");
            }
        }
        piclogload.IsVisible = false;
    }
}

