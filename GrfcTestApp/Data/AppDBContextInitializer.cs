using GrfcTestApp.Data.Entities;
using GrfcTestApp.Data.Entities.Engines;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrfcTestApp.Data
{
    public class AppDBContextInitializer
    {

        public static void Initialize(AppDBContext context, bool initTestData)
        {
            //Применение миграций к БД
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDBContext, Configuration>());


            if (initTestData)
            {
                DBContextErase(context);

                DBContextTestDataInit(context);

            }
        }

        /// <summary>
        /// Инициализация БД тестовыми данными
        /// </summary>
        /// <param name="context">Контекст БД</param>
        private static void DBContextTestDataInit(AppDBContext context)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            #region Записываем двигатели в БД
            var diesel = new DieselEngine { Name = "Дизельный двигатель" };
            var gas = new GasEngine { Name = "Бензиновый двигатель" };
            context.DieselEngines.Add(diesel);
            context.GasEngines.Add(gas);
            context.SaveChanges();
            #endregion

            #region Записываем работы в БД
            context.Operations.Add(new Operation { EngineType = diesel, Description = "ТО-1 дизельного двигателя" });
            context.Operations.Add(new Operation { EngineType = diesel, Description = "ТО-2 дизельного двигателя" });
            context.Operations.Add(new Operation { EngineType = diesel, Description = "ТО-3 дизельного двигателя" });
            context.Operations.Add(new Operation { EngineType = diesel, Description = "Какие то работы с дизельным двигателем" });
            context.Operations.Add(new Operation { EngineType = diesel, Description = "Еще какие то работы с дизельным двигателем" });

            context.Operations.Add(new Operation { EngineType = gas, Description = "ТО-1 бензинового двигателя" });
            context.Operations.Add(new Operation { EngineType = gas, Description = "ТО-2 бензинового двигателя" });
            context.Operations.Add(new Operation { EngineType = gas, Description = "ТО-3 бензинового двигателя" });
            context.Operations.Add(new Operation { EngineType = gas, Description = "Раскоксовка бензинового двигателя" });
            context.Operations.Add(new Operation { EngineType = gas, Description = "Промывка форсунок бензинового двигателя" });

            #endregion

            List<(string Mark, string Model)> carList = ModelsFromCsv();

            #region Формируем список уникальных марок автомобилей
            var marks = carList
                .Select(c => c.Mark)
                .Distinct()
                .Select(c => new CarMark { Name = c });
            #endregion

            #region Записываем модели и марки автомобилей в БД
            var models = carList
                .Join(marks,
                    c => c.Mark,
                    m => m.Name,
                    (c, m) => new CarModel
                    {
                        Name = c.Model,
                        CarMark = m,
                        EngineType = RandomEngine(context.EngineTypes.ToList())
                    }
                )
                .ToList<CarModel>();
            context.CarModels.AddRange(models);
            context.SaveChanges();
            #endregion

            #region Записываем регистрационные номера автомобилей

            var carModels = context.CarModels.ToList();

            var autos = Enumerable
                .Range(1, 1000)
                .Select(p => new Automobile
                {
                    RegistrationNumber = RegistrationNumberGenerator(p),
                    CarModel = RandomCarModel(carModels, p)
                })
                .ToList();

            context.Automobiles.AddRange(autos);
            context.SaveChanges();
            #endregion

            #region Заполняем список проведенных ТО

            DateTime date = new DateTime(2021, 01, 01);
            Random rnd = new Random();
            List<Maintenance> maintenances = new List<Maintenance>();
            List<Automobile> automobiles = context.Automobiles.ToList();
            List<Operation> allOperations = context.Operations.ToList();
            int days = 0;
            do
            {                
                //До 10 записей проведенных работ ежедневно
                for (int i = 0; i < rnd.Next(10); i++)
                {
                    Automobile newAuto = RandomAutomobile(automobiles, i);
                    ICollection<Operation> operations = RandomOperation(allOperations, newAuto.CarModel.EngineType, i);
                    maintenances.Add(new Maintenance
                    {
                        DateTime = date.AddDays(days),
                        Automobile = newAuto,
                        Operation = new List<Operation>(operations)
                    });
                }
                days++;
            } while (date.AddDays(days).Year == 2021);
            context.Maintenances.AddRange(maintenances);
            context.SaveChanges();
            #endregion

            stopwatch.Stop();
            var s = stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Очистка таблиц БД
        /// </summary>
        /// <param name="context">Контекст БД</param>
        private static void DBContextErase(AppDBContext context)
        {
            context.Maintenances.RemoveRange(context.Maintenances);
            context.Operations.RemoveRange(context.Operations);
            context.Automobiles.RemoveRange(context.Automobiles);
            context.CarModels.RemoveRange(context.CarModels);
            context.CarMarks.RemoveRange(context.CarMarks);
            context.EngineTypes.RemoveRange(context.EngineTypes);

            context.SaveChanges();
        }

        /// <summary>
        /// Чтение тестовых данных по моделям автомобилей из подготовленного файла
        /// </summary>
        /// <returns>Список марок и моделей автомобилей</returns>
        private static List<(string Mark, string Model)> ModelsFromCsv()
        {
            List<(string Mark, string Model)> result = new List<(string Mark, string Model)>();

            string[] car;

            using (FileStream fs = new FileStream("Data/TestData/cars.csv", FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    car = sr.ReadLine().Split(';');
                    result.Add((car[0], car[1]));
                }
            }
            return result;
        }

        /// <summary>
        /// Функция возвращает случайный двигатель из списка
        /// </summary>
        /// <param name="engines">Список двигателей</param>
        /// <returns>Двигатель</returns>
        private static EngineBase RandomEngine(List<EngineBase> engines)
        {
            Random rnd = new Random();
            return engines[rnd.Next(engines.Count)];
        }

        /// <summary>
        /// Функция генерирует случайные регистрационный номер автомобиля
        /// </summary>
        /// <returns>Регистрационный номер автомобиля</returns>
        private static string RegistrationNumberGenerator(int seed)
        {

            StringBuilder regNum = new StringBuilder(15);
            Random rnd = new Random(seed);
            regNum.AppendFormat("{0} {1:d3} {2}{3} {4:d2}RUS",
                GetNextChar(seed++),
                rnd.Next(1, 999),
                GetNextChar(seed++),
                GetNextChar(seed),
                rnd.Next(10, 199));
            return regNum.ToString();
        }

        /// <summary>
        /// Функция возвращает случайную модель автомобяля из списка
        /// </summary>
        /// <param name="carModels">Список моделей автомобилей</param>
        /// <returns>Модель автомобиля</returns>
        private static CarModel RandomCarModel(List<CarModel> carModels, int seed)
        {
            Random rnd = new Random(seed);
            return carModels[rnd.Next(carModels.Count)];
        }

        /// <summary>
        /// Генерация символа для регистрационного номера (из числа разрешенных)
        /// </summary>
        /// <param name="seed"></param>
        /// <returns>Очередной символ</returns>
        private static char GetNextChar(int seed)
        {
            Random rnd = new Random(seed);
            char c;
            do
            {
                c = (char)rnd.Next(0x0410, 0x42F);
            } while (!"АВЕКМНОРСТУХ".Contains(c)); //Буквы используемые в номерах
            return c;
        }

        /// <summary>
        /// Функция возвращает случайный автомобяль из списка
        /// </summary>
        /// <param name="automobiles">Список автомобилей</param>
        /// <param name="seed"></param>
        /// <returns>Автомобиль</returns>
        private static Automobile RandomAutomobile(List<Automobile> automobiles, int seed)
        {
            Random rnd = new Random(seed);
            return automobiles[rnd.Next(automobiles.Count)];
        }

        /// <summary>
        /// Функция возвращает случайный автомобяль из списка
        /// </summary>
        /// <param name="automobiles">Список автомобилей</param>
        /// <param name="seed"></param>
        /// <returns>Автомобиль</returns>
        private static ICollection<Operation> RandomOperation(List<Operation> operations, EngineBase engineBase, int seed)
        {
            Random rnd = new Random(seed);
            var allowedOperations = operations.FindAll(o=>o.EngineType==engineBase).ToList();
            List<Operation> result = new List<Operation>();
            int operationsCount = rnd.Next(1, allowedOperations.Count);
            do
            {
                var nextOper = allowedOperations[rnd.Next(allowedOperations.Count)];
                if (!result.Contains(nextOper))
                    result.Add(nextOper);
            } while (result.Count< operationsCount);
            return result;
        }
    }
}
