using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace TestChooser {
    public partial class TestChooser : Form {
        public TestChooser(String[] args) {
            InitializeComponent();

            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.InitialDirectory = @"C:\Program Files\ABT\Test\TestPlans\";
                openFileDialog.Filter = "TestPlan Programs|*.exe";

Choose: if (openFileDialog.ShowDialog() != DialogResult.OK) {
                    DialogResult dialogResult = MessageBox.Show(ActiveForm, $"Do you want to exit?{Environment.NewLine}{Environment.NewLine}", "Exit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dialogResult == DialogResult.Yes) Application.Exit();
                    else goto Choose;
                } else {
                    Process testPlanNew = Process.Start($"\"{openFileDialog.FileName}\"");
                    Int32 iterations = 0;
                    Cursor.Current = Cursors.WaitCursor;
                    while (testPlanNew.MainWindowHandle == IntPtr.Zero && iterations <= 60) {
                        // TODO: Soon; display visual progress bar.
                        Thread.Sleep(500);
                        testPlanNew.Refresh();
                        iterations++; // 60 iterations with 0.5 second sleeps = 30 seconds max.
                    }
                    Cursor.Current = Cursors.Default;
                    if (testPlanNew.MainWindowHandle == IntPtr.Zero) {
                        _ = MessageBox.Show(ActiveForm, $"New TestPlan hasn't started.{Environment.NewLine}{Environment.NewLine}" +
                        "Please contact Test Engineering.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                    }

                    Process testPlanOld = null;
                    Int32 testPlanOldPID = 0;
                    if (args.Length > 0) try { testPlanOldPID = Convert.ToInt32(args[0]); } catch { }
                    if (testPlanOldPID != 0) {
                        try {
                            testPlanOld = Process.GetProcessById(testPlanOldPID);
                            iterations = 0;
                            Cursor.Current = Cursors.WaitCursor;
                            while (!testPlanOld.HasExited && iterations <= 60) {
                                // TODO: Soon; display visual progress bar.
                                Thread.Sleep(500);
                                testPlanOld.Refresh();
                                iterations++; // 60 iterations with 0.5 second sleeps = 30 seconds max.
                            }
                            Cursor.Current = Cursors.Default;
                        } catch { }
                    }

                    if (testPlanOld != null && !testPlanOld.HasExited) _ = MessageBox.Show(ActiveForm, $"Old TestPlan hasn't exited.{Environment.NewLine}{Environment.NewLine}" +
                        "Please contact Test Engineering.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
        }
    }
}
