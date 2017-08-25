using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        String path = "https://www.naver.com";
        public Form1()
        {
            InitializeComponent();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.AbsoluteUri == webBrowser1.Url.AbsoluteUri)
            {
                string data = "";
                HtmlDocument Doc = webBrowser1.Document;
                HtmlElementCollection elems = Doc.GetElementsByTagName("div");
                foreach (HtmlElement elem in elems)
                {
                    if (elem.GetAttribute("className") == "ah_roll_area PM_CL_realtimeKeyword_rolling")
                    {
                        for (int i = 0; i < elem.GetElementsByTagName("span").Count; i++)
                        {

                            if (elem.GetElementsByTagName("span")[i].GetAttribute("className") == "ah_r")
                            {
                                data = elem.GetElementsByTagName("span")[i].InnerText;
                                databox.AppendText(data + "\n");
                            }
                            if (elem.GetElementsByTagName("span")[i].GetAttribute("className") == "ah_k")
                            {
                                data = elem.GetElementsByTagName("span")[i].InnerText;
                                databox.AppendText(data + "\n");

                            }

                        }
                        break;
                    }
                }

            }
        }

        private void start_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(path);
        }
    }
}

