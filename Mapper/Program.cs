using MapperSqlite;
using System;
using System.Diagnostics;

namespace Mapper
{
    class Program
    {
        static void Main(string[] args)
        {
            //OldTest();
            string configFile = @"../../../Extras/mapperconfig.yaml";

            var etl =ETLFactory.GetETL("flatfilecompany", configFile);
            etl.GenerateOutput();
        }

        //public static void OldTest()
        //{
        //    Console.Write("Configuring database...   ");
        //    Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();

        //    string configFile = @"C:\code\Mapper\Mapper\Extras\mapperconfig.yaml";
        //    var manager = new FlatfileExtractorManager(configFile);
        //    manager.ConfigToTable();
        //    stopwatch.Stop();
        //    Console.WriteLine($"[OK] {stopwatch.ElapsedMilliseconds}ms");

        //    Console.Write("Extracting data...        ");
        //    stopwatch.Restart();
        //    manager.GetData();
        //    stopwatch.Stop();
        //    Console.WriteLine($"[OK] {stopwatch.ElapsedMilliseconds}ms");

        //    Console.Write("Validating FK integrity...");
        //    Console.WriteLine(manager.IntegrityCheck());

        //    stopwatch.Restart();
        //    Console.Write("Starting transform...     ");
        //    FlatfileToXmlTransformModule transformManager = new FlatfileToXmlTransformModule(databaseName);
        //    var data = transformManager.TransformData();
        //    stopwatch.Stop();
        //    Console.WriteLine($"[OK] {stopwatch.ElapsedMilliseconds}ms");

        //    Console.Write("Writing Xml...            ");
        //    stopwatch.Restart();
        //    transformManager.MakeXml(data, "C:/code/Mapper/Mapper/Extras/output.xml");
        //    stopwatch.Stop();
        //    Console.WriteLine($"[OK] {stopwatch.ElapsedMilliseconds}ms");
        //}
    }
}
