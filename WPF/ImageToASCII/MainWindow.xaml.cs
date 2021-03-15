using System;
using System.Windows;
using System.IO;
using System.Drawing;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;

namespace ImageToASCII
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] chars;
        string outputPath;
        string[] imagePaths;

        public MainWindow()
        {
            if (!File.Exists("chars.txt"))
            {
                File.WriteAllText("chars.txt", "#\n@\n&\n+\n*\n/");
            }
            chars = File.ReadAllLines("chars.txt");
            InitializeComponent();
        }

        private void Button_ImageDialogue(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.Multiselect = true;
            dialog.Title = "Select Image(s)";
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string[] extensions = new string[] { ".png", ".jpg", ".gif", ".jpeg", ".bmp" };
                foreach (string fName in dialog.FileNames)
                {
                    var isValid = false;
                    foreach (string extension in extensions)
                    {
                        if (fName.EndsWith(extension))
                        {
                            isValid = true;
                            break;
                        }
                    }
                    if (!isValid)
                    {
                        MessageBox.Show("Detected a non-image file! \n\nAllowed Extensions: " + String.Join(", ", extensions), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        imagePaths = new string[0];
                        ImagesText.Text = "Not Selected";
                        return;
                    }
                }
                imagePaths = dialog.FileNames.ToArray();

                List<string> images = new List<string>();

                foreach (string image in imagePaths)
                {
                    string[] nameArr = image.Split('\\');
                    images.Add(nameArr[nameArr.Length - 1]);
                }

                ImagesText.Text = "[ " + string.Join(", ", images.ToArray()) + " ]";
            }
        }

        private void Button_FolderDialog(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Title = "Select Output Folder";
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                outputPath = dialog.FileName;
                FoldersText.Text = "[ " + dialog.FileName + " ] ";
            }
            else if (result == CommonFileDialogResult.None)
            {
                FoldersText.Text = "Not Selected";
                outputPath = "";
            }
        }

        void ConvertToASCII()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            foreach (string image in imagePaths)
            {
                Bitmap bmap = new Bitmap(@image);

                using (StreamWriter outStream = File.AppendText(Path.Combine(outputPath, DateTimeOffset.Now.ToUnixTimeSeconds().ToString() + "-output.txt")))
                {
                    for (int y = 0; y < bmap.Height; y++)
                    {
                        for (int x = 0; x < bmap.Width; x++)
                        {
                            Color pColor = bmap.GetPixel(x, y);
                            int brightness = (int)Math.Round((double)(chars.Length - 1) / 255f * (0.2126 * pColor.R + 0.7152 * pColor.G + 0.0722 * pColor.B));
                            outStream.Write(chars[brightness]);
                        }
                        outStream.Write("\n");
                    }
                    outStream.Close();
                }
            }
            Mouse.OverrideCursor = Cursors.Arrow;
            System.Diagnostics.Process.Start(outputPath);
            MessageBox.Show("Done!", "Image to ASCII Converter", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_OpenGithub(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/EliasVal");
        }

        private void Button_ASCIIfy(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(outputPath) && !string.IsNullOrEmpty(outputPath)) {
                foreach (string image in imagePaths)
                {
                    if (!File.Exists(image))
                    {
                        MessageBox.Show("Couldn't find image at " + image, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                ConvertToASCII();

            }
            else
            {
                MessageBox.Show("Output folder [ " + outputPath + " ]  is invalid!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
