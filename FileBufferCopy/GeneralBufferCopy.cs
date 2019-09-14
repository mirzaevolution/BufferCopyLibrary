using System;
using System.Collections.Generic;

namespace BufferCopyCore
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
                        Console.Write($"[*] Copying buffer {bytesRead} ...........");
                    try
                    {
                        bufferItem.Id = Guid.NewGuid().ToString("n");
                        saveCallback?.Invoke(bufferItem);
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

    }
}
