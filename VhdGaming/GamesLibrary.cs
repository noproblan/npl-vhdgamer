using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VhdGamer.Gaming
{
    public class GamesLibrary
    {
        private VhdStorage storage;

        public GamesLibrary(VhdStorage storage)
        {
            this.storage = storage;
        }

        public ICollection<String> GetGameNames()
        {
            return storage.GetVhdNames().Select(n => Path.GetFileNameWithoutExtension(n)).ToList();
        }

        public void Play(String filename)
        {
            VhdGame game = new VhdGame(storage.GetVhd(filename));
            game.Play();
        }

        public VhdStorage GetStorage()
        {
            return storage;
        }
    }
}
