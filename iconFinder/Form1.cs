using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Net;

namespace iconFinder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap bitmap;
        string ability;
        string hero;

        public string fixStuff(string x, TextBox y)
        {
            x = y.Text;
            x = x.Replace('.'.ToString(), "")
                 .Replace(':'.ToString(), "")
                 .Replace('!'.ToString(), "")
                 .Replace('ú'.ToString(), "u")
                 .Replace(' '.ToString(), "-");
            x = x.ToLower();
            return x;
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbHero.Text != "")
                {
                    bool done = false;
                    string link = "";
                    hero = fixStuff("", tbHero);


                    if (rbAbility.Checked)
                    {
                        if (tbAbility.Text != "")
                        {
                            ability = fixStuff("", tbAbility);
                            link = String.Format("https://blzgdapipro-a.akamaihd.net/hero/{0}/ability-{1}/icon-ability.png", hero, ability);
                            done = true;
                        }
                        else
                        {
                            MessageBox.Show("Please enter text");
                        }
                    }
                    else
                    {
                        link = String.Format("https://d1u1mce87gyfbn.cloudfront.net/hero/{0}/icon-portrait.png", hero);
                        done = true;
                    }

                    if (done)
                    {
                        btnCopy.Enabled = true;
                        btnSave.Enabled = true;

                        // Open Webclient and put the image in a bitmap
                        WebClient webClient = new WebClient();
                        Stream stream = webClient.OpenRead(link);
                        bitmap = new Bitmap(stream);

                        // Display the image
                        pbImage.Image = bitmap;
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(bitmap);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "PNG (*.png)|*.png|All files (*.*)|*.*";
            if (rbAbility.Checked)
                dialog.FileName = ability;
            else
                dialog.FileName = hero;
            if (dialog.ShowDialog() == DialogResult.OK)
                bitmap.Save(dialog.FileName);
        }

        private void rbPortrait_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPortrait.Checked)
            {
                tbAbility.ReadOnly = true;
                tbAbility.Text = "";
            }
            else
            {
                tbAbility.ReadOnly = false;
            }
                
        }
    }
}
