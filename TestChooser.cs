using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TestChooser {
    public partial class TestChooser : Form {
        public TestChooser(String[] args) {
            InitializeComponent();
            Int32 testPlanOldPID = 0;
            if (args.Length > 0) try { testPlanOldPID = Convert.ToInt32(args[0]); } catch { }
            if (testPlanOldPID != 0) {
                Process testPlanOld = null;
                try {
                    Show();
                    Cursor.Current = Cursors.WaitCursor;
                    testPlanOld = Process.GetProcessById(testPlanOldPID);
                    Int32 iterations = 0, iterationsMax = 60;
                    progressBarWorking.Minimum = 0; progressBarWorking.Maximum = iterationsMax; progressBarWorking.Value = 0;
                    while (!testPlanOld.HasExited && iterations <= iterationsMax) {
                        progressBarWorking.Value = iterations;
                        Thread.Sleep(500);
                        testPlanOld.Refresh();
                        iterations++; // 60 iterations with 0.5 second sleeps = 30 seconds max.
                    }
                } finally {
                    Hide();
                    Cursor.Current = Cursors.Default;
                }
                if (testPlanOld != null && !testPlanOld.HasExited) {
                    _ = MessageBox.Show($"Old TestPlan hasn't exited.{Environment.NewLine}{Environment.NewLine}Please contact Test Engineering.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }

            String ChooserDefinitionXML = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ChooserDefinition.xml";
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.InitialDirectory = XElement.Load(ChooserDefinitionXML).Element("OpenFileDialog").Attribute("InitialDirectory").Value;
                openFileDialog.Filter = XElement.Load(ChooserDefinitionXML).Element("OpenFileDialog").Attribute("Filter").Value;
                if (openFileDialog.ShowDialog() == DialogResult.OK) _ = Process.Start($"\"{openFileDialog.FileName}\"");
            }
            Close();
        }

        private void Form_Load(Object sender, EventArgs e) { Hide(); }
    }

    public static class Program {
        [STAThread]
        public static void Main(String[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TestChooser(args));
        }
    }
}
