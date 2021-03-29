using System;
using System.Collections.Generic;
using DefectMessageNamespace;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Drawing;
using OpenCvSharp;


namespace WSC_AI
{
    class Excel_UPLoad : Globals
    {
        Excel.Application ex;
        Excel.Workbook workBook;
        Excel.Workbooks Workbooks;
        Excel.Worksheet sheet;
        List<String> LIST_DEFECTS;

        public Excel_UPLoad()
        {
            LIST_DEFECTS = new List<String>(DICTIONARY_DEFECTS.Values);
            LIST_DEFECTS.Sort();

            ex = new Excel.Application();
            //Отобразить Excel
            ex.Visible = false;

            if (Directory.Exists(path_stat_defects))
            {
                if (File.Exists(path_stat_defects + "\\" + NameExcelBook))
                {
                    Workbooks = ex.Workbooks;
                    workBook = Workbooks.Open(path_stat_defects + "\\" + NameExcelBook,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);
                    //Отключить отображение окон с сообщениями
                    ex.DisplayAlerts = false;
                }

                else
                {
                    ex.SheetsInNewWorkbook = 1;
                    //Добавить рабочую книгу
                    Workbooks = ex.Workbooks;
                    workBook = Workbooks.Add(Type.Missing);
                    //Отключить отображение окон с сообщениями
                    ex.DisplayAlerts = false;
                    //Получаем первый лист документа (счет начинается с 1)
                    sheet = (Excel.Worksheet)ex.Worksheets.get_Item(1);
                    //Название листа (вкладки снизу)
                    sheet.Name = "Статистика";

                    sheet.Cells[1, 1] = "Дата выгрузки";
                    sheet.Cells[1, 2] = "ДСЕ и серийный №";
                    sheet.Cells[1, 3] = "Количество дефектов шт.";

                    Excel.Range c1 = sheet.Cells[1, 1];
                    Excel.Range c2 = sheet.Cells[2, 1];
                    //Захватываем диапазон ячеек
                    Excel.Range range1 = sheet.get_Range(c1, c2);
                    range1.Interior.Color = ColorTranslator.ToOle(Color.LightGray);
                    range1.Cells.Font.Size = 14;
                    range1.Merge();

                    c1 = sheet.Cells[1, 2];
                    c2 = sheet.Cells[2, 2];
                    //Захватываем диапазон ячеек
                    range1 = sheet.get_Range(c1, c2);
                    range1.Interior.Color = ColorTranslator.ToOle(Color.LightGray);
                    range1.Cells.Font.Size = 14;
                    range1.Merge();

                    c1 = sheet.Cells[1, 3];
                    c2 = sheet.Cells[1, LIST_DEFECTS.Count + 2];
                    //Захватываем диапазон ячеек
                    range1 = sheet.get_Range(c1, c2);
                    range1.Interior.Color = ColorTranslator.ToOle(Color.LightGray);
                    range1.Cells.Font.Size = 14;
                    range1.Merge();

                    for (int i = 3; i < LIST_DEFECTS.Count + 3; i++)
                    {
                        sheet.Cells[2, i] = LIST_DEFECTS[i - 3];
                    }



                    ex.Application.ActiveWorkbook.SaveAs(path_stat_defects + "\\" + NameExcelBook, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);



                    range1 = null;
                    c1 = null;
                    c2 = null;
                }
                }
            else
            {
                Directory.CreateDirectory(path_stat_defects);

                if (File.Exists(path_stat_defects + "\\" + NameExcelBook))
                {
                    Workbooks = ex.Workbooks;
                    workBook = Workbooks.Open(path_stat_defects + "\\" + NameExcelBook,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);
                    //Отключить отображение окон с сообщениями
                    ex.DisplayAlerts = false;
                }

                else
                {
                    ex.SheetsInNewWorkbook = 1;
                    //Добавить рабочую книгу
                    Workbooks = ex.Workbooks;
                    workBook = Workbooks.Add(Type.Missing);
                    //Отключить отображение окон с сообщениями
                    ex.DisplayAlerts = false;
                    //Получаем первый лист документа (счет начинается с 1)
                    sheet = (Excel.Worksheet)ex.Worksheets.get_Item(1);
                    //Название листа (вкладки снизу)
                    sheet.Name = "Статистика";

                    sheet.Cells[1, 1] = "Дата выгрузки";
                    sheet.Cells[1, 2] = "ДСЕ и серийный №";
                    sheet.Cells[1, 3] = "Количество дефектов шт.";

                    Excel.Range c1 = sheet.Cells[1, 1];
                    Excel.Range c2 = sheet.Cells[2, 1];
                    //Захватываем диапазон ячеек
                    Excel.Range range1 = sheet.get_Range(c1, c2);
                    range1.Interior.Color = ColorTranslator.ToOle(Color.LightGray);
                    range1.Cells.Font.Size = 14;
                    range1.Merge();

                    c1 = sheet.Cells[1, 2];
                    c2 = sheet.Cells[2, 2];
                    //Захватываем диапазон ячеек
                    range1 = sheet.get_Range(c1, c2);
                    range1.Interior.Color = ColorTranslator.ToOle(Color.LightGray);
                    range1.Cells.Font.Size = 14;
                    range1.Merge();

                    c1 = sheet.Cells[1, 3];
                    c2 = sheet.Cells[1, LIST_DEFECTS.Count + 2];
                    //Захватываем диапазон ячеек
                    range1 = sheet.get_Range(c1, c2);
                    range1.Interior.Color = ColorTranslator.ToOle(Color.LightGray);
                    range1.Cells.Font.Size = 14;
                    range1.Merge();

                    for (int i = 3; i < LIST_DEFECTS.Count + 3; i++)
                    {
                        sheet.Cells[2, i] = LIST_DEFECTS[i - 3];
                    }



                    ex.Application.ActiveWorkbook.SaveAs(path_stat_defects + "\\" + NameExcelBook, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);



                    range1 = null;
                    c1 = null;
                    c2 = null;
                }
            }

            

            

            
        }

