using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPLV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            disableControls(this.Controls);
            osToolStripMenuItem.Text = SystemInfo.getOS();
            lANIPToolStripMenuItem.Text = SystemInfo.getLANIP();
            wANIPToolStripMenuItem.Text = SystemInfo.getWANIP();

            dataGridView1.ColumnCount = 3;
            dataGridView1.RowCount = 1000;
            dataGridView1.Columns[0].Name = "Time";
            dataGridView1.Columns[1].Name = "Preys";
            dataGridView1.Columns[2].Name = "Predators";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                MessageBox.Show(this, "File is opened!",
                    "Message",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Something went wrong!" + "\r\n" + ex.ToString(),
                    "Message",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                if (sfd.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                MessageBox.Show(this, "File is saved!",
                    "Message",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Something went wrong!" + "\r\n" + ex.ToString(),
                    "Message",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        public void clearControls(Control.ControlCollection ctrlCollection)
        {
            foreach (Control ctrl in ctrlCollection)
            {
                if (ctrl is TextBoxBase)
                {
                    ctrl.Text = String.Empty;
                }
                else
                {
                    clearControls(ctrl.Controls);
                }
            }
        }

        public void enableControls(Control.ControlCollection ctrlCollection)
        {
            foreach (Control ctrl in ctrlCollection)
            {
                if (ctrl is TextBoxBase)
                {
                    ctrl.Enabled = true;
                }
                else if (ctrl is ButtonBase)
                {
                    ctrl.Enabled = true;
                }
                else
                {
                    enableControls(ctrl.Controls);
                }
            }
        }

        public void disableControls(Control.ControlCollection ctrlCollection)
        {
            foreach (Control ctrl in ctrlCollection)
            {
                if (ctrl is TextBoxBase)
                {
                    ctrl.Enabled = false;
                }
                else if (ctrl is ButtonBase)
                {
                    ctrl.Enabled = false;
                }
                else
                {
                    disableControls(ctrl.Controls);
                }
            }
        }

        private void unlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enableControls(this.Controls);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearControls(this.Controls);
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Predator-Prey Simulation\r\n"
                        + "Artur Zhadan\r\n"
                        + "2020",
                        "Message",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dateToolStripMenuItem.Text = SystemInfo.getDate();
            timeToolStripMenuItem.Text = SystemInfo.getTime();
            stopwatchToolStripMenuItem.Text = SystemInfo.getStopwatch();
        }

        public static int cnt;

        EcoSystem ecoSystem;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                dataGridView1.RowCount = 1000;
                dataGridView1.Refresh();

                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();

                cnt = 0;

                ecoSystem = new EcoSystem(Convert.ToDouble(textBox1.Text),
                    Convert.ToDouble(textBox2.Text),
                    Convert.ToDouble(textBox3.Text),
                    Convert.ToDouble(textBox4.Text),
                    Convert.ToDouble(textBox5.Text),
                    Convert.ToDouble(textBox6.Text),
                    Convert.ToDouble(textBox7.Text));

                timer2.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Something went wrong!" + "\r\n" + ex.ToString(),
                    "Message",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            dotPrey.Clear();
            dotPredators.Clear();
            tempPrey = 0;
            tempPredators = 0;
            Graphics g = panel1.CreateGraphics();
            g.Clear(Color.White);
        }

        Random random = new Random();

        public static List<Dot> dotPrey = new List<Dot>();

        public static List<Dot> dotPredators = new List<Dot>();

        public static int tempPrey = 0;

        public static int tempPredators = 0;

        private void timer2_Tick(object sender, EventArgs e)
        {
            try 
            {
                ecoSystem.doEcoSystem();

                Graphics g = panel1.CreateGraphics();

                if ((int)Math.Round(ecoSystem.getPrey()) > tempPrey)
                {
                    for (int i = tempPrey; i < (int)Math.Round(ecoSystem.getPrey()); i++)
                    {
                        dotPrey.Add(new Dot(g)
                        {
                            X = random.Next(0, panel1.Width),
                            Y = random.Next(0, panel1.Height),
                            Widthsize = random.Next(8, 10),
                            Heightsize = random.Next(8, 10)
                        });
                    }
                }
                else if (tempPrey > (int)Math.Round(ecoSystem.getPrey()))
                {
                    for (int i = 0; i < Math.Abs((int)Math.Round(ecoSystem.getPrey()) - tempPrey); i++)
                    {
                        if (i >= 0)
                        {
                            dotPrey.ElementAt(i).Clear();
                            dotPrey.RemoveAt(i);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if ((int)Math.Round(ecoSystem.getPredators()) > tempPredators)
                {
                    for (int i = tempPredators; i < (int)Math.Round(ecoSystem.getPredators()); i++)
                    {
                        dotPredators.Add(new Dot(g)
                        {
                            X = random.Next(0, panel1.Width),
                            Y = random.Next(0, panel1.Height),
                            Widthsize = random.Next(8, 10),
                            Heightsize = random.Next(8, 10)
                        });
                    }
                }
                else if (tempPredators > (int)Math.Round(ecoSystem.getPredators()))
                {
                    for (int i = 0; i < Math.Abs((int)Math.Round(ecoSystem.getPredators()) - tempPredators); i++)
                    {
                        if (i >= 0)
                        {
                            dotPredators.ElementAt(i).Clear();
                            dotPredators.RemoveAt(i);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                dotPrey.ForEach(dot => { dot.Plot(Color.Green); });
                dotPredators.ForEach(dot => { dot.Plot(Color.Red); });

                tempPrey = (int)Math.Round(ecoSystem.getPrey());
                tempPredators = (int)Math.Round(ecoSystem.getPredators());

                dataGridView1[0, cnt].Value = ecoSystem.getTime().ToString("F1");
                dataGridView1[1, cnt].Value = ecoSystem.getPrey().ToString("F3");
                dataGridView1[2, cnt].Value = ecoSystem.getPredators().ToString("F3");

                chart1.Series[0].Points.AddXY(ecoSystem.getTime(), ecoSystem.getPrey());
                chart1.Series[1].Points.AddXY(ecoSystem.getTime(), ecoSystem.getPredators());

                cnt++;
            }
            catch (Exception ex)
            {
                timer2.Enabled = false;
                MessageBox.Show(this, "Something went wrong!" + "\r\n" + ex.ToString(),
                    "Message",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }
}
