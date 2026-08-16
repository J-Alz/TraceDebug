// Copyright (c) 2026 J-Alz
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Text;

namespace TraceDebug
{
    internal partial class Core
    {

        public string TraceException(Exception e, string name = "")
        {
            StringBuilder sb = new StringBuilder($"Exception {name} {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n");
            sb.AppendLine($"TIPO...{e.GetType().FullName}");
            sb.AppendLine($"MENSAJE...{e.Message}");
            sb.AppendLine($"Pila:{e.StackTrace}");
            if (e.InnerException != null)
            {
                sb.AppendLine($"INTERNA: \n {e.InnerException.Message}");
            }

            return sb.ToString();
        }


    }
}
