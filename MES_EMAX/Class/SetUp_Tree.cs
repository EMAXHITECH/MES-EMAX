using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES
{
    public static class SetUp_Tree
    {
        public static void Tree_Columns_Set(TreeList treeList, string sCaption, string sFieldName, int iWidth, bool visible, bool Edit = false, bool dBack = false)
        {
            TreeListColumn t_Column = new TreeListColumn();
            treeList.Columns.Add(t_Column);

            t_Column.Caption = sCaption;
            t_Column.FieldName = sFieldName;
            if (visible)
                t_Column.VisibleIndex = treeList.Columns.Count;
            else
                t_Column.VisibleIndex = -1;

            //t_Column.OptionsColumn.FixedWidth = false;
            t_Column.Width = iWidth;

            //Edit
            t_Column.OptionsColumn.AllowEdit = Edit;
            t_Column.OptionsColumn.ReadOnly = !Edit;

            if (!Edit)
            {
                if (dBack)
                    t_Column.AppearanceCell.BackColor = Color.LightGray;
            }
        }

        public static void Tree_Columns_Key(TreeList treeList, string sKeyName, string sParentName)
        {
            treeList.KeyFieldName = sKeyName;
            treeList.ParentFieldName = sParentName;
        }

        //숫자 칼럼
        public static void Tree_Column_NumSet(TreeList treeList, string sColumn, int iDecimal)
        {
            RepositoryItemTextEdit NumEdit = new RepositoryItemTextEdit();
            NumEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            NumEdit.Mask.EditMask = "n" + iDecimal.ToString();
            NumEdit.Mask.UseMaskAsDisplayFormat = true;
            NumEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            NumEdit.EditFormat.FormatString = "n" + iDecimal.ToString();

            treeList.Columns[sColumn].ColumnEdit = NumEdit;
        }

        //헬프창 칼럼
        public static void Tree_Column_Help(TreeList treeList, string sColumn, string sRequiredCHK)
        {
            RepositoryItemButtonEdit button_Help = new RepositoryItemButtonEdit();
            button_Help.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Search;

            treeList.Columns[sColumn].ColumnEdit = button_Help;

            if (sRequiredCHK == "Y")
            {
                treeList.Columns[sColumn].AppearanceHeader.ForeColor = Color.FromArgb(44, 85, 152);
                treeList.Columns[sColumn].AppearanceHeader.Font = new System.Drawing.Font("나눔바름고딕", 11F, FontStyle.Bold);
                string col_name = treeList.Columns[sColumn].Caption;
                treeList.Columns[sColumn].Caption = "* " + col_name;
            }
        }

        //날짜
        public static void Tree_Column_Date(TreeList treeList, string sColumn)
        {
            RepositoryItemDateEdit date_Edit = new RepositoryItemDateEdit();
            date_Edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            date_Edit.Mask.EditMask = "yyyy-MM-dd";
            date_Edit.Mask.UseMaskAsDisplayFormat = true;
            date_Edit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            date_Edit.EditFormat.FormatString = "yyyy-MM-dd";

            treeList.Columns[sColumn].ColumnEdit = date_Edit;
        }

        public static void Tree_Column_Check(TreeList treeList, string sColumn)
        {
            RepositoryItemCheckEdit edit_Chk = new RepositoryItemCheckEdit();
            edit_Chk.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            edit_Chk.ValueChecked = "Y";
            edit_Chk.ValueUnchecked = "N";

            treeList.Columns[sColumn].ColumnEdit = edit_Chk;
        }

        //해당 노드 하위 노드 전체 조회
        public static void Get_AllNodes(TreeListNode Nodes, List<TreeListNode> list_Node)
        {
            foreach(TreeListNode node in Nodes.Nodes)
            {
                list_Node.Add(node);

                Get_AllNodes(node, list_Node);
            }
        }
    }
}
