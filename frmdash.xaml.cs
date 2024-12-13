using Microsoft.Data.SqlClient;
using Microsoft.Maui.Platform;
using System.Data;
using System.Formats.Asn1;

namespace Younique_Pro_Assistant
{
    public partial class Frmdash : ContentPage
    {
        public string fname, uname, pword, bal, constr, phone;
        SqlDataAdapter da1;
        DataRowView dtr1;
        DataSet ds1, ds2; FrmSchool schapp;
        DataTable dt1, dt2, dt3, dt4;
        SqlCommand cmd1, cmd2;
        SqlConnection conn = new SqlConnection();
        SqlDataReader qr1, qr2, qr3, qr4, qr5;
        List<string> dets, txt, img = new List<string> { },picklist = new List<string> {};
        public string pic, lev, busid, projid, col1, col2, col3, col4, col5, col6, col7,
            businfo, bustit, buscat, usmail, ustat, usbus, uslev;

        private async void btnlaunch_clicked(object sender, EventArgs e)
        {
            //To launch the business apps
            schapp = new FrmSchool();
            switch (picbus.SelectedItem)
            {
                default:
                    await Navigation.PushAsync(schapp);
                    break;
            }
        }

        int buswitch = 0, idpost = 0;

        private void txtbn_Focused(object sender, FocusEventArgs e)
        {
            try
            {
                txtbusname.Text = dt1.Rows[Convert.ToInt32((sender as Entry).AutomationId)][col1].ToString();
                txtbusinfo.Text = dt1.Rows[Convert.ToInt32((sender as Entry).AutomationId)][col4].ToString();
                picbus.SelectedItem = dt1.Rows[Convert.ToInt32((sender as Entry).AutomationId)][col2].ToString();
                projid = dt1.Rows[Convert.ToInt32((sender as Entry).AutomationId)][6].ToString();
                txtbusname.Focus();
                //DisplayAlert("Getting projid", projid, "Okay");
            }
            catch (Exception ex)
            {
                txtbusname.Focus(); txtbusname.Text = txtbusinfo.Text = "";
                picbus.SelectedIndex = 0; projid = "0";
                DisplayAlert("Datagrid Error", "Please select a valid business.", "Okay");

            }
        }

       

        private async void msg_select(object sender, EventArgs e)
        {
            //When messages/notifications are clicked
            lblmsg.Text = "";
        }
        
        public System.Windows.Input.ICommand msgtable
        {
            get;
            set;
            
        }

        private async void btnbus_Click(object sender, EventArgs e)
        {
            buswitch = 1;
            btnbus_Clicked(sender, e);
        }

        private async void btnus_Click(object sender, EventArgs e)
        {
            buswitch = 2;
            btnbus_Clicked(sender, e);
        }


