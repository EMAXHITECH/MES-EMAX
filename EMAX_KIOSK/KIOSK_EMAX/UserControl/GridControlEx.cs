using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using System.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace KIOSK_EMAX
{
    public class GridControlEx : GridControl
    {
        DataTable dt = new DataTable();
        GridView MainGrid = new GridView();

        private Point Origin_Point = new Point();
        private Size Origin_Size = new Size();
        private Control Parent_Control = null;

        //그리드 판넬 사이즈 한번만 설정
        private string sSizeCHK = "N";

        //GridView 생성할때 두번 발생 하는 부분 방지
        private string sCHK = "Y";

        //체크박스 더블클릭 확인
        private string sCheck_All = "Y";

        // 헤더 더블클릭 구분
        public bool Head_DoubleChk { get; set; } = true;

        //중간 행 삽입시 이벤트 처리
        public event EventHandler<InitNewRowEventArgs> NewRowAdd;

        //엑셀 붙여넣기 설정
        public enum Excel_GB
        {
            Update = 0,
            Append = 1
        }

        public GridControlEx()
        {
            this.EditorKeyPress += new System.Windows.Forms.KeyPressEventHandler(OnEditorKeyPress);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(OnKeyPress);
            this.MouseWheel += new MouseEventHandler(OnMouseWheel);

            this.GotFocus += new EventHandler(OnGotFocust);
        }

        private void Control_Set()
        {
            if(Parent_Control == null)
            {
                Parent_Control = this.Parent;
                Control_Set();
            }
            else if(Parent_Control.GetType() != typeof(PanelControl))
            {
                Parent_Control = Parent_Control.Parent;
                Control_Set();
            }
        }

        public bool MouseWheelChk { get; set; } = true;
        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            if (!MouseWheelChk)
                return;

            if (MainGrid.GetFocusedDataSourceRowIndex() < 0)
                return;

            int iLastRow = MainGrid.RowCount - 1;
            int iRow = MainGrid.GetFocusedDataSourceRowIndex();

            if (e.Delta > 0)
            {
                iRow--;

                if (iRow < 0)
                    return;

                MainGrid.FocusedRowHandle = iRow;
            }
            else
            {
                if (iRow == iLastRow)
                {
                    MainGrid.AddNewRow();
                    MainGrid.UpdateCurrentRow();
                    OnNewRow(MainGrid.RowCount - 1);
                }
                else
                {
                    iRow++;
                    MainGrid.FocusedRowHandle = iRow;
                }
            }
        }

        private int Last_Column(int iLastColumn)
        {
            if (!MainGrid.Columns[iLastColumn].Visible)
            {
                iLastColumn = Last_Column(iLastColumn - 1);
            }

            return iLastColumn;
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!EnterYN)
                return;

            int iLastColumn = Last_Column(MainGrid.Columns.Count - 1);
            int iLastRow = MainGrid.RowCount - 1;
            int iRow = MainGrid.GetFocusedDataSourceRowIndex();

            if (MainGrid.GetFocusedDataSourceRowIndex() < 0)
                return;

            if (e.KeyChar == 13)
            {
                if (MainGrid.FocusedColumn == MainGrid.Columns[iLastColumn])
                {
                    if (iRow == iLastRow)
                    {
                        MainGrid.AddNewRow();
                        MainGrid.UpdateCurrentRow();
                        MainGrid.FocusedColumn = MainGrid.Columns[0];
                        OnNewRow(MainGrid.RowCount - 1);
                    }
                    else
                    {
                        iRow++;
                        MainGrid.FocusedRowHandle = iRow;
                        MainGrid.FocusedColumn = MainGrid.Columns[0];
                    }
                }
                else
                {
                    if (MainGrid.FocusedColumn.OptionsColumn.ReadOnly)
                    {
                        int iNext = MainGrid.FocusedColumn.AbsoluteIndex;
                        MainGrid.FocusedColumn = MainGrid.Columns[iNext + 1];
                    }
                }
            }
        }

        private void OnEditorKeyPress(object sender, KeyPressEventArgs e)
        {
            int iLastColumn = Last_Column(MainGrid.Columns.Count - 1); //MainGrid.Columns.Count - 1;

            if (MainGrid.GetFocusedDataSourceRowIndex() < 0)
                return;

            if (e.KeyChar == 13)
            {
                if (MainGrid.FocusedColumn.ColumnEdit != null && MainGrid.FocusedColumn.ColumnEdit.GetType() == typeof(RepositoryItemMemoEdit))
                    return;

                //추가(21.05.06), 헬프 칼럼일 경우 무시
                if (MainGrid.FocusedColumn.AppearanceHeader.ForeColor == Color.FromArgb(44, 85, 152) && 
                    MainGrid.GetRowCellValue(MainGrid.FocusedRowHandle, MainGrid.Columns[MainGrid.FocusedColumn.AbsoluteIndex + 1]).ToString() == "")
                    return;

                if (MainGrid.FocusedColumn != MainGrid.Columns[iLastColumn])
                {
                    int iNext = MainGrid.FocusedColumn.AbsoluteIndex;
                    MainGrid.FocusedColumn = MainGrid.Columns[iNext + 1];
                }
            }
        }

        private void View_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (!Hide_Point)
                return;

            GridView View = (GridView)sender;
            Type Col_Type = View.Columns[e.Column.FieldName].ColumnType;

            if (Col_Type == typeof(int) || Col_Type == typeof(decimal))
            {
                string Value = e.Value.NumString();

                if (e.DisplayText.Contains("."))
                {
                    int n = Convert.ToInt32(Convert.ToDecimal(Value));
                    decimal d = Convert.ToDecimal(Value);

                    if (n == d)
                    {
                        e.DisplayText = e.DisplayText.Substring(0, e.DisplayText.IndexOf("."));
                    }
                }
            }
        }

        protected override void CreateMainView()
        {
            base.CreateMainView();

            if (sCHK == "Y")
            {
                sCHK = "N";
                return;
            }

            MainGrid = MainView as GridView;

            if(this.Execl_GB == Excel_GB.Update)
                MainGrid.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Update;
            else
                MainGrid.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Append;

            //MultiSelect 선택 여부
            MainGrid.OptionsSelection.MultiSelect = MultiSelectChk;

            PopMenu();

            //체크박스 더블 클릭 이벤트 연결
            if (Head_DoubleChk)
                MainGrid.DoubleClick += new EventHandler(Double_Click);

            //엑셀 복사기능 값 강제로 넣기
            MainGrid.ClipboardRowPasting += Gird_Value_Set;

            if (CellFocus)
            {
                MainGrid.FocusRectStyle = DrawFocusRectStyle.CellFocus;
                //MainGrid.Appearance.FocusedRow.BackColor = Color.Transparent;
                MainGrid.Appearance.FocusedRow.BackColor = Color.FromArgb(35, Color.DeepSkyBlue);
            }

            //정렬시 포커스 맨위로 가기
            MainGrid.EndSorting += new EventHandler(EndSort_Grid);

            ((GridView)this.MainView).CustomColumnDisplayText += View_CustomColumnDisplayText;
        }

        protected override void OnCreateControl()
        {
            if (ExpansionCHK)
            {
                Control_Set();
                Origin_Point = Parent_Control.Location;
                Origin_Size = Parent_Control.Size;
            }
        }

        //처음 FORM 띄울때 사이즈 처리
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if(Parent_Control != null && sSizeCHK == "N")
            {
                if (Origin_Size != Parent_Control.Size)
                    Origin_Size = Parent_Control.Size;

                sSizeCHK = "Y";
            }
        }

        private class RowInfo
        {
            public GridView view;
            public int iRow;
            public RowInfo(GridView view, int iRow)
            {
                this.view = view;
                this.iRow = iRow;
            }
        }

        private void Grid_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            GridView gridView = sender as GridView;
            if (e.MenuType == GridMenuType.Row)
            {
                int iRow = e.HitInfo.RowHandle;
                e.Menu.Items.Clear();
                e.Menu.Items.Add(CreateMenu(gridView, iRow, "추가"));
                e.Menu.Items.Add(CreateMenu(gridView, iRow, "삭제"));
                //21.05.07. 추가(확장)
                if (ExpansionCHK)
                {
                    if (Parent_Control.Size == Origin_Size)
                    {
                        e.Menu.Items.Add(CreateMenu(gridView, iRow, "그리드확장"));
                    }
                    else
                    {
                        e.Menu.Items.Add(CreateMenu(gridView, iRow, "그리드축소"));
                    }
                }
            }
        }

        private DXMenuItem CreateMenu(GridView view, int iRow, string sMenuNM)
        {
            DXMenuItem menuItem = new DXMenuItem(sMenuNM, new EventHandler(OnRowClick), null);
            menuItem.Tag = new RowInfo(view, iRow);
            menuItem.Enabled = true;

            return menuItem;
        }

        private void OnRowClick(object sender, EventArgs e)
        {
            DXMenuItem menuItem = sender as DXMenuItem;
            RowInfo ri = menuItem.Tag as RowInfo;

            int iRow = ri.iRow;

            if (ri != null)
            {
                if (menuItem.Caption == "추가")
                {
                    Insert_Row(iRow + 1);
                    OnNewRow(iRow + 1);
                }
                else if (menuItem.Caption == "삭제")
                {
                    if (XtraMessageBox.Show("삭제하시겠습니까?", "삭제", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;

                    if (ri.view.GetDataRow(iRow).RowState == DataRowState.Added)
                    {
                        ri.view.DeleteRow(ri.iRow);
                    }
                    else
                    {
                        Delete_Row();
                    }
                }
                else if(menuItem.Caption == "그리드확장")
                {
                    //처음 Form이 아닐 경우 버튼 클릭시 'Y' 처리
                    if (sSizeCHK == "N")
                        sSizeCHK = "Y";

                    Parent_Control.BringToFront();
                    //if(Parent_Control.Size != MainForm.MainTab.Size && Parent_Control.Size != Origin_Size)
                    //{
                    //    Origin_Size = Parent_Control.Size;
                    //}

                    //Parent_Control.Size = MainForm.MainTab.Size;
                    //Parent_Control.Location = new Point(0, 0);

                    //if (Parent_Control.Size == Origin_Size)
                    //{
                    //    Parent_Control.Size = MainForm.MainTab.Size;
                    //    Parent_Control.Location = new Point(0, 0);
                    //}
                    //else
                    //{
                    //    Parent_Control.Size = Origin_Size;
                    //    Parent_Control.Location = Origin_Point;
                    //}
                }
                else if(menuItem.Caption == "그리드축소")
                {
                    Parent_Control.Size = Origin_Size;
                    Parent_Control.Location = Origin_Point;
                }
            }
        }

        //팝업 메뉴 표시 여부
        public bool PopMenuChk { get; set; } = true;

        //그리드 행 멀티 선택 기능 설정
        public bool MultiSelectChk { get; set; } = true;

        //포커스가 갔을 경우 첫 행 추가 기능 여부
        public bool AddRowYN { get; set; } = false;

        //Help창 Enter키 반음 없애기
        public bool EnterYN { get; set; } = true;

        //그리드 확장 기능을 넣을것인지 말것인지 설정
        public bool ExpansionCHK { get; set; } = false;

        //엑셀 붙여넣기 설정 여부
        public Excel_GB Execl_GB { get; set; }

        //셀 포커스(Help 창은 제외)
        public bool CellFocus { get; set; } = true;

        public bool Hide_Point { get; set; } = false;

        private void PopMenu()
        {
            MainGrid.OptionsView.ShowGroupPanel = false;

            if (PopMenuChk)
            {
                MainGrid.PopupMenuShowing += Grid_PopupMenuShowing;
            }
        }

        private void Insert_Row(int iRow)
        {
            dt = this.DataSource as DataTable;

            DataRow dr;

            dr = dt.NewRow();

            dt.Rows.InsertAt(dr, iRow);

            this.DataSource = dt;
        }

        //행 삭제 이벤트 설정
        public event EventHandler DeleteRowEventHandler;

        private void Delete_Row()
        {
            if (DeleteRowEventHandler != null)
            {
                Invoke(DeleteRowEventHandler, null);
            }

            XtraMessageBox.Show("삭제되었습니다");
        }

        //포커스 잡혔을때 Row 추가
        private void OnGotFocust(object sender, EventArgs e)
        {
            if (!AddRowYN)
                return;

            if (MainGrid.RowCount == 0)
            {
                MainGrid.AddNewRow();
                MainGrid.UpdateCurrentRow();
                OnNewRow(MainGrid.RowCount - 1);
            }
        }

        //중간에 행 삽입시 에빈트
        private void OnNewRow(int iRow)
        {
            if(NewRowAdd != null)
            {
                NewRowAdd(MainGrid, new InitNewRowEventArgs(iRow));
            }
        }

        //체크박스 더블클릭 이벤트
        private void Double_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            DXMouseEventArgs mouse = e as DXMouseEventArgs;

            GridHitInfo hInfo = view.CalcHitInfo(mouse.Location);

            if (hInfo.InColumn)
            {
                if(hInfo.Column.ColumnEdit != null && hInfo.Column.ColumnEdit.GetType() == typeof(RepositoryItemCheckEdit))
                {
                    RepositoryItemCheckEdit check = (RepositoryItemCheckEdit)hInfo.Column.ColumnEdit;

                    for (int j = 0; j < MainGrid.RowCount; j++)
                    {
                        bool chk_state = (MainGrid.GetRowCellValue(j, hInfo.Column) == check.ValueChecked) ? true : false;

                        if (chk_state == false)
                        {
                            sCheck_All = "Y";
                            break;
                        }
                        else if (chk_state == true)
                            sCheck_All = "N";
                    }

                    for (int i = 0; i < MainGrid.RowCount; i++)
                    {
                        MainGrid.SetRowCellValue(i, hInfo.Column, sCheck_All == "Y" ? check.ValueChecked : check.ValueUnchecked);
                    }

                    if (sCheck_All == "Y")
                        sCheck_All = "N";
                    else
                        sCheck_All = "Y";
                }
            }
        }

        private void EndSort_Grid(object sender, EventArgs e)
        {
            if(MainGrid.RowCount > 0)
            {
                MainGrid.UnselectRow(MainGrid.FocusedRowHandle);
                MainGrid.FocusedRowHandle = 0;
                MainGrid.SelectRow(MainGrid.FocusedRowHandle);
            }
        }

        //그리드 복사 기능 강제 값 넣기
        private void Gird_Value_Set(object sender, ClipboardRowPastingEventArgs e)
        {
            //int iGrid = MainGrid.FocusedColumn.AbsoluteIndex;
            //int iCount = e.Values.Count + iGrid;

            //for (int i = iGrid; i < iCount; i++)
            //{
            //    if (!MainGrid.Columns[i].Visible)
            //    {
            //        iCount++;
            //    }
            //    else //if (!MainGrid.Columns[i].OptionsColumn.AllowEdit)
            //    {
            //        MainGrid.SetRowCellValue(e.RowHandle, MainGrid.Columns[i], e.Values[MainGrid.Columns[i]]);
            //    }
            //}
        }

        public string Get_Cell_Data(string Field_Name, int Index = -1)
        {
            GridView View = (GridView)this.MainView;
            Type type = View.Columns[Field_Name].ColumnType;

            if (Index < 0)
                Index = View.FocusedRowHandle;

            if (type == typeof(int) || type == typeof(decimal))
                return View.GetRowCellValue(Index, Field_Name).NumString();
            else if (type == typeof(DateTime))
                return Convert.ToDateTime(View.GetRowCellValue(Index, Field_Name)).ToString("yyyyMMdd");
            else
                return View.GetRowCellValue(Index, Field_Name).NullString();
        }
    }
}