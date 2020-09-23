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

namespace Analisador_Lexico_Linguagem_C
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> KeyWords_C = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            addKeyWords();

        }

        public void addKeyWords()
        {
            KeyWords_C.Add("asm");
            KeyWords_C.Add("auto");
            KeyWords_C.Add("break");
            KeyWords_C.Add("case");
            KeyWords_C.Add("char");
            KeyWords_C.Add("const");
            KeyWords_C.Add("continue");
            KeyWords_C.Add("default");
            KeyWords_C.Add("do");
            KeyWords_C.Add("double");
            KeyWords_C.Add("else");
            KeyWords_C.Add("enum");
            KeyWords_C.Add("extern");
            KeyWords_C.Add("float");
            KeyWords_C.Add("for");
            KeyWords_C.Add("goto");
            KeyWords_C.Add("if");
            KeyWords_C.Add("int");
            KeyWords_C.Add("long");
            KeyWords_C.Add("register");
            KeyWords_C.Add("return");
            KeyWords_C.Add("short");
            KeyWords_C.Add("signed");
            KeyWords_C.Add("sizeof");
            KeyWords_C.Add("static");
            KeyWords_C.Add("struct");
            KeyWords_C.Add("switch");
            KeyWords_C.Add("typedef");
            KeyWords_C.Add("union");
            KeyWords_C.Add("unsigned");
            KeyWords_C.Add("void");
            KeyWords_C.Add("volatile");
            KeyWords_C.Add("while");
        }

        private void BT_Abrir_Arquivo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog Ofp = new OpenFileDialog();
            Ofp.Title = "Abra o Arquivo em C";
            Ofp.Filter = "Arquivos C (*.c)|*.c|All files (*.*)|*.*";

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
                int QtdPalavras = 0;

                listBox.Items.Clear();
                foreach (var item in st1)
                {
                    //Inicia a concatenação da tag
                    if (!item.Equals('\r') && !item.Equals('\n') && !concatenar && !item.Equals(' '))
                    {
                        concatenar = true;
                    }

                    //Se esta concatenando e diferente de \r e \n que são os caracteres de quebra de linha
                    if (concatenar && !item.Equals('\r') && !item.Equals('\n') && !item.Equals(' '))
                    {
                        tag += item;
                    }

                    //Finaliza a concatenãção da tag
                    if ((item.Equals(' ') || item.Equals('\r') || item.Equals('\n')  ) && concatenar)
                    {

                        listBox.Items.Add(tag); //Adiciona na lista a tag

                        foreach (var key in KeyWords_C)
                        {
                            if (tag.Contains(key))
                            {
                                listBox_Reserved.Items.Add(key);
                                QtdPalavras += 1;       //conta uma tag encontrada
                            }
                        }

                        tag = "";               //Limpa a tag
                        concatenar = false;     //habilita iniciar a concatenar outra tag
                    }

                }
                Qtd_Tags.Content = QtdPalavras.ToString();
            }
        }
    }
}
