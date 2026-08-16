// Copyright (c) 2026 J-Alz
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics;
using System.IO;

namespace TraceDebug
{
    public class Dial
    {
        private Stopwatch _watch;
        private int id = 0;
        private string _path = string.Empty;
        private bool _valida = false;

        public Dial(string path = "")
        {
            if (string.IsNullOrWhiteSpace(path))
                _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                _path = path;

            _path = Path.Combine(_path,"Dial.txt");
        }

        public void Start()
        {
            _valida = true;
            id++;
            _watch = new Stopwatch();

            Directory.CreateDirectory(_path);

            File.AppendAllText(_path, $"RELOJ {id}°: INICIADO → {DateTime.Now}" + Environment.NewLine);

            _watch.Start();
        }


        public void Stop()
        {
            if (_valida)
            {
                _watch?.Stop();
                TimeSpan ts = _watch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                File.AppendAllText(_path, $"{id}°: Tiempo transcurrido: {elapsedTime}" + Environment.NewLine);
            }
        }



    }
}
