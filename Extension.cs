// Copyright (c) 2026 J-Alz
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace TraceDebug
{
    public static class Extension
    {
        public static string _env = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string _path = Path.Combine(_env,"Debug");

        private static void CreateDocument(string name, string content, bool reset = false)
        {
            string path = Path.Combine(_path, name);

            if (reset)
            {
                File.Delete(path);
            }

            File.AppendAllText(path, content);
        }

        //Data
        public static void TraceRows(this DataRow[] rows, bool reset = true)
        {
            string content = new Core().TraceRows(rows);
            CreateDocument("data.txt",content,reset);
        } 

        public static void TraceRow(this DataRow row, bool reset = true)
        {
            string content = new Core().TraceRow(row);
            CreateDocument("data.txt", content, reset);
        } 

        public static void TraceTable(this DataTable dt, bool reset = true)
        {
            string content = new Core().TraceTable(dt);
            CreateDocument("data.txt", content, reset);
        } 

        public static void TraceSet(this DataSet ds,bool reset = true)
        {
            string content = new Core().TraceSet(ds);
            CreateDocument("data.txt", content, reset);
        }

        public static void TraceReader(this IDataReader dr,bool reset = true)
        {
            string content = new Core().TraceReader(dr);
            CreateDocument("data.txt", content, reset);
        } 

        //Exception
        public static void TraceException(this Exception e,bool reset = true)
        {
            string content = new Core().TraceException(e);
            CreateDocument("exception.txt", content, reset);
        } 

        //Types
        public static void TraceString(this string str,bool reset = true)
        {
            string content = new Core().TraceLines(str);
            CreateDocument("types.txt", content, reset);
        } 

        public static void TraceChars(this string str,bool reset = true)
        {
            string content = new Core().TraceChars(str);
            CreateDocument("types.txt", content, reset);
        } 

        public static void TraceBytes(this byte[] bytes,bool reset = true)
        {
            string content = new Core().TraceBytes(bytes);
            CreateDocument("types.txt", content, reset);
        } 

        public static void TraceSizeBytes(this object obj,bool reset = true)
        {
            string content = new Core().TraceSizeBytes(obj);
            CreateDocument("types.txt", content, reset);
        } 

        //Collections
        public static void TraceArray(this object[] array,bool reset = true)
        {
            string content = new Core().TraceArray(array);
            CreateDocument("collections.txt", content, reset);
        }

        public static void TraceListT<T>(List<T> list, bool reset = true) where T : class
        {
            string content = new Core().TraceList(list);
            CreateDocument("collections.txt", content, reset);
        }

        public static void TraceListObject(List<object> list,bool reset = true)
        {
            string content = new Core().TraceList(list);
            CreateDocument("collections.txt", content, reset);
        }

        public static void TraceListString(List<string> list,bool reset = true)
        {
            string content = new Core().TraceList(list);
            CreateDocument("collections.txt", content, reset);
        }

        //XML
        public static void TraceXml<T>(T obj,bool reset = true)
        {
            string content = new Core().TraceXml(obj);
            CreateDocument("result.xml", content, reset);
        }

        public static void TraceXml<T>(T Tclass, bool reset = true,bool indent = false, bool omit_declaration = true)
        {
            string content = new Core().TraceXml(Tclass,indent,omit_declaration);
            CreateDocument("result.xml", content, reset);
        }

        //Models
        public static void TraceModelList<T>(this List<T> list,bool reset = true)
        {
            string content = new Core().DumpLineList(list.Select(x => (object)x).ToList());
            CreateDocument("models.txt", content, reset);
        }

        public static void TraceModel(this object model,bool reset = true)
        {
            string content = new Core().DumpObj(model);
            CreateDocument("models.txt", content, reset);
        }

    }
}
