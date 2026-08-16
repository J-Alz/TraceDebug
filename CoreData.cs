// Copyright (c) 2026 J-Alz
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TraceDebug
{
    internal partial class Core
    {

        public string TraceRow(DataRow row, string name = null)
        {
            StringBuilder sb = new StringBuilder($"-----ROW {name} -----");

            Dictionary<DataColumn, int> lengths = new Dictionary<DataColumn, int>();
            foreach (DataColumn col in row.Table.Columns)
            {
                int max = col.ColumnName.Length;
                max = Math.Max(max, row[col].ToString().Length);
                lengths[col] = max;
            }

            sb.Append(" MCDU. ");
            foreach (DataColumn col in row.Table.Columns)
            {
                sb.Append($"{col.ColumnName.PadRight(lengths[col] + 2)}");
            }
            sb.AppendLine();

            sb.Append("$ 0001| ");
            foreach (DataColumn col in row.Table.Columns)
            {
                sb.Append($"{row[col].ToString().PadRight(lengths[col] + 2)}");
            }
            sb.AppendLine();

            return sb.ToString();
        }

        public string TraceRows(DataRow[] rows, string name = null)
        {
            StringBuilder sb = new StringBuilder($"-----ROW {name} -----");
            Dictionary<DataColumn, int> lengths = new Dictionary<DataColumn, int>();
            DataRow row = rows[0];
            foreach (DataColumn col in row.Table.Columns)
            {
                int max = col.ColumnName.Length;
                max = Math.Max(max, row[col].ToString().Length);
                lengths[col] = max;
            }
            //Terminar de agregar para que se puedan escribir a partir de Datatable.Rows
            sb.Append(" MCDU. ");
            foreach (DataColumn col in row.Table.Columns)
            {
                sb.Append($"{col.ColumnName.PadRight(lengths[col] + 2)}");
            }
            sb.AppendLine();

            sb.Append("$ 0001| ");
            foreach (DataColumn col in row.Table.Columns)
            {
                sb.Append($"{row[col].ToString().PadRight(lengths[col] + 2)}");
            }
            sb.AppendLine();
            return sb.ToString();
        }

        public string TraceTable(DataTable dt, string name = null)
        {
            StringBuilder sb = new StringBuilder($"----- TABLE {name} -----");

            if (dt.Rows.Count == 0)
            {
                sb.AppendLine("TABLE ROWS 0");
                return sb.ToString();
            }

            Dictionary<DataColumn, int> lengths = new Dictionary<DataColumn, int>();
            foreach (DataColumn col in dt.Columns)
            {
                //Debo revisar el funcionamiento de esto
                int max = col.ColumnName.Length;
                foreach (DataRow row in dt.Rows)
                {
                    max = Math.Max(max, row[col].ToString().Length);
                }
                lengths[col] = max;
            }

            sb.Append(" MCDU. ");
            foreach (DataColumn col in dt.Columns)
            {
                sb.Append($"{col.ColumnName.PadRight(lengths[col] + 2)}");
            }
            sb.AppendLine("");

            //Revisar posibilidad de hacer uso de for sin each
            int count = 0;
            foreach (DataRow row in dt.Rows)
            {
                sb.Append($" {count:D4}| ");
                foreach (DataColumn col in dt.Columns)
                {
                    sb.Append($"{row[col].ToString().PadRight(lengths[col] + 2)}");
                }
                count++;
                sb.AppendLine("");
            }

            return sb.ToString();
        }

        public string TraceSet(DataSet ds)
        {
            StringBuilder sb = new StringBuilder("----- DATASET -----");
            foreach (DataTable dt in ds.Tables)
            {
                sb.AppendLine($"----- TABLE {dt.TableName} -----");

                Dictionary<DataColumn, int> lengths = new Dictionary<DataColumn, int>();
                foreach (DataColumn col in dt.Columns)
                {
                    //Debo revisar el funcionamiento de esto
                    int max = col.ColumnName.Length;
                    foreach (DataRow row in dt.Rows)
                    {
                        max = Math.Max(max, row[col].ToString().Length);
                    }
                    lengths[col] = max;
                }

                sb.Append(" MCDU. ");
                foreach (DataColumn col in dt.Columns)
                {
                    sb.Append($"{col.ColumnName.PadRight(lengths[col] + 2)}");
                }
                sb.AppendLine("");

                //Revisar posibilidad de hacer uso de for sin each
                int count = 0;
                foreach (DataRow row in dt.Rows)
                {
                    sb.Append($" {count:D4}| ");
                    foreach (DataColumn col in dt.Columns)
                    {
                        sb.Append($"{row[col].ToString().PadRight(lengths[col] + 2)}");
                    }
                    count++;
                    sb.AppendLine("");
                }
            }

            return sb.ToString();
        }

        public string TraceReader(IDataReader dr, string name = null)
        {
            //interfaz de DataReader y SqlDataReader
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"CDU. DATAREADER {name}");
            while (dr.Read())
            {
                int numCol = dr.FieldCount;
                for (int i = 0; i < numCol; i++)
                {
                    sb.Append($"\n{dr.GetName(i)}: {dr[i]} \t");
                }
                sb.AppendLine("");
            }

            return sb.ToString();
        }

    }
}
