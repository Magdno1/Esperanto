using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Esperanto
{
    public partial class MainForm : Form
    {
        RomHandler primaryRom, secondaryRom;

        public MainForm()
        {
            InitializeComponent();

            Text = Application.ProductName;
            tvMessages.Font = SystemFonts.MessageBoxFont;

            FormClosing += ((s, ev) => { Properties.Settings.Default.Save(); });
        }

        private void openROMsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.File1 != string.Empty)
            {
                ofdRomFirst.InitialDirectory = Path.GetDirectoryName(Properties.Settings.Default.File1);
                ofdRomFirst.FileName = Path.GetFileName(Properties.Settings.Default.File1);
            }

            if (Properties.Settings.Default.File2 != string.Empty)
            {
                ofdRomSecond.InitialDirectory = Path.GetDirectoryName(Properties.Settings.Default.File2);
                ofdRomSecond.FileName = Path.GetFileName(Properties.Settings.Default.File2);
            }

            int romsLoaded = 0;

            if (ofdRomFirst.ShowDialog() == DialogResult.OK)
            {
                RomHandler loadedRom = new RomHandler(ofdRomFirst.FileName);
                Properties.Settings.Default.File1 = ofdRomFirst.FileName;

                if (loadedRom.Language == GameLanguage.Japanese)
                    primaryRom = loadedRom;
                else if (loadedRom.Language == GameLanguage.English)
                    secondaryRom = loadedRom;

                romsLoaded++;
            }

            if (ofdRomSecond.ShowDialog() == DialogResult.OK)
            {
                RomHandler loadedRom = new RomHandler(ofdRomSecond.FileName);
                Properties.Settings.Default.File2 = ofdRomSecond.FileName;

                if (primaryRom == null)
                    primaryRom = loadedRom;
                else if (secondaryRom == null)
                    secondaryRom = loadedRom;

                romsLoaded++;
            }

            if (romsLoaded == 0)
                Text = Application.ProductName;
            else if (romsLoaded == 1)
                Text = string.Format("{0} - [{1}]", Application.ProductName, Path.GetFileName(ofdRomFirst.FileName));
            else if (romsLoaded == 2)
                Text = string.Format("{0} - [{1}, {2}]", Application.ProductName, Path.GetFileName(ofdRomFirst.FileName), Path.GetFileName(ofdRomSecond.FileName));
            else
                throw new Exception("The fuck did you do!?!");

            previewControlPrimary.Enabled = (primaryRom != null);
            previewControlSecondary.Enabled = (secondaryRom != null);

            createComparisonToolStripMenuItem.Enabled = true;

            InitializeViewer();
        }

        private void createComparisonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ComparePath != string.Empty)
            {
                fbdCreateCompare.SelectedPath = Properties.Settings.Default.ComparePath;
            }

            if (fbdCreateCompare.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.ComparePath = fbdCreateCompare.SelectedPath;
                string htmlFile = Path.Combine(fbdCreateCompare.SelectedPath, "compare.htm");
                DataDumpers.DumpComparison(htmlFile, primaryRom, secondaryRom);
            }
        }

        public void InitializeViewer()
        {
            previewControlPrimary.Initialize(primaryRom);
            previewControlSecondary.Initialize(secondaryRom);

            tvMessages.BeginUpdate();
            tvMessages.Nodes.Clear();

            foreach (MessageBank bank in primaryRom.MessageBanks)
            {
                TreeNode bankNode = new TreeNode((bank.Number != -1 ? string.Format("Bank #{0}", bank.Number) : "General"));
                bankNode.Tag = bank;
                for (int i = 0; i < bank.Messages.Count; i++)
                {
                    TreeNode messageNode = new TreeNode(string.Format("Message #{0}", i + 1));
                    messageNode.Tag = bank.Messages[i];
                    bankNode.Nodes.Add(messageNode);
                }
                tvMessages.Nodes.Add(bankNode);
            }
            tvMessages.AfterSelect += ((s, ev) =>
            {
                TreeNode node = (s as TreeView).SelectedNode;

                if (node.Tag is Message && node.Parent?.Tag is MessageBank)
                {
                    int bankIndex = Array.IndexOf(primaryRom.MessageBanks, node.Parent.Tag);
                    int messageIndex = Array.IndexOf(primaryRom.MessageBanks[bankIndex].Messages.ToArray(), node.Tag);

                    previewControlPrimary.UpdateMessage(bankIndex, messageIndex);
                    previewControlSecondary.UpdateMessage(bankIndex, messageIndex);
                }
            });
            tvMessages.EndUpdate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();

            Version programVersion = new Version(Application.ProductVersion);
            builder.AppendFormat("{0} v{1}.{2}", Application.ProductName, programVersion.Major, programVersion.Minor);
            if (programVersion.Build != 0) builder.AppendFormat(".{0}", programVersion.Build);
            builder.Append(" - ");
            builder.AppendLine((Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyDescriptionAttribute)) as AssemblyDescriptionAttribute).Description);
            builder.AppendLine();
            builder.AppendLine((Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyCopyrightAttribute)) as AssemblyCopyrightAttribute).Copyright);

            MessageBox.Show(builder.ToString(), "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Debug crap like CSGeom
        [System.Diagnostics.Conditional("DEBUG")]
        private void Test()
        {
            if (Environment.MachineName != "NANAMI-X") return;

            if (false)
            {
                for (int i = 0; i < primaryRom.MessageStyles.Length; i++)
                    primaryRom.MessageStyles[i].StyleImage?.Save(@"E:\temp\mmz\" + i.ToString() + ".png");
                primaryRom.FontImage?.Save(@"E:\temp\mmz\font.png");
                primaryRom.FontImageHighlighted?.Save(@"E:\temp\mmz\font-hilight.png");

                primaryRom.GetMessageImage(primaryRom.MessageBanks[0].Messages[13], 0)?.Save(@"E:\temp\mmz\TEST.png");

                System.Threading.Thread.CurrentThread.Abort();
            }

            if (false)
            {
                string tmpxx =
                    "ぁあぃいぅうぇえぉおかがきぎくぐ" +
                    "けげこごさざしじすずせぜそぞただ" +
                    "ちぢっつづてでとどなにぬねのはば" +
                    "ぱひびぴふぶぷへべぺほぼぽまみむ" +
                    "めもゃやゅゆょよらりるれろゎわを" +
                    "んァアィイゥウェエォオカガキギク" +
                    "グケゲコゴサザシジスズセゼソゾタ" +
                    "ダチヂッツヅテデトドナニヌネノハ" +
                    "バパヒビピフブプヘベペホボポマミ" +
                    "ムメモャヤュユョヨラリルレロヮワ" +
                    "ヲンヴヵヶ";
                tmpxx =
                    "思気力人間機械本当年月日伝説私〇" +//1
                    "自由平和〇存〇不安聞言知成功失敗" +//2
                    "理想郷新古旧全滅他最近遠〇方敵助" +//3
                    "未来過去生死科学同点口目大〇感地" +//4
                    "終長動止〇右左上下時列車都市転送" +//5
                    "工場信破壊高〇多少防御攻撃真回路" +//6
                    "所在軍彼〇出入街声必鉄型廃砂漠爆" +//7
                    "発底塔〇前後〇守現会基誰士作品団" +//8
                    "事無神聖域脱主救世戦〇手捕実〇涙" +//9
                    "名〇〇四天王赤青〇〇頼者内外〇〇" +//A
                    "強弱数使用悪〇何〇呼以再々〇‥英" +//B
                    "雄見消〇度女行分部形話体倍巨侵活" +//C
                    "続界永処仲中隊室心明情報収集利向" +//D
                    "〇配〇闘〇〇〇今系　★☆　“・Σ";   //E

                StringBuilder tmp = new StringBuilder();
                for (int i = 0; i < tmpxx.Length; i++)
                {
                    if ((i % 16) == 0) { tmp.AppendFormat("            /* 0x{0:X2} */ ", i + 0x10); }
                    tmp.AppendFormat("'{0}'", tmpxx[i]);
                    if (((i + 1) % 16) == 0) tmp.Append("," + Environment.NewLine);
                    else if (i != tmpxx.Length - 1) tmp.Append(", ");
                }
                Clipboard.SetText(tmp.ToString());
            }
        }
    }
}
