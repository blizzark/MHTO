using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace MHTO2
{
    public partial class Form1 : Form
    {
        public MethodInfo MainFunc;
        public MethodInfo Condit;
        private double numTime = 0;
        public Form1()
        {
            InitializeComponent();
           // Type TestType = Type.GetType("MHTO2.Tasks", false, true);
           // MainFunc = TestType.GetMethod("MainFunction1");
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            #region Наименование осей графика
            chart1.ChartAreas[0].AxisX.Title = "Время, мин";
            chart1.ChartAreas[0].AxisY.Title = "Выходная концентрация А, моль/л";
            chart2.ChartAreas[0].AxisX.Title = "Время, мин";
            chart2.ChartAreas[0].AxisY.Title = "Выходная концентрация В, моль/л";
            chart3.ChartAreas[0].AxisX.Title = "Координата по длине реактора, м";
            chart3.ChartAreas[0].AxisY.Title = "Конечная концентрация А, моль/л";
            chart4.ChartAreas[0].AxisX.Title = "Координата по длине реактора, м";
            chart4.ChartAreas[0].AxisY.Title = "Конечная концентрация В, моль/л";
            #endregion
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            numTime += 0.1;
        }

        private void Begin_Click(object sender, EventArgs e)
        {
            numTime = 0;
            // Отчистка таблицы и графиков
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
            chart3.Series[0].Points.Clear();
            chart4.Series[0].Points.Clear();

            //// Задаю минимальные значения для графика по осям
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart2.ChartAreas[0].AxisX.Minimum = 0;
            chart3.ChartAreas[0].AxisX.Minimum = 0;
            chart4.ChartAreas[0].AxisX.Minimum = 0;


            #region Передача данных с формы в класс
            Calc.D = Convert.ToDouble(numericD.Value);
            Calc.L = Convert.ToDouble(numericL.Value);
            Calc.Q = Convert.ToDouble(numericQ.Value);
            Calc.CAin = Convert.ToDouble(numericCAin.Value);
            Calc.T = Convert.ToDouble(numericT.Value);
            Calc.k01 = Convert.ToDouble(numericK01.Value) * 10000000000000;
            Calc.Ea1 = Convert.ToDouble(numericEa1.Value);
            Calc.k02 = Convert.ToDouble(numericK02.Value) * 1000000000000000;
            Calc.Ea2 = Convert.ToDouble(numericEa2.Value);
            Calc.del_x0 = Convert.ToDouble(numericDel_x0.Value);
            Calc.Ku = Convert.ToDouble(numericKu.Value);
            Calc.e_max = Convert.ToDouble(numericE_max.Value);
            Calc.q_max = Convert.ToDouble(numericQ_max.Value);
            #endregion
            // try
            {
                if (Calc.Ku > 0)
                {
                    if ((Calc.L / 2) >= Calc.del_x0)
                    {
                        Calc.CB.Clear();
                        Calc.CA.Clear();
                        Calc.CB1.Clear();
                        Calc.x.Clear();
                        Calc.t.Clear();

                        System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
                        myStopwatch.Start(); //запуск
                        Calc.Calucation(); // метод основных расчётов
                        myStopwatch.Stop(); //остановить


                        chart3.ChartAreas[0].AxisY.Minimum = Math.Round(Calc.CA[Calc.N][Calc.M], 1);
                        #region Заполнелнение вывода на вкладке
                        labelS.Text = Convert.ToString(Math.Round(Calc.S * 1000, 3) + " *10^(-3) м^2");
                        labelU.Text = Convert.ToString(Math.Round(Calc.u, 3)) + " м/мин";
                        labelK1.Text = Convert.ToString(Math.Round(Calc.k1, 3)) + " 1/мин";
                        labelK2.Text = Convert.ToString(Math.Round(Calc.k2, 3)) + " 1/мин";
                        labelTR.Text = Convert.ToString(Math.Round(Calc.tR, 3)) + " мин";
                        labelTeta.Text = Convert.ToString(Math.Round(Calc.Teta, 3)) + " мин";
                        labelDelX.Text = Convert.ToString(Math.Round(Calc.del_x, 4)) + " м";
                        labelDelT.Text = Convert.ToString(Math.Round(Calc.del_t, 4)) + " мин";
                        labelM.Text = Convert.ToString(Calc.M);
                        labelN.Text = Convert.ToString(Calc.N);
                        labelQ.Text = Convert.ToString(Math.Round(Calc.q - 1, 3));
                        labelEA.Text = Convert.ToString(Math.Round(Calc.ea, 3)) + " моль/л";
                        labelE.Text = Convert.ToString(Math.Round(Calc.e, 3)) + " %";
                        labelCBmax.Text = Convert.ToString(Math.Round(Calc.CBmax, 3)) + " моль/л";
                        labelTau_Calc.Text = Convert.ToString(myStopwatch.ElapsedMilliseconds) + " мс";
                        #endregion

                        #region Отрисовка графиков
                        for (int i = 0; i < Calc.N; i += 10)
                        {

                            chart1.Series[0].Points.AddXY(Math.Round(Calc.t[i], 2), Math.Round(Calc.CA[i][Calc.M], 3));
                            chart2.Series[0].Points.AddXY(Math.Round(Calc.t[i], 2), Math.Round(Calc.CB[i][Calc.M], 3));
                        }



                        for (int i = 1; i < Calc.M; i += 10)
                        {

                            chart3.Series[0].Points.AddXY(Math.Round(Calc.x[i], 2), Math.Round(Calc.CA[Calc.N][i], 3));
                            chart4.Series[0].Points.AddXY(Math.Round(Calc.x[i], 2), Math.Round(Calc.CB[Calc.N][i], 3));
                        }
                        #endregion

                        #region Таблица 1
                        dataGridView1.Columns.Add("l", "Время\\Длина");

                        dataGridView1.Columns[0].Width = 150;
                        for (int i = 0; i <= Calc.M; i += 10)
                        {
                            dataGridView1.Columns.Add(i.ToString(), Convert.ToString(Calc.x[i]) + " м");
                        }



                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[0].Cells[0].Value = 0 + " мин";
                        for (int i = 10; i < Calc.t.Count; i += 10)
                        {

                            dataGridView1.Rows.Add(Math.Round(Calc.t[i], 3) + " мин");

                        }
                        dataGridView1.Rows.Add(Math.Round(Calc.t[Calc.t.Count - 1], 3) + " мин");
                        for (int i = 0; i <= Math.Ceiling((double)Calc.N / 10); i++)
                        {
                            dataGridView1.Rows[i].Cells[0].Style.Font = new Font("Microsoft Sans Serif", 12);
                            dataGridView1.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.LightGray;
                        }

                        for (int j = 0; j <= Calc.M / 10; j++)
                        {

                            for (int i = 0; i <= Math.Ceiling((double)Calc.N / 10); i++)
                            {

                                dataGridView1.Rows[i].Cells[j + 1].Value = Math.Round(Calc.CA[i][j], 4);

                            }
                            if (j != 0)
                                dataGridView1.Columns[j].Width = 110;
                        }
                        #endregion

                        #region Таблица 2

                        dataGridView2.Columns.Add("l", "Время\\Длина");

                        dataGridView2.Columns[0].Width = 150;
                        for (int i = 0; i <= Calc.M; i += 10)
                        {
                            dataGridView2.Columns.Add(i.ToString(), Convert.ToString(Calc.x[i]) + " м");
                        }



                        dataGridView2.Rows.Add();
                        dataGridView2.Rows[0].Cells[0].Value = 0 + " мин";
                        for (int i = 10; i < Calc.t.Count; i += 10)
                        {

                            dataGridView2.Rows.Add(Math.Round(Calc.t[i], 3) + " мин");

                        }
                        dataGridView2.Rows.Add(Math.Round(Calc.t[Calc.t.Count - 1], 3) + " мин");
                        for (int i = 0; i <= Math.Ceiling((double)Calc.N / 10); i++)
                        {
                            dataGridView2.Rows[i].Cells[0].Style.Font = new Font("Microsoft Sans Serif", 12);
                            dataGridView2.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.LightGray;
                        }

                        for (int j = 0; j <= Calc.M / 10; j++)
                        {

                            for (int i = 0; i <= Math.Ceiling((double)Calc.N / 10); i++)
                            {

                                dataGridView2.Rows[i].Cells[j + 1].Value = Math.Round(Calc.CB[i][j], 4);

                            }
                            if (j != 0)
                                dataGridView2.Columns[j].Width = 110;
                        }
                        #endregion

                        MessageBox.Show("Расчёт успешно выполнен!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Export.Enabled = true;
                        gragics.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Шаг должен быть меньче чем L/2!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Сеточное число Куранта должно быть больше нуля!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Попробуйте изменить входные данные\n\nИнформация разработчику:\n" + ex.Message, "Упс! Что-то сломалось!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void Export_Click(object sender, EventArgs e)
        {
            CSV.ExportToCSV();
        }

        private void Develop_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Данную программу разработали студенты 475 группы:\nАндрианова Карина Ивановна\nОвчинников Роман Сергеевич", "Информация о разработчиках", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public MethodInfo MF;
        private void gragics_Click(object sender, EventArgs e)
        {
            //  MainFunc = TestType.GetMethod("MainFunction0");


          
            _3DGraph qwe = new _3DGraph(0, Calc.L, 0, Calc.Teta);
            qwe.ShowDialog();

            //  clearWPF._3DGraph qwe = new clearWPF._3DGraph(MainFunc, 0, Calc.L, 0, Calc.T);
            // ElementHost.EnableModelessKeyboardInterop(qwe);
        }
    }
}
