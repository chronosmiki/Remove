using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Remove
{
    class Program
    {
        //Strings com o caminho das pastas que contém o conteudo a ser apagado.
        static string SE = @"G:\Dados\Producao\Scanner_Estagiarios";
        static string SA = @"G:\Dados\Producao\Scanner_Administrativo";
        static string SAdv = @"G:\Dados\Producao\Scanner_Advogados";
        static string SS = @"G:\Dados\Producao\Scanner_Socios";
        static string TP = @"G:\Dados\Producao\TemporadioPeticoes"; 

        //Metodo principal que executa o codigo e seus metodos.
        static void Main(string[] args)
        {
            //Tenta executar o metodo Apagar, se for encontrado erro, imprime o mesmo na tela.
            try
            {
                Apagar(SE);
                Apagar(SA);
                Apagar(SAdv);
                Apagar(SS);
                Apagar(TP);
            }catch(IOException ex)
            {
                Console.Write("Erro: " + ex);
                Console.ReadKey();                
            }                        
        }

        //Metodo apagar que pede como parametro uma String com o caminho do diretorio..
        //Que tera seu conteudo apagado.
        private static void Apagar(string path)
        {
            try
            {   
                //Array com os arquivos que serão apagados, a expreção "*.*" serve para determinar
                //Que todos os arquivos de todas as extensões serão apagados.
                string[] _files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                
                //Array com os subdiretorios que serão apagados.
                string[] _folders = Directory.GetDirectories(path);

                //Para cada arquivo contido no Array, ele procura por atributos somente leitura
                //E após a leitura ele muda o atributo dos arquivos somente leitura para normal.
                //Ele ira realizar a exclusão...
                //Somente se o arquivo tiver a data de modificação anterior a 7 dias do dia atual da execução.
                foreach (string f in _files)
                {
                    FileAttributes fa = File.GetAttributes(f);

                    //Condicionamento usado para filtrar a data dos documentos.
                    //DateTime.Now.AddDays(-7) serve para tirar 7 dias da data atual.
                    //Pode ser alterado por minutos, horas, meses e anos.
                    if (File.GetLastWriteTime(f) < DateTime.Now.AddDays(-7))
                    {   
                        //Muda o atributo do arquivo para Normal, caso o mesmo seja Somente Leitura.
                        File.SetAttributes(f, FileAttributes.Normal);
                        //Deleta o arquivo.
                        File.Delete(f);
                    }                                  
                }

                //Para cada subpasta contida no array, ele ira apagar somente as que tiverem a
                //Data de modificação anterior a 7 dias do dia atual da execução.
                foreach (string f in _folders)
                {  
                    //Condicionamento usado para filtrar a data dos diretorios.
                    if (Directory.GetLastWriteTime(f) < DateTime.Now.AddDays(-7))
                    {   
                        //Deleta o diretorio.
                        Directory.Delete(f);
                    }
                }
            //Em caso de erro, captura a exceção e imprime na tela.                    
            }catch(IOException ex)
            {
                Console.Write("Erro: " + ex);
                Console.ReadKey();
            }
        }  
    }
}