        private async void btnbus_Clicked(object sender, EventArgs e)
        {
            hi_form(sender, e);
            //When business or users button is clicked
            switch (buswitch)
            {
                case 1:
                    lblmenu.Text = "Business";
                    q1 = "SELECT * FROM tblbus";
                    idpost = 4;
                    col1 = "Business Name"; col2 = "Business Type"; col3 = "Date Created"; col4 = "descr";
                    lblcol1.Text = "Business Name"; lblcol2.Text = "Business Category"; lblcol3.Text = "Date Created";
                    qstat = 0;
                    qry = "s";
                    cmd1 = new SqlCommand(q1);
                    cmd1.Connection = conn;
                    cmd1.CommandType = CommandType.Text;
                    da1 = new SqlDataAdapter(cmd1);
                    da1.SelectCommand.CommandTimeout = 36000;
                    dt1 = new DataTable();
                    await Task.Run(() => sp_thread());
                    if (dt1.Rows.Count > 0 && qstat == 1)
                    {
                        int rwdt = 0;
                        //To populate the message grid
                        tblsec.Clear(); picbus.ItemsSource = null;
                        picklist = new List<string>();
                        picklist.Add("Select Business Category");
                        foreach (DataRow rws in dt1.Rows)
                        {
                            picklist.Add(dt1.Rows[rwdt]["bus"].ToString());
                            rwdt += 1;
                        }
                        picbus.ItemsSource = picklist;
                        picbus.SelectedIndex = 0;
                    }
                    conn.Close();
                    q1 = "SELECT * FROM sqlusbus " +
                  " WHERE usid = '" + uname + "'";
                    break;
                case 2:
                    lblmenu.Text = "Users List"; idpost = 1;
                    col1 = "Staff Name"; col2 = "Business"; col3 = "Category"; col4 = "Level";
                    lblcol1.Text = "Staff Name"; lblcol2.Text = "Business"; lblcol3.Text = "Level";
                    tblsec.Clear(); picbus.ItemsSource = null;
                    picklist = new List<string>();
                    picklist.Add("Select User Level");
                    picklist.Add("Select Admin");
                    picklist.Add("Select User");
                    picbus.ItemsSource = picklist;
                    picbus.SelectedIndex = 0;
                    if (lev == "user")
                    {
                        q1 = "SELECT * from sqlusbuslist WHERE uname = '" + uname + "'" +
                               " ORDER BY datreg DESC";
                    }
                    else
                    {
                        q1 = "SELECT * from sqlusbuslist";
                    }
                    break;
            }
            qstat = 0;
            qry = "s";
            cmd1 = new SqlCommand(q1);
            cmd1.Connection = conn;
            cmd1.CommandType = CommandType.Text;
            da1 = new SqlDataAdapter(cmd1);
            da1.SelectCommand.CommandTimeout = 36000;
            dt1 = new DataTable();
            await Task.Run(() => sp_thread());
            if (dt1.Rows.Count > 0 && qstat == 1)
            {
              
                int rowcheck = dt1.Rows.Count;
                int rwdt = 0;
                do
                {
                    switch (rwdt)
                    {
                        case 0:
                            txtbn1.Text = dt1.Rows[rwdt][col1].ToString();
                            txtbc1.Text = dt1.Rows[rwdt][col2].ToString();
                            txtbndat1.Text = dt1.Rows[rwdt][col3].ToString();
                            break;
                        case 1:
                            txtbn2.Text = dt1.Rows[rwdt][col1].ToString();
                            txtbc2.Text = dt1.Rows[rwdt][col2].ToString();
                            txtbndat2.Text = dt1.Rows[rwdt][col3].ToString();
                            break;
                        case 2:
                            txtbn3.Text = dt1.Rows[rwdt][col1].ToString();
                            txtbc3.Text = dt1.Rows[rwdt][col2].ToString();
                            txtbndat3.Text = dt1.Rows[rwdt][col3].ToString();
                            break;
                        case 3:
                            txtbn4.Text = dt1.Rows[rwdt][col1].ToString();
                            txtbc4.Text = dt1.Rows[rwdt][col2].ToString();
                            txtbndat4.Text = dt1.Rows[rwdt][col3].ToString();
                            break;
                        case 4:
                            txtbn5.Text = dt1.Rows[rwdt][col1].ToString();
                            txtbc5.Text = dt1.Rows[rwdt][col2].ToString();
                            txtbndat5.Text = dt1.Rows[rwdt][col3].ToString();
                            break;
                        case 5:
                            txtbn6.Text = dt1.Rows[rwdt][col1].ToString();
                            txtbc6.Text = dt1.Rows[rwdt][col2].ToString();
                            txtbndat6.Text = dt1.Rows[rwdt][col3].ToString();
                            break;
                        case 6:
                            txtbn7.Text = dt1.Rows[rwdt][col1].ToString();
                            txtbc7.Text = dt1.Rows[rwdt][col2].ToString();
                            txtbndat7.Text = dt1.Rows[rwdt][col3].ToString();
                            break;
                        case 7:
                            txtbn8.Text = dt1.Rows[rwdt][col1].ToString();
                            txtbc8.Text = dt1.Rows[rwdt][col2].ToString();
                            txtbndat8.Text = dt1.Rows[rwdt][col3].ToString();
                            break;
                        case 8:
                            txtbn9.Text = dt1.Rows[rwdt][col1].ToString();
                            txtbc9.Text = dt1.Rows[rwdt][col2].ToString();
                            txtbndat9.Text = dt1.Rows[rwdt][col3].ToString();
                            break;
                        case 9:
                            txtbn10.Text = dt1.Rows[rwdt][col1].ToString();
                            txtbc10.Text = dt1.Rows[rwdt][col2].ToString();
                            txtbndat10.Text = dt1.Rows[rwdt][col3].ToString();
                            break;
                        default:
                            break;
                    }
                    rwdt += 1;
                } while (rwdt < rowcheck);
                
                //To populate the datagrid for businesses
                foreach (DataRow rws in dt1.Rows)
                {
                   
                }

            }
            gdbus.IsVisible = true;
            btn_fly(sender, e);
            conn.Close();
            //await DisplayAlert("Grid Height",Convert.ToString(gdbusgrid.Height),"Okay");
            txtbusinfo.Text = txtbusname.Text = "";
        }

