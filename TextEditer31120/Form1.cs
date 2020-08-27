using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditer31120
{
    public partial class Form1 : Form
    {
        //現在編集中のファイル名
        private string fileName = "";       //Camel形式（⇔Pascal形式）                                                                    ca

        public Form1()
        {
            InitializeComponent();
        }

        private void ExitXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveMessage(sender, e);
            //アプリケーション終了
            Application.Exit();
        }

        private void OpenOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveMessage(sender, e);


            //開くダイアログを表示
            if (ofdFileOpen.ShowDialog() == DialogResult.OK)
            {
                //開く
                using (StreamReader sr = new StreamReader(ofdFileOpen.FileName, Encoding.GetEncoding("utf-8"), false))
                {
                    rtTextArea.Text = sr.ReadToEnd();
                    this.fileName = ofdFileOpen.FileName;
                }
            }
        }

        //名前を付けて保存
        private void SavaNameAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdFileSave.ShowDialog() == DialogResult.OK)
            {
                FileSave(sfdFileSave.FileName);
                //using (StreamWriter sw = new StreamWriter(sfdFileSave.FileName, false, Encoding.GetEncoding("utf-8")))
                //{
                //    sw.WriteLine(rtTextArea.Text);
                //}
            }
        }

        //上書き保存
        private void SaveSToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (this.fileName != "")
            {
                FileSave(fileName);
            }
            else
            {
                SavaNameAToolStripMenuItem_Click(sender, e);
            }
        }

        private void FileSave(string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.GetEncoding("utf-8")))
            {
                sw.WriteLine(rtTextArea.Text);
                Text = Path.GetFileName(fileName);
            }
        }

        //元に戻す
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Undo();
        }

        //やり直し
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Redo();
        }

        //切り取り
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Cut();
        }

        //コピー
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Copy();
        }

        //貼り付け
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Paste();
        }

        //削除
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.SelectedText = "";
        }

        //色
        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cdColorEdit.ShowDialog() == DialogResult.OK)
            {
                rtTextArea.SelectionColor = cdColorEdit.Color;
            }
        }

        //フォント
        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fdFontEdit.ShowDialog() == DialogResult.OK)
            {
                rtTextArea.SelectionFont = fdFontEdit.Font;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditMenuMaskCheck();
            //if (rtTextArea.CanUndo)
            //{
            //    UndoToolStripMenuItem.Enabled = true;
            //}
            //else
            //{
            //    UndoToolStripMenuItem.Enabled = false;
            //}
            //
            //if (rtTextArea.CanRedo)
            //{
            //    RedoToolStripMenuItem.Enabled = true;
            //}
            //else
            //{
            //    RedoToolStripMenuItem.Enabled = false;
            //}
            //
            //if (rtTextArea.SelectedText == "")
            //{
            //    CutToolStripMenuItem.Enabled = false;
            //    CopyToolStripMenuItem.Enabled = false;
            //    DeleteToolStripMenuItem.Enabled = false;
            //}
            //else
            //{
            //    CutToolStripMenuItem.Enabled = true;
            //    CopyToolStripMenuItem.Enabled = true;
            //    DeleteToolStripMenuItem.Enabled = true;
            //}
            //
            //if (Clipboard.ContainsText())
            //{
            //    PasteToolStripMenuItem.Enabled = true;
            //}
        }

        //マスク
        private void EditMenuMaskCheck()
        {
            UndoToolStripMenuItem.Enabled = rtTextArea.CanUndo;
            RedoToolStripMenuItem.Enabled = rtTextArea.CanRedo;
            CutToolStripMenuItem.Enabled = (rtTextArea.SelectionLength > 0);
            CopyToolStripMenuItem.Enabled = (rtTextArea.SelectionLength > 0);
            PasteToolStripMenuItem.Enabled = Clipboard.GetDataObject().GetDataPresent(DataFormats.Rtf);
            DeleteToolStripMenuItem.Enabled = (rtTextArea.SelectionLength > 0);
        }

        //新規作成
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveMessage(sender, e);
            rtTextArea.Text = "";
            fileName = "無題";
        }

        

        private void SaveMessage(object sender, EventArgs e)
        {
            if (rtTextArea.Text!="")
            {
                DialogResult result = MessageBox.Show("ファイルを保存しますか？", "質問",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                //何が選択されたか調べる
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    SaveSToolStripMenuItem_Click(sender, e);
                }
                else if (result == DialogResult.No)
                {
                    //「いいえ」が選択された時
                    
                }
            }
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveMessage(sender, e);
        }

        

    }
}
