using System;
using System.IO;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace MHTO2
{
    class CSV
    {
        private static char separator = ';';
        /// <summary>
        /// Выгрузка в CSV файл
        /// </summary>
        public static bool ExportToCSV()
        {
            DataTable table = new DataTable();
         

            table.Columns.Add(Convert.ToString("Наименование характеристики"));
            table.Columns.Add(Convert.ToString("Значение"));

            DataRow row;

            #region входные и выходные данные
            row = table.NewRow();
            row["Наименование характеристики"] = "Входные данные";
            row["Значение"] = "";
            table.Rows.Add(row);
            row = table.NewRow();
            row["Наименование характеристики"] = "";
            row["Значение"] = "";
            table.Rows.Add(row);
            row = table.NewRow();
            row["Наименование характеристики"] = "Диаметр трубчатого реактора:";
            row["Значение"] = Convert.ToString(Calc.D) + " м";
            table.Rows.Add(row);
            row = table.NewRow();
            row["Наименование характеристики"] = "Длина трубчатого реактора";
            row["Значение"] = Convert.ToString(Calc.L) + " м";
            table.Rows.Add(row);


            row = table.NewRow();
            row["Наименование характеристики"] = "Расход потока через реактор";
            row["Значение"] = Convert.ToString(Calc.Q) + " л/мин";
            table.Rows.Add(row);


            row = table.NewRow();
            row["Наименование характеристики"] = "Входная концентрация компонента A";
            row["Значение"] = Convert.ToString(Calc.CAin) + " моль/л";
            table.Rows.Add(row);


            row = table.NewRow();
            row["Наименование характеристики"] = "Температура смеси в реакторе";
            row["Значение"] = Convert.ToString(Calc.T) + " С";
            table.Rows.Add(row);

            row = table.NewRow();
            row["Наименование характеристики"] = "Предэкспоненциальный множитель для константы скорости первой реакции";
            row["Значение"] = Convert.ToString(Calc.k01/10000000000000) + " * 10^13 1/мин";
            table.Rows.Add(row);

            row = table.NewRow();
            row["Наименование характеристики"] = "Энергия активации первой реакции";
            row["Значение"] = Convert.ToString(Calc.Ea1) + " Дж/моль";
            table.Rows.Add(row);

            row = table.NewRow();
            row["Наименование характеристики"] = "Предэкспоненциальный множитель для константы скорости второй реакции";
            row["Значение"] = Convert.ToString(Calc.k02/1000000000000000) + " * 10^15 1/мин";
            table.Rows.Add(row);

            row = table.NewRow();
            row["Наименование характеристики"] = "Энергия активации второй реакции";
            row["Значение"] = Convert.ToString(Calc.Ea2) + " Дж/моль";
            table.Rows.Add(row);

            row = table.NewRow();
            row["Наименование характеристики"] = "Начальный шаг сетки по длине реактора";
            row["Значение"] = Convert.ToString(Calc.del_x) + " м";
            table.Rows.Add(row);

            row = table.NewRow();
            row["Наименование характеристики"] = "Сеточное чисто Куранта";
            row["Значение"] = Convert.ToString(Calc.Ku);
            table.Rows.Add(row);

            row = table.NewRow();
            row["Наименование характеристики"] = "Предельно допустимая погрешность расчёта концентрации целевого продукта";
            row["Значение"] = Convert.ToString(Calc.e_max) + " м";
            table.Rows.Add(row);

            row = table.NewRow();
            row["Наименование характеристики"] = "Максимальное число деления шагов сетки пополам";
            row["Значение"] = Convert.ToString(Calc.q_max) + " м";
            table.Rows.Add(row);


            row = table.NewRow();
            row["Наименование характеристики"] = "";
            row["Значение"] = "";
            table.Rows.Add(row);

            row = table.NewRow();
            row["Наименование характеристики"] = "Выходные данные";
            row["Значение"] = "";

            table.Rows.Add(row);
            row = table.NewRow();
            row["Наименование характеристики"] = "Площадь поперечного сечениея реактора:";
            row["Значение"] = Convert.ToString(Math.Round(Calc.S * 1000, 3) + " *10^(-3) м^2");
            table.Rows.Add(row);
            row = table.NewRow();
            row["Наименование характеристики"] = "Средняя линейная скорость потока:";
            row["Значение"] = Convert.ToString(Math.Round(Calc.u, 3)) + " м/мин";
            table.Rows.Add(row);
            row = table.NewRow();
            row["Наименование характеристики"] = "Константа скорости 1 химической реакции:";
            row["Значение"] = Convert.ToString(Math.Round(Calc.k1, 3)) + " 1/мин";
            table.Rows.Add(row);
            row = table.NewRow();
            row["Наименование характеристики"] = "Константа скорости 2 химической реакции:";
            row["Значение"] = Convert.ToString(Math.Round(Calc.k2, 3)) + " 1/мин";
            table.Rows.Add(row);
            row = table.NewRow();
            row["Наименование характеристики"] = "Среднее время пребывания смеси в реакторе:";
            row["Значение"] = Convert.ToString(Math.Round(Calc.tR, 3)) + " мин";
            table.Rows.Add(row);
            row = table.NewRow();
            row["Наименование характеристики"] = "Время моделирования объекта:";
            row["Значение"] = Convert.ToString(Math.Round(Calc.Teta, 3)) + " мин";
            table.Rows.Add(row);
            row = table.NewRow();
            row["Наименование характеристики"] = "Шаги сетки по длине реактора:";
            row["Значение"] = Convert.ToString(Math.Round(Calc.del_x, 4)) + " м";
            table.Rows.Add(row);
            row = table.NewRow();
            row["Наименование характеристики"] = "Шаги сетки по времени:";
            row["Значение"] = Convert.ToString(Math.Round(Calc.del_t, 4)) + " мин";
            table.Rows.Add(row);
            row = table.NewRow();
            row["Наименование характеристики"] = "Число шагов сетки по длине реактора: ";
            row["Значение"] = Convert.ToString(Calc.M);
            table.Rows.Add(row);
            row = table.NewRow();
            row["Наименование характеристики"] = "Число шагов сетки по времени: ";
            row["Значение"] = Convert.ToString(Calc.N);
            table.Rows.Add(row);
            row = table.NewRow();
            row["Наименование характеристики"] = "Число делений шагов сетки пополам: ";
            row["Значение"] = Convert.ToString(Math.Round(Calc.q - 1, 3));
            table.Rows.Add(row);
            row = table.NewRow();
            row["Наименование характеристики"] = "Абсолютная погрешность расчета концентрации целевого продукта:";
            row["Значение"] = Convert.ToString(Math.Round(Calc.ea, 3)) + " моль/л";
            table.Rows.Add(row);
            row = table.NewRow();
            row["Наименование характеристики"] = "Приведенная погрешность расчета концентрации целевого продукта:";
            row["Значение"] = Convert.ToString(Math.Round(Calc.e, 3)) + " %";
            table.Rows.Add(row);
            row = table.NewRow();
            row["Наименование характеристики"] = "Максимальная концентрация целевого продукта в смеси:";
            row["Значение"] = Convert.ToString(Math.Round(Calc.CBmax, 3)) + " моль/л";
            table.Rows.Add(row);
            row = table.NewRow();
            row["Наименование характеристики"] = "Время расчёта:";
            row["Значение"] = Convert.ToString(Math.Round(Calc.CBmax, 3)) + " с";
            table.Rows.Add(row);

            row = table.NewRow();
            row["Наименование характеристики"] = "";
            row["Значение"] = "";
            table.Rows.Add(row);
            #endregion


            table.Columns.Add(Convert.ToString("   "));
            table.Columns.Add(Convert.ToString(" "));
            table.Columns.Add(Convert.ToString("Время\\Длина"));

            row = table.NewRow();
            row["Время\\Длина"] = "Выходная концентрация компонента В";
            table.Rows.Add(row);
            for (int i = 0; i < Calc.x.Count; i++)
            {
                table.Columns.Add(Convert.ToString(Calc.x[i]));
            }
            row = table.NewRow();
            row["Время\\Длина"] = "Время\\Длина";
            for (int i = 0; i < Calc.x.Count; i++)
            {
                row[Convert.ToString(Calc.x[i])] = Calc.x[i];
           
            }
            table.Rows.Add(row);
            for (int j = 0; j < Calc.N; j++)
            {

                row = table.NewRow();
                row["Время\\Длина"] = Calc.t[j];
                for (int i = 0; i <= Calc.M; i++)
                {

                    row[Convert.ToString(Calc.x[i])] = Math.Round(Calc.CB[j][i],3);
                    
                }
                table.Rows.Add(row);

            }

      







            string fileName;

            SaveFileDialog Ofd = new SaveFileDialog { Filter = "Описание файла|*.csv" };
            if (Ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (table != null)
                {
                    FileStream fs = null;
                    try
                    {

                        fileName = Ofd.FileName;
                        fs = File.OpenWrite(fileName);

                    }
                    catch
                    {
                        return false;
                    }
                    using (TextWriter tw = new StreamWriter(fs, Encoding.GetEncoding(1251)))
                    {
                        String line = "";
                        //Выводим имя таблицы
                        if (!String.IsNullOrEmpty(table.TableName))
                            tw.WriteLine(table.TableName);
                        //Вывод названий столбцов
                        foreach (DataColumn colName in table.Columns)
                        {
                           // line += String.Format("\"{0}\"{1}", colName.ColumnName, separator);
                        }
                        tw.WriteLine(line.TrimEnd(separator));
                        //Вывод данных
                        foreach (DataRow dr in table.Rows)
                        {
                            line = "";
                            Array.ForEach(dr.ItemArray, obj => line += String.Format("\"{0}\"{1}", obj, separator));
                            tw.WriteLine(line.TrimEnd(separator));
                        }
                    }
                    fs.Close();
                    fs.Dispose();
                    MessageBox.Show("Отчёт успешно сохранён!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            return false;
        }
    }
}
