using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Timer = System.Windows.Forms.Timer;

namespace InWinFormsApp
{
    public partial class InputUserDetailsForm : Form
    {
        // Form Title
        private Label label = new();
        private TextBox textBox = new()
        {
            Text = "a textbox in InputUserDetailsForm",
        };

        private Timer timer = new Timer();

        public InputUserDetailsForm()
        {
            InitializeComponent();
            Font headerFont = new("ArialBlack", 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            Font textFont = new("Arial", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            textBox = new()
            {
                Text = "a textbox in InputUserDetailsForm",
                AutoSize = true,
                Location = new Point { X = 100, Y = 350 },
                TextAlign = HorizontalAlignment.Center,
                AcceptsReturn = true,
                AcceptsTab = true,
                BackColor = Color.White,
                ForeColor = Color.DarkBlue,
                PlaceholderText = textBox.Text,
                Font = textFont
            };
            textBox.TextChanged += textBox_TextChanged;
            textBox.Text = String.Format("User Details Form");
            label.Text = "Input UserDetails Form";
            label.Font = headerFont;
            label.Size = new Size { Width = 600, Height = 100 };
            label.Location = new Point { X = 50, Y = 100 };
            label.BackColor = Color.Gold;
            label.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(label);
            this.Controls.Add(textBox);
            this.ResumeLayout(false);

            Button button = new Button();
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Text = "OK";
            button.TextImageRelation = TextImageRelation.ImageBeforeText;
            button.ImageAlign = ContentAlignment.MiddleCenter;
            button.Location = new Point(650, 400);
            button.Size = new Size(100, 40);
            button.Click +=
                async (sender, e) =>
                {
                    button.Text = "Done"; 
                    button.BackColor = Color.Green;
                    button.Enabled = false; // Disable button (optional, prevents multiple clicks)
                    button.Text = "Please wait..."; // Informative text

                    await Task.Delay(5000);  // Non-blocking wait
                    ShowAboutBox();

                    button.Enabled = true; // Re-enable the button
                    button.Text = "OK"; // Restore the original text
                };

            this.Controls.Add(button);

        }


        private void textBox_TextChanged(object sender, EventArgs e)
        {
            int requiredWidth = TextRenderer.MeasureText(textBox.Text, textBox.Font).Width;
            textBox.Width = requiredWidth + 5;
        }

        private void ShowAboutBox()
        {
            // ... (About box)
            AboutBox1 aboutBox1 = new();
            aboutBox1.Show();

            // Option 1: Close the current form
            this.Close();

            // Option 2: Minimize the current form
            this.WindowState = FormWindowState.Minimized;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;  //  Stop the timer
            ShowAboutBox();
        }

    }
}
