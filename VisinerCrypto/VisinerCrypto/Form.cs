using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisinerCrypto
{
    public class VisinerForm : Form
    {
        public VisinerForm()
        {
            Size = new Size { Height = 500, Width = 700 };

            var visiner = new Visiner();

            RichTextBox inputTextBox = new RichTextBox()
            {
                Text = "Текст"
            };
            RichTextBox outputTextBox = new RichTextBox()
            {
                ReadOnly = true,
                Text = "Зашифрованный текст"
            };
            Button encipherButton = new Button()
            {
                Text = "Зашифровать"
            };
            Button decipherButton = new Button()
            {
                Text = "Расшифровать",
                Enabled = false
            };
            Button chooseFileButton = new Button()
            {
                Text = "Выбрать файл"
            };
            Button encipherFileButton = new Button()
            {
                Text = "Зашифровать файл"
            };
            Button decipherFileButton = new Button()
            {
                Text = "Расшифровать файл",
                Enabled = false
            };
            RichTextBox cryptoWordBox = new RichTextBox()
            {
                Text = "Ключ"
            };

            Controls.Add(inputTextBox);
            Controls.Add(outputTextBox);
            Controls.Add(encipherButton);
            Controls.Add(decipherButton);
            Controls.Add(cryptoWordBox);
            Controls.Add(chooseFileButton);
            Controls.Add(encipherFileButton);
            Controls.Add(decipherFileButton);

            SizeChanged += (sender, args) =>
            {
                inputTextBox.Location = new Point(0, ClientSize.Height / 2);
                inputTextBox.Size = new Size(ClientSize.Width / 12 * 5, ClientSize.Height);

                encipherButton.Location = new Point(inputTextBox.Size.Width, ClientSize.Height / 2);
                encipherButton.Size = new Size(ClientSize.Width / 6, ClientSize.Height / 4);

                decipherButton.Location = new Point(inputTextBox.Size.Width, encipherButton.Bottom);
                decipherButton.Size = new Size(ClientSize.Width / 6, ClientSize.Height / 4);

                outputTextBox.Location = new Point(encipherButton.Right, ClientSize.Height / 2);
                outputTextBox.Size = new Size(ClientSize.Width / 12 * 5, ClientSize.Height);

                cryptoWordBox.Location = new Point(0, 0);
                cryptoWordBox.Size = new Size(ClientSize.Width, ClientSize.Height / 6);

                chooseFileButton.Location = new Point(ClientSize.Width / 20, cryptoWordBox.Bottom + ClientSize.Height / 20);
                chooseFileButton.Size = new Size(ClientSize.Width / 6, ClientSize.Height / 6);

                encipherFileButton.Location = new Point(chooseFileButton.Right + ClientSize.Width / 20, cryptoWordBox.Bottom + ClientSize.Height / 20);
                encipherFileButton.Size = new Size(ClientSize.Width / 6, ClientSize.Height / 6);

                decipherFileButton.Location = new Point(encipherFileButton.Right + ClientSize.Width / 20, cryptoWordBox.Bottom + ClientSize.Height / 20);
                decipherFileButton.Size = new Size(ClientSize.Width / 6, ClientSize.Height / 6);
            };

            encipherButton.Click += (sender, args) =>
            {
                encipherButton.Enabled = false;
                decipherButton.Enabled = true;
                outputTextBox.Text = visiner.Encipher(cryptoWordBox.Text, inputTextBox.Text);
                inputTextBox.Text = "";
            };

            decipherButton.Click += (sender, args) =>
            {
                encipherButton.Enabled = true;
                decipherButton.Enabled = false;
                inputTextBox.Text = visiner.Decipher(cryptoWordBox.Text, outputTextBox.Text);
                outputTextBox.Text = "";
            };

            OpenFileDialog openFileDialog = new OpenFileDialog();
            string fileText = null;
            string filename = null;

            chooseFileButton.Click += (sender, args) =>
            {
                openFileDialog.Filter = "(*.txt) | *.txt";
                if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }

                filename = openFileDialog.FileName;
                fileText = File.ReadAllText(filename);

                MessageBox.Show("Файл выбран");
            };

            encipherFileButton.Click += (sender, args) =>
            {
                var result = visiner.Encipher(cryptoWordBox.Text, fileText);
                if (result == null)
                {
                    MessageBox.Show("Файл не выбран");
                }

                using (StreamWriter stream = new StreamWriter(filename, false, Encoding.UTF8))
                {
                    stream.Write(result);
                }

                MessageBox.Show("Файл зашифрован");

                decipherFileButton.Enabled = true;
            };

            decipherFileButton.Click += (sender, args) =>
            {
                if (filename == null)
                {
                    MessageBox.Show("Файл не выбран");
                }

                fileText = File.ReadAllText(filename);

                var result = visiner.Decipher(cryptoWordBox.Text, fileText);

                if (result == null)
                {
                    MessageBox.Show("Файл не выбран");
                }

                using (StreamWriter stream = new StreamWriter(filename, false, Encoding.UTF8))
                {
                    stream.Write(result);
                }

                MessageBox.Show("Файл расшифрован");

                decipherFileButton.Enabled = false;
            };

            Load += (sender, args) =>
            {
                OnSizeChanged(EventArgs.Empty);
            };
        }

        public class Visiner
        {
            private char[] alphabet;

            public Visiner()
            {
                alphabet = GetFirstAlphabet();
            }

            public string Encipher(string cipher, string input)
            {
                if (String.IsNullOrEmpty(input) || String.IsNullOrEmpty(cipher))
                {
                    return null;
                }

                char[] currentAlphabet = alphabet;
                var result = new char[input.Length];

                for (var i = 0; i < input.Length; i++)
                {
                    var cipherIndex = Array.IndexOf(alphabet, cipher[i % cipher.Length]);

                    currentAlphabet = GetEncipherAlphabet(cipherIndex);

                    var inputIndex = Array.IndexOf(alphabet, input[i % input.Length]);

                    result[i] = currentAlphabet[inputIndex];
                }

                return new string(result);
            }

            public string Decipher(string cipher, string output)
            {
                if (String.IsNullOrEmpty(output) || String.IsNullOrEmpty(cipher))
                {
                    return null;
                }

                char[] currentAlphabet = alphabet;
                var result = new char[output.Length];

                for (var i = 0; i < output.Length; i++)
                {
                    var cipherIndex = Array.IndexOf(alphabet, cipher[i % cipher.Length]);

                    currentAlphabet = GetEncipherAlphabet(cipherIndex);

                    var outputIndex = Array.IndexOf(currentAlphabet, output[i % output.Length]);

                    result[i] = alphabet[outputIndex];
                }

                return new string(result);
            }

            private char[] GetFirstAlphabet()
            {
                var alphabet =
                    Enumerable.Range('A', 'Z' - 'A' + 1)
                    .Select(c => (char)c)
                    .Concat(Enumerable.Range('a', 'z' - 'a' + 1)
                    .Select(c => (char)c))
                    .Concat(Enumerable.Range('А', 'Я' - 'А' + 1)
                    .Select(c => (char)c))
                    .Concat(Enumerable.Range('а', 'я' - 'а' + 1)
                    .Select(c => (char)c))
                    .Concat(Enumerable.Range(' ', ' ').Select(c => (char)c));

                return alphabet.ToArray();
            }

            private char[] GetEncipherAlphabet(int k)
            {
                var result = new char[alphabet.Length];
                for (var i = 0; i < alphabet.Length; i++)
                {
                    var index = (i + k) % alphabet.Length;
                    result[i] = alphabet[index];
                }

                return result;
            }
        }
    }
}
