using MapperSqlite.Config;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace MapperSqlite
{
    public class FlatfileExtractorManager:IExtractModule
    {
        private bool rebuild=true;
        private string databaseName= "MapperDb.sqlite";
        private MapperConfig config;

        private SQLiteConnection dbConnection;
        public FlatfileExtractorManager(MapperConfig config)
        {
            ConnectDatabase();
            this.config = config;
        }

        private void ConnectDatabase()
        {
            if (rebuild)            
                SQLiteConnection.CreateFile(databaseName);

            dbConnection = new SQLiteConnection($"Data Source={databaseName};");            
            dbConnection.Open();
        }

        /// <summary>
        /// Create the database tables from the config file.
        /// </summary>
        /// <param name="ConfigFilePath"></param>
        public void ConfigToTable()
        {
            string CreateTableScript = "";
            foreach(var table in config.tables)
            {
                CreateTableScript = $"{CreateTableScript} create table {table.tablename} ( \n";
                string ForeignKeySegment = "";
                foreach(var column in table.columns)
                {
                    if (!string.IsNullOrEmpty(column.references))
                    {//Foreign key
                        ForeignKeySegment += $" FOREIGN KEY({column.name}) REFERENCES {column.references.Replace('.','(')}),\n";
                    }

                    string columnType = "";
                    if (config.definitions.ContainsKey(column.type.Trim()))
                        columnType = config.definitions[column.type.Trim()];
                    else
                        columnType = column.type;
                    CreateTableScript += $" {column.name} {columnType},\n";
                }
                //Foreign key declaration go at the end of the create table statement on SQLITE
                CreateTableScript += ForeignKeySegment;
                
                //Fix the last trailing coma that will make the statement invalid.
                CreateTableScript = CreateTableScript.Remove(CreateTableScript.LastIndexOf(','), 1);

                //Closing the create table statement
                CreateTableScript += ");\n"; 
            }

            using (var command = new SQLiteCommand(CreateTableScript, dbConnection))
                command.ExecuteNonQuery();
        }

        public void GetData()
        {
            ConfigToTable();//Create the database tables

            //Fill in the data
            if (config.configs.delimitertypes== "offset")
            {
                var offsetReader = new OffsetFileReader(config, dbConnection);
                offsetReader.LoadData();
            }
        }

        public string IntegrityCheck()
        {
            string pragmaCheckCommand = "PRAGMA foreign_key_check;";
            string IntegrityCheckOutput = "[OK]";
            using (var command = new SQLiteCommand(pragmaCheckCommand, dbConnection))
            {
                try
                {
                    command.ExecuteScalar();
                }catch(Exception ex)
                {
                    IntegrityCheckOutput = ex.Message;
                }

            }

            return IntegrityCheckOutput;
        }
        
    }
}
