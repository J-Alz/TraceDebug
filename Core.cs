// Copyright (c) 2026 J-Alz
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TraceDebug
{
    internal partial class Core
    {
        StringBuilder _sb = new StringBuilder();
        string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        Dictionary<string,int> maxis = new Dictionary<string, int>();

        string _token { get; set; } = "";

        public Core()
        {
            _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Directory.CreateDirectory(_path);
        }

        public Dictionary<string, int> AddTitles(StringBuilder sb, object obj,int tabs = 0)
        {
            tabs = Math.Abs(tabs);

            sb.AppendLine();
            sb.AppendLine($"{new string('\t', tabs)}{obj.GetType().Name}");
            sb.Append($"{new string('\t', tabs)}MDCU. |");
            foreach (var prop in obj.GetType().GetProperties())
            {
                maxis[prop.Name] = prop.Name.Length;

                int max1 = prop.Name.Length;
                int max2 = 0;
                max2 = $"{prop.GetValue(obj)}".Length;

                maxis[prop.Name] = (max1 > max2) ? max1 : max2;
                sb.Append($" {prop.Name.PadRight(maxis[prop.Name], ' ')} |");
            }
            sb.AppendLine();
            return maxis;
        }

        public string TraceProperty(object obj, Dictionary<string, int> maxis,int tabs = 0, int n = 0)
        {
            var sb2 = new StringBuilder();
            var sb = new StringBuilder();
            try
            {
                var props = obj.GetType().GetProperties();
                tabs = Math.Abs(tabs);
                sb.Append($"{new string('\t', tabs)}{n:D4}. |");
                foreach (var prop in props)
                {
                    _token = $"{prop.Name} => {prop.PropertyType}";
                    var type = prop.PropertyType;
                    string result = string.Empty;

                    switch (type)
                    {
                        case Type t when t == typeof(string):
                            result = $"{prop.GetValue(obj)}";
                            result = result.Replace("\n", " ").Replace("\r\n", " ").Replace("\r", " ");
                            break;
                        case Type t when Nullable.GetUnderlyingType(t) != null:
                            result = $"{prop.GetValue(obj)}";
                            break;
                        case Type t when t == typeof(List<string>):
                            result = $"{t.GetGenericArguments()[0]}";
                            if (prop.GetValue(obj) is IEnumerable enumerable1)
                            {
                                result += $"({enumerable1.Cast<object>().Count()})";
                            }
                                //string tot = string.Join(",", prop.GetValue(obj));
                                //sb2.AppendLine("LIST");
                                //sb2.AppendLine(tot);
                            break;
                        case Type t when t.IsGenericType:
                            result = $"{t.GetGenericArguments()[0]}";

                            if (prop.GetValue(obj) is IEnumerable enumerable)
                            {
                                result += $"({enumerable.Cast<object>().Count()})";

                                int index = 0;
                                foreach (var item in enumerable)
                                {
                                    if (index == 0)
                                    {
                                        //sb2.Append();
                                        this.AddTitles(sb2, item, tabs + 1);
                                    }

                                    index++;
                                    string res = TraceProperty(item, maxis, tabs + 1, index);
                                    sb2.AppendLine(res);
                                }
                            }
                            break;
                        case Type t when t.IsArray:
                            var arrayValue = prop.GetValue(obj) as Array;
                            if (arrayValue != null)
                            {
                                result = string.Join(",", arrayValue
                                    .Cast<object>()
                                    .Select(x => x?.ToString()));
                            }
                            break;
                        case Type t when t == typeof(byte[]):
                            var valor = prop.GetValue(obj);
                            if (valor is byte[] datos && datos.Length > 0)
                            {
                                result = $"{string.Join(" ", datos.Take(10).Select(b => b.ToString("X2")))}";
                            }
                            break;
                        default:
                            result = $"{prop.GetValue(obj)}";
                            break;
                    }
                    int max = maxis[prop.Name];
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        if (result.Length > 3)
                        {
                            result = result.Length <= max ? result : result.Substring(0, max - 3) + "...";
                        }
                    }
                    sb.Append($" {result.PadRight(max, ' ')} |");
                }

                sb.Append(sb2.ToString());
            }
            catch(Exception e)
            {
                e.TraceException(true);
            }
            
            return sb.ToString();
        }

        



    }
}
