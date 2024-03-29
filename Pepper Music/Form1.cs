using Pepper_Music.Model;

namespace Pepper_Music
{
    public partial class Form1 : Form
    {
        WMPLib.WindowsMediaPlayer player = new();
        List<Song> songs = new();
        int count = 0;

        private void openFileNow(List<string> fileName)
        {
            try
            {
                bool autoplay = false;
                if(fileName.Count == 1) 
                {
                    autoplay = true;
                }
                foreach(var s in fileName)
                {
                    if (s.EndsWith(".mp3"))
                    {
                        //Obviously you'll do something different with an .mp3
                        songs.Add(new Song(s, s.Replace(".mp3", "").Split(@"\").Last()));
                        listBox1.Items.Add(s.Replace(".mp3", "").Split(@"\").Last());
                    }
                    else
                    {
                        MessageBox.Show("Can't open that file.");
                        return;
                    }
                }
                if(autoplay)
                {
                    Play();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        public Form1(List<string> fileName)
        {
            InitializeComponent();
            openFileNow(fileName);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            player.settings.volume = 50;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (player.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                player.controls.pause();
                button1.Text = "Play";
                timer1.Stop();
                return;
            }
            Play();
        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if(player.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                label1.Text = player.currentMedia.durationString;
                trackBar1.Minimum = 0;
                trackBar1.Maximum = Convert.ToInt32(player.currentMedia.duration);
                label2.Text = player.controls.currentPositionString;
                trackBar1.Value = Convert.ToInt32(player.controls.currentPosition);

            }
            if(player.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                if(trackBar1.Value == trackBar1.Minimum)
                {
                    NextSong();
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            player.controls.currentPosition = trackBar1.Value;
        }

        public void Play()
        {
            if (songs.Count == 0)
            {
                MessageBox.Show("Keine Songs ausgewählt", "Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (player.URL == "" || player.URL is null)
            {
                player.URL = songs.ElementAt(count).Path;
            }
            button1.Text = "Pause";
            label3.Text = songs.ElementAt(count).Name;
            player.controls.play();
            timer1.Start();
        }

        public void NextSong()
        {
            if (songs.Count == 0)
            {
                MessageBox.Show("Keine Songs ausgewählt", "Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (count < songs.Count-1)
            {
                count++;
            }
            else
            {
                count = 0;
            }
            player.URL = songs.ElementAt(count).Path;
            Play();
        }

        public void PreviousSong()
        {
            if (songs.Count == 0)
            {
                MessageBox.Show("Keine Songs ausgewählt", "Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (count > 0)
            {
                count++;
            }
            else
            {
                count = songs.Count-1;
            }
            player.URL = songs.ElementAt(count).Path;
            Play();
            
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            //player.controls.currentPosition = trackBar1.Value;
        }

        private void trackBar1_MouseClick(object sender, MouseEventArgs e)
        {
            player.controls.currentPosition = trackBar1.Value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            player.controls.pause();
            PreviousSong();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            player.controls.pause();
            NextSong();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new();
            dialog.Multiselect = true;
            dialog.Title = "MP3-Files auswählen";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            dialog.Filter = "MP3 Files (*.mp3)|*.mp3";
            dialog.ShowDialog();
            if (dialog.FileNames.Length == 0)
                return;
            for(int i = 0; i < dialog.FileNames.Length; i++)
            {
                songs.Add(new Song(dialog.FileNames[i], dialog.FileNames[i].Replace(".mp3", "").Split(@"\").Last()));
                listBox1.Items.Add(dialog.FileNames[i].Replace(".mp3", "").Split(@"\").Last());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex > songs.Count-1 || listBox1.SelectedIndex < 0)
            {
                return;
            }
            count = listBox1.SelectedIndex;
            player.URL = songs.ElementAt(count).Path;
            player.controls.pause();
            player.controls.currentPosition = 0;
            Play();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            player.settings.volume = trackBar2.Value;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] dropped = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach(string s in dropped)
            {
                if(s.EndsWith(".mp3"))
                {
                    songs.Add(new Song(s, s.Replace(".mp3", "").Split(@"\").Last()));
                    listBox1.Items.Add(s.Replace(".mp3", "").Split(@"\").Last());
                }
                else
                {
                    MessageBox.Show("Der Song" + s.Replace(".mp3", "").Split(@"\").Last() + "kann nicht geladen werden", "Achtung!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void From1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
    }
}