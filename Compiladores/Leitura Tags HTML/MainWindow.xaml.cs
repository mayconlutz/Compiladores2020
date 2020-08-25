using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Leitura_Tags_HTML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BT_Abrir_Arquivo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog Ofp = new OpenFileDialog();
            Ofp.Title = "Abra o Arquivo de Texto";
            Ofp.Filter = "Arquivos TXT (*.txt)|*.txt|All files (*.*)|*.*";

            if (Ofp.ShowDialog() == true)
            {
                var sr = new StreamReader(Ofp.FileName);

                var st1 = sr.ReadToEnd();

                //Comentários sobre caracteres especiais e sistemas operacionais.
                // \r = CR(Carriage Return) // Usado como quebra de linha no Mac OS anterior à versão X
                // \n = LF(Line Feed) // Usado como quebra de linha Unix/Mac OS superior à versão X
                // \r\n = CR + LF // Usado como quebra de linha no Windows

                string tag = "";
                bool concatenar = false;
                int QtdTags = 0;

                listBox.Items.Clear();
                foreach (var item in st1)
                {
                    //Inicia a concatenação da tag
                    if (item == '<' && !concatenar)
                    {
                        concatenar = true;
                    }

                    //Se esta concatenando e diferente de \r e \n que são os caracteres de quebra de linha
                    if (concatenar && !item.Equals('\r') && !item.Equals('\n'))
                    {
                        tag += item;
                    }

                    //Finaliza a concatenãção da tag
                    if (item == '>' && concatenar)
                    {
                        QtdTags += 1;           //conta uma tag encontrada
                        tag += " ---> Tag " + QtdTags;
                        listBox.Items.Add(tag); //Adiciona na lista a tag
                        tag = "";               //Limpa a tag
                        concatenar = false;     //habilita iniciar a concatenar outra tag
                    }

                }
                Qtd_Tags.Content = QtdTags.ToString();
            }
        }
    }
}
