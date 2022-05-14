using System.Globalization;
using System.Windows.Forms;

namespace SharpDXWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DSharpDX.System.DSystem.StartRenderForm("test", 1280, 720, false, false, 3);
        }
    }
}
