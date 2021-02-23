using System;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace Engine
{
    public static class Screenshot
    {
        private static void SaveScreenshot(string filename, int windowWidth, int windowHeight)
        {
            var pixels = new byte[windowWidth * windowHeight * 3];

            GL.PixelStore(PixelStoreParameter.PackAlignment, 1);
            GL.ReadBuffer(ReadBufferMode.Front);
            GL.ReadPixels(0, 0, windowWidth, windowHeight, PixelFormat.Bgr, PixelType.UnsignedByte, pixels);

            var width = BitConverter.GetBytes((short) windowWidth);
            var height = BitConverter.GetBytes((short) windowHeight);
            byte[] header = {0,  0,  2,  0, 0, 0, 0, 0,  0, 0,  0, 0,  width[0], width[1], height[0], height[1], 24, 0b00001000};

            using (var fs = File.OpenWrite(filename))
            {
                fs.Write(header, 0, header.Length);
                fs.Write(pixels, 0, pixels.Length);
            }
        }

    }
}
