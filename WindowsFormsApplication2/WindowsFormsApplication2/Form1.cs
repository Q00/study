using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        enum mat { 경기수, 승, 패, 무, 게임차, 연속, 출루율, 장타율, 최근10경기 };

        String path1 = "http://sports.news.naver.com/kbaseball/record/index.nhn?category=kbo";

        String path3 = "http://sports.news.naver.com/kbaseball/news/index.nhn?type=team&team=LG";


        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(path1);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.AbsoluteUri == webBrowser1.Url.AbsoluteUri)
            {
                HtmlDocument Doc = webBrowser1.Document;
                HtmlElementCollection elems = Doc.GetElementsByTagName("tr");

                ////split메서드를 사용하기 위한 선언
                //String[] matches;
                //String[] seperator = new String[] { "/span>" };

                foreach (HtmlElement elem in elems)
                {
                    if (elem.InnerHtml.Contains("<span id=\"team_LG\">LG</span>"))
                    {

                        int count = 0;
                        foreach (Match m in Regex.Matches(elem.InnerHtml, "<span>(.*)</span>", RegexOptions.Multiline))
                        {
                            count++;
                            //group이란 메서드는 정규식으로 일치시킨 그룹의 콜렉션을 가져온다.. 따라서 0값-> 내가 검색한 정규식을 포함한 값.. [1]값 -> 내가 찾고자 하는 값.. 
                            //만약 정규식에서 1개 초과한 수의 갯수를 찾는다고한다면 1이상의 인덱스가 나올수 있다.
                            matchbox.AppendText((mat)(count) + " : " + m.Groups[1].ToString() + "\n");

                        }

                    }
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String str = getURL();
            if (str == "false")
            {
                MessageBox.Show("오류");
            }
            else
            {
                webBrowser1.Navigate("http://sports.news.naver.com"+str);
            }
        }

        private string GetWebPageHtmlFromUrl(string url)
        {
            var hw = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = hw.Load(url);
            return doc.DocumentNode.OuterHtml;
        }
        private String getURL()
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(GetWebPageHtmlFromUrl("http://sports.news.naver.com/kbaseball/index.nhn"));
            HtmlAgilityPack.HtmlNodeCollection NodeList = doc.DocumentNode.SelectNodes("//li[@class='hmb_list_items']");
            foreach (HtmlAgilityPack.HtmlNode node in NodeList)
            {
                if (node.InnerHtml.Contains("<span class=\"name\">LG</span>"))
                {
                    if (node.InnerHtml.Contains("<div class=\"vs_list vs_list1\">"))
                    {
                        Match m = Regex.Match(node.InnerHtml, "<a href=\"(.*?)\"");
                        return m.Groups[1].ToString();
                    }
                }


            }
            return "false";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(path3);
        }
    }
}
