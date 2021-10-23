using WinFormsDi.Services;

namespace WinFormsDi
{
    public partial class frmMain : Form
    {
        private readonly IWaitService _waitService;

        public frmMain(IWaitService waitService)
        {
            InitializeComponent();
            this._waitService = waitService;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            lblOut.Text = "Waiting...";
            await _waitService.Wait(30000);
            lblOut.Text = "Waited";
        }
    }
}