        private void lstmsgpics_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private async void BtnBusAdd_Clicked(object sender, EventArgs e)
        {
            //To add a business
            piclogload.IsVisible = true;
            BtnBusAdd.IsEnabled = false;
             lblmsg.IsVisible = true;
            if (txtbusinfo.Text != "" && txtbusname.Text != "" && picbus.SelectedIndex > 0)
            {
                switch (buswitch)
                {
                    case 1:
                        lblmsg.Text = "Add a Business";
                        businfo = txtbusinfo.Text; bustit = txtbusname.Text;
                        buscat = picbus.SelectedIndex.ToString();
                        q1 = "SELECT * FROM tblproj WHERE tit = '" + bustit + "'";
                        q2 = "INSERT INTO tblproj (busid, tit, descr, usid, lev)" +
                            " VALUES (" + buscat + ",'" + bustit + "','" + businfo + "','" + uname
                            + "','" + lev + "');";
                        break;
                    case 2:
                        break;
                    default:

                        break;
                }
                //Update process
                qstat = 0; qry = "s"; msg = "";
                cmd1 = new SqlCommand(q1);
                cmd1.Connection = conn;
                cmd1.CommandType = CommandType.Text;
                da1 = new SqlDataAdapter(cmd1);
                da1.SelectCommand.CommandTimeout = 36000;
                dt1 = new DataTable();
                await DisplayAlert("Query", q1, "Okay");
                await Task.Run(() => sp_thread());
                if (qstat == 0)
                {
                    conn.Close();
                    qstat = 0; qry = "i"; msg = "";
                    cmd1 = new SqlCommand(q2);
                    await DisplayAlert("Adding a business", q2, "Okay");
                    cmd1.Connection = conn;
                    cmd1.CommandType = CommandType.Text;
                    da1 = new SqlDataAdapter(cmd1);
                    da1.SelectCommand.CommandTimeout = 36000;
                    dt1 = new DataTable();
                    await Task.Run(() => sp_thread());
                    conn.Close();
                    msg = col1 + " added successfully!";
                    //To reload the data grid
                    btnbus_Clicked(sender, e);
                }
                else
                {
                    conn.Close();
                    msg = col1 + " already exists...";
                }
            }
            else
            {
                msg = "Please put the necessary details.";
            }
            lblmsg.Text = msg;
            await DisplayAlert("Adding a " + col1, msg, "Okay");
            BtnBusAdd.IsEnabled = true;
            piclogload.IsVisible = false;
        }


        private async void BtnBusUpdate(object sender, EventArgs e)
        {
            //To add a business
            piclogload.IsVisible = true;
            BtnBusUpdt.IsEnabled = false;
            lblmsg.Text = "Updating business"; lblmsg.IsVisible = true;
            if (txtbusinfo.Text != "" && txtbusname.Text != "" && picbus.SelectedIndex > 0 && projid != "0")
            {
                businfo = txtbusinfo.Text; bustit = txtbusname.Text;
                buscat = picbus.SelectedIndex.ToString();
                //Update process
                qstat = 0; qry = "u"; msg = "";
                q2 = "UPDATE tblproj SET tit = '" + bustit + "'," +
                      " descr = '" + businfo + "' WHERE projid = " + projid + ";";
                cmd1 = new SqlCommand(q2);
                cmd1.Connection = conn;
                cmd1.CommandType = CommandType.Text;
                da1 = new SqlDataAdapter(cmd1);
                da1.SelectCommand.CommandTimeout = 36000;
                dt1 = new DataTable();
                await Task.Run(() => sp_thread());
                if (qstat == 1)
                {
                    conn.Close();
                    msg = "Business updated successfully!";
                    //To reload the data grid
                    btnbus_Clicked(sender, e);
                }
                else
                {
                    conn.Close();
                    msg = "Bad network, please try again...";
                }
            }
            lblmsg.Text = msg;
            await DisplayAlert("Business Update", msg, "Okay");
            BtnBusUpdt.IsEnabled = true;
            piclogload.IsVisible = false;
        }


