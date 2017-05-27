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
    }
}
