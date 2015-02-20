using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Iris
{
    public partial class AddUserForm : Form
    {
        public AddUserForm()
        {
            InitializeComponent();
        }
        string imagename;
        SqlDataAdapter irisadap;
        DataSet dset;
        private void insertrecord()
        {
            try
            {
                if(imagename != "")
                {
                    FileStream fs;
                    fs = new FileStream(@imagename, FileMode.Open, FileAccess.Read);
                    byte[] picbyte = new byte[fs.Length];
                    fs.Read(picbyte, 0, System.Convert.ToInt32(fs.Length));
                    fs.Close();

                    string connstr = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Anwar\Documents\Iris.mdf;Integrated Security=True;Connect Timeout=30";
                    SqlConnection conn = new SqlConnection( connstr);
                    conn.Open();
                    string query;
                    query = "INSERT INTO PERSON (PersonFname, PersonLname, DoB, CNIC, Age, Sex, Photo) values(" + fnametextBox.Text + ", " + lnametextBox.Text + ", " + DoBdateTimePicker.Text + ", " + CNICtextBox.Text + ", " + agetextBox.Text + ", " + sexcomboBox.Text + ", "+"  @Photo)";
                    SqlParameter picparameter = new SqlParameter();
                    picparameter.SqlDbType = SqlDbType.Image;
                    picparameter.ParameterName = "Photo";
                    picparameter.Value = picbyte;
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add(picparameter);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Added");
                    cmd.Dispose();
                    conn.Close();
                    conn.Dispose();
                    connection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void connection()
        {
            try
            {
                string connstr = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Anwar\Documents\Iris.mdf;Integrated Security=True;Connect Timeout=30";
                SqlConnection conn = new SqlConnection(connstr);
                conn.Open();

                irisadap = new SqlDataAdapter();
                irisadap.SelectCommand = new SqlCommand("SELECT * FROM PERSON", conn);
                dset = new DataSet("dset");
                irisadap.Fill(dset);
                DataTable dtable;
                dtable = dset.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {

        }

        private void browsebutton_Click(object sender, EventArgs e)
        {
            //OpenFileDialog op = new OpenFileDialog();
            //op.Filter = "image files|*.jpg; *.png; *.gif";
            //DialogResult dr = op.ShowDialog();
            //if (dr == DialogResult.Cancel)
            //    return;

            //pictureBox1.Image = Image.FromFile(op.FileName);
            //imagepathtextBox.Text = op.FileName;

            try
            {
                FileDialog fd = new OpenFileDialog();
                fd.Filter = "Image File (*.jpg; *.png; *.gif) | *.jpg;*.bmp;*.gif";

                if(fd.ShowDialog() == DialogResult.OK)
                {
                    imagename = fd.FileName;
                    Bitmap newimg = Bitmap (imagename);
                    pictureBox1.Image = (Image)newimg;
                }
                fd = null;
            }

            catch (System.ArgumentException ae)
            {
                imagename = " ";

                MessageBox.Show(ae.Message.ToString());
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

private Bitmap Bitmap(string imagename)
{
 	throw new NotImplementedException();
} 
        void savebutton_Click(object sender, EventArgs e)
        {
            insertrecord();

            //SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Anwar\Documents\Iris.mdf;Integrated Security=True;Connect Timeout=30");
            //MemoryStream ms = new MemoryStream();
            //pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //byte[] pic_arr = new byte[ms.Length];
            //ms.Position = 0;
            //ms.Read(pic_arr, 0, pic_arr.Length);

            
            
            //SqlCommand cmd = new SqlCommand ("INSERT INTO PERSON (PersonFname, PersonLname, DoB, CNIC, Age, Sex, Photo) values(" + fnametextBox.Text + ", " + lnametextBox.Text + ", " + DoBdateTimePicker.Text + ", " + CNICtextBox.Text + ", " + agetextBox.Text + ", " + sexcomboBox.Text + ", "+"  @Photo)");
            //cmd.Parameters.AddWithValue("@Photo", pic_arr);
            //cn.Open();
            //try
            //{
            //    int res = cmd.ExecuteNonQuery();
            //    if (res > 0)
            //    {
            //        MessageBox.Show("Image Stored");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    cn.Close();
            //}


        }
    }
}
