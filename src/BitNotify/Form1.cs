using BitInformation;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace BitNotify
{
    public partial class Form1 : Form
    {
        private string Titulo { get; set; }
        private string Message { get; set; }
        private double Ultimo { get; set; }
        private bool Atualiza { get; set; } = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txTempo.Text = ConfigurationManager.AppSettings["TempoAtualizacao"];
            cbExchange.Text = ConfigurationManager.AppSettings["Exchange"];
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
            var price = new Price();

            if (Ultimo != root.ticker_1h.exchanges.FOX.last)
                Atualiza = true;
            
            switch (cbExchange.Text)
            {
                case "BitValor":
                    price = root.ticker_1h.total;
                    break;
                case "FoxBit":
                    price = root.ticker_1h.exchanges.FOX;
                    break;
                case "MercadoBitcoin":
                    price = root.ticker_1h.exchanges.MBT;
                    break;
                case "BitcoinToYou":
                    price = root.ticker_1h.exchanges.B2U;
                    break;
                default:
                    price = root.ticker_1h.total;
                    break;
            }

            Titulo = Ultimo > price.last
                    ? $"BitNotify (Cotação {cbExchange.Text}) - Baixa" : $"BitNotify (Cotação {cbExchange.Text}) - Alta";


            Message = $@"ÚLTIMO: {price.last.ToString("N")}
ANTERIOR: {Ultimo.ToString("N")}
MÍN: {price.low.ToString("N")}
MÁX: {price.high.ToString("N")}
USD COM: {root.rates.USDCBRL.ToString("N")}
USD TUR: {root.rates.USDTBRL.ToString("N")}";

            Ultimo = price.last;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Get();

            if (Atualiza)
            {
                mynotifyicon.BalloonTipTitle = Titulo;
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
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove("TempoAtualizacao");
            config.AppSettings.Settings.Remove("Exchange");
            config.AppSettings.Settings.Add("TempoAtualizacao", txTempo.Text);
            config.AppSettings.Settings.Add("Exchange", cbExchange.Text);
            config.Save(ConfigurationSaveMode.Modified);

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

        private void mynotifyicon_Click(object sender, EventArgs e)
        {
            this.timer1_Tick(sender, e);
        }

        private void sairToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.btSair_Click(sender, e);
        }

        private void atualizarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.timer1_Tick(sender, e);
        }
    }
}
