using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace TestChooser {
    public partial class TestChooser : Form {
        public TestChooser(String[] args) {
            InitializeComponent();

            if (args.Length == 0) {
Choose:         using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                    openFileDialog.InitialDirectory = @"C:\Program Files\ABT\Test\TestPlans\";
                    openFileDialog.Filter = "TestPlan Programs|*.exe";

                    if (openFileDialog.ShowDialog() == DialogResult.OK) {
                        Process process = Process.Start($"\"{openFileDialog.FileName}\"");

                        Int32 iterations = 0;
                        Cursor.Current = Cursors.WaitCursor;
                        while (process.MainWindowHandle == IntPtr.Zero && iterations <= 60) {
                            Console.WriteLine("Waiting for main window to open...");
                            Thread.Sleep(500);
                            iterations++; // 60 iterations with 0.5 second sleeps = 30 seconds max.
                            process.Refresh();
                        }
                        Cursor.Current = Cursors.Default;
                        if (process.MainWindowHandle == IntPtr.Zero) {
                            _ = MessageBox.Show(ActiveForm, $"Non-existent Window handle for '{openFileDialog.FileName}'.{Environment.NewLine}{Environment.NewLine}" +
                                "Please contact Test Engineering.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        Application.Exit();
                    }
                    if (DialogResult.Yes == MessageBox.Show(ActiveForm, $"Do you want to exit?{Environment.NewLine}{Environment.NewLine}",
                        "No TestPlan selected.", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) Application.Exit();
                }
                goto Choose;
            }
        }



    }
}

