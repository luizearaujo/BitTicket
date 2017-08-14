using BitInformation;
using System;
using System.Windows.Forms;

namespace BitNotify
{
    public partial class Form1 : Form
    {
        private string Titulo => "BitNotify (Cotação FoxBit)";
        private string Message { get; set; }
        private double Ultimo { get; set; }
        private bool Atualiza { get; set; } = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            mynotifyicon.BalloonTipTitle = Titulo;
            mynotifyicon.BalloonTipText = Message;

            if (FormWindowState.Minimized == this.WindowState)
            {
              
                mynotifyicon.Visible = true;
                mynotifyicon.ShowBalloonTip(1000);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                mynotifyicon.Visible = false;
            }
        }

        private void mynotifyicon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void Get()
        {
            var repo = new Repositorio();
            var root = repo.Get();

            if (Ultimo != root.ticker_1h.exchanges.FOX.last)
            {
                Atualiza = true;
                mynotifyicon.BalloonTipTitle = Ultimo > root.ticker_1h.exchanges.FOX.last
                    ? $"{Titulo} - Baixa" : $"{Titulo} - Alta";
            }
            

            Message = $@"ÚLTIMO: {root.ticker_1h.exchanges.FOX.last.ToString("N")}
ANTERIOR: {Ultimo.ToString("N")}
MÍN: {root.ticker_1h.exchanges.FOX.low.ToString("N")}
MÁX: {root.ticker_1h.exchanges.FOX.high.ToString("N")}
USD COM: {root.rates.USDCBRL.ToString("N")}
USD TUR: {root.rates.USDTBRL.ToString("N")}";

            Ultimo = root.ticker_1h.exchanges.FOX.last;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Get();

            if (Atualiza)
            {
                mynotifyicon.BalloonTipText = Message;
                mynotifyicon.Visible = true;
                mynotifyicon.ShowBalloonTip(1000);
                Atualiza = false;
            }
            
        }

        private void btSair_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Get();

            mynotifyicon.BalloonTipTitle = Titulo;
            mynotifyicon.BalloonTipText = Message;

            mynotifyicon.Visible = true;
            mynotifyicon.ShowBalloonTip(1000);
            this.Hide();

            var segundos = Convert.ToInt32(txTempo.Text);
            timer1.Interval = 1000 * segundos;
            timer1.Start();
        }
    }
}