        private async void btnotf_Clicked(object sender, EventArgs e)
        {
            //When notifications button is clicked
            /*qstat = 0;
            q1 = "SELECT * FROM sqlmsg " +
                " WHERE recid = '" + uname + "' ORDER BY stat ASC;";
            qry = "s";
            cmd1 = new SqlCommand(q1);
            cmd1.Connection = conn;
            cmd1.CommandType = CommandType.Text;
            da1 = new SqlDataAdapter(cmd1);
            da1.SelectCommand.CommandTimeout = 36000;
            dt1 = new DataTable();
            await Task.Run(() => sp_thread());
            if (dt1.Rows.Count > 0 && qstat == 1)
            {

                int rwdt = 0;
                //To populate the message grid
                lblmenu.Text = "Notification / Messages";
                tblsec.Clear();
                lstfname.ItemsSource = dt1.AsEnumerable().Select(x => x["fname"].ToString()).ToList();
                lstfname.RowHeight = 20;
                lstdet.ItemsSource = dt1.AsEnumerable().Select(x => x["msg"].ToString()).ToList();
                lstdet.RowHeight = 20;
                foreach (DataRow rws in dt1.Rows)
                {
                    tblsec.Add(
                        new ImageCell
                        {
                            Command = msgtable,
                            Text = dt1.Rows[rwdt]["fname"].ToString() + " - " + dt1.Rows[rwdt]["subj"].ToString(),
                            Detail = dt1.Rows[rwdt]["msg"].ToString() ,
                            ImageSource = dt1.Rows[rwdt]["msgtpe"].ToString() + ".png"
                        });
                    rwdt += 1;
                }
                foreach (DataRow rws in dt1.Rows)
                {
                    
                    rwdt += 1;
                }
                try
                {
                    tblmsg.Root[0][0].Tapped += msg_select;
                }
                catch (Exception)
                {

                }
                rwdt = 0;
                conn.Close();
                qstat = 0;
                qry = "s";
                q1 = "SELECT fname FROM tbluser WHERE usid != '" + uname + "'";
                cmd1 = new SqlCommand(q1);
                cmd1.Connection = conn;
                cmd1.CommandType = CommandType.Text;
                da1 = new SqlDataAdapter(cmd1);
                da1.SelectCommand.CommandTimeout = 36000;
                dt1 = new DataTable();
                await Task.Run(() => sp_thread());
                var picklist = new List<string>();
                picklist.Add("Select User");
                foreach (DataRow rws in dt1.Rows)
                {
                    picklist.Add(dt1.Rows[rwdt]["fname"].ToString());
                    rwdt += 1;
                }
                try
                {
                    pickusers.ItemsSource = null;
                }
                catch (Exception)
                {

                }
                pickusers.ItemsSource = picklist;
                pickusers.SelectedItem = "Select User";
                hi_form(sender, e);
                gdmsg.IsVisible = true;
                btn_fly(sender, e);

            }
            else
            {
                await DisplayAlert("Loading Messages...", "You have no messages / notifications", "Okay");
            }
            try
            {

            }
            catch (Exception)
            {
                conn.Close();
            }
            */
            await DisplayAlert("Messages", "Unavailable for now", "Okay");
        }

        int pswd = 0;
        public string notf, sendr, det;

        private async void btnprofpass_Clicked(object sender, EventArgs e)
        {
            //To update password
            if (txtnp.Text == txtcp.Text)
            {
                pswd = 1;
            }
            else { pswd = 0; }
            if (txtoldp.Text == pword)
            {
                piclogload.IsVisible = true;
                if (txtnp.Text.Length > 0 || pswd > 0)
                {
                    qstat = 0;
                    q1 = "UPDATE tbluser SET pword = '" + txtnp.Text + "'" +
                        " WHERE usid = '" + uname + "';";
                    qry = "u";
                    cmd1 = new SqlCommand(q1);
                    cmd1.Connection = conn;
                    cmd1.CommandType = CommandType.Text;
                    da1 = new SqlDataAdapter(cmd1);
                    da1.SelectCommand.CommandTimeout = 36000;
                    dt1 = new DataTable();
                    await Task.Run(() => sp_thread());
                    if (qstat == 1)
                    {
                        //Update worked
                        await DisplayAlert("Updating Password", "Password Upate Successful!", "Okay");
                        pword = txtnp.Text;
                        conn.Close();

                    }
                    else
                    {
                        txtnp.Text = txtoldp.Text = txtcp.Text = "";
                        await DisplayAlert("Updating Profile", "Unable to update password, please try again!", "Okay");
                    }
                }
                piclogload.IsVisible = false;
            }
            else
            {
                await DisplayAlert("Updating Password", "Password does not match! Please try again!", "Okay");
                txtoldp.Text = "";
            }
        }

