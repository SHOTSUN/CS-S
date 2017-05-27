using System;
using System.IO;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            


            int port = 8888;

            String IP = "127.0.0.1";

            Client client = new Client(port, IP);

            String menu = comboBox1.SelectedIndex.ToString();
            String addrres = Path.GetFullPath(textBox2.Text);
            String fileName = textBox3.Text;
            String text = textBox4.Text;


            client.send(menu + "|"  + addrres + "|" + fileName + "|" + text);


            textBox1.Text += Environment.NewLine + client.receive();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text += Environment.NewLine + "\t\t   СLIENT STARTED\n";
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();

            
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    label1.Visible = true;
                    textBox2.Visible = true;

                    label2.Visible = false;
                    textBox3.Visible = false;

                    label3.Visible = false;
                    textBox4.Visible = false;
                    break;

                case 1:
                    label1.Visible = true;
                    textBox2.Visible = true;

                    label2.Visible = true;
                    textBox3.Visible = true;

                    label3.Visible = false;
                    textBox4.Visible = false;
                    break;

                case 2:
                    label1.Visible = true;
                    textBox2.Visible = true;

                    label2.Visible = true;
                    textBox3.Visible = true;

                    label3.Visible = true;
                    label3.AutoSize = true;
                    label3.Text = "Текст, записываемый в файл";
                    textBox4.Text = "Текст";
                    textBox4.Visible = true;
                    break;

                case 3:
                    label1.Visible = true;
                    textBox2.Visible = true;

                    label2.Visible = true;
                    textBox3.Visible = true;

                    label3.Visible = false;
                    textBox4.Visible = false;
                    break;

                case 4:
                    label1.Visible = true;
                    textBox2.Visible = true;

                    label2.Visible = true;
                    textBox3.Visible = true;

                    label3.Visible = false;
                    textBox4.Visible = false;
                    break;

                case 5:
                    label1.Visible = true;
                    textBox2.Visible = true;

                    label2.Visible = true;
                    textBox3.Visible = true;

                    label3.Visible = false;
                    textBox4.Visible = false;
                    break;

                case 6:
                    label1.Visible = true;
                    textBox2.Visible = true;

                    label2.Visible = true;
                    textBox3.Visible = true;

                    label3.Visible = true;
                    label3.AutoSize = true;
                    label3.Text = "Дата создания файла (ДД.ММ.ГГГГ)";
                    textBox4.Text = "25.05.2017";
                    textBox4.Visible = true;
                    break;

                case 7:
                    label1.Visible = true;
                    textBox2.Visible = true;

                    label2.Visible = true;
                    textBox3.Visible = true;

                    label3.Visible = false;
                    textBox4.Visible = false;
                    break;

                case 8:
                    label1.Visible = true;
                    textBox2.Visible = true;

                    label2.Visible = true;
                    textBox3.Visible = true;

                    label3.Visible = true;
                    label3.AutoSize = true;
                    label3.Text = "Дата изменения файла (ДД.ММ.ГГГГ)";
                    textBox4.Text = "25.05.2017";
                    textBox4.Visible = true;
                    break;

                case 9:
                    label1.Visible = true;
                    textBox2.Visible = true;

                    label2.Visible = false;
                    textBox3.Visible = false;

                    label3.Visible = false;
                    textBox4.Visible = false;
                    break;

                case 10:
                    label1.Visible = true;
                    textBox2.Visible = true;

                    label2.Visible = false;
                    textBox3.Visible = false;

                    label3.Visible = false;
                    textBox4.Visible = false;
                    break;

                case 11:
                    label1.Visible = true;
                    textBox2.Visible = true;

                    label2.Visible = false;
                    textBox3.Visible = false;

                    label3.Visible = false;
                    textBox4.Visible = false;
                    break;
            }
        }
    }
}
