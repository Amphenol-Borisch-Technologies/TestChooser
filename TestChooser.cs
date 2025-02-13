using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace TestChooser {
    public partial class TestChooser : Form {
        public TestChooser(String[] args) {
            InitializeComponent();
            Hide();
            Int32 testPlanOldPID = 0;
            if (args.Length > 0) try { testPlanOldPID = Convert.ToInt32(args[0]); } catch { }
            if (testPlanOldPID != 0) {
                Process testPlanOld = null;
                try {
                    testPlanOld = Process.GetProcessById(testPlanOldPID);
                    Int32 iterations = 0, iterationsMax = 60;
                    Cursor.Current = Cursors.WaitCursor;
                    Show();
                    while (!testPlanOld.HasExited && iterations <= iterationsMax) {
                        progressBarWorking.Value = iterations;
                        Thread.Sleep(500);
                        testPlanOld.Refresh();
                        iterations++; // 60 iterations with 0.5 second sleeps = 30 seconds max.
                    }
                    Cursor.Current = Cursors.Default;
                } catch { }
                if (testPlanOld != null && !testPlanOld.HasExited) _ = MessageBox.Show($"Old TestPlan hasn't exited.{Environment.NewLine}{Environment.NewLine}" +
                    "Please contact Test Engineering.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

Choose:     using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.InitialDirectory = @"C:\Program Files\ABT\Test\TestPlans\";
                openFileDialog.Filter = "TestPlan Programs|*.exe";
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    _ = Process.Start($"\"{openFileDialog.FileName}\"");
                    Close();
                }
            }
            DialogResult dialogResult = MessageBox.Show($"Do you want to exit?{Environment.NewLine}{Environment.NewLine}", "Exit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes) Close();
            else goto Choose;
        }
    }
}
