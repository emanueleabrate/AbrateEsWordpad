using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using clsFile_ns;

namespace AbrateEsWordpad
{
    public partial class frmWordpad : Form
    {

        clsFile fileManager;
        public frmWordpad()
        {
            InitializeComponent();
        }

        private void frmWordpad_Load(object sender, EventArgs e)
        {
            fileManager = new clsFile();
            fileManager.Modificato = true;
           
        }
        private void tagliaToolStripButton_Click(object sender, EventArgs e)
        {
            taglia();
        }
        private void tagliaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            taglia();
        }
        private void taglia()
        {
            rtb.Cut();
        }

        private void copiaToolStripButton_Click(object sender, EventArgs e)
        {
            copia();
        }
        private void copiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copia();
        }
        private void copia()
        {
            rtb.Copy();
        }

        private void incollaToolStripButton_Click(object sender, EventArgs e)
        {
            incolla();
        }
        private void incollaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            incolla();
        }
        private void incolla()
        {
            rtb.Paste();
        }

        private void AnnullaToolStripButton_Click(object sender, EventArgs e)
        {
            annulla();
        }

        private void annullaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            annulla();
        }

        private void annulla()
        {
            rtb.Undo(); 
        }

        private void RipristinaToolStripButton_Click(object sender, EventArgs e)
        {
            ripristina();
        }

        private void ripristinaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ripristina();
        }
        private void ripristina()
        {
            rtb.Redo();
        }

        private void AlignSxToolStripButton_Click(object sender, EventArgs e)
        {
            rtb.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void AlignCenterToolStripButton_Click(object sender, EventArgs e)
        {
            rtb.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void AlignRxToolStripButton_Click(object sender, EventArgs e)
        {
            rtb.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void ColorToolStripButton_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            if (cd.ShowDialog() == DialogResult.OK)
                rtb.SelectionColor = cd.Color;
        }

        private void FontToolStripButton_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();

            if (fd.ShowDialog() == DialogResult.OK)
                rtb.SelectionFont = fd.Font;
        }

        private void ElencoPuntToolStripButton_Click(object sender, EventArgs e)
        {
            rtb.SelectionBullet = true;
        }
    }
}
