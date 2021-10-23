using Microsoft.Extensions.Options;
using WinFormsDi.Services;
using WinFormsNonBlockingAsync.Config;

namespace WinFormsDi
{
    public partial class frmMain : Form
    {
        private readonly IWaitService _waitService;
        private readonly Configuration _config;

        public frmMain(IWaitService waitService, IOptions<Configuration> config)
        {
            InitializeComponent();
            this._waitService = waitService;
            this._config = config.Value;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            lblOut.Text = "Waiting...";
            await _waitService.Wait(5000);
            lblOut.Text = _config.TestConfig;
            button1.Enabled = true;
        }
    }
}