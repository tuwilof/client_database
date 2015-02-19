using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDatabase
{
    class Filling
    {
        Table table;
        private List<Column> columns;


        private void fillingTable(string myNameRu, string myNameEn)
        {
            table = new Table() { nameRu = myNameRu, nameEn = myNameEn };
            columns = new List<Column>();
        }

        private void fillingColumn(string myType, string myNameRu, string myNameEn)
        {
            columns.Add(new Column() { type = myType, nameRu = myNameRu, nameEn = myNameEn });
        } 

        public void run(ref Model model)
        {
            List<Table>tables = new List<Table>();

            fillingTable("Абитуриенты", "Abiturient");
            fillingColumn("int", "Номер аттестата", "Id_attestata");
            fillingColumn("string", "Фамилия", "Familiya");
            fillingColumn("string", "Имя", "Imya");
            fillingColumn("string", "Отчество", "Otchestvo");
            fillingColumn("string", "Адрес", "Adres");
            table.column = columns;
            tables.Add(table);

            fillingTable("Дисциплина", "Distsiplina");
            fillingColumn("int", "Номер дисциплины", "Id_distsipliny");
            fillingColumn("string", "Название дисциплины", "Nazvanie_distsipliny");
            table.column = columns;
            tables.Add(table);

            fillingTable("Специальность", "Spetsialnost");
            fillingColumn("int", "Номер специальности", "Id_spetsialnosti");
            fillingColumn("string", "Название специальности", "Nazvanie_spetsialnosti");
            table.column = columns;
            tables.Add(table);

            fillingTable("Кафедра", "Kafedra");
            fillingColumn("int", "Номер кафедры", "Id_kafedry");
            fillingColumn("string", "Название кафедры", "Nazvanie_kafedry");
            table.column = columns;
            tables.Add(table);

            fillingTable("Преподаватель", "Prepodavatel");
            fillingColumn("int", "Номер табельного номера", "Id_tabelnyy_nomer");
            fillingColumn("string", "Фамилия", "Familiya");
            fillingColumn("string", "Имя", "Imya");
            fillingColumn("string", "Отчество", "Otchestvo");
            fillingColumn("int", "Номер кафедры", "Id_kafedry");
            table.column = columns;
            tables.Add(table);

            fillingTable("Экзамены", "Ekzamen");
            fillingColumn("int", "Номер экзамена", "Id_ekzamena");
            fillingColumn("int", "Номер аттестата", "Id_attestata");
            fillingColumn("int", "Номер табельного номера", "Id_tabelnyy_nomer");
            fillingColumn("int", "Номер дисциплины", "Id_distsipliny");
            fillingColumn("date", "Дата сдачи экзамена", "Data_sdachi_ekzamena");
            fillingColumn("int", "Оценка", "Otsenka");
            table.column = columns;
            tables.Add(table);

            fillingTable("Ведомость", "Vedomost");
            fillingColumn("int", "Номер ведомости", "Id_vedomosti");
            fillingColumn("int", "Номер экзамена", "Id_ekzamena");
            fillingColumn("int", "Номер аттестата", "Id_attestata");
            fillingColumn("int", "Номер табельного номера", "Id_tabelnyy_nomer");
            fillingColumn("int", "Номер дисциплины", "Id_distsipliny");
            fillingColumn("int", "Номер специальности", "Id_spetsialnosti");
            table.column = columns;
            tables.Add(table);

            model.table = tables;
        }
    }
}
