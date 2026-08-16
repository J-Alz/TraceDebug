// Copyright (c) 2026 J-Alz
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TraceDebug
{
    internal partial class Core
    {
        public string DumpBlock<T>(T model) where T : class
        {
            StringBuilder sb = new StringBuilder();

            if (model == null)
            {
                sb.AppendLine($"{model.GetType().Name} is null");
                return sb.ToString();
            }

            sb.AppendLine($"[{model.GetType().Name.ToUpper()}]");

            var props = model.GetType().GetProperties();
            int max = props.Max(x => x.Name.Length);
            foreach(var prop in props)
            {
                sb.Append($"  {prop.Name.PadRight(max, '.')}..: ");
                var type = prop.PropertyType;
                object value = prop.GetValue(model, null);

                switch (type)
                {
                    case Type t when t == typeof(string):
                        sb.Append($"{prop.GetValue(model)}");
                        break;
                    case Type t when Nullable.GetUnderlyingType(t) != null:
                        sb.Append($"{prop.GetValue(model)}");
                        break;
                    case Type t when t.IsGenericType && t.GetGenericTypeDefinition() == typeof(List<>):
                        //if (prop.PropertyType.IsGenericType)
                        //if (prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                        //Type type2 = prop.PropertyType.GetGenericArguments()[0];
                        //var lista = prop.GetValue(model) as System.Collections.IEnumerable;
                        //if (lista != null)
                        //{
                        //    foreach (var element in lista)
                        //    {
                        //        result += $"{element}";
                        //    }
                        //}
                        break;
                    case Type t when t.IsArray:
                        var arrayValue = prop.GetValue(model) as Array;
                        if (arrayValue != null)
                        {
                            sb.Append(
                                string.Join(",",arrayValue
                                .Cast<object>()
                                .Select(x => x?.ToString()))
                                );
                        }
                        break;
                    case Type t when t == typeof(byte[]):
                        var valor = prop.GetValue(model);
                        if (valor is byte[] datos && datos.Length > 0)
                        {
                            sb.Append($"{string.Join(" ", datos.Take(10).Select(b => b.ToString("X2")))}");

                        }
                        break;
                    default:
                        sb.Append($"{prop.GetValue(model)}");
                        break;
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        public string DumpLineList(List<object> list)
        {
            string token = "";
            try
            {
                if (list == null) return "ERROR";

                if (list.Count > 0)
                {
                    AddTitles(_sb, list[0]);
                }

                int n = 0;
                foreach (var unit in list)
                {
                    var props = unit.GetType().GetProperties();
                    token = unit.GetType().Name;
                    string result = TraceProperty(unit, maxis, 0, n);
                    _sb.AppendLine(result);
                    n++;
                }
            }
            catch (Exception e)
            {
                //_sb.Append(e.DumpException() + $"{token}>{_token}");
            }

            return _sb.ToString();
        }

        public string DumpObj(object obj)
        {
            string token = "";
            try
            {
                if (obj == null) return "ERROR";

                var props = obj.GetType().GetProperties();
                Console.WriteLine($"CHK → {obj.GetType()}");
                AddTitles(_sb, obj);
                string result = TraceProperty(obj, maxis, 0, 0);
                _sb.Append(result);
                
            }
            catch (Exception e)
            {
                //_sb.Append(e.DumpException() + $"{token}>{_token}");
            }

            return _sb.ToString();
        }

    }
}
