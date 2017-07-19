using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SDL2;

namespace SDL2_STD.TESTS
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
			SDL.SDL_Init(SDL.SDL_INIT_VIDEO);
        }
    }
}
