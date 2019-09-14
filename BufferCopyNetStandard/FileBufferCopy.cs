using System;
using System.IO;
using System.Diagnostics;
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
                    {
                        //-- Uncomment this to log to console
                        //Console.Write($"[*] Copying buffer {bytesRead} ...........");

                        Trace.Write($"[*] Copying buffer {bytesRead} ..........."); 
                    }
                    try
                    {
                        using (FileStream fs = new FileStream(newFileName, FileMode.Append, FileAccess.Write))
                        {
                            fs.Write(buffer, 0, buffer.Length);
                            fs.Close();
                        }
                        if (logging)
                        {
                            //-- Uncomment this to log to console
                            //Console.WriteLine(" [OK]");

                            Trace.WriteLine(" [OK]");
                        }
                    }
                    catch (Exception ex)
                    {
                        if (logging)
                        {
                            //-- Uncomment this to log to console
                            //Console.WriteLine(" [OK]");

                            Trace.WriteLine(" [ERROR]");
                        }

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
                //-- Uncomment this to log to console
                //Console.WriteLine(ex);

                Trace.WriteLine(ex);

                throw ex;
            }
        }
        public void CopyData(string sourceFileName, long bufferSize, string newFileName, bool logging = false)
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
                        {
                            //-- Uncomment this to log to console
                            //Console.Write($"[*] Copying buffer {bytesRead} ...........");

                            Trace.Write($"[*] Copying buffer {bytesRead} ...........");
                        }
                        try
                        {
                            using (FileStream fs = new FileStream(newFileName, FileMode.Append, FileAccess.Write))
                            {
                                fs.Write(buffer, 0, buffer.Length);
                                fs.Close();
                            }
                            if (logging)
                            {
                                //-- Uncomment this to log to console
                                //Console.WriteLine(" [OK]");

                                Trace.WriteLine(" [OK]");
                            }
                        }
                        catch (Exception ex)
                        {
                            if (logging)
                            {
                                //-- Uncomment this to log to console
                                //Console.WriteLine(" [OK]");

                                Trace.WriteLine(" [ERROR]");
                            }

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
                //-- Uncomment this to log to console
                //Console.WriteLine(ex);

                Trace.WriteLine(ex);

                throw ex;
            }
        }

    }
}
