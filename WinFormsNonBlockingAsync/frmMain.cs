using Microsoft.Extensions.Options;
using WinFormsNonBlockingAsync.Config;
using WinFormsNonBlockingAsync.Extensions;
using WinFormsNonBlockingAsync.Services;

namespace WinFormsNonBlockingAsync;

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

    private async void btnWaitButton_Click(object sender, EventArgs e)
    {
        var originalText = btnWaitButton.Text;
        await btnWaitButton.DisableUntilTaskComplete(_waitService.Wait(5000), () =>
        {
            btnWaitButton.Text = "Waiting...";
            lblOut.Text = "";
        },
        () =>
        {
            btnWaitButton.Text = originalText;
            lblOut.Text = _config.TestConfig;
        });
    }


}
