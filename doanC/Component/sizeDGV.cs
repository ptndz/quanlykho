using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace doanC.Component
{
     class sizeDGV
    {
        public sizeDGV(DataGridView dgv)
        {
                DataGridViewElementStates states = DataGridViewElementStates.None;
                dgv.ScrollBars = ScrollBars.None;
                var totalHeight = dgv.Rows.GetRowsHeight(states) + dgv.ColumnHeadersHeight;
                totalHeight += dgv.Rows.Count * 4; // a correction I need
                var totalWidth = dgv.Columns.GetColumnsWidth(states) + dgv.RowHeadersWidth;
                dgv.ClientSize = new Size(totalWidth, totalHeight);       
        }
    }
}
