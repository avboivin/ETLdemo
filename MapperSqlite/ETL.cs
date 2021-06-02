using MapperSqlite.Config;
using MapperSqlite.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace MapperSqlite
{
    public abstract class ETL<ExtractModule, TransformModule, LoadModule> : IETL
        where ExtractModule : IExtractModule
        where TransformModule : ITransformModule
        where LoadModule : ILoadModule
    {
        public ETL(string ConfigFilePath)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(typeof(IExtractModule), typeof(ExtractModule));
            serviceCollection.AddSingleton(typeof(ITransformModule), typeof(TransformModule));
            serviceCollection.AddSingleton(typeof(ILoadModule), typeof(LoadModule));
            serviceCollection.AddSingleton(GetConfig(ConfigFilePath));
            provider = serviceCollection.BuildServiceProvider();
        }

        ServiceProvider provider;

        public virtual void GenerateOutput() {
            var extractor = provider.GetService<IExtractModule>();
            var transformer = provider.GetService<ITransformModule>();
            var loader = provider.GetService<ILoadModule>();

            extractor.GetData();
            var data = transformer.TransformData();
            loader.OutputData(data, "../../../Extras/output.xml");
        }

        private MapperConfig GetConfig(string ConfigFilePath)
        {
            MapperConfig mapperConfig = new MapperConfig();
            var deserializer = new DeserializerBuilder()
    .WithNamingConvention(UnderscoredNamingConvention.Instance)
    .Build();
            FileInfo yamlTableConfig = new FileInfo(ConfigFilePath);
            string yaml = yamlTableConfig.OpenText().ReadToEnd();
            return deserializer.Deserialize<MapperConfig>(yaml);
        }
    }
}