        private async void btnupdateprof_Clicked(object sender, EventArgs e)
        {
            piclogload.IsVisible = true;
            //To update profile
            if (fname.Length > 0 || /*&&*/ uname.Contains("@"))
            {
                qstat = 0;
                q2 = "UPDATE tbluser SET usid = '" + txtmail.Text + "', fname ='" + txtfname.Text + "', phone = '" + txtphone.Text +
                    "' WHERE usid = '" + uname + "';";
                qry = "u";
                cmd1 = new SqlCommand(q2);
                cmd1.Connection = conn;
                cmd1.CommandType = CommandType.Text;
                da1 = new SqlDataAdapter(cmd1);
                da1.SelectCommand.CommandTimeout = 36000;
                dt1 = new DataTable();
                await Task.Run(() => sp_thread());
                if (qstat == 1)
                {
                    //Update worked
                    await DisplayAlert("Updating Profile", "Profile Update Successful!", "Okay");
                    uname = txtmail.Text; fname = txtfname.Text; phone = txtphone.Text;
                    lbldstitle.Text = "Welcome " + fname + "!";
                    conn.Close();

                }
                else
                {
                    await DisplayAlert("Updating Profile", "Unable to update profile, please try again!", "Okay");
                }
            }
            piclogload.IsVisible = false;
        }

        string msg = "", sql, q1, q2, q3, q4, qry = "", tp = "";
        int qstat = 0;

        public Frmdash()
        {
            InitializeComponent();
            //To give variables default values
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
                            qstat = 1; msg = "active";
                        }
                        break;
                    default:
                       
                        try
                        {
                            conn.Open();
                            qr1 = cmd1.ExecuteReader();
                            
                                qstat = 1; msg = "active";
                        }
                        catch (Exception ex)
                        {

                            msg = ex.Message;
                            await DisplayAlert("Query Message", msg, "Okay");
                        }
                       
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
                    await DisplayAlert("Query Message", msg, "Okay");
                }
                else
                {
                    await DisplayAlert("Processing Info","Network error, please try again...", "Okay");
                    msg = "";
                }
            }

        }


        private void ContentPage_Loaded(object sender, EventArgs e)
        {
            //On load of the page
            var theme = Application.Current.RequestedTheme;
            if (theme is AppTheme.Light)
                lightThemeRadioButton.IsChecked = true;
            else if (theme is AppTheme.Dark)
                darkThemeRadioButton.IsChecked = true;
            //Load event
            piclogload.IsVisible = false;
            fname = mvcvalues.fname; uname = mvcvalues.uname;
            pword = mvcvalues.pword; bal = mvcvalues.bal;
            constr = mvcvalues.constr; phone = mvcvalues.phone;
            lev = mvcvalues.lev;
            lbldstitle.Text = "Welcome " + fname + "!";
            conn.ConnectionString = constr;
            btnprof_Clicked(sender, e);
            btn_fly(sender, e);
            conn.Close();
           
        }

        private void lightThemeRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Application.Current.UserAppTheme = AppTheme.Light;
        }

        private void darkThemeRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
        }
        private async void btnlog_Clicked(object sender, EventArgs e)
        {
            //To logout from the database
            try
            {
                conn.Close();
            }
            catch (Exception)
            {

            }
            piclogload.IsVisible = true;
            qstat = 0;
            q1 = "INSERT INTO tblog (usid, stat) " +
                  "VALUES ('" + uname + "', 'offline');"
                  + " UPDATE tbluser SET stat = 'offline' WHERE usid = '" + uname + "';";
            qry = "i";
                 cmd1 = new SqlCommand(q1);
            cmd1.Connection = conn;
            cmd1.CommandType = CommandType.Text;
            da1 = new SqlDataAdapter(cmd1);
            da1.SelectCommand.CommandTimeout = 36000;
            dt1 = new DataTable();
            await Task.Run(() => sp_thread());
            if (qstat == 1)
            {
                try
                {
                    conn.Close();
                   
                }
                catch (Exception)
                {

                }
                //Logout sequence back to home
                await DisplayAlert("Logging out...", "Logout Successful!", "Goodbye!");
                MainPage newmain = new MainPage();
                await Navigation.PushAsync(newmain);
            }
            piclogload.IsVisible = false;
        }

        private void btn_fly(object sender, EventArgs e)
        {
            if (colfly.Width.Value > colbtn.Width.Value)
            {
                colfly.Width = 0; btnfly.Text = "≡";
            }
            else
            {
                colfly.Width = 200; btnfly.Text = "⟸";
            }
        }
        private void hi_form(object sender, EventArgs e)
        {
            //To hide all forms elements
            gdprof.IsVisible = false;
            gdmsg.IsVisible = false;
            gdbus.IsVisible = false;
        }
        private void btnprof_Clicked(object sender, EventArgs e)
        {
            //When profile is clicked
            hi_form(sender, e);
            txtnp.Text = txtoldp.Text = txtcp.Text = "";
            txtfname.Text = fname;
            txtmail.Text = uname;
            txtphone.Text = phone;
            gdprof.IsVisible = true;
            lblmenu.Text = "Profile";
            btn_fly(sender, e);
        }
    }
}
