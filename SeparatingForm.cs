using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace Separating
{
    public partial class SeparatingForm : Form
    {
        //private readonly Dictionary<string, ListViewGroup> _groups; 
        private readonly Dictionary<string, StringCollection> _categories;
        private List<SourceItem> _sourceItems;
        private readonly StringCollection _extractCategories;
        private readonly StringCollection _extractKnownNames;
        private string _sourcepath = "";
        private bool _loaded;

        public SeparatingForm()
        {
            InitializeComponent();
            //_groups = new Dictionary<string, ListViewGroup>();
            _sourceItems = new List<SourceItem>();
            _categories = new Dictionary<string, StringCollection>
                {
                    #region
                    /*
                    {"Метизы", new StringCollection
                        {
                            "Болт", "Анкерный болт", "Винт", "Шайб", "Шпильк", "Саморез", "Гайк", "Метиз"
                        }},
                    {"Металлопрокат", new StringCollection
                        {
                            "Угол", "Швеллер", "Арматура", "Профиль", "Лист стальной", "Сталь", 
                            "Круг", "Рифленка", "Прокат", "Горячекатаная", "Лента стальная"
                        }},
                    {"Труба", new StringCollection {"Труб"}},
                    {"Узлы крепления трубопроводов", new StringCollection {"Опор"}},
                    {"Бетон", new StringCollection {"Бетон"}},
                    {"Монтажные узлы и изделия", new StringCollection
                        {
                            "Металлорукав", "Стойка кабельная", "Полка кабельная", "Скоба", "Сгон", "Перемычка",
                            "DIN-рейка", "Полоса", "Штуцер под", "Короб", "Коробка", "Кабель-канал", "Рукав", 
                            "Секция", "Соединение навертное", "Пластина торцевая", "Муфта кабельная"
                        }},
                    {"Кабельно-проводниковая продукция", new StringCollection
                        {
                            "Кабель ", "Кабели ", "Провод"
                        }},
                    {"Детали трубопроводов", new StringCollection
                        {
                            "Тройник", "Переход", "Отвод", "Заглушка"
                        }},
                    {"Запорно-регулирующая арматура", new StringCollection
                        {
                            "Кран", "Задвижк", "Клапан"
                        }},
                    {"Теплоизоляционные материалы", new StringCollection
                        {
                            "Цилиндры", "Маты", "Стеклопластик", "Асбокартон"
                        }},
                    {"Лакокрасочные материалы", new StringCollection
                        {
                            "Белила", "Эмаль", "Грунтовка", "Лак", "Краска"
                        }},
                    {"Электротехническое оборудование", new StringCollection
                        {
                            "Выключатель", "Амперметр", "Клемма", "ИБП", "Модул", "Контакт", "Пост",
                            "Трансформатор", "Шкаф", "Щит", "Лампа", "Резистор", "АВР", "Блок", "Преобразователь"
                        }}
                     */
                    #endregion
                };
            _extractCategories = new StringCollection();
            _extractKnownNames = new StringCollection();
        }

        #region Загрузка и выгрузка

        private void SeparatingForm_Load(object sender, EventArgs e)
        {
            var mif = new MemIniFile(Path.ChangeExtension(Application.ExecutablePath, "ini"));
            foreach (var catfilter in mif.ReadSectionValues("FilterCategories"))
            {
                var fsc = new StringCollection();
                fsc.AddRange(mif.ReadSectionValues(catfilter));
                _categories.Add(catfilter, fsc);
            }
            FillDictionaries();
            foreach (var line in mif.ReadSectionValues("ExtractCategories"))
            {
                _extractCategories.Add(line);
            }
            FillExtractCategories();
            foreach (var line in mif.ReadSectionValues("ExtractKnownNames"))
            {
                _extractKnownNames.Add(line);
            }
            FillExtractKnownNames();
            var sourceFolder = mif.ReadString("TablesSource", "TablesSourceFolder", "");
            if (Directory.Exists(sourceFolder))
                lbTablesSourceFolder.Text = sourceFolder;
            _sourcepath = lbTablesSourceFolder.Text;
            FillSourceTablesList();
            var sc = new StringCollection();
            foreach (var line in mif.ReadSectionValues("TablesSourceSelected"))
            {
                sc.Add(line);
            }
            foreach (var li in lvSourceTables.Items.Cast<ListViewItem>().Where(li => sc.Contains(li.Text)))
            {
                li.Checked = true;
            }
            var binfile = Path.Combine(Application.StartupPath, "SourceItems.bin");
            if (File.Exists(binfile))
            {
                IFormatter formatter = new BinaryFormatter();
                using (var stream = new FileStream(binfile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    _sourceItems = (List<SourceItem>) formatter.Deserialize(stream);
                }
                FillList();
                BuildResultTree();
            }
            _loaded = true;
        }

        private void SeparatingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var mif = new MemIniFile(Path.ChangeExtension(Application.ExecutablePath, "ini"));
            mif.Clear();
            mif.WriteString("TablesSource", "TablesSourceFolder", lbTablesSourceFolder.Text);
            var n = 1;
            foreach (var li in lvSourceTables.Items.Cast<ListViewItem>().Where(li => li.Checked))
            {
                mif.WriteString("TablesSourceSelected", n.ToString("0"), li.Text);
                n++;
            }
            n = 1;
            foreach (var catkey in _categories.Keys)
            {
                mif.WriteString("FilterCategories", n.ToString("0"), catkey);
                var k = 1;
                foreach (var filter in _categories[catkey])
                {
                    mif.WriteString(catkey, k.ToString("0"), filter);
                    k++;
                }
                n++;
            }
            n = 1;
            foreach (var item in _extractCategories)
            {
                mif.WriteString("ExtractCategories", n.ToString("0"), item);
                n++;
            }
            n = 1;
            foreach (var item in _extractKnownNames)
            {
                mif.WriteString("ExtractKnownNames", n.ToString("0"), item);
                n++;
            }
            mif.UpdateFile();
            var formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(Path.Combine(
                Application.StartupPath, "SourceItems.bin"), FileMode.Create,
                                                  FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, _sourceItems);
            }
        }

        #endregion

        #region Выбор исходных таблиц

        private void btnSelecTablestSourceFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(_sourcepath))
                folderBrowserDialog1.SelectedPath = _sourcepath;
            if (folderBrowserDialog1.ShowDialog(this) != DialogResult.OK) return;
            lbTablesSourceFolder.Text = folderBrowserDialog1.SelectedPath;
            FillSourceTablesList();
            foreach (var li in lvSourceTables.Items.Cast<ListViewItem>())
            {
                li.Checked = true;
            }
        }

        private void FillDictionaries()
        {
            try
            {
                tvDictionaries.BeginUpdate();
                tvDictionaries.Nodes.Clear();
                foreach (var cat in _categories)
                {
                    var catnode = new TreeNode(cat.Key);
                    tvDictionaries.Nodes.Add(catnode);
                    foreach (var childnode in from string item in cat.Value select new TreeNode(item))
                    {
                        catnode.Nodes.Add(childnode);
                    }
                }
                tvDictionaries.Sort();
            }
            finally
            {
                tvDictionaries.EndUpdate();
            }
        }

        private void FillExtractCategories()
        {
            try
            {
                lbExtractCategory.BeginUpdate();
                lbExtractCategory.Items.Clear();
                foreach (var item in _extractCategories)
                {
                    lbExtractCategory.Items.Add(item);
                }
            }
            finally
            {
                lbExtractCategory.EndUpdate();
            }
        }

        private void FillExtractKnownNames()
        {
            try
            {
                lbExtractKnownNames.BeginUpdate();
                lbExtractKnownNames.Items.Clear();
                foreach (var item in _extractKnownNames)
                {
                    lbExtractKnownNames.Items.Add(item);
                }
            }
            finally
            {
                lbExtractKnownNames.EndUpdate();
            }
        }

        private void FillSourceTablesList()
        {
            lvSourceTables.Items.Clear();
            if (!Directory.Exists(_sourcepath)) return;
            var list = Directory.GetFiles(_sourcepath, "*.xls?");
            foreach (var filename in list.Select(Path.GetFileName)
                .Where(filename => filename == null || !filename.StartsWith("~")))
            {
                lvSourceTables.Items.Add(filename);
                toolStripStatusLabel1.Text = @"Добавлен файл " + filename;
            }
        }

        #endregion

        #region Извлечение материалов и оборудования

        private void btnExtractFromSourceTables_Click(object sender, EventArgs e)
        {
            ExtractFromSourceTable();
            tvResults.Nodes.Clear();
        }

        private void ExtractFromSourceTable()
        {
            lvGoods.Items.Clear();
            if (!lvSourceTables.Items.Cast<ListViewItem>().Any(li => li.Checked)) return;
            toolStripStatusLabel1.Text = @"Извлечение материалов и оборудования...";
            btnExtractFromSourceTables.Enabled = false;
            Cursor = Cursors.WaitCursor;
            dynamic xl = Activator.CreateInstance(Type.GetTypeFromProgID("Excel.Application"));
            try
            {
                _sourceItems.Clear();
                toolStripProgressBar1.Value = 0;
                toolStripProgressBar1.Maximum = lvSourceTables.Items.Cast<ListViewItem>().Count(item => item.Checked);
                toolStripProgressBar1.Visible = true;
                var knownExtractNames = new List<string>(_extractKnownNames.OfType<string>());
                var knownCategories = new List<string>();
                knownCategories.AddRange(_extractCategories.OfType<string>());
                foreach (var filename in from ListViewItem li in lvSourceTables.Items
                                         where li.Checked
                                         select Path.Combine(_sourcepath, li.Text)
                                         into filename
                                         where File.Exists(filename)
                                         select filename)
                {
                    toolStripStatusLabel1.Text = @"Обработка файла: " + filename + @"...";
                    var fileExt = Path.GetExtension(filename);
                    if (fileExt != null && fileExt.ToLowerInvariant() == ".xls")
                    {
                        // обработка файлов со спецификациями для многостраничных форм
                        var wb1 = xl.Workbooks.Open(filename, 0, true);
                        try
                        {
                            var count = wb1.Sheets.Count;
                            if (count >= 3)
                            {
                                var sheet2 = wb1.Sheets[2];
                                string baseNames = sheet2.Range["H" + 2].Text.Trim();
                                Console.WriteLine(baseNames);
                                var sheet3 = wb1.Sheets[3];
                                string colB1 = sheet3.Range["B" + 1].Text.Trim();
                                if (colB1 == "Наименование и техническая характеристика")
                                {
                                    var row = 2;
                                    bool? customer = null;
                                    var category = "";
                                    while (true)
                                    {
                                        string colA = sheet3.Range["A" + row].Text.Trim();
                                        string colB = sheet3.Range["B" + row].Text.Trim();
                                        string colC = sheet3.Range["C" + row].Text.Trim();
                                        string colF = sheet3.Range["F" + row].Text.Trim();
                                        string colG = sheet3.Range["G" + row].Text.Trim();
                                        if ((customer == null || !(bool)customer) && colB.EndsWith("заказчиком"))
                                        {
                                            customer = true;
                                            continue;
                                        }
                                        if ((customer == null || (bool) customer) && colB.EndsWith("подрядчиком"))
                                        {
                                            customer = false;
                                            continue;
                                        }
                                        if (customer != null &&
                                            !String.IsNullOrWhiteSpace(colA) && !String.IsNullOrWhiteSpace(colB) &&
                                            !String.IsNullOrWhiteSpace(colC) && !String.IsNullOrWhiteSpace(colF) &&
                                            !String.IsNullOrWhiteSpace(colG))
                                        {
                                            var desc = (colB + " " + colC).Trim();
                                            foreach (var sc1 in _categories)
                                            {
                                                var stringCollection = sc1.Value;
                                                if (stringCollection == null) continue;
                                                var sc2 = sc1;
                                                foreach (var sc in (stringCollection.Cast<string>()
                                                                                    .Where(s => desc.StartsWith(s,
                                                                                                                StringComparison
                                                                                                                    .OrdinalIgnoreCase)))
                                                    .Select(s1 => sc2))
                                                {
                                                    category = sc.Key;
                                                    goto lbl;
                                                }
                                            }
                                        lbl:
                                            double ncount;
                                            double.TryParse(colG.Split('\n')[0].Replace(',', '.'), NumberStyles.Float,
                                                            CultureInfo.GetCultureInfo("en-US"),
                                                            out ncount);
                                            if (
                                                _sourceItems.Any(
                                                    item =>
                                                    item.Desc == desc && item.Eu == colF && item.Document == baseNames))
                                            {
                                                var aitem =
                                                    _sourceItems.First(
                                                        item =>
                                                        item.Desc == desc && item.Eu == colF && item.Document == baseNames);
                                                aitem.Numbers += ncount;
                                            }
                                            else
                                            {
                                                var sourceItem = new SourceItem
                                                {
                                                    Category = category,
                                                    Group = "",
                                                    ShortItem = "",
                                                    Standart = "",
                                                    Document = baseNames,
                                                    Desc = desc,
                                                    Eu = colF,
                                                    Customer = customer ?? false,
                                                    Numbers = ncount,
                                                    FileName = Path.GetFileName(filename)
                                                };
                                                _sourceItems.Add(sourceItem);
                                            }                                          
                                        }
                                        row++;
                                        if (row > 500) break;
                                    }
                                }
                            }
                        }
                        finally
                        {
                            wb1.Close(false);
                            toolStripProgressBar1.Value++;
                        }
                    }
                    if (fileExt != null && fileExt.ToLowerInvariant() == ".xlsx")
                    {
                        var wb1 = xl.Workbooks.Open(filename, 0, true);
                        try
                        {
                            var sheet1 = wb1.Sheets[1];

                            var xlCellTypeLastCell = 11;
                            var lastCell = sheet1.Cells.SpecialCells(xlCellTypeLastCell);
                            var lastCellRow = lastCell.Row;
                            var colName = IndexToAbc(lastCell.Column);
                            var arrData = (object[,])sheet1.Range[$"A1:{colName}{lastCell.Row}"].Value;

                            var baseNames = "";
                            var fileName = Path.GetFileName(filename);
                            if (fileName != null && char.IsDigit(fileName[0]))
                            {
                                // обработка файлов со спецификациями
                                baseNames = Path.GetFileNameWithoutExtension(fileName);
                                var row = 1;
                                var category = "";
                                var subcat = "";
                                var shortitem = "";
                                while (true)
                                {
                                    string col1 = $"{arrData[row, 1]}"; //sheet1.Range["A" + row].Text.Trim();
                                    var customer = col1.IndexOf('*') >= 0;
                                    string colB = Normalize($"{arrData[row, 2]}"); // наименование, sheet1.Range["B" + row].Text.Trim()
                                    if (knownCategories.Contains(colB))
                                        category = colB;
                                    var isKnownExtractName = knownExtractNames.Any(colB.StartsWith);
                                    string colC = $"{arrData[row, 3]}"; //sheet1.Range["C" + row].Text.Trim(); // тип, марка
                                    string colF = $"{arrData[row, 6]}"; //sheet1.Range["F" + row].Text.Trim(); // ед. изм.
                                    string colG = $"{arrData[row, 7]}"; //sheet1.Range["G" + row].Text.Trim(); // кол-во
                                    if (!string.IsNullOrWhiteSpace(colB) &&
                                        (char.IsUpper(colB[0]) || char.IsDigit(colB[0])) &&
                                        !knownCategories.Contains(colB) &&
                                        !string.IsNullOrWhiteSpace(colC) && !isKnownExtractName &&
                                        string.IsNullOrWhiteSpace(colF) && string.IsNullOrWhiteSpace(colG))
                                    {
                                        subcat = colB;
                                        shortitem = colC;
                                    }
                                    if (!string.IsNullOrWhiteSpace(colB) &&
                                        (char.IsUpper(colB[0]) || char.IsDigit(colB[0])) &&
                                        !knownCategories.Contains(colB) &&
                                        !isKnownExtractName && !string.IsNullOrWhiteSpace(colF) &&
                                        !string.IsNullOrWhiteSpace(colG))
                                    {
                                        if (!String.IsNullOrWhiteSpace(colC)) shortitem = colC;
                                        var desc = (subcat + " " + colB + " " + shortitem).Trim();
                                        foreach (var sc1 in _categories)
                                        {
                                            var stringCollection = sc1.Value;
                                            if (stringCollection == null) continue;
                                            var sc2 = sc1;
                                            foreach (var sc in (stringCollection.Cast<string>()
                                                                                .Where(s => desc.StartsWith(s,
                                                                                                            StringComparison
                                                                                                                .OrdinalIgnoreCase)))
                                                .Select(s1 => sc2))
                                            {
                                                category = sc.Key;
                                                goto lbl;
                                            }
                                        }
                                        lbl:
                                        if (category.Length == 0)
                                            category = "яПрочие материалы";
                                        double ncount;
                                        double.TryParse(colG.Split('\n')[0].Replace(',', '.'), NumberStyles.Float,
                                                        CultureInfo.GetCultureInfo("en-US"),
                                                        out ncount);
                                        if (
                                            _sourceItems.Any(
                                                item =>
                                                item.Desc == desc && item.Eu == colF && item.Document == baseNames))
                                        {
                                            var aitem =
                                                _sourceItems.First(
                                                    item =>
                                                    item.Desc == desc && item.Eu == colF && item.Document == baseNames);
                                            aitem.Numbers += ncount;
                                        }
                                        else
                                        {
                                            var sourceItem = new SourceItem
                                                {
                                                    Category = category,
                                                    Group = subcat,
                                                    ShortItem = shortitem,
                                                    Standart = "",
                                                    Document = baseNames,
                                                    Desc = desc,
                                                    Eu = colF,
                                                    Customer = customer,
                                                    Numbers = ncount,
                                                    FileName = fileName
                                                };
                                            string newshortvalue;
                                            string newstandart;
                                            ExtractStandart(shortitem, out newshortvalue, out newstandart);
                                            sourceItem.Standart = newstandart;
                                            sourceItem.ShortItem = newshortvalue;
                                            _sourceItems.Add(sourceItem);
                                        }

                                    }
                                    row++;
                                    if (row > lastCellRow) break;
                                }
                            }
                            else
                            {
                                // обработка файлов со сметами
                                var row = 1;
                                while (true)
                                {
                                    string rawBaseNames = $"{arrData[row, 4]}"; //sheet1.Range[String.Format("D{0}", row)].Text.Trim();
                                    if (rawBaseNames.Trim().StartsWith("Основание:"))
                                    {
                                        var a1 = rawBaseNames.Split(new[] {"Основание:"},
                                                                    StringSplitOptions.RemoveEmptyEntries);
                                        if (a1.Length == 1)
                                        {
                                            baseNames = a1[0].Trim().Split(new[] {' '})[0];
                                            break;
                                        }
                                    }
                                    row++;
                                    if (row > lastCellRow) break;
                                }
                                if (!string.IsNullOrWhiteSpace(baseNames))
                                {
                                    SelectGoods("Материалы", baseNames, sheet1, row, fileName);
                                    SelectGoods("Оборудование", baseNames, sheet1, row, fileName);
                                }
                            }
                        }
                        finally
                        {
                            wb1.Close(false);
                            toolStripProgressBar1.Value++;
                        }
                    }
                }

                #region группировка наименований

                toolStripStatusLabel1.Text = @"Группировка наименований...";
                foreach (var group in _sourceItems.GroupBy(item => item.Category).OrderBy(group => @group.Key))
                {
                    var query = from item in @group
                                let filter = Extract(item.Desc)
                                group item by filter
                                into g
                                select g;
                    foreach (var subgroup in query)
                    {
                        foreach (var sourceItem in subgroup)
                        {
                            var aout = sourceItem.Desc.Split(new[] {subgroup.Key}, StringSplitOptions.None);
                            var shortvalue = aout.Length > 1 ? aout[1] : "";
                            string newshortvalue;
                            string newstandart;
                            ExtractStandart(shortvalue, out newshortvalue, out newstandart);
                            sourceItem.Standart = newstandart;
                            if (string.IsNullOrWhiteSpace(sourceItem.ShortItem))
                                sourceItem.ShortItem = newshortvalue;
                            sourceItem.Group = subgroup.Key;
                        }
                    }
                }

                #endregion

                FillList();
            }
            finally
            {
                toolStripProgressBar1.Visible = false;
                xl.Quit();
                Cursor = Cursors.Default;
                btnExtractFromSourceTables.Text = @"Обновить...";
                btnExtractFromSourceTables.Enabled = true;
            }
        }

        private static void ExtractStandart(string shortvalue, out string newshortvalue, out string newstandart)
        {
            newshortvalue = shortvalue;
            newstandart = "";
            var ashsort = shortvalue.Split(new[] {' '});
            if (!ashsort.Any(item =>
                             (String.Equals(item, "ГОСТ") || String.Equals(item, "ОСТ") ||
                              String.Equals(item, "ТУ")))) return;
            var list = new List<string>();
            var standart = new List<string>();
            var flag = 0;
            foreach (var item in ashsort)
            {
                if (String.Equals(item, "ГОСТ") || String.Equals(item, "ОСТ") ||
                    String.Equals(item, "ТУ"))
                {
                    standart.Add(item.Trim());
                    flag = 1;
                }
                else
                {
                    switch (flag)
                    {
                        case 2:
                        case 0:
                            list.Add(item.Trim());
                            break;
                        case 1:
                            standart.Add(item.Trim());
                            flag = 2;
                            break;
                    }
                }
            }
            newshortvalue = String.Join(" ", list);
            newstandart = String.Join(" ", standart);
        }

        private void FillList()
        {
// заполнение списка
            toolStripStatusLabel1.Text = @"Заполнение списка...";
            try
            {
                lvGoods.BeginUpdate();
                lvGoods.ListViewItemSorter = null;
                _lastColumn = -1;
                lvGoods.Items.Clear();
                lvGoods.Groups.Clear();
                foreach (var si in _sourceItems)
                {
                    var goodItem = new ListViewItem(new[]
                        {
                            si.Desc, si.Eu, 
                            si.Numbers.ToString(CultureInfo.GetCultureInfo("en-US")), 
                            si.Document, si.Customer ? "Заказчик" : "Подрядчик", 
                            si.Category.StartsWith("яПрочие материалы") ? si.Category.Substring(1) : si.Category, 
                            si.Group, si.ShortItem, si.Standart
                        });
                    //var filename = si.FileName;
                    //var @group = ListViewGroup(filename, filename);
                    //goodItem.Group = @group;
                    lvGoods.Items.Add(goodItem);
                }
            }
            finally
            {
                lvGoods.EndUpdate();
                lvGoods.ListViewItemSorter = new ListViewItemComparer(0);
                _lastColumn = 0;
            }

            toolStripStatusLabel1.Text = @"Готово.";
        }

        private static string Extract(string value)
        {
            var aitems = value.Split(new[] {' '});
            var buff = new List<String>();
            var n = 0;
            foreach (var aitem in aitems)
            {
                if (String.IsNullOrWhiteSpace(aitem)) continue;
                if (n > 0 && !Char.IsLower(aitem.Trim(), 0)) break;
                buff.Add(aitem.Trim());
                n++;
            }
            return String.Join(" ", buff);
        }

        private void SelectGoods(string goods, string baseNames, dynamic sheet1, int row, string filename)
        {
            var xlCellTypeLastCell = 11;
            var lastCell = sheet1.Cells.SpecialCells(xlCellTypeLastCell);
            //string[,] list = new string[lastCell.Column, lastCell.Row];
            var lastCellRow = lastCell.Row;
            var colName = IndexToAbc(lastCell.Column);
            var arrData = (object[,])sheet1.Range[$"A1:{colName}{lastCell.Row}"].Value;

            // поиск раздела материалы
            var materials = false;
            while (true)
            {
                string col1 = $"{arrData[row, 1]}"; //sheet1.Range["A" + row].Text.Trim();
                if (col1.StartsWith("Раздел") && col1.IndexOf(goods, StringComparison.Ordinal) >= 0)
                {
                    materials = true;
                    break;
                }
                row++;
                if (row > lastCellRow) break;
            }
            if (!materials) return;
            row++;
            var part = "";
            var basepart = "";
            while (true)
            {
                string col1 = $"{arrData[row, 1]}"; //sheet1.Range["A" + row].Text.Trim();
                var acols = col1.Split(new [] {'\n'});
                int npp;
                if (int.TryParse(acols[0].Trim(), out npp))
                {
                    var customer = acols.Length > 1 && acols[1] == "*";
                    string obosnovanie = $"{arrData[row, 2]}"; //sheet1.Range["B" + row].Text.Trim();
                    if (obosnovanie.Length > 0 && Char.IsDigit(obosnovanie[0])) goto lbl2;
                    string rawDesc = $"{arrData[row, 3]}"; //sheet1.Range["C" + row].Text.Trim();
                    string eu = $"{arrData[row, 4]}"; //sheet1.Range["D" + row].Text.Trim();
                    if (eu == "шт") eu += ".";
                    string count = $"{arrData[row, 5]}"; //sheet1.Range["E" + row].Text.Trim();
                    var desc = rawDesc.Split(new[] {'\n'})[0].Trim();
                    var document = part.Length > 0 ? basepart + part : baseNames;
                    var category = "";
                    foreach (var sc1 in _categories)
                    {
                        var stringCollection = sc1.Value;
                        if (stringCollection == null) continue;
                        var sc2 = sc1;
                        foreach (var sc in (stringCollection.Cast<string>()
                                                            .Where(s => desc.StartsWith(s,
                                                                                        StringComparison
                                                                                            .OrdinalIgnoreCase))).Select(s1 => sc2))
                        {
                            category = sc.Key;
                            goto lbl;
                        }
                    }
                    lbl:
                    if (category.Length == 0)
                        category = "яПрочие материалы";
                    double ncount;
                    double.TryParse(count.Split('\n')[0].Replace(',', '.'), NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"),
                                    out ncount);
                    if (_sourceItems.Any(item => item.Desc == desc && item.Eu == eu && 
                        item.Customer == customer && item.Document == document))
                    {
                        var aitem =
                            _sourceItems.First(
                                item => item.Desc == desc && item.Eu == eu && item.Customer == customer && item.Document == document);
                        aitem.Numbers += ncount;
                    }
                    else
                        _sourceItems.Add(new SourceItem
                            {
                                Category = category,
                                Group = "",
                                ShortItem = "",
                                Standart = "",
                                Document = document,
                                Desc = desc,
                                Eu = eu,
                                Customer = customer,
                                Numbers = ncount,
                                FileName = filename
                            });
                }
                else
                {
                    if (col1.StartsWith("Итого")) break;
                    if (col1.Trim().StartsWith("Удаленные и замененные ресурсы")) break;
                    if (baseNames.IndexOf(',') > 0)
                    {
                        var first = part.Length == 0;
                        part = col1.Trim();
                        if (first)
                            basepart = baseNames.Split(new[] {part}, StringSplitOptions.None)[0].Trim();
                    }
                }
            lbl2:
                row++;
                if (row > 5000) break;
            }
        }

        private int _lastColumn = -1;
        private void LvListColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (_lastColumn != e.Column)
            {
                lvGoods.ListViewItemSorter = new ListViewItemComparer(e.Column);
                _lastColumn = e.Column;
            }
            else
            {
                if (lvGoods.ListViewItemSorter is ListViewItemComparer)
                    lvGoods.ListViewItemSorter = new ListViewItemReverseComparer(e.Column);
                else
                    lvGoods.ListViewItemSorter = new ListViewItemComparer(e.Column);
            }
            if (lvGoods.FocusedItem != null)
                lvGoods.FocusedItem.EnsureVisible();
        }

        private void lbTablesSourceFolder_TextChanged(object sender, EventArgs e)
        {
            _sourcepath = lbTablesSourceFolder.Text;
        }

        private void tvDictionaries_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
            {
                btnRenameCategory.Enabled = false;
                btnDeleteCategory.Enabled = false;
                EmptyFilters();
                return;
            }
            btnRenameCategory.Enabled = true;
            btnDeleteCategory.Enabled = true;
            btnAddCatFilter.Enabled = true;
            var node = e.Node;
            if (e.Node.Level == 1) node = node.Parent;
            lbSelectedCategory.Text = @"Категория: " + node.Text;
            btnRenameCatFilter.Enabled = false;
            btnDeleteCatFilter.Enabled = false;
            lbCategoryFilters.Items.Clear();
            foreach (var childnode in node.Nodes.Cast<TreeNode>())
            {
                lbCategoryFilters.Items.Add(childnode.Text);
            }
        }

        private void EmptyFilters()
        {
            btnRenameCategory.Enabled = false;
            btnDeleteCategory.Enabled = false;
            lbCategoryFilters.Items.Clear();
            btnAddCatFilter.Enabled = false;
            btnRenameCatFilter.Enabled = false;
            btnDeleteCatFilter.Enabled = false;
            lbSelectedCategory.Text = @"Категория: (не выбрано)";
        }

        private void lbCategoryFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbCategoryFilters.SelectedItem == null)
            {
                btnRenameCatFilter.Enabled = false;
                btnDeleteCatFilter.Enabled = false;
            }
            else
            {
                btnRenameCatFilter.Enabled = true;
                btnDeleteCatFilter.Enabled = true;                
            }
        }

        public string InputBox(string prompt, string title, string defaultResponce)
        {
            using (var frm = new InputStringForm())
            {
                frm.Text = title;
                frm.lbPrompt.Text = prompt;
                frm.tbValue.Text = defaultResponce;
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    return frm.tbValue.Text;
                }
            }
            return defaultResponce;
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            var newvalue = InputBox(@"Введите название категории:", @"Добавление категории", "");
            if (newvalue == "") return;
            if (tvDictionaries.Nodes.Cast<TreeNode>().Any(item => item.Text == newvalue))
            {
                MessageBox.Show(this, @"Введённое название категории уже существует!", @"Добавление категории",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _categories.Add(newvalue, new StringCollection());
            EmptyFilters();
            FillDictionaries();
            FindCategory(newvalue);
        }

        private void btnRenameCategory_Click(object sender, EventArgs e)
        {
            var newvalue = InputBox(@"Введите новое название категории:", @"Переименование категории",
                                    tvDictionaries.SelectedNode.Text);
            if (newvalue == tvDictionaries.SelectedNode.Text) return;
            if (tvDictionaries.Nodes.Cast<TreeNode>().Any(item => item.Text == newvalue))
            {
                MessageBox.Show(this, @"Введённое название категории уже существует!", @"Добавление категории",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var list = _categories[tvDictionaries.SelectedNode.Text];
            _categories.Remove(tvDictionaries.SelectedNode.Text);
            _categories.Add(newvalue, list);
            EmptyFilters();
            FillDictionaries();
            FindCategory(newvalue);
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Категория \"" + tvDictionaries.SelectedNode.Text +
                                      "\" будет безвозвратно удалена вместе с фильтрами! Удалить?",
                                @"Удаление категории",
                                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button3) != DialogResult.Yes) return;
            _categories.Remove(tvDictionaries.SelectedNode.Text);
            EmptyFilters();
            FillDictionaries();
        }

        private void FindCategory(string text)
        {
            foreach (var node in tvDictionaries.Nodes.Cast<TreeNode>().Where(node => node.Text == text))
            {
                tvDictionaries.SelectedNode = node;
                node.Expand();
                break;
            }
        }

        private void btnAddCatFilter_Click(object sender, EventArgs e)
        {
            var newvalue = InputBox(@"Введите текст начала предложения фильтра:", @"Добавление фильтра", "");
            if (newvalue == "") return;
            if (lbCategoryFilters.Items.Contains(newvalue))
            {
                MessageBox.Show(this, @"Введённое значение фильтра уже существует!", @"Добавление фильтра",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _categories[tvDictionaries.SelectedNode.Text].Add(newvalue);
            var node = tvDictionaries.SelectedNode;
            if (node.Level == 1) node = node.Parent;
            var category = node.Text;
            EmptyFilters();
            FillDictionaries();
            FindCategory(category);
        }

        private void btnRenameCatFilter_Click(object sender, EventArgs e)
        {
            var newvalue = InputBox(@"Введите новый текст начала предложения фильтра:", @"Изменение текста фильтра",
                                    lbCategoryFilters.SelectedItem.ToString());
            if (newvalue == lbCategoryFilters.SelectedItem.ToString()) return;
            if (lbCategoryFilters.Items.Contains(newvalue))
            {
                MessageBox.Show(this, @"Введённое значение фильтра уже существует!", @"Добавление фильтра",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var node = tvDictionaries.SelectedNode;
            if (node.Level == 1) node = node.Parent;
            var key = node.Text;
            if (_categories.ContainsKey(key))
            {
                var filter = lbCategoryFilters.SelectedItem.ToString();
                _categories[key].Remove(filter);
                _categories[key].Add(newvalue);
            }
            var category = node.Text;
            EmptyFilters();
            FillDictionaries();
            FindCategory(category);
        }

        private void btnDeleteCatFilter_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Условие фильтра \"" + lbCategoryFilters.SelectedItem +
                                      "\" будет безвозвратно удалено! Удалить?",
                                      "Удаление фильтра для категории \"" + tvDictionaries.SelectedNode.Text + "\"",
                                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button3) == DialogResult.Yes)
            {
                _categories[tvDictionaries.SelectedNode.Text].Remove(lbCategoryFilters.SelectedItem.ToString());
                var node = tvDictionaries.SelectedNode;
                if (node.Level == 1) node = node.Parent;
                var category = node.Text;
                EmptyFilters();
                FillDictionaries();
                FindCategory(category);
            }
        }

        private void btnBuildTree_Click(object sender, EventArgs e)
        {
            BuildResultTree();
        }

        private void BuildResultTree()
        {
            tvResults.Nodes.Clear();
            tvResults.BeginUpdate();
            Cursor = Cursors.WaitCursor;
            try
            {
                foreach (var category in _sourceItems.GroupBy(item => item.Category).OrderBy(group => @group.Key))
                {
                    var categoryNode = new TreeNode(category.Key.StartsWith("яПрочие материалы") 
                        ? category.Key.Substring(1) 
                        : category.Key);
                    tvResults.Nodes.Add(categoryNode);
                    foreach (var @group in category.GroupBy(item => item.Group).OrderBy(group => @group.Key))
                    {
                        var groupNode = new TreeNode(@group.Key);
                        categoryNode.Nodes.Add(groupNode);
                        foreach (var sourceItem in @group.GroupBy(item => item.ShortItem).OrderBy(groupitem => groupitem.Key))
                        {
                            if (sourceItem.Count() > 1)
                            {
                                var shortItemNode = new TreeNode(Normalize(sourceItem.Key));
                                groupNode.Nodes.Add(shortItemNode);
                                foreach (
                                    var sourceNode in
                                        sourceItem.Select(item => new TreeNode(String.Format("{0}|{1}|{2}|{3}|{4}",
                                                                                             item.Document,
                                                                                             item.Customer,
                                                                                             item.Standart, item.Eu,
                                                                                             item.Numbers.ToString(
                                                                                                 CultureInfo
                                                                                                     .GetCultureInfo(
                                                                                                         "en-US"))))))
                                {
                                    shortItemNode.Nodes.Add(sourceNode);
                                }
                            }
                            else
                            {
                                foreach (var sourceNode in sourceItem.Select(item =>
                                                                             new TreeNode(
                                                                                 String.Format(
                                                                                     "{0}|{1}|{2}|{3}|{4}|{5}",
                                                                                     Normalize(item.ShortItem),
                                                                                     item.Document, item.Customer,
                                                                                     item.Standart, item.Eu,
                                                                                     item.Numbers.ToString(
                                                                                         CultureInfo.GetCultureInfo(
                                                                                             "en-US"))))))
                                {
                                    groupNode.Nodes.Add(sourceNode);
                                }
                            }
                        }
                        groupNode.Expand();
                    }
                    foreach (var groupNode in categoryNode.Nodes.Cast<TreeNode>()
                        .Where(groupNode => groupNode.Nodes.Count == 1))
                    {
                        groupNode.Text += @"|" + groupNode.Nodes[0].Text;
                        if (groupNode.Nodes[0].Nodes.Count > 0)
                        {
                            var childs = groupNode.Nodes[0].Nodes;
                            groupNode.Nodes.Clear();
                            foreach (var child in childs.Cast<TreeNode>())
                            {
                                groupNode.Nodes.Add(child);
                            }
                        }
                        else
                            groupNode.Nodes.Clear();
                    }
                    categoryNode.Expand();
                }
                // допобработка
                foreach (var categoryNode in tvResults.Nodes.Cast<TreeNode>())
                {
                    foreach (var groupNode in categoryNode.Nodes.Cast<TreeNode>())
                    {
                        if (groupNode.Nodes.Count == 0) continue;
                        var founddiff = false;
                        while (true)
                        {
                            var firstword = groupNode.Nodes[0].Text.Split(new[] { ' ' })[0];
                            foreach (var itemNode in groupNode.Nodes.Cast<TreeNode>())
                            {
                                if (firstword != itemNode.Text.Split(new[] {' '})[0])
                                {
                                    founddiff = true;
                                    break;
                                }
                            }
                            if (founddiff)
                                break;
                            groupNode.Text += @" " + firstword;
                            foreach (var itemNode in groupNode.Nodes.Cast<TreeNode>())
                            {
                                if (firstword.Length + 1 < itemNode.Text.Length)
                                    itemNode.Text = itemNode.Text.Substring(firstword.Length + 1);
                            }
                        }
                    }
                }
            }
            finally
            {
                tvResults.EndUpdate();
                btnExportTable.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void btnExportTable_Click(object sender, EventArgs e)
        {
            ExportToTable();
        }

        private void ExportToTable()
        {
            var path = Application.StartupPath;
            var templateName = Path.Combine(path, "Разделительная ведомость оборудования и материалов52.xltx");
            var outputName = Path.ChangeExtension(Path.Combine(path, templateName), ".xlsx");
            if (File.Exists(outputName))
            {
                try
                {
                    File.Delete(outputName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, @"Ошибка удаления предыдущей версии таблицы", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }
            }
            btnBuildTree.Enabled = false;
            btnExportTable.Enabled = false;
            toolStripStatusLabel1.Text = @"Выгрузка...";
            dynamic xl = Activator.CreateInstance(Type.GetTypeFromProgID("Excel.Application"));
            try
            {
                Cursor = Cursors.WaitCursor;
                var wb = xl.Workbooks.Open(templateName, 0, true);
                var sheet = wb.Sheets[1];
                var addrs = new[] 
                {
                    "B13","B14","B15","B16","B17",
                    "D13","D14","D15","D16","D17",
                    "G13","G14","G15","G16","G17",
                    "K13","K14","K15","K16","K17",
                    "O13","O14","O15","O16","O17",
                    "S13","S14","S15","S16","S17",
                    "W13","W14","W15","W16","W17",
                    "AA13","AA14","AA15","AA16","AA17",
                    "AE13","AE14","AE15","AE16","AE17",
                    "AI13","AI14","AI15","AI16","AI17",
                    "AM13","AM14"
                };
                var dict = new Dictionary<string, int>();
                var n = 0;
                var ncolStart = AbcToIndex("G");
                foreach (var document in _sourceItems.GroupBy(item => item.Document).OrderBy(item => item.Key))
                {
                    var item = addrs[n];
                    var colName = item.Length == 3 ? item.Substring(0, 1) : item.Substring(0, 2);
                    var rowName = item.Length == 3 ? item.Substring(1) : item.Substring(2);
                    var ncol = AbcToIndex(colName);
                    var nrow = int.Parse(rowName);
                    sheet.Cells[nrow, ncol] = document.Key;
                    sheet.Cells[19, ncolStart] = document.Key;
                    dict.Add(document.Key, ncolStart);
                    ncolStart++;
                    n++;
                    if (n >= addrs.Length) break;
                }
                var row = 25;
                var npp = 1;
                foreach (var categoryNode in tvResults.Nodes.Cast<TreeNode>())
                {
                    tvResults.SelectedNode = categoryNode;
                    var range1 = sheet.Rows("23:24");
                    range1.Select();
                    sheet.Range("B23").Activate();
                    range1.Copy();
                    sheet.Rows(string.Format("{0}:{0}", row)).Select();
                    sheet.Range(string.Format("B{0}", row)).Activate();
                    sheet.Paste();

                    var range2 = sheet.Rows("22:22");
                    range2.Select();
                    sheet.Range("B22").Activate();
                    range2.Copy();
                    sheet.Rows(string.Format("{0}:{0}", row - 1)).Select();
                    sheet.Range(string.Format("B{0}", row - 1)).Activate();
                    sheet.Paste();
                    sheet.Cells[row - 1, AbcToIndex("B")] = categoryNode.Text;
                    foreach (var groupNode in categoryNode.Nodes.Cast<TreeNode>())
                    {
                        tvResults.SelectedNode = groupNode;
                        var avals = groupNode.Text.Trim().Split(new[] {'|'});
                        if (avals.Length == 7)
                        {
                            var groupName = avals[0].Trim();
                            var shortItem = avals[1].Trim();
                            var document = avals[2].Trim();
                            var customer = avals[3].Trim();
                            var standart = avals[4].Trim();
                            var eu = avals[5].Trim();
                            var numbers = avals[6].Trim();
                            sheet.Cells[row, AbcToIndex("B")] = (npp++).ToString("0");
                            sheet.Cells[row, AbcToIndex("C")] = groupName;
                            sheet.Cells[row, AbcToIndex("D")] = shortItem;
                            sheet.Cells[row, AbcToIndex("E")] = standart;
                            sheet.Cells[row, AbcToIndex("F")] = eu;
                            if (dict.ContainsKey(document))
                                sheet.Cells[row, dict[document]] = numbers;
                            if (customer == "True")
                                sheet.Cells[row, AbcToIndex("BG")] = numbers; // AB
                            else
                                sheet.Cells[row, AbcToIndex("BH")] = numbers; // AC
                            PasteBlankRow(sheet, row);
                            row++;
                        }
                        else if (avals.Length == 2)
                        {
                            var groupName = avals[0].Trim();
                            var shortItem = avals[1].Trim();
                            sheet.Cells[row, AbcToIndex("B")] = (npp++).ToString("0");
                            sheet.Cells[row, AbcToIndex("C")] = groupName;
                            sheet.Cells[row, AbcToIndex("D")] = shortItem;
                            FillRow(sheet, row, groupNode, dict);
                            PasteBlankRow(sheet, row);
                            row++;                                             
                        }
                        else
                        {
                            sheet.Cells[row, AbcToIndex("C")] = groupNode.Text.Trim();
                            PasteBlankRow(sheet, row);
                            row++;
                            foreach (var itemNode in groupNode.Nodes.Cast<TreeNode>())
                            {
                                tvResults.SelectedNode = itemNode;
                                var aitemvals = itemNode.Text.Trim().Split(new[] { '|' });
                                if (aitemvals.Length == 6)
                                {
                                    var shortItem = aitemvals[0].Trim();
                                    var document = aitemvals[1].Trim();
                                    var customer = aitemvals[2].Trim();
                                    var standart = aitemvals[3].Trim();
                                    var eu = aitemvals[4].Trim();
                                    var numbers = aitemvals[5].Trim();
                                    sheet.Cells[row, AbcToIndex("B")] = (npp++).ToString("0");
                                    sheet.Cells[row, AbcToIndex("C")] = "      " + shortItem;
                                    sheet.Cells[row, AbcToIndex("E")] = standart;
                                    sheet.Cells[row, AbcToIndex("F")] = eu;
                                    if (dict.ContainsKey(document))
                                        sheet.Cells[row, dict[document]] = numbers;
                                    if (customer == "True")
                                        sheet.Cells[row, AbcToIndex("BG")] = numbers;   //AB
                                    else
                                        sheet.Cells[row, AbcToIndex("BH")] = numbers;   //AC
                                    PasteBlankRow(sheet, row);
                                    row++;
                                }
                                else
                                {
                                    sheet.Cells[row, AbcToIndex("B")] = (npp++).ToString("0");
                                    sheet.Cells[row, AbcToIndex("C")] = "      " + itemNode.Text.Trim();
                                    FillRow(sheet, row, itemNode, dict);
                                    PasteBlankRow(sheet, row);
                                    row++;                    
                                }
                            }
                        }
                    }
                    row += 2;
                }
 
                var range = sheet.Rows("22:23");
                range.Select();
                sheet.Range("B22").Activate();
                range.Delete();

                wb.SaveAs(outputName);
                wb.Close();
            }
            finally
            {
                xl.Quit();
                Cursor = Cursors.Default;
                btnExportTable.Enabled = true;
                btnBuildTree.Enabled = true;
                tvResults.SelectedNode = null;
                toolStripStatusLabel1.Text = @"Готово.";
            }
        }

        private static string Normalize(string value)
        {
            var avals = value.Split(new [] { " " }, StringSplitOptions.RemoveEmptyEntries);
            return String.Join(" ", avals);
        }

        private void FillRow(dynamic sheet, int row, TreeNode itemNode, IDictionary<string, int> dict)
        {
            double allNumbers = 0, custNumbers = 0, workNumbers = 0;
            var lastdoc = "";
            foreach (var subitemNode in itemNode.Nodes.Cast<TreeNode>())
            {
                tvResults.SelectedNode = subitemNode;
                var asubitemvals = subitemNode.Text.Trim().Split(new[] {'|'});
                if (asubitemvals.Length == 5)
                {
                    var document = asubitemvals[0].Trim();
                    var customer = asubitemvals[1].Trim();
                    var standart = asubitemvals[2].Trim();
                    var eu = asubitemvals[3].Trim();
                    var numbers = asubitemvals[4].Trim();
                    double num;
                    if (double.TryParse(numbers, NumberStyles.Any,
                                        CultureInfo.GetCultureInfo("en-US"), out num))
                    {
                        if (customer == "True")
                            custNumbers += num;
                        else
                            workNumbers += num;
                        if (lastdoc != document)
                        {
                            lastdoc = document;
                            allNumbers = 0;
                        }
                        allNumbers += num;
                        sheet.Cells[row, AbcToIndex("E")] = standart;
                        sheet.Cells[row, AbcToIndex("F")] = eu;
                        if (dict.ContainsKey(document))
                            sheet.Cells[row, dict[document]] = allNumbers.ToString(CultureInfo.GetCultureInfo("en-US"));
                        sheet.Cells[row, AbcToIndex("BG")] = custNumbers > 0
                                                                 ? custNumbers.ToString(CultureInfo.GetCultureInfo("en-US"))
                                                                 : "";
                        sheet.Cells[row, AbcToIndex("BH")] = workNumbers > 0
                                                                 ? workNumbers.ToString(CultureInfo.GetCultureInfo("en-US"))
                                                                 : "";
                    }
                }
            }
        }

        private static void PasteBlankRow(dynamic sheet, int row)
        {
            var range = sheet.Rows("23:23");
            range.Select();
            sheet.Range("B23").Activate();
            range.Copy();
            sheet.Rows(String.Format("{0}:{0}", row + 1)).Select();
            sheet.Range(String.Format("B{0}", row + 1)).Activate();
            sheet.Paste();
        }

        private static int AbcToIndex(string abc)
        {
            const string s = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (abc.Length == 1 && s.IndexOf(abc, StringComparison.Ordinal) >= 0)
                return s.IndexOf(abc, StringComparison.Ordinal) + 1;
            if (abc.Length == 2 &&
                s.IndexOf(abc.Substring(0, 1), StringComparison.Ordinal) >= 0 &&
                s.IndexOf(abc.Substring(1, 1), StringComparison.Ordinal) >= 0)
                return AbcToIndex(abc.Substring(0, 1)) * 26 + AbcToIndex(abc.Substring(1, 1));
            return 1;
        }

        private static string IndexToAbc(int index)
        {
            const string s = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var letters = s.ToCharArray();
            var result = $"{letters[index - 1]}";
            if (index <= letters.Length)
                return result;
            while (true)
            {
                var big = (index - 1) % 26;
                result = $"{big}{result}";
                index = (index - 1) / 26;
                if (index <= letters.Length)
                    return result;
            }
        }

        private void lvSourceTables_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!_loaded) return;
        }

        private void btnAddExtractCategory_Click(object sender, EventArgs e)
        {
            var newvalue = InputBox(@"Введите название категории:", @"Добавление категории", "");
            if (newvalue == "") return;
            if (lbExtractCategory.Items.Cast<string>().Any(item => item == newvalue))
            {
                MessageBox.Show(this, @"Введённое название категории уже существует!", @"Добавление категории",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _extractCategories.Add(newvalue);
            FillExtractCategories();
        }

        private void btnRenameExtractCategory_Click(object sender, EventArgs e)
        {
            var newvalue = InputBox(@"Введите новое название категории:", @"Переименование категории",
                                    lbExtractCategory.SelectedItem.ToString());
            if (newvalue == lbExtractCategory.SelectedItem.ToString()) return;
            if (lbExtractCategory.Items.Cast<string>().Any(item => item == newvalue))
            {
                MessageBox.Show(this, @"Введённое название категории уже существует!", @"Переименование категории",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _extractCategories.Remove(lbExtractCategory.SelectedItem.ToString());
            _extractCategories.Add(newvalue);
            FillExtractCategories();
            btnRenameExtractCategory.Enabled = false;
            btnDeleteExtractCategory.Enabled = false;
        }

        private void lbExtractCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbExtractCategory.SelectedItem == null)
            {
                btnRenameExtractCategory.Enabled = false;
                btnDeleteExtractCategory.Enabled = false;
            }
            else
            {
                btnRenameExtractCategory.Enabled = true;
                btnDeleteExtractCategory.Enabled = true;
            }
        }

        private void btnDeleteExtractCategory_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Категория \"" + lbExtractCategory.SelectedItem +
                                      "\" будет безвозвратно удалена! Удалить?",
                                @"Удаление категории",
                                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button3) != DialogResult.Yes) return;
            _extractCategories.Remove(lbExtractCategory.SelectedItem.ToString());
            FillExtractCategories();
            btnRenameExtractCategory.Enabled = false;
            btnDeleteExtractCategory.Enabled = false;
        }

        private void lbExtractKnownNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbExtractKnownNames.SelectedItem == null)
            {
                btnChangeExtractKnownName.Enabled = false;
                btnDeleteExtractKnownName.Enabled = false;
            }
            else
            {
                btnChangeExtractKnownName.Enabled = true;
                btnDeleteExtractKnownName.Enabled = true;
            }
        }

        private void btnAddExtractKnownName_Click(object sender, EventArgs e)
        {
            var newvalue = InputBox(@"Введите новое исключение:", @"Добавление исключения", "");
            if (newvalue == "") return;
            if (lbExtractKnownNames.Items.Cast<string>().Any(item => item == newvalue))
            {
                MessageBox.Show(this, @"Введённое исключение уже существует!", @"Добавление исключения",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _extractKnownNames.Add(newvalue);
            FillExtractKnownNames();

        }

        private void btnChangeExtractKnownName_Click(object sender, EventArgs e)
        {
            var newvalue = InputBox(@"Введите новое исключение:", @"Переименование исключения",
                                    lbExtractKnownNames.SelectedItem.ToString());
            if (newvalue == lbExtractKnownNames.SelectedItem.ToString()) return;
            if (lbExtractKnownNames.Items.Cast<string>().Any(item => item == newvalue))
            {
                MessageBox.Show(this, @"Введённое исключение уже существует!", @"Переименование исключения",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _extractKnownNames.Remove(lbExtractKnownNames.SelectedItem.ToString());
            _extractKnownNames.Add(newvalue);
            FillExtractKnownNames();
            btnChangeExtractKnownName.Enabled = false;
            btnDeleteExtractKnownName.Enabled = false;
        }

        private void btnDeleteExtractKnownName_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Исключение \"" + lbExtractKnownNames.SelectedItem +
                                      "\" будет безвозвратно удалено! Удалить?",
                                @"Удаление исключения",
                                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button3) != DialogResult.Yes) return;
            _extractKnownNames.Remove(lbExtractKnownNames.SelectedItem.ToString());
            FillExtractKnownNames();
            btnChangeExtractKnownName.Enabled = false;
            btnDeleteExtractKnownName.Enabled = false;
        }

        //private ListViewGroup ListViewGroup(string key, string header)
        //{
        //    if (String.IsNullOrWhiteSpace(key)) throw new Exception("Пустой ключ");
        //    ListViewGroup group;
        //    if (!_groups.ContainsKey(key))
        //    {
        //        @group = new ListViewGroup(key, header);
        //        _groups.Add(key, @group);
        //        lvGoods.Groups.Add(@group);
        //    }
        //    else
        //        @group = _groups[key];
        //    return @group;
        //}

        #endregion

    }

    #region Вспомогательные классы

    [Serializable]
    public class SourceItem
    {
        public string Category { get; set; }
        public string Group { get; set; }
        public string ShortItem { get; set; }
        public string Desc { get; set; }
        public string Eu { get; set; }
        public double Numbers { get; set; }
        public string Document { get; set; }
        public bool Customer { get; set; }
        public string Standart { get; set; }
        public string FileName { get; set; }
    }

    // Implements the manual sorting of items by columns.
    public class ListViewItemComparer : IComparer
    {
        private readonly int _col;
        /*
                public ListViewItemComparer()
                {
                    _col = 0;
                }
        */

        public ListViewItemComparer(int column)
        {
            _col = column;
        }

        public int Compare(object x, object y)
        {
            var itemX = (ListViewItem) x;
            var itemY = (ListViewItem) y;
            if (_col < itemX.SubItems.Count && _col < itemY.SubItems.Count)
                return String.CompareOrdinal(itemX.SubItems[_col].Text, itemY.SubItems[_col].Text);
            return 0;
        }
    }

    // Implements the manual reverse sorting of items by columns.
    public class ListViewItemReverseComparer : IComparer
    {
        private readonly int _col;
        /*
                public ListViewItemReverseComparer()
                {
                    _col = 0;
                }
        */

        public ListViewItemReverseComparer(int column)
        {
            _col = column;
        }

        public int Compare(object x, object y)
        {
            var itemX = (ListViewItem) x;
            var itemY = (ListViewItem) y;
            if (_col < itemX.SubItems.Count && _col < itemY.SubItems.Count)
                return String.CompareOrdinal(itemY.SubItems[_col].Text, itemX.SubItems[_col].Text);
            return 0;
        }
    }

    // Implements the manual sorting of items last column by date.
    public class ListViewItemDateComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            var itemX = (ListViewItem) x;
            var itemY = (ListViewItem) y;
            var colX = itemX.SubItems.Count - 1;
            var colY = itemY.SubItems.Count - 1;
            var dt = DateTime.MinValue;
            if (itemX.SubItems[colX].Tag != null &&
                itemX.SubItems[colX].Tag.GetType() == dt.GetType() &&
                itemY.SubItems[colY].Tag != null &&
                itemY.SubItems[colY].Tag.GetType() == dt.GetType())
            {
                return DateTime.Compare((DateTime) itemX.SubItems[colX].Tag,
                                        (DateTime) itemY.SubItems[colY].Tag);
            }
            return 0;
        }
    }

    // Implements the manual sorting of items last column by date.
    public class ListViewItemReverseDateComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            var itemX = (ListViewItem) x;
            var itemY = (ListViewItem) y;
            var colX = itemX.SubItems.Count - 1;
            var colY = itemY.SubItems.Count - 1;
            var dt = DateTime.MinValue;
            if (itemX.SubItems[colX].Tag != null &&
                itemX.SubItems[colX].Tag.GetType() == dt.GetType() &&
                itemY.SubItems[colY].Tag != null &&
                itemY.SubItems[colY].Tag.GetType() == dt.GetType())
            {
                return DateTime.Compare((DateTime) itemY.SubItems[colY].Tag,
                                        (DateTime) itemX.SubItems[colX].Tag);
            }
            return 0;
        }
    }

    #endregion

}
