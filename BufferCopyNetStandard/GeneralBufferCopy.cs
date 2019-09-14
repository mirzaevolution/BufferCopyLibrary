using System;
using System.Diagnostics;

namespace BufferCopyNetStandard
{
    public class GeneralBufferCopy
    {
        public void CopyData(byte[] sourceBuffer, long bufferSize, Action<BufferItem> saveCallback, bool logging = false)
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
                    BufferItem bufferItem = new BufferItem();
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

                        //Log to output
                        Trace.Write($"[*] Copying buffer {bytesRead} ...........");
                    }
                    try
                    {
                        bufferItem.Id = Guid.NewGuid().ToString("n");
                        saveCallback?.Invoke(bufferItem);
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
                            //Console.WriteLine(" [ERROR]");

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

    }
}
