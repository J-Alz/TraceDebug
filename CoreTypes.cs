// Copyright (c) 2026 J-Alz
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace TraceDebug
{
    internal partial class Core
    {
        public string TraceLines(string str)
        {
            StringBuilder sb = new StringBuilder();
            string[] lines = str.Split('\n');
            foreach (string line in lines)
            {
                sb.AppendLine($"{line} -> {line.Length}");
            }

            return sb.ToString();
        }

        public string TraceChars(string str)
        {
            StringBuilder sb = new StringBuilder();
            char[] chares = str.ToCharArray();
            for (int pos = 0; pos < chares.Length; pos++)
            {
                sb.AppendLine($"{pos:D3}| {chares[pos]}");
            }
            return sb.ToString();
        }

        public string TraceBytes(byte[] valor)
        {
            StringBuilder sb = new StringBuilder();
            if (valor is byte[] datos && datos.Length > 0)
            {
                sb.AppendLine(string.Join(" ", datos.Take(10).Select(b => b.ToString("X2"))));
            }
            return sb.ToString();
        }

        public string TraceSizeBytes(object obj)
        {
            StringBuilder sb = new StringBuilder();
            using (var stream = new MemoryStream())
            {
                //No se recomienda el uso de BinaryFormatter por seguridad, ojo
                //por esas razones se hace uso solo dentro de DEBUG
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);

                long size = stream.Length;
                long kb = size / 1024;
                long mb = kb / 1024;
                long gb = mb / 1024;

                sb.AppendLine($"SIZE IN {obj.GetType()}");
                sb.AppendLine($"Bytes......: {size,12:N0}");
                sb.AppendLine($"KiloBytes..: {kb,12:N0}");
                sb.AppendLine($"MegaBytes..: {mb,12:N0}");
                sb.AppendLine($"GigaBytes..: {gb,12:N0}");

            }
            return sb.ToString();
        }





    }
}
