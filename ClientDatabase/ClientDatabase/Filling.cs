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

        private void fillingColumn(string myType, string myNameRu, string myNameEn, string myParentTable)
        {
            columns.Add(new Column() { type = myType, nameRu = myNameRu, nameEn = myNameEn, parentTable = myParentTable });
        } 

        public void run(ref Model model)
        {
            List<Table>tables = new List<Table>();

            fillingTable("Абитуриенты", "Abiturient");
            fillingColumn("int", "Номер аттестата", "Id_attestata", "null");
            fillingColumn("string", "Фамилия", "Familiya", "null");
            fillingColumn("string", "Имя", "Imya", "null");
            fillingColumn("string", "Отчество", "Otchestvo", "null");
            fillingColumn("string", "Адрес", "Adres", "null");
            table.column = columns;
            tables.Add(table);

            fillingTable("Дисциплина", "Distsiplina");
            fillingColumn("int", "Номер дисциплины", "Id_distsipliny", "null");
            fillingColumn("string", "Название дисциплины", "Nazvanie_distsipliny", "null");
            table.column = columns;
            tables.Add(table);

            fillingTable("Специальность", "Spetsialnost");
            fillingColumn("int", "Номер специальности", "Id_spetsialnosti", "null");
            fillingColumn("string", "Название специальности", "Nazvanie_spetsialnosti", "null");
            table.column = columns;
            tables.Add(table);

            fillingTable("Кафедра", "Kafedra");
            fillingColumn("int", "Номер кафедры", "Id_kafedry", "null");
            fillingColumn("string", "Название кафедры", "Nazvanie_kafedry", "null");
            table.column = columns;
            tables.Add(table);

            fillingTable("Преподаватель", "Prepodavatel");
            fillingColumn("int", "Номер табельного номера", "Id_tabelnogo_nomer", "null");
            fillingColumn("string", "Фамилия", "Familiya", "null");
            fillingColumn("string", "Имя", "Imya", "null");
            fillingColumn("string", "Отчество", "Otchestvo", "null");
            fillingColumn("foreignKey", "Номер кафедры", "Id_kafedry", "Kafedra");
            table.column = columns;
            tables.Add(table);

            fillingTable("Экзамены", "Ekzamen");
            fillingColumn("int", "Номер экзамена", "Id_ekzamena", "null");
            fillingColumn("date", "Дата сдачи экзамена", "Data_sdachi_ekzamena", "null");
            fillingColumn("int", "Оценка", "Otsenka", "null");
            fillingColumn("foreignKey", "Номер аттестата", "Id_attestata", "Abiturient");
            fillingColumn("foreignKey", "Номер табельного номера", "Id_tabelnogo_nomer", "Prepodavatel");
            fillingColumn("foreignKey", "Номер дисциплины", "Id_distsipliny", "Distsiplina");
            fillingColumn("foreignKey", "Номер ведомости", "Id_vedomosti", "Vedomost");
            table.column = columns;
            tables.Add(table);

            fillingTable("Ведомость", "Vedomost");
            fillingColumn("int", "Номер ведомости", "Id_vedomosti", "null");
            fillingColumn("foreignKey", "Номер специальности", "Id_spetsialnosti", "Spetsialnost");
            table.column = columns;
            tables.Add(table);

            model.table = tables;
        }
    }
}
