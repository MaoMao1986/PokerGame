using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IConfigRow
{
    string Id { get; }
    void ParseDataRow(string[] p_dataRowString, string[] p_Type);
}