        private Dictionary<String, int> calculate_count(List<Defect> defect_out)
        {
            Dictionary<String, int> COUNT_DEFECTS = new Dictionary<String, int>();

            foreach (var item in DICTIONARY_DEFECTS.Values)
            {
                COUNT_DEFECTS.Add(item, 0);
            }

            for (int i = 0; i < defect_out.Count; i++)
            {
                foreach (var item in defect_out[i].Descriptions)
                {
                    COUNT_DEFECTS[item] += 1;
                }
            }

            return COUNT_DEFECTS;

        }

        public void WriteStat(List<Defect> defect_out)
        {

            Dictionary<String, int> COUNT_DEFECTS = calculate_count(defect_out);

            //Получаем первый лист документа (счет начинается с 1)
            sheet = (Excel.Worksheet)ex.Worksheets.get_Item(1);
            Excel.Range last = sheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);

            int row_start = last.Row;
            int col_start = last.Column;


            int col = 3;
            foreach (var item in LIST_DEFECTS)
            {
                sheet.Cells[row_start+1, col] = COUNT_DEFECTS[item];
                col++;

            }

            String current_DateTime = DateTime.Now.ToShortDateString() + "__"+ DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString();
            WriteImages(defect_out, current_DateTime);

            //Записываем дату выгрузки
            sheet.Cells[row_start + 1, 1] = current_DateTime;


            Excel.Range c1 = sheet.Cells[1, 1];
            Excel.Range c2 = sheet.Cells[row_start+1, col_start];

            //Захватываем диапазон ячеек
            Excel.Range range1 = sheet.get_Range(c1, c2);

            range1.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            range1.Borders.get_Item(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous;
            range1.Borders.get_Item(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlContinuous;
            range1.Borders.get_Item(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlContinuous;
            range1.Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
            range1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            range1.Font.Name = "Times New Roman";
            range1.EntireColumn.AutoFit();
            range1.EntireRow.AutoFit();



            c1 = sheet.Cells[row_start+1, 2];
            c2 = sheet.Cells[row_start+1, 2];
            //Захватываем диапазон ячеек
            range1 = sheet.get_Range(c1, c2);
            range1.Interior.Color = ColorTranslator.ToOle(Color.Yellow);
            range1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;


            


            ex.Application.ActiveWorkbook.SaveAs(path_stat_defects + "\\" + NameExcelBook, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);


            workBook.Close();
            //Workbooks.Close();
            sheet = null;
            c1 = null;
            c2 = null;
            range1 = null;
            last = null;
            workBook = null;
            Workbooks = null;
            ex.Quit();
            ex = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();

        }

        private void WriteImages(List<Defect> defect_out, String DateTime)
        {
            if (Directory.Exists(path_stat_defects))
            {
                //Create directores
                Directory.CreateDirectory(path_stat_defects + "\\" + DateTime);
                for (int i = 0; i < LIST_DEFECTS.Count; i++)
                {
                    Directory.CreateDirectory(path_stat_defects + "\\" + DateTime + "\\" + LIST_DEFECTS[i]);
                }

                for (int i = 0; i < defect_out.Count; i++)
                {
                    Mat Image = Base64Image.Base64Decode(defect_out[i].ImageBase64);

                    for (int j = 0; j < defect_out[i].Descriptions.Count; j++)
                    {
                        Cv2.ImWrite(path_stat_defects + "\\" + DateTime + "\\" + defect_out[i].Descriptions[j]+"\\" + i.ToString() + j.ToString()+ ".jpg", Image);
                    }
                    
                }
                
            }
        }
    }
}
