using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;

namespace BitTicket
{
    public partial class Form1 : Form
    {
        private string mensagem = string.Empty;
        private int tempoAtualizacao = Convert.ToInt32(ConfigurationManager.AppSettings["TempoAtualizacao"]) * 1000;
        private int positionX = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            label1.Location = new Point(positionX, 0);
            positionX -= 1;
            if (positionX < label1.Size.Width * -1)
            {
                positionX = this.Width;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Get();
            label1.Text = mensagem;

            timer1.Start();

            if (timer2.Enabled)
                timer2.Stop();
            timer2.Interval = tempoAtualizacao;
            timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Get();
            label1.Text = mensagem;
        }

        private void Get()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var responseString = client.GetStringAsync("https://api.bitvalor.com/v1/ticker.json").Result;
                    var root = JsonConvert.DeserializeObject<RootObject>(responseString);

                    mensagem = $"ÚLTIMO: {root.ticker_1h.exchanges.FOX.last.ToString("N")} - MÍN: {root.ticker_1h.exchanges.FOX.low.ToString("N")} - MÁX: {root.ticker_1h.exchanges.FOX.high.ToString("N")} - USD COM: {root.rates.USDCBRL.ToString("N")} - USD TUR: {root.rates.USDTBRL.ToString("N")}";

                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
