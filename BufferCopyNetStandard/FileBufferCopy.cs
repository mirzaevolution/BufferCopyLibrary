using System;
using System.IO;
namespace BufferCopyNetStandard
{
    public class FileBufferCopy
    {
        public void CopyData(byte[] sourceBuffer, long bufferSize, string newFileName, bool logging = false)
        {
            try
            {
                long sourceSize = sourceBuffer.Length;
                if (bufferSize >= sourceSize)
                {
                    bufferSize = sourceSize / 2;
                }
                long bytesRead = bufferSize;
                int index = 0;
                do
                {
                    byte[] buffer = new byte[bufferSize];
                    int currentIndex = 0;
                    for (; index < bytesRead; index++)
                    {
                        buffer[currentIndex++] = sourceBuffer[index];
                    }
                    if (logging)
                        Console.Write($"[*] Copying buffer {bytesRead} ...........");
                    try
                    {
                        using (FileStream fs = new FileStream(newFileName, FileMode.Append, FileAccess.Write))
                        {
                            fs.Write(buffer, 0, buffer.Length);
                            fs.Close();
                        }
                        if (logging)
                            Console.WriteLine(" [OK]");
                    }
                    catch (Exception ex)
                    {
                        if (logging)
                            Console.WriteLine(" [ERROR]");

                        throw ex;
                    }
                    if ((sourceSize - bytesRead) > bufferSize)
                    {
                        bytesRead += bufferSize;
                    }
                    else
                    {
                        bufferSize = sourceSize - bytesRead;
                        bytesRead = sourceSize;
                    }

                } while (index < sourceSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void CopyFileWithBuffer(string sourceFileName, long bufferSize, string newFileName, bool logging = false)
        {
            try
            {

                byte[] sourceBuffer = null;
                try
                {
                    using (FileStream fs = new FileStream(sourceFileName, FileMode.Open, FileAccess.Read))
                    {
                        sourceBuffer = new byte[fs.Length];
                        fs.Read(sourceBuffer, 0, sourceBuffer.Length);
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                if(sourceBuffer!=null)
                {

                    long sourceSize = sourceBuffer.Length;
                    if (bufferSize >= sourceSize)
                    {
                        bufferSize = sourceSize / 2;
                    }
                    long bytesRead = bufferSize;
                    int index = 0;
                    do
                    {
                        byte[] buffer = new byte[bufferSize];
                        int currentIndex = 0;
                        for (; index < bytesRead; index++)
                        {
                            buffer[currentIndex++] = sourceBuffer[index];
                        }
                        if (logging)
                            Console.Write($"[*] Copying buffer {bytesRead} ...........");
                        try
                        {
                            using (FileStream fs = new FileStream(newFileName, FileMode.Append, FileAccess.Write))
                            {
                                fs.Write(buffer, 0, buffer.Length);
                                fs.Close();
                            }
                            if (logging)
                                Console.WriteLine(" [OK]");
                        }
                        catch (Exception ex)
                        {
                            if (logging)
                                Console.WriteLine(" [ERROR]");

                            throw ex;
                        }
                        if ((sourceSize - bytesRead) > bufferSize)
                        {
                            bytesRead += bufferSize;
                        }
                        else
                        {
                            bufferSize = sourceSize - bytesRead;
                            bytesRead = sourceSize;
                        }

                    } while (index < sourceSize);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}
