// Copyright (c) 2026 J-Alz
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TraceDebug
{
    internal partial class Core
    {

        public string TraceXml<T>(T obj)
        {
            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(T));
            var settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = Encoding.UTF8,
                OmitXmlDeclaration = false
            };

            using (var sw = new StringWriter())
            using (var writer = XmlWriter.Create(sw, settings))
            {
                serializer.Serialize(writer, obj);
                sb.Append(sw.ToString());
            }
            return sb.ToString();
        }

        public string TraceXml<T>(T Tclass, bool indent = false, bool omit_declaration = true)
        {
            StringBuilder sb = new StringBuilder();
            //no se usa StringWriter produce UTF-16
            var serializer = new XmlSerializer(typeof(T));

            var settings = new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(false),//evita el BOM
                Indent = indent,
                OmitXmlDeclaration = omit_declaration,
            };

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            using (var ms = new MemoryStream())
            using (var writer = XmlWriter.Create(ms, settings))
            {
                serializer.Serialize(writer, Tclass, ns);
                sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
                return sb.ToString();
            }
        }

    }
}
