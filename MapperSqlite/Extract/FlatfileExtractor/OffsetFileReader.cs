using MapperSqlite.Config;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperSqlite
{
    public class OffsetFileReader
    {
        private MapperConfig config;
        private SQLiteConnection dbConnection;
        public OffsetFileReader(MapperConfig config, SQLiteConnection dbConnection)
        {
            this.config = config;
            this.dbConnection = dbConnection;
        }

        public void LoadData()
        {
            foreach (var file in config.tables)
            {
                int lineCounter = 0;
                string line;
                using (StreamReader filereader = new StreamReader($"{config.configs.workdirectory}/{file.filename}"))
                using (var transaction = dbConnection.BeginTransaction())
                {
                    string columns = string.Join(',', file.columns.Select(x => x.name));
                    string columnsWithAlias = string.Join(',', file.columns.Select(x => "$" + x.name));
                    var command = dbConnection.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandText =
                    $@"INSERT INTO {file.tablename} ({columns})
                                VALUES ({columnsWithAlias})";
                    while ((line = filereader.ReadLine()) != null)
                    {
                        if (lineCounter == 0 && file.skip_header)
                        {
                            lineCounter++;
                            continue;
                        }

                        foreach (var column in file.columns)
                        {
                            var parameter = command.CreateParameter();
                            parameter.ParameterName = $"${column.name}";
                            parameter.Value = line.Substring(column.offset, column.length).Trim();
                            command.Parameters.Add(parameter);
                        }
                        command.ExecuteNonQuery();

                        lineCounter++;
                    }
                    transaction.Commit();
                }
            }
        }
    }
}
