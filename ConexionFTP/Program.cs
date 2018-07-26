using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ConexionFTP
{
    class Program
    {
        static void Main(string[] args)
        {
            string user = "alejandro";
            string pass = "alejandro";
            string host = "127.0.0.1";
            int port = 21;

            string fileName = "";
            string origen = @"E:\Documentos\Prueba SFTP\Archivos\BRS REPORTE DEL COSTO REAL (LANDED COST) V20160914 (1).xlsx";
            string destino = @"E:\Documentos\Prueba SFTP\Prueba\BRS REPORTE DEL COSTO REAL (LANDED COST) V20160914 (1).xlsx";

            //File.Move(origen, destino);

            var conf = new ConFTP(host,user,pass,port);
            //conf.Connection(host, user, pass, port);
            //conf.uploadFile(host, user, pass, port);
            //--conf.downLoadFile(user, pass, port);
            //conf.downloadFile2();
            conf.uploadFile(host, user, pass, port);
        }
    }
}
