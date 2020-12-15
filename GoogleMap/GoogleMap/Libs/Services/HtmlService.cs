using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Ganss.XSS;

namespace GoogleMap.Libs.Services
{
    /// <summary>
    /// HTML解析服务
    /// </summary>
    public class HtmlService
    {
        /// <summary>
        /// HTML内容转纯文本（即去掉所有HTML标签）
        /// </summary>
        public string HtmlToText(string html)
        {
            var parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(html);
            return document.Body.Text();
        }
    }
}
