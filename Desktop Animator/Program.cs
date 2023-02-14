using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.ComponentModel;

namespace Desktop_Animator
{
    static class Program
    {
        private static readonly string PROFILE_LOC = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\destelyk\\animator_profiles.ini";

        private static List<AnimatorProfile> profiles;

        private static BackgroundWorker save, saveWaiter;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            save = new BackgroundWorker();
            save.DoWork += new DoWorkEventHandler(Save_DoWork);

            saveWaiter = new BackgroundWorker();

            LoadAllProfiles();

            if (profiles == null)
            {
                profiles = new List<AnimatorProfile>();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ApplicationExit += Application_ApplicationExit;

            if (profiles.Count == 0)
            {
                if (!AddGIF())
                {
                    MessageBox.Show("No file selected. Application closing.", "Closing Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                Console.WriteLine(profiles.Count);

                Application.Run(new Form1(profiles[0], profiles));
            }
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (Form1.application == null && profiles.Count != 0)
            {
                Application.Run(new Form1(profiles[0]));
            }
        }

        static void Save_DoWork(object sender, DoWorkEventArgs e)
        {
            string json = JsonConvert.SerializeObject(profiles);
            using (StreamWriter r = new StreamWriter(PROFILE_LOC))
            {
                r.Write(json);
            }
        }

        static void SaveWaiter_DoWork(object sender, DoWorkEventArgs e)
        {
            while (save.IsBusy)
            {
                Thread.Sleep(100);
            }

            save.RunWorkerAsync();
        }

        static bool LoadAllProfiles()
        {
            try
            {
                using (StreamReader r = new StreamReader(PROFILE_LOC))
                {
                    string json = r.ReadToEnd();
                    profiles = JsonConvert.DeserializeObject<List<AnimatorProfile>>(json);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static void WriteAllProfiles()
        {
            if (!save.IsBusy)
                save.RunWorkerAsync();
            else if (!saveWaiter.IsBusy)
                saveWaiter.RunWorkerAsync();
        }

        public static bool AddGIF()
        {
            MessageBox.Show("Select GIF to use.", "Select GIF", MessageBoxButtons.OK, MessageBoxIcon.Information);

            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "GIF Files (*.gif)|*.gif",
                Multiselect = false
            };

            if (openFile.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            AnimatorProfile profile = new AnimatorProfile(openFile.FileName, System.Drawing.Point.Empty, 1);

            profiles.Add(profile);

            if (profiles.Count == 1)
                Application.Run(new Form1(profile));
            else
                new Form1(profile).Show();

            WriteAllProfiles();

            return true;
        }

        public static void RemoveProfile(AnimatorProfile profile)
        {
            if (MessageBox.Show("Do you want to delete this GIF?", "Deletion Confirmination", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            profiles.Remove(profile);

            WriteAllProfiles();

            profile.form.Dispose();
        }
    }
}
