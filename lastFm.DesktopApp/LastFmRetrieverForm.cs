using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace lastFm.DesktopApp
{
    public partial class LastFmRetrieverForm : Form
    {
        private Settings UserSettings { get; }
        private RecentlyPlayedResponse recentlyPlayed;
        private bool IsPaused { get; set; }

        public LastFmRetrieverForm()
        {
            IsPaused = false;
            InitializeComponent();
            UserSettings = ReadSettings();
            SetFieldsFromSettings();
            var retrieverTask = new Task(GetAndWriteFormattedString);
            retrieverTask.Start();
        }

        public void GetAndWriteFormattedString()
        {
            while (true)
            {
                while (IsPaused)
                    Thread.Sleep(3000);

                if (UserSettings.Username != null)
                {
                    try
                    {
                        var response = LastFmClient.GetRecentlyPlayedResponse(UserSettings.Username);
                        if (recentlyPlayed == response)
                            return;
                        WriteFiles(response);
                        recentlyPlayed = response;
                    }
                    catch (WebException ex)
                    {
                        var response = (HttpWebResponse) ex.Response;
                        switch (response.StatusCode)
                        {
                            case HttpStatusCode.NotFound:
                                var result =
                                    MessageBox.Show(@"We couldn't find your user in Last.fm! Please check it!");
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    catch (IOException _)
                    {
                        MessageBox.Show(@"Couldn't write the file! Do you have permission to write to: " +
                                        UserSettings.OutputDirectory + @"\\" + UserSettings.OutputFileName);
                    }
                }

                Thread.Sleep(5000);
            }
        }

        public void SetFieldsFromSettings()
        {
            OutputDirectory.Text = UserSettings.OutputDirectory;
            OutputFilename.Text = UserSettings.OutputFileName;
            Username.Text = UserSettings.Username;
            OutputFormat.Text = UserSettings.Format;
        }

        private void save_Click(object sender, EventArgs e)
        {
            UserSettings.OutputDirectory = OutputDirectory.Text;
            UserSettings.OutputFileName = OutputFilename.Text;
            UserSettings.Username = Username.Text;
            UserSettings.Format = OutputFormat.Text;
            WriteSettings(UserSettings);
        }

        private Settings ReadSettings()
        {
            if (File.Exists("settings.json"))
            {
                var result = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("settings.json"));
                WriteSettings(result);
                return result;
            }

            var settings = new Settings()
            {
                Format = "$song - $artist",
                OutputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\.lastfm",
                OutputFileName = "lastfm.txt",
                Username = ""
            };
            WriteSettings(settings);
            return settings;
        }

        private void WriteSettings(Settings settings)
        {
            Directory.CreateDirectory(settings.OutputDirectory);
            File.WriteAllText("settings.json", JsonConvert.SerializeObject(settings, Formatting.Indented));
        }

        private void WriteFiles(RecentlyPlayedResponse response)
        {
            var formattedString = UserSettings.Format.Replace("$song", response.Song)
                .Replace("$artist", response.Artist).Replace("$album", response.Album);
            File.WriteAllText(UserSettings.OutputDirectory + "\\" + UserSettings.OutputFileName,
                formattedString);
            File.WriteAllText(UserSettings.OutputDirectory + "\\" + "artist.txt",
                response.Artist);
            File.WriteAllText(UserSettings.OutputDirectory + "\\" + "song.txt",
                response.Song);
            File.WriteAllText(UserSettings.OutputDirectory + "\\" + "album.txt",
                response.Album);
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            Pause.Text = IsPaused ? "Pause" : "Unpause";
            IsPaused = !IsPaused;
        }



        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            notifyIcon.Visible = false;
            ShowInTaskbar = true;
        }

        private void LastFmRetrieverForm_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized) return;
            Hide();
            notifyIcon.Visible = true;
            ShowInTaskbar = false;
        }

        private void PauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pauseToolStripMenuItem.Text = IsPaused ? "Pause" : "Unpause";
            IsPaused = !IsPaused;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}