using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Server
{
    class Server
    {

        private int port = 1025;
        private IPAddress localAddr = null;

        private TcpClient client = null;
        private NetworkStream stream = null;



        public Server(int port, IPAddress localAddr)
        {
            this.port = port;
            this.localAddr = localAddr;
        }
        
        

        public void run()
        {
            Console.WriteLine("SERVER RUNNING");
            TcpListener serverListener = new TcpListener(localAddr, port);

            try
            {
                while (true)
                {

                    serverListener.Start();

                    Console.WriteLine("\nWaiting for connections... ");


                    client = serverListener.AcceptTcpClient();
                    // получаем входящее подключение

                    Console.WriteLine("Client is connected. Executing the query...");

                    // поток для чтения и записи
                    stream = client.GetStream();

                    Console.WriteLine("****");


                    byte[] data2 = new byte[256];
                    String responseData = String.Empty;
                    Int32 bytes = stream.Read(data2, 0, data2.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data2, 0, bytes);
                    Console.WriteLine("Received: {0}", responseData);
                    
                    manage(responseData);
                    


                }
                 
            }
            
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
            finally
            {
                if (serverListener != null)
                    serverListener.Stop();
                Console.ReadLine();
            }
            
        }


        public void manage(String msg)
        {

            String[] data = msg.Split('|');

            int index = Convert.ToInt32(data[0]);
            string directory = data[1];
            string fileName = data[2];
            string text = data[3];

            switch (index)
            {
                case 0:
                    {
                        send(stream, showDirInDir(directory));
                        break;
                    }
                case 1:
                    {
                        send(stream, sizeFile(directory, fileName));
                        break;
                    }

                case 2:
                    {
                        send(stream, createFile(directory, fileName, text));
                        break;
                    }
                case 3:
                    {
                        send(stream, deleteFile(directory, fileName));
                        break;
                    }
                case 4:
                    {
                        send(stream, findFile(directory, fileName));
                        break;
                    }
                case 5:
                    {
                        send(stream, creationTimeFile(directory, fileName));
                        break;
                    }
                case 6:
                    {
                        send(stream, SetCreationTimeFile(directory, fileName, text));
                        break;
                    }

                case 7:
                    {
                        send(stream, modificationTimeFile(directory, fileName));
                        break;
                    }

                case 8:
                    {
                        send(stream, setModificationTimeFile(directory, fileName, text));
                        break;
                    }
                case 9:
                    {
                        send(stream, numberDirInDir(directory));
                        break;
                    }
                case 10:
                    {
                        send(stream, numberFilesInDir(directory));
                        break;
                    }
                case 11:
                    {
                        send(stream, "Size of directory: " + dirSize(directory).ToString());
                        break;
                    }
                    


                default:
                    {
                        send(stream, "SOMETHING WRONG");
                        break;
                    }
                   
            }

        }


        private void send(NetworkStream stream, String message)
        {

            byte[] data = Encoding.UTF8.GetBytes(message);
            
            // отправка сообщения
            stream.Write(data, 0, data.Length);
            Console.WriteLine("Sent: {0}", message);
           

        }
        

        //показать все папки директории
        public static String showDirInDir(string dirName)
        {
            string[] dirs = null;

            if (Directory.Exists(dirName))
            {
                dirs = Directory.GetDirectories(dirName);
            }

            StringBuilder builder = new StringBuilder();
            
            for (int n = 0; n < dirs.Length; n++)
            {
                builder.Append(dirs[n]);
                builder.Append(Environment.NewLine);
            }
            
            return "Directories:" + Environment.NewLine + builder.ToString();
        }


        //размер файла
        public static String sizeFile(string dirName, string name)
        {
            String rezult = "SOMETHING WRONG";
            if (File.Exists(dirName + name))
            {
                long fileSize = new FileInfo(dirName + name).Length;
                rezult = "Size of file: " + fileSize.ToString() + " bytes;";
            }
            return rezult;
            
        }


        //создать файл
        public static String createFile(string dirName, string name, string text)
        {
            String rezult = "SOMETHING WRONG";

            if (Directory.Exists(dirName))
            {

                File.AppendAllText(dirName + name, text);
                rezult = "File " + name + " created;";
                

            }

            return rezult;

        }

        //удалить файл
        public static String deleteFile(string dirName, string name)
        {
            String rezult = "FILE DOES NOT EXIST";

            

            if (File.Exists(dirName + name))
            {
                File.Delete(dirName + name);
                rezult = "File " + name + " deleted;";
            }

            return rezult;

        }

        //поиск файла
        public static String findFile(string dirName, string name)
        {
            String rezult = "File not found";
            
            if  (File.Exists(dirName + name))
            {
                rezult = "File " + name + " in " + dirName + " exist;";

            }

            return rezult;
        }

        //время создания файла
        public static String creationTimeFile(string dirName, string name)
        {
            String rezult = "File not found";

            if (File.Exists(dirName + name))
            {
                rezult = "File creation time: ";
                rezult += File.GetCreationTime(dirName + name);
             
            }

            return rezult;
        }

        //изменение времени создания
        public static String SetCreationTimeFile(string dirName, string name, string text)
        {
            String rezult = "File not found";

            if (File.Exists(dirName + name))
            {
                string [] date = text.Split('.');

                File.SetCreationTime(dirName + name, new DateTime(Convert.ToInt16(date[2]), Convert.ToInt16(date[1]), Convert.ToInt16(date[0])));
                rezult = "Creation time changed.";
            
            }

            return rezult;
        }

        //время изменения файла
        public static String modificationTimeFile(string dirName, string name)
        {
            String rezult = "File not found";

            if (File.Exists(dirName + name))
            {
                rezult = "Last modification time: ";
                rezult += File.GetLastWriteTime(dirName + name);

            }

            return rezult;
        }

        //изменение  времени изменения файла
        public static String setModificationTimeFile(string dirName, string name, string text)
        {
            String rezult = "File not found";

            if (File.Exists(dirName + name))
            {
                string[] date = text.Split('.');

                File.SetLastWriteTime(dirName + name, new DateTime(Convert.ToInt16(date[2]), Convert.ToInt16(date[1]), Convert.ToInt16(date[0])));
                rezult = "Modification time changed.";

            }

            return rezult;
        }

        //количество папок в директории
        public static String numberDirInDir(string dirName)
        {
            string rezult = "SOMETHING WRONG";
            string[] dirs = null;

            

            if (Directory.Exists(dirName))
            {
                dirs = Directory.GetDirectories(dirName);
                rezult = "The number of directories in the directory - ";
                rezult += Convert.ToString(dirs.Length);
                rezult += ";";
            }
            
            return rezult;
        }

        //количество файлов в директории
        public static String numberFilesInDir(string dirName)
        {
            string rezult = "SOMETHING WRONG";
            string[] dirs = null;

            if (Directory.Exists(dirName))
            {
                dirs = Directory.GetFiles(dirName);
                rezult = "The number of directories in the directory - ";
                rezult += Convert.ToString(dirs.Length);
                rezult += ";";
            }

            return rezult;
        }

        //размер каталога
        public long dirSize(string dirName)
        {
            long size = 0;

            if (Directory.Exists(dirName))
            {
                DirectoryInfo DrInfo = new DirectoryInfo(dirName);
                DirectoryInfo[] folder = DrInfo.GetDirectories();
                FileInfo[] Fi = DrInfo.GetFiles();

                foreach (FileInfo fl in Fi)
                {
                    size += fl.Length;
                }

                for (int j = 0; j < folder.Length; j++)
                {
                    size += dirSize(dirName + "\\" + folder[j].Name);
                }
            }

            return size;
        }
        
    }
    
}
