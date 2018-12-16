namespace SliceFile
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class StartUp
    {
        public static void Main()
        {
            var filePath = Console.ReadLine();
            var destinationPath = Console.ReadLine();
            var pieces = int.Parse(Console.ReadLine());

            SliceAsync(filePath, destinationPath, pieces);

            Console.WriteLine("Anything else");
            while (true)
            {
                Console.ReadLine();
            }
        }

        private static void SliceAsync(string filePath, string destinationPath, int pieces)
        {
            Task.Run(() =>
            {
                SliceTheFile(filePath, destinationPath, pieces);
            });
        }

        private static void SliceTheFile(string filePath, string destinationPath, int pieces)
        {
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            using (var source = new FileStream(filePath, FileMode.Open))
            {
                var fileInfo = new FileInfo(filePath);

                long sliceLength = (source.Length / pieces) + 1;
                long currentByte = 0;

                for (int i = 1; i <= pieces; i++)
                {
                    var fileName = string.Format($"{destinationPath}/Slice-{i}{fileInfo.Extension}");

                    using (var destination = new FileStream(fileName, FileMode.Create))
                    {
                        var buffer = new byte[2 * 1024];
                        while (currentByte <= sliceLength * i)
                        {
                            var readBytesCount = source.Read(buffer, 0, buffer.Length);
                            if (readBytesCount == 0)
                            {
                                break;
                            }

                            destination.Write(buffer, 0, readBytesCount);
                            currentByte += readBytesCount;
                        }
                    }
                }
            }

            Console.WriteLine("Slice complete.");
        }
    }
}