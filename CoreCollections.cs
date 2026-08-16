// Copyright (c) 2026 J-Alz
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Text;

namespace TraceDebug
{
    internal partial class Core
    {
        public string TraceArray(object[] array, string name = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"ARRAY: {name}");
            int pos = 0;
            foreach (object element in array)
            {
                sb.AppendLine($"{pos:D2}.[{element.ToString().Length:D3}] {element}");
                pos++;
            }
            return sb.ToString();
        }

        //No funciona para clases
        public string TraceList<T>(List<T> list, string name = "") where T : class
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"CDU. LIST {name}");
            foreach (T item in list)
            {
                sb.AppendLine(this.DumpBlock(item));
            }
            return sb.ToString();
        }

        public string TraceList(List<object> list, string name = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"CDU. LIST {name}");
            int n = 0;
            foreach (object str in list)
            {
                sb.AppendLine($"{n:D2}|{str}");
                n++;
            }
            return sb.ToString();
        }

        public string TraceList(List<string> list, string name = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"CDU. LIST {name}");
            int n = 0;
            foreach (string str in list)
            {
                sb.AppendLine($"{n:D2}|{str}");
                n++;
            }
            return sb.ToString();
        }


    }
}
