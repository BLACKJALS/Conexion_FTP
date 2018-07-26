using ArxOne.Ftp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ConexionFTP
{
    public class ConFTP
    {
        public string host { get; set; }
        public string user { get; set; }
        public string pass { get; set; }
        public int port { get; set; }
        public FtpWebRequest ftpRequest { get; set; }
        public FtpWebResponse ftpResponse { get; set; }
        public Stream ftpStream { get; set; }
        public int bufferSize { get; set; }


        public ConFTP(string hostIP, string usuario, string password, int port) {
            host = hostIP;
            user = usuario;
            pass = password;
            port = port;
        }

        public void Connection(string host, string pass, string user, int port) {
            var listaArchivos = new List<FtpEntry>();
            string rutaFinal = @"E:\Documentos\Prueba SFTP\Prueba";
            var rutaP = "ftp://127.0.0.1/Archivos/TestFile.xlsx";
            var rutaF = "ftp://127.0.0.1/Prueba/";
            var ftp = new FtpClient(FtpProtocol.Ftp, host, port, new NetworkCredential(user,pass), new FtpClientParameters { ConnectTimeout = TimeSpan.FromSeconds(15), SessionTimeout = TimeSpan.FromSeconds(15) });
            var ftpPath = new FtpPath(@"/Archivos");
            listaArchivos.AddRange(ftp.ListEntries(ftpPath));

            foreach (var archivo in listaArchivos) {
                var fileName = archivo.Path.ToString();
                //ftp.RnfrTo(fileName, rutaF);
                File.Copy(rutaP, rutaFinal);
            }
        }

        public void uploadFile(string host, string pass, string user, int port) {
            Uri uri = new Uri(string.Format(@"{0}/{1}/", "ftp://127.0.0.1", "Archivos"));

            WebClient client = new WebClient();
            client.Credentials = new NetworkCredential(user, pass);
            var a = client.Encoding;
            //var listDirectorios = Directory.GetFiles("");
            client.UploadFile(uri+ "Prueba.txt", @"E:\Documentos\ArchivoSFTPrueba\Prueba.txt");


            /* -- Este método sirve para mover archivos locales al servido utilizando FtpWebRequest.
             * 
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://127.0.0.1/Archivos/Prueba.txt");
            request.Credentials = new NetworkCredential(user, pass);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            
            using (Stream fileStream = File.OpenRead(@"E:\Documentos\ArchivoSFTPrueba\Prueba.txt"))
            using (Stream ftpStream = request.GetRequestStream()) {
                fileStream.CopyTo(ftpStream);
            }
            */
        }

        public void downLoadFile(string pass, string user, int port) {
            try {
                var listaArchivos = new List<FtpEntry>();
                var ftp = new FtpClient(FtpProtocol.Ftp, host, port, new NetworkCredential(user, pass), new FtpClientParameters { ConnectTimeout = TimeSpan.FromSeconds(15), SessionTimeout = TimeSpan.FromSeconds(15) });
                var ftpPath = new FtpPath(@"/Archivos");
                listaArchivos.AddRange(ftp.ListEntries(ftpPath));

                WebClient client = new WebClient();
                client.Credentials = new NetworkCredential(user, pass);
                foreach (var archivo in listaArchivos) {
                    var fileName = archivo.Path.ToString();
                    client.DownloadFile("ftp://127.0.0.1/Archivos/"+archivo.Name, @"E:\Documentos\Prueba SFTP\Prueba\" + archivo.Name);
                }
                //WebClient client = new WebClient();
                //client.Credentials = new NetworkCredential(user, pass);
                //client.DownloadFile("ftp://127.0.0.1/Archivos/Prueba.txt", @"E:\Documentos\Prueba SFTP\Prueba.txt");
            } catch (WebException e) {
                String status = ((FtpWebResponse)e.Response).StatusDescription;
            }
        }

        public void downloadFile2() {
            FtpWebRequest request =(FtpWebRequest)WebRequest.Create("ftp://127.0.0.1/Archivos/Prueba.txt");
            request.Credentials = new NetworkCredential("alejandro", "alejandro");
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            //request.EnableSsl = true;

            using (Stream ftpStream = request.GetResponse().GetResponseStream())
            using (Stream fileStream = File.Create(@"E:\Documentos\Prueba SFTP\Prueba.txt")) {
                ftpStream.CopyTo(fileStream);
            }
            //FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://127.0.0.1/Archivos");
            //request.Method = WebRequestMethods.Ftp.ListDirectory;
            //request.Credentials = new NetworkCredential("alejandro", "alejandro");
            //FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            //Stream responseStream = response.GetResponseStream();
            //StreamReader reader = new StreamReader(responseStream);
            //var names = reader.ReadToEnd();
        }
    }
}
