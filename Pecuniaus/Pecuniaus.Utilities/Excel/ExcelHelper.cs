using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using OfficeOpenXml;
using System.IO;

namespace Pecuniaus.Utilities
{
    public class ExcelHelper
    {
        public bool UpdateExcelUsingADO(string FolderPath, string SourceFileName, string DestinationFileName, bool DeleteExistingCopy, string Query)
        {
            OleDbConnection ExcelConnection = null;
            OleDbCommand ExcelCommand = new OleDbCommand();
            try
            {
                if (System.IO.File.Exists(FolderPath + "\\" + DestinationFileName) && DeleteExistingCopy)
                {
                    System.IO.File.Delete(FolderPath + "\\" + DestinationFileName);
                    System.IO.File.Copy(FolderPath + "\\" + SourceFileName, FolderPath + "\\" + DestinationFileName);
                }
                else if (!System.IO.File.Exists(FolderPath + "\\" + DestinationFileName))
                {
                    System.IO.File.Copy(FolderPath + "\\" + SourceFileName, FolderPath + "\\" + DestinationFileName);
                }

                ExcelConnection = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + FolderPath + "\\" + DestinationFileName + "';Extended Properties=\"Excel 12.0 Xml;HDR=No;\"");
                ExcelConnection.Open();
                ExcelCommand.Connection = ExcelConnection;
                ExcelCommand.CommandText = Query;
                ExcelCommand.ExecuteNonQuery();
                ExcelConnection.Close();
                return true;
            }
            catch
            {
                if (ExcelConnection.State == ConnectionState.Open)
                    ExcelConnection.Close();
                ExcelCommand = null;
                ExcelConnection = null;
                return false;
            }
            //return File(Path + "/Template1.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Template1");
        }
        public bool UpdateExcelTemplate(string FolderPath, string SourceFileName, string DestinationFileName, bool DeleteExistingCopy, string CellName, string CellValue)
        {
            if (System.IO.File.Exists(FolderPath + "\\" + DestinationFileName) && DeleteExistingCopy)
            {
                System.IO.File.Delete(FolderPath + "\\" + DestinationFileName);
                System.IO.File.Copy(FolderPath + "\\" + SourceFileName, FolderPath + "\\" + DestinationFileName);
            }
            else if (!System.IO.File.Exists(FolderPath + "\\" + DestinationFileName))
            {
                System.IO.File.Copy(FolderPath + "\\" + SourceFileName, FolderPath + "\\" + DestinationFileName);
            }

            var File = new FileInfo(FolderPath + "\\" + DestinationFileName);
            using (ExcelPackage package = new ExcelPackage(File))
            {
                ExcelWorkbook workBook = package.Workbook;
                if (workBook != null)
                {
                    if (workBook.Worksheets.Count > 0)
                    {
                        ExcelWorksheet cSheet = workBook.Worksheets.First();

                        cSheet.Cells[CellName].Value = CellValue;
                    }
                }
                package.Save();
            }
            return false;
        }
        public bool UpdateExcelTemplate(string FolderPath, string SourceFileName, string DestinationFileName, bool DeleteExistingCopy, int RowIndex, int CellIndex, string CellValue)
        {
            if (System.IO.File.Exists(FolderPath + "\\" + DestinationFileName) && DeleteExistingCopy)
            {
                System.IO.File.Delete(FolderPath + "\\" + DestinationFileName);
                System.IO.File.Copy(FolderPath + "\\" + SourceFileName, FolderPath + "\\" + DestinationFileName);
            }
            else if (!System.IO.File.Exists(FolderPath + "\\" + DestinationFileName))
            {
                System.IO.File.Copy(FolderPath + "\\" + SourceFileName, FolderPath + "\\" + DestinationFileName);
            }

            var File = new FileInfo(FolderPath + "\\" + DestinationFileName);
            using (ExcelPackage package = new ExcelPackage(File))
            {
                ExcelWorkbook workBook = package.Workbook;
                if (workBook != null)
                {
                    if (workBook.Worksheets.Count > 0)
                    {
                        ExcelWorksheet cSheet = workBook.Worksheets.First();

                        cSheet.Cells[RowIndex, CellIndex].Value = CellValue;
                    }
                }
                package.Save();
            }
            return false;
        }
        public bool InsertNewRow(string FolderPath, string DestinationFileName,int AddNewRowAfter, int NumberOfRowsToAdd)
        {
            try
            {
                var File = new FileInfo(FolderPath + "\\" + DestinationFileName);
                using (ExcelPackage package = new ExcelPackage(File))
                {
                    ExcelWorkbook workBook = package.Workbook;
                    if (workBook != null)
                    {
                        if (workBook.Worksheets.Count > 0)
                        {
                            ExcelWorksheet cSheet = workBook.Worksheets.First();
                            cSheet.InsertRow(AddNewRowAfter, NumberOfRowsToAdd);
                        }
                    }
                    package.Save();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
