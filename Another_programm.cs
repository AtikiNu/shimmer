using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Xml.Linq;

//Задание 1
/*
namespace Test
{
    class Base
    {
        public static void Main()
        {
            var appConfig = new AppConfig();

            ConfigLoader.LoadConfig(appConfig);

            Console.WriteLine($"Setting1: {appConfig.Setting1}");
            Console.WriteLine($"Setting2: {appConfig.Setting2}");
            Console.WriteLine($"Setting3: {appConfig.Setting3}");
            Console.ReadKey();
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class ParamsAttribute : Attribute
        {
            public string IniFileName { get; }

            public ParamsAttribute(string iniFileName)
            {
                IniFileName = iniFileName;
            }
        }

        public static class ConfigLoader
        {
            public static void LoadConfig(object obj)
            {
                var properties = obj.GetType().GetProperties()
                    .Where(p => p.IsDefined(typeof(ParamsAttribute), false));

                foreach (var property in properties)
                {
                    var attribute = property.GetCustomAttribute<ParamsAttribute>();
                    var config = ReadConfig(attribute.IniFileName);
                    if (config.TryGetValue(property.Name, out var value))
                    {
                        property.SetValue(obj, Convert.ChangeType(value, property.PropertyType));
                    }
                }
            }

            private static Dictionary<string, string> ReadConfig(string filePath)
            {
                var config = new Dictionary<string, string>();

                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath)
                                    .Where(line => !string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"));
                    foreach (var line in lines)
                    {
                        var keyValue = line.Split('=');
                        if (keyValue.Length == 2)
                        {
                            config[keyValue[0].Trim()] = keyValue[1].Trim();
                        }
                    }
                }

                return config;
            }
        }

        public class AppConfig
        {
            [Params("config1.ini")]
            public string Setting1 { get; set; }

            [Params("config2.ini")]
            public int Setting2 { get; set; }

            [Params("config3.ini")]
            public bool Setting3 { get; set; }
        }
    }
}*/

//Задание 2
/*
namespace Test
{
    class Base
    {
        public static void Main()
        {
            var formData = new FormData
            {
                Age = 25,
                Score = 1100
            };

            ValidationHelper.Validate(formData);
            Console.ReadKey();
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class ValidateRangeAttribute : Attribute
        {
            public int Min { get; }
            public int Max { get; }

            public ValidateRangeAttribute(int min, int max)
            {
                Min = min;
                Max = max;
            }

            public bool IsValid(int value) => value >= Min && value <= Max;
        }

        public class FormData
        {
            [ValidateRange(1, 100)]
            public int Age { get; set; }

            [ValidateRange(0, 1000)]
            public int Score { get; set; }
        }

        public static class ValidationHelper
        {
            public static void Validate(object obj)
            {
                var properties = obj.GetType().GetProperties()
                    .Where(p => p.IsDefined(typeof(ValidateRangeAttribute), false));

                foreach (var property in properties)
                {
                    var attribute = property.GetCustomAttribute<ValidateRangeAttribute>();
                    var value = (int)property.GetValue(obj);
                    if (!attribute.IsValid(value))
                    {
                        Console.WriteLine($"Значение свойства {property.Name} выходит за пределы допустимого диапазона ({attribute.Min} - {attribute.Max})");
                    }
                    else
                    {
                        Console.WriteLine($"Значение свойства {property.Name} ({value}) находится в допустимом диапазоне.");
                    }
                }
            }
        }

    }
}*/


