namespace Separating
{
    partial class SeparatingForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tcSteps = new System.Windows.Forms.TabControl();
            this.tabSources = new System.Windows.Forms.TabPage();
            this.lvSourceTables = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSelecTablestSourceFolder = new System.Windows.Forms.Button();
            this.lbTablesSourceFolder = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabDictionaries = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.tvDictionaries = new System.Windows.Forms.TreeView();
            this.btnDeleteCatFilter = new System.Windows.Forms.Button();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.btnAddCatFilter = new System.Windows.Forms.Button();
            this.btnDeleteCategory = new System.Windows.Forms.Button();
            this.btnRenameCategory = new System.Windows.Forms.Button();
            this.btnRenameCatFilter = new System.Windows.Forms.Button();
            this.lbSelectedCategory = new System.Windows.Forms.Label();
            this.lbCategoryFilters = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDeleteExtractKnownName = new System.Windows.Forms.Button();
            this.btnAddExtractCategory = new System.Windows.Forms.Button();
            this.lbExtractKnownNames = new System.Windows.Forms.ListBox();
            this.btnDeleteExtractCategory = new System.Windows.Forms.Button();
            this.btnChangeExtractKnownName = new System.Windows.Forms.Button();
            this.btnRenameExtractCategory = new System.Windows.Forms.Button();
            this.btnAddExtractKnownName = new System.Windows.Forms.Button();
            this.lbExtractCategory = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabResults = new System.Windows.Forms.TabPage();
            this.lvGoods = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnExtractFromSourceTables = new System.Windows.Forms.Button();
            this.tabExport = new System.Windows.Forms.TabPage();
            this.tvResults = new System.Windows.Forms.TreeView();
            this.btnExportTable = new System.Windows.Forms.Button();
            this.btnBuildTree = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.tcSteps.SuspendLayout();
            this.tabSources.SuspendLayout();
            this.tabDictionaries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabResults.SuspendLayout();
            this.tabExport.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcSteps
            // 
            this.tcSteps.Controls.Add(this.tabSources);
            this.tcSteps.Controls.Add(this.tabDictionaries);
            this.tcSteps.Controls.Add(this.tabResults);
            this.tcSteps.Controls.Add(this.tabExport);
            this.tcSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSteps.Location = new System.Drawing.Point(0, 0);
            this.tcSteps.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tcSteps.Name = "tcSteps";
            this.tcSteps.SelectedIndex = 0;
            this.tcSteps.Size = new System.Drawing.Size(1357, 649);
            this.tcSteps.TabIndex = 0;
            // 
            // tabSources
            // 
            this.tabSources.Controls.Add(this.lvSourceTables);
            this.tabSources.Controls.Add(this.btnSelecTablestSourceFolder);
            this.tabSources.Controls.Add(this.lbTablesSourceFolder);
            this.tabSources.Controls.Add(this.label1);
            this.tabSources.Location = new System.Drawing.Point(4, 26);
            this.tabSources.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabSources.Name = "tabSources";
            this.tabSources.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabSources.Size = new System.Drawing.Size(1289, 619);
            this.tabSources.TabIndex = 0;
            this.tabSources.Text = "Исходные таблицы";
            this.tabSources.UseVisualStyleBackColor = true;
            // 
            // lvSourceTables
            // 
            this.lvSourceTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSourceTables.CheckBoxes = true;
            this.lvSourceTables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvSourceTables.FullRowSelect = true;
            this.lvSourceTables.HideSelection = false;
            this.lvSourceTables.Location = new System.Drawing.Point(8, 39);
            this.lvSourceTables.Name = "lvSourceTables";
            this.lvSourceTables.Size = new System.Drawing.Size(1273, 549);
            this.lvSourceTables.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvSourceTables.TabIndex = 3;
            this.lvSourceTables.UseCompatibleStateImageBehavior = false;
            this.lvSourceTables.View = System.Windows.Forms.View.Details;
            this.lvSourceTables.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvSourceTables_ItemChecked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Таблицы для выбора иcходных данных:";
            this.columnHeader1.Width = 750;
            // 
            // btnSelecTablestSourceFolder
            // 
            this.btnSelecTablestSourceFolder.Location = new System.Drawing.Point(475, 7);
            this.btnSelecTablestSourceFolder.Name = "btnSelecTablestSourceFolder";
            this.btnSelecTablestSourceFolder.Size = new System.Drawing.Size(75, 26);
            this.btnSelecTablestSourceFolder.TabIndex = 1;
            this.btnSelecTablestSourceFolder.Text = "Выбор...";
            this.btnSelecTablestSourceFolder.UseVisualStyleBackColor = true;
            this.btnSelecTablestSourceFolder.Click += new System.EventHandler(this.btnSelecTablestSourceFolder_Click);
            // 
            // lbTablesSourceFolder
            // 
            this.lbTablesSourceFolder.AutoEllipsis = true;
            this.lbTablesSourceFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTablesSourceFolder.Location = new System.Drawing.Point(221, 8);
            this.lbTablesSourceFolder.Name = "lbTablesSourceFolder";
            this.lbTablesSourceFolder.Size = new System.Drawing.Size(248, 22);
            this.lbTablesSourceFolder.TabIndex = 0;
            this.lbTablesSourceFolder.TextChanged += new System.EventHandler(this.lbTablesSourceFolder_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Каталог с исходными таблицами:";
            // 
            // tabDictionaries
            // 
            this.tabDictionaries.Controls.Add(this.splitContainer1);
            this.tabDictionaries.Location = new System.Drawing.Point(4, 26);
            this.tabDictionaries.Name = "tabDictionaries";
            this.tabDictionaries.Padding = new System.Windows.Forms.Padding(3);
            this.tabDictionaries.Size = new System.Drawing.Size(1289, 619);
            this.tabDictionaries.TabIndex = 2;
            this.tabDictionaries.Text = "Словари";
            this.tabDictionaries.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.tvDictionaries);
            this.splitContainer1.Panel1.Controls.Add(this.btnDeleteCatFilter);
            this.splitContainer1.Panel1.Controls.Add(this.btnAddCategory);
            this.splitContainer1.Panel1.Controls.Add(this.btnAddCatFilter);
            this.splitContainer1.Panel1.Controls.Add(this.btnDeleteCategory);
            this.splitContainer1.Panel1.Controls.Add(this.btnRenameCategory);
            this.splitContainer1.Panel1.Controls.Add(this.btnRenameCatFilter);
            this.splitContainer1.Panel1.Controls.Add(this.lbSelectedCategory);
            this.splitContainer1.Panel1.Controls.Add(this.lbCategoryFilters);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.btnDeleteExtractKnownName);
            this.splitContainer1.Panel2.Controls.Add(this.btnAddExtractCategory);
            this.splitContainer1.Panel2.Controls.Add(this.lbExtractKnownNames);
            this.splitContainer1.Panel2.Controls.Add(this.btnDeleteExtractCategory);
            this.splitContainer1.Panel2.Controls.Add(this.btnChangeExtractKnownName);
            this.splitContainer1.Panel2.Controls.Add(this.btnRenameExtractCategory);
            this.splitContainer1.Panel2.Controls.Add(this.btnAddExtractKnownName);
            this.splitContainer1.Panel2.Controls.Add(this.lbExtractCategory);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Size = new System.Drawing.Size(1283, 613);
            this.splitContainer1.SplitterDistance = 317;
            this.splitContainer1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(331, 39);
            this.label2.TabIndex = 1;
            this.label2.Text = "      Для смет: наименование категорий (для наименований, начинающихся со слов из" +
    " списка...):";
            // 
            // tvDictionaries
            // 
            this.tvDictionaries.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvDictionaries.HideSelection = false;
            this.tvDictionaries.Location = new System.Drawing.Point(4, 42);
            this.tvDictionaries.Name = "tvDictionaries";
            this.tvDictionaries.ShowNodeToolTips = true;
            this.tvDictionaries.Size = new System.Drawing.Size(302, 268);
            this.tvDictionaries.TabIndex = 0;
            this.tvDictionaries.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDictionaries_AfterSelect);
            // 
            // btnDeleteCatFilter
            // 
            this.btnDeleteCatFilter.Enabled = false;
            this.btnDeleteCatFilter.Location = new System.Drawing.Point(624, 234);
            this.btnDeleteCatFilter.Name = "btnDeleteCatFilter";
            this.btnDeleteCatFilter.Size = new System.Drawing.Size(146, 26);
            this.btnDeleteCatFilter.TabIndex = 6;
            this.btnDeleteCatFilter.Text = "Удалить условие";
            this.btnDeleteCatFilter.UseVisualStyleBackColor = true;
            this.btnDeleteCatFilter.Click += new System.EventHandler(this.btnDeleteCatFilter_Click);
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.Location = new System.Drawing.Point(312, 42);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(210, 26);
            this.btnAddCategory.TabIndex = 2;
            this.btnAddCategory.Text = "Добавить категорию...";
            this.btnAddCategory.UseVisualStyleBackColor = true;
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // btnAddCatFilter
            // 
            this.btnAddCatFilter.Enabled = false;
            this.btnAddCatFilter.Location = new System.Drawing.Point(624, 170);
            this.btnAddCatFilter.Name = "btnAddCatFilter";
            this.btnAddCatFilter.Size = new System.Drawing.Size(146, 26);
            this.btnAddCatFilter.TabIndex = 2;
            this.btnAddCatFilter.Text = "Добавить условие...";
            this.btnAddCatFilter.UseVisualStyleBackColor = true;
            this.btnAddCatFilter.Click += new System.EventHandler(this.btnAddCatFilter_Click);
            // 
            // btnDeleteCategory
            // 
            this.btnDeleteCategory.Enabled = false;
            this.btnDeleteCategory.Location = new System.Drawing.Point(312, 106);
            this.btnDeleteCategory.Name = "btnDeleteCategory";
            this.btnDeleteCategory.Size = new System.Drawing.Size(210, 26);
            this.btnDeleteCategory.TabIndex = 6;
            this.btnDeleteCategory.Text = "Удалить категорию";
            this.btnDeleteCategory.UseVisualStyleBackColor = true;
            this.btnDeleteCategory.Click += new System.EventHandler(this.btnDeleteCategory_Click);
            // 
            // btnRenameCategory
            // 
            this.btnRenameCategory.Enabled = false;
            this.btnRenameCategory.Location = new System.Drawing.Point(312, 74);
            this.btnRenameCategory.Name = "btnRenameCategory";
            this.btnRenameCategory.Size = new System.Drawing.Size(210, 26);
            this.btnRenameCategory.TabIndex = 3;
            this.btnRenameCategory.Text = "Переименовать категорию...";
            this.btnRenameCategory.UseVisualStyleBackColor = true;
            this.btnRenameCategory.Click += new System.EventHandler(this.btnRenameCategory_Click);
            // 
            // btnRenameCatFilter
            // 
            this.btnRenameCatFilter.Enabled = false;
            this.btnRenameCatFilter.Location = new System.Drawing.Point(624, 202);
            this.btnRenameCatFilter.Name = "btnRenameCatFilter";
            this.btnRenameCatFilter.Size = new System.Drawing.Size(146, 26);
            this.btnRenameCatFilter.TabIndex = 3;
            this.btnRenameCatFilter.Text = "Изменить условие...";
            this.btnRenameCatFilter.UseVisualStyleBackColor = true;
            this.btnRenameCatFilter.Click += new System.EventHandler(this.btnRenameCatFilter_Click);
            // 
            // lbSelectedCategory
            // 
            this.lbSelectedCategory.AutoSize = true;
            this.lbSelectedCategory.Location = new System.Drawing.Point(313, 149);
            this.lbSelectedCategory.Name = "lbSelectedCategory";
            this.lbSelectedCategory.Size = new System.Drawing.Size(157, 17);
            this.lbSelectedCategory.TabIndex = 4;
            this.lbSelectedCategory.Text = "Категория: (не выбрано)";
            // 
            // lbCategoryFilters
            // 
            this.lbCategoryFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbCategoryFilters.FormattingEnabled = true;
            this.lbCategoryFilters.ItemHeight = 17;
            this.lbCategoryFilters.Location = new System.Drawing.Point(316, 170);
            this.lbCategoryFilters.Name = "lbCategoryFilters";
            this.lbCategoryFilters.Size = new System.Drawing.Size(302, 140);
            this.lbCategoryFilters.Sorted = true;
            this.lbCategoryFilters.TabIndex = 5;
            this.lbCategoryFilters.SelectedIndexChanged += new System.EventHandler(this.lbCategoryFilters_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(615, 39);
            this.label3.TabIndex = 1;
            this.label3.Text = "      Для спецификаций: наименование категорий (для исключения из поиска наименов" +
    "аний, точное совпадение, исключая начальные и конечные пробелы):";
            // 
            // btnDeleteExtractKnownName
            // 
            this.btnDeleteExtractKnownName.Enabled = false;
            this.btnDeleteExtractKnownName.Location = new System.Drawing.Point(1052, 109);
            this.btnDeleteExtractKnownName.Name = "btnDeleteExtractKnownName";
            this.btnDeleteExtractKnownName.Size = new System.Drawing.Size(188, 26);
            this.btnDeleteExtractKnownName.TabIndex = 6;
            this.btnDeleteExtractKnownName.Text = "Удалить исключение";
            this.btnDeleteExtractKnownName.UseVisualStyleBackColor = true;
            this.btnDeleteExtractKnownName.Click += new System.EventHandler(this.btnDeleteExtractKnownName_Click);
            // 
            // btnAddExtractCategory
            // 
            this.btnAddExtractCategory.Location = new System.Drawing.Point(528, 45);
            this.btnAddExtractCategory.Name = "btnAddExtractCategory";
            this.btnAddExtractCategory.Size = new System.Drawing.Size(210, 26);
            this.btnAddExtractCategory.TabIndex = 2;
            this.btnAddExtractCategory.Text = "Добавить категорию...";
            this.btnAddExtractCategory.UseVisualStyleBackColor = true;
            this.btnAddExtractCategory.Click += new System.EventHandler(this.btnAddExtractCategory_Click);
            // 
            // lbExtractKnownNames
            // 
            this.lbExtractKnownNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbExtractKnownNames.FormattingEnabled = true;
            this.lbExtractKnownNames.ItemHeight = 17;
            this.lbExtractKnownNames.Location = new System.Drawing.Point(744, 45);
            this.lbExtractKnownNames.Name = "lbExtractKnownNames";
            this.lbExtractKnownNames.Size = new System.Drawing.Size(302, 225);
            this.lbExtractKnownNames.Sorted = true;
            this.lbExtractKnownNames.TabIndex = 5;
            this.lbExtractKnownNames.SelectedIndexChanged += new System.EventHandler(this.lbExtractKnownNames_SelectedIndexChanged);
            // 
            // btnDeleteExtractCategory
            // 
            this.btnDeleteExtractCategory.Enabled = false;
            this.btnDeleteExtractCategory.Location = new System.Drawing.Point(528, 109);
            this.btnDeleteExtractCategory.Name = "btnDeleteExtractCategory";
            this.btnDeleteExtractCategory.Size = new System.Drawing.Size(210, 26);
            this.btnDeleteExtractCategory.TabIndex = 6;
            this.btnDeleteExtractCategory.Text = "Удалить категорию";
            this.btnDeleteExtractCategory.UseVisualStyleBackColor = true;
            this.btnDeleteExtractCategory.Click += new System.EventHandler(this.btnDeleteExtractCategory_Click);
            // 
            // btnChangeExtractKnownName
            // 
            this.btnChangeExtractKnownName.Enabled = false;
            this.btnChangeExtractKnownName.Location = new System.Drawing.Point(1052, 77);
            this.btnChangeExtractKnownName.Name = "btnChangeExtractKnownName";
            this.btnChangeExtractKnownName.Size = new System.Drawing.Size(188, 26);
            this.btnChangeExtractKnownName.TabIndex = 3;
            this.btnChangeExtractKnownName.Text = "Изменить исключение...";
            this.btnChangeExtractKnownName.UseVisualStyleBackColor = true;
            this.btnChangeExtractKnownName.Click += new System.EventHandler(this.btnChangeExtractKnownName_Click);
            // 
            // btnRenameExtractCategory
            // 
            this.btnRenameExtractCategory.Enabled = false;
            this.btnRenameExtractCategory.Location = new System.Drawing.Point(528, 77);
            this.btnRenameExtractCategory.Name = "btnRenameExtractCategory";
            this.btnRenameExtractCategory.Size = new System.Drawing.Size(210, 26);
            this.btnRenameExtractCategory.TabIndex = 3;
            this.btnRenameExtractCategory.Text = "Переименовать категорию...";
            this.btnRenameExtractCategory.UseVisualStyleBackColor = true;
            this.btnRenameExtractCategory.Click += new System.EventHandler(this.btnRenameExtractCategory_Click);
            // 
            // btnAddExtractKnownName
            // 
            this.btnAddExtractKnownName.Location = new System.Drawing.Point(1052, 45);
            this.btnAddExtractKnownName.Name = "btnAddExtractKnownName";
            this.btnAddExtractKnownName.Size = new System.Drawing.Size(188, 26);
            this.btnAddExtractKnownName.TabIndex = 2;
            this.btnAddExtractKnownName.Text = "Добавить исключение...";
            this.btnAddExtractKnownName.UseVisualStyleBackColor = true;
            this.btnAddExtractKnownName.Click += new System.EventHandler(this.btnAddExtractKnownName_Click);
            // 
            // lbExtractCategory
            // 
            this.lbExtractCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbExtractCategory.FormattingEnabled = true;
            this.lbExtractCategory.ItemHeight = 17;
            this.lbExtractCategory.Location = new System.Drawing.Point(6, 45);
            this.lbExtractCategory.Name = "lbExtractCategory";
            this.lbExtractCategory.Size = new System.Drawing.Size(516, 225);
            this.lbExtractCategory.Sorted = true;
            this.lbExtractCategory.TabIndex = 5;
            this.lbExtractCategory.SelectedIndexChanged += new System.EventHandler(this.lbExtractCategory_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(741, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(318, 39);
            this.label4.TabIndex = 1;
            this.label4.Text = "      Для спецификаций: при поиске наименований не добавляются строки, начинающие" +
    "ся с...";
            // 
            // tabResults
            // 
            this.tabResults.Controls.Add(this.lvGoods);
            this.tabResults.Controls.Add(this.btnExtractFromSourceTables);
            this.tabResults.Location = new System.Drawing.Point(4, 26);
            this.tabResults.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabResults.Name = "tabResults";
            this.tabResults.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabResults.Size = new System.Drawing.Size(1349, 619);
            this.tabResults.TabIndex = 1;
            this.tabResults.Text = "Материалы и оборудование";
            this.tabResults.UseVisualStyleBackColor = true;
            // 
            // lvGoods
            // 
            this.lvGoods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvGoods.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10});
            this.lvGoods.FullRowSelect = true;
            this.lvGoods.Location = new System.Drawing.Point(9, 40);
            this.lvGoods.Name = "lvGoods";
            this.lvGoods.ShowGroups = false;
            this.lvGoods.ShowItemToolTips = true;
            this.lvGoods.Size = new System.Drawing.Size(1332, 549);
            this.lvGoods.TabIndex = 0;
            this.lvGoods.UseCompatibleStateImageBehavior = false;
            this.lvGoods.View = System.Windows.Forms.View.Details;
            this.lvGoods.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LvListColumnClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Наименование материала, оборудования";
            this.columnHeader2.Width = 470;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Ед.изм.";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Кол-во";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 70;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Основание";
            this.columnHeader5.Width = 150;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Поставка";
            this.columnHeader6.Width = 100;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Категория";
            this.columnHeader7.Width = 100;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Группа";
            this.columnHeader8.Width = 100;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Тип, марка";
            this.columnHeader9.Width = 100;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Стандарт";
            this.columnHeader10.Width = 100;
            // 
            // btnExtractFromSourceTables
            // 
            this.btnExtractFromSourceTables.Location = new System.Drawing.Point(9, 8);
            this.btnExtractFromSourceTables.Name = "btnExtractFromSourceTables";
            this.btnExtractFromSourceTables.Size = new System.Drawing.Size(75, 26);
            this.btnExtractFromSourceTables.TabIndex = 0;
            this.btnExtractFromSourceTables.Text = "Извлечь...";
            this.btnExtractFromSourceTables.UseVisualStyleBackColor = true;
            this.btnExtractFromSourceTables.Click += new System.EventHandler(this.btnExtractFromSourceTables_Click);
            // 
            // tabExport
            // 
            this.tabExport.Controls.Add(this.tvResults);
            this.tabExport.Controls.Add(this.btnExportTable);
            this.tabExport.Controls.Add(this.btnBuildTree);
            this.tabExport.Location = new System.Drawing.Point(4, 26);
            this.tabExport.Name = "tabExport";
            this.tabExport.Padding = new System.Windows.Forms.Padding(3);
            this.tabExport.Size = new System.Drawing.Size(1349, 619);
            this.tabExport.TabIndex = 3;
            this.tabExport.Text = "Результирующая таблица";
            this.tabExport.UseVisualStyleBackColor = true;
            // 
            // tvResults
            // 
            this.tvResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvResults.Location = new System.Drawing.Point(9, 40);
            this.tvResults.Name = "tvResults";
            this.tvResults.Size = new System.Drawing.Size(1332, 549);
            this.tvResults.TabIndex = 5;
            // 
            // btnExportTable
            // 
            this.btnExportTable.Enabled = false;
            this.btnExportTable.Location = new System.Drawing.Point(114, 7);
            this.btnExportTable.Name = "btnExportTable";
            this.btnExportTable.Size = new System.Drawing.Size(161, 26);
            this.btnExportTable.TabIndex = 4;
            this.btnExportTable.Text = "Выгрузить в таблицу...";
            this.btnExportTable.UseVisualStyleBackColor = true;
            this.btnExportTable.Click += new System.EventHandler(this.btnExportTable_Click);
            // 
            // btnBuildTree
            // 
            this.btnBuildTree.Location = new System.Drawing.Point(8, 7);
            this.btnBuildTree.Name = "btnBuildTree";
            this.btnBuildTree.Size = new System.Drawing.Size(100, 26);
            this.btnBuildTree.TabIndex = 4;
            this.btnBuildTree.Text = "Построить...";
            this.btnBuildTree.UseVisualStyleBackColor = true;
            this.btnBuildTree.Click += new System.EventHandler(this.btnBuildTree_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 627);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1357, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(1000, 17);
            this.toolStripStatusLabel1.Text = "Загрузка...";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(200, 16);
            this.toolStripProgressBar1.Visible = false;
            // 
            // SeparatingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1357, 649);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tcSteps);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SeparatingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Скрипт разделительной ведомости";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SeparatingForm_FormClosing);
            this.Load += new System.EventHandler(this.SeparatingForm_Load);
            this.tcSteps.ResumeLayout(false);
            this.tabSources.ResumeLayout(false);
            this.tabSources.PerformLayout();
            this.tabDictionaries.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabResults.ResumeLayout(false);
            this.tabExport.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcSteps;
        private System.Windows.Forms.TabPage tabSources;
        private System.Windows.Forms.TabPage tabResults;
        private System.Windows.Forms.Button btnSelecTablestSourceFolder;
        private System.Windows.Forms.Label lbTablesSourceFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ListView lvSourceTables;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnExtractFromSourceTables;
        private System.Windows.Forms.ListView lvGoods;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.TabPage tabDictionaries;
        private System.Windows.Forms.TreeView tvDictionaries;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDeleteCatFilter;
        private System.Windows.Forms.Button btnDeleteCategory;
        private System.Windows.Forms.ListBox lbCategoryFilters;
        private System.Windows.Forms.Label lbSelectedCategory;
        private System.Windows.Forms.Button btnRenameCatFilter;
        private System.Windows.Forms.Button btnRenameCategory;
        private System.Windows.Forms.Button btnAddCatFilter;
        private System.Windows.Forms.Button btnAddCategory;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.TabPage tabExport;
        private System.Windows.Forms.Button btnBuildTree;
        private System.Windows.Forms.Button btnExportTable;
        private System.Windows.Forms.TreeView tvResults;
        private System.Windows.Forms.Button btnDeleteExtractCategory;
        private System.Windows.Forms.ListBox lbExtractCategory;
        private System.Windows.Forms.Button btnRenameExtractCategory;
        private System.Windows.Forms.Button btnAddExtractCategory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDeleteExtractKnownName;
        private System.Windows.Forms.ListBox lbExtractKnownNames;
        private System.Windows.Forms.Button btnChangeExtractKnownName;
        private System.Windows.Forms.Button btnAddExtractKnownName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

