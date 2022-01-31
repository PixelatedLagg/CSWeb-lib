using System.Text;
using System;

namespace Impart
{
    /// <summary>Generate HTML and CSS with some Javascript functionality soon to be added.</summary>
    /// <remarks>This is the main class used in Impart.</remarks>
    public class WebPage
    {
        private string defaultMargin;
        internal StringBuilder textBuilder;
        internal StringBuilder styleBuilder;
        internal StringBuilder bodyBuilder;

        /// <summary>Creates a WebPage instance.</summary>
        /// <returns>A WebPage instance.</returns>
        protected WebPage()
        {
            defaultMargin = "0px";
            textBuilder = new StringBuilder();
            styleBuilder = new StringBuilder("overflow-x: auto; overflow-y: auto;");
            bodyBuilder = new StringBuilder();
        }

        /// <summary>Add <paramref name="style"/> to the WebPage.</summary>
        /// <param name="style">The Style instance to add.</param>
        protected void AddStyle(Style style)
        {
            styleBuilder.Append(style.Render());
        }

        /// <summary>Add <paramref name="text"/> to the WebPage.</summary>
        /// <param name="text">The Text instance to add.</param>
        protected void AddText(Text text)
        {
            switch (text.type)
            {
                case TextType.Regular:
                    if (text.id == null)
                    {
                        textBuilder.Append($"<p{text.attributeBuilder.ToString()}{text.style}>{text.text}</p>");
                    }
                    else
                    {
                        textBuilder.Append($"<p id=\"{text.id}\"{text.attributeBuilder.ToString()}{text.style}>{text.text}</p>");
                    }
                    break;
                case TextType.Bold:
                    if (text.id == null)
                    {
                        textBuilder.Append($"<b{text.attributeBuilder.ToString()}{text.style}>{text.text}</b>");
                    }
                    else
                    {
                        textBuilder.Append($"<b id=\"{text.id}\"{text.attributeBuilder.ToString()}{text.style}>{text.text}</b>");
                    }
                    break;
                case TextType.Delete:
                    if (text.id == null)
                    {
                        textBuilder.Append($"<del{text.attributeBuilder.ToString()}{text.style}>{text.text}</del>");
                    }
                    else
                    {
                        textBuilder.Append($"<del id=\"{text.id}\"{text.attributeBuilder.ToString()}{text.style}>{text.text}</del>");
                    }
                    break;
                case TextType.Emphasize:
                    if (text.id == null)
                    {
                        textBuilder.Append($"<em{text.attributeBuilder.ToString()}{text.style}>{text.text}</em>");
                    }
                    else
                    {
                        textBuilder.Append($"<em id=\"{text.id}\"{text.attributeBuilder.ToString()}{text.style}>{text.text}</em>");
                    }
                    break;
                case TextType.Important:
                    if (text.id == null)
                    {
                        textBuilder.Append($"<strong{text.attributeBuilder.ToString()}{text.style}>{text.text}</strong>");
                    }
                    else
                    {
                        textBuilder.Append($"<strong id=\"{text.id}\"{text.attributeBuilder.ToString()}{text.style}>{text.text}</strong>");
                    }
                    break;
                case TextType.Insert:
                    if (text.id == null)
                    {
                        textBuilder.Append($"<ins{text.attributeBuilder.ToString()}{text.style}>{text.text}</ins>");
                    }
                    else
                    {
                        textBuilder.Append($"<ins id=\"{text.id}\"{text.attributeBuilder.ToString()}{text.style}>{text.text}</ins>");
                    }
                    break;
                case TextType.Italic:
                    if (text.id == null)
                    {
                        textBuilder.Append($"<i{text.attributeBuilder.ToString()}{text.style}>{text.text}</i>");
                    }
                    else
                    {
                        textBuilder.Append($"<i id=\"{text.id}\"{text.attributeBuilder.ToString()}{text.style}>{text.text}</i>");
                    }
                    break;
                case TextType.Mark:
                    if (text.id == null)
                    {
                        textBuilder.Append($"<mark{text.attributeBuilder.ToString()}{text.style}>{text.text}</mark>");
                    }
                    else
                    {
                        textBuilder.Append($"<mark id=\"{text.id}\"{text.attributeBuilder.ToString()}{text.style}>{text.text}</mark>");
                    }
                    break;
                case TextType.Small:
                    if (text.id == null)
                    {
                        textBuilder.Append($"<small{text.attributeBuilder.ToString()}{text.style}>{text.text}</small>");
                    }
                    else
                    {
                        textBuilder.Append($"<small id=\"{text.id}\"{text.attributeBuilder.ToString()}{text.style}>{text.text}</small>");
                    }
                    break;
                case TextType.Subscript:
                    if (text.id == null)
                    {
                        textBuilder.Append($"<sub{text.attributeBuilder.ToString()}{text.style}>{text.text}</sub>");
                    }
                    else
                    {
                        textBuilder.Append($"<sub id=\"{text.id}\"{text.attributeBuilder.ToString()}{text.style}>{text.text}</sub>");
                    }
                    break;
                case TextType.Superscript:
                    if (text.id == null)
                    {
                        textBuilder.Append($"<sup{text.attributeBuilder.ToString()}{text.style}>{text.text}</sup>");
                    }
                    else
                    {
                        textBuilder.Append($"<sup id=\"{text.id}\"{text.attributeBuilder.ToString()}{text.style}>{text.text}</sup>");
                    }
                    break;
            }
        }

        /// <summary>Set the WebPage title to <paramref name="title"/>.</summary>
        /// <param name="title">The string to set the title to.</param>
        protected void SetTitle(string title)
        {
            if (String.IsNullOrEmpty(title))
            {
                throw new ImpartError("Title cannot be null or empty!");
            }
            textBuilder.Append($"<title>{title}</title>");
        }

        /// <summary>Add <paramref name="image"/> to the WebPage.</summary>
        /// <param name="image">The Image instance to add.</param>
        protected void AddImage(Image image)
        {
            textBuilder.Append($"<img src=\"{image.path}\"{image.attributeBuilder.ToString()}{image.style}>");
        }

        /// <summary>Add <paramref name="header"/> to the WebPage.</summary>
        /// <param name="header">The Header instance to add.</param>
        protected void AddHeader(Header header)
        {
            textBuilder.Append($"<h{header.number}{header.attributes}{header.style}>{header.text}</h{header.number}>");
        }

        /// <summary>Add <paramref name="link"/> to the WebPage.
        /// <param name="link">The Link instance to add.</param>
        protected void AddLink(Link link)
        {
            if (link.linkType == typeof(Image))
            {
                switch (link.id, link.image.id)
                {
                    case (null, null):
                        textBuilder.Append($"<a href=\"{link.path}\"><img src=\"{link.image.path}\" {link.image.style}></a>");
                        break;
                    case (string, string) a when a.Item1 != null && a.Item2 != null:
                        textBuilder.Append($"<a href=\"{link.path}\" id=\"{link.id}\"><img src=\"{link.image.path}\" id=\"{link.image.id}\" {link.image.style}></a>");
                        break;
                    case (string, string) b when b.Item1 == null && b.Item2 != null:
                        textBuilder.Append($"<a href=\"{link.path}\"><img src=\"{link.image.path}\" id=\"{link.image.id}\" {link.image.style}></a>");
                        break;
                    case (string, string) c when c.Item1 != null && c.Item2 == null:
                        textBuilder.Append($"<a href=\"{link.path}\" id=\"{link.id}\"><img src=\"{link.image.path}\" {link.image.style}></a>");
                        break;
                }
            }
            else
            {
                switch (link.id, link.text.id)
                {
                    case (null, null):
                        textBuilder.Append($"<a href=\"{link.path}\"><p>{link.text.text}</p></a>");
                        break;
                    case (string, string) a when a.Item1 != null && a.Item2 != null:
                        textBuilder.Append($"<a href=\"{link.path}\" id=\"{link.id}\"><p id=\"{link.image.id}\">{link.text.text}</p></a>");
                        break;
                    case (string, string) b when b.Item1 == null && b.Item2 != null:
                        textBuilder.Append($"<a href=\"{link.path}\"><p id=\"{link.image.id}\">{link.text.text}</p></a>");
                        break;
                    case (string, string) c when c.Item1 != null && c.Item2 == null:
                        textBuilder.Append($"<a href=\"{link.path}\" id=\"{link.id}\"><p>{link.text.text}</p></a>");
                        break;
                }
            }
        }

        /// <summary>Add a table to the WebPage with <paramref name="rowNum"/> as the number of rows and a string[] as the entries.</summary>
        /// <param name="rowNum">The number of rows.</param>
        /// <param name="obj">An array of strings to add as entries.</param>
        protected void AddTable(int rowNum, params string[] obj)
        {
            if (rowNum > obj.Length)
            {
                throw new ImpartError("Number of table rows cannot be bigger than number of table entries!");
            }
            int length = 0;
            foreach (string s in obj)
            {
                length += s.Length;
            }
            StringBuilder tempBuilder = new StringBuilder(length * (rowNum + 10));
            tempBuilder.Append("<table>");
            tempBuilder.Append($"<tr>");
            for (int x = 0; x < rowNum; x++)
            {
                tempBuilder.Append($"<th>{obj[x]}</th>");
            }
            tempBuilder.Append($"</tr>"); 
            int vertRowNum = (int)Math.Round(System.Convert.ToDouble(((double)obj.Length - (double)rowNum) / (double)rowNum), MidpointRounding.AwayFromZero);
            if ((obj.Length - rowNum) % rowNum > 0)
            {
                int _currentobj = 0;
                for (int a = 0; a < vertRowNum + 1; a++)
                {
                    tempBuilder.Append($"<tr>");
                    for (int x = 0; x < rowNum; x++)
                    {
                        if (obj.Length <= rowNum + _currentobj)
                        {
                            tempBuilder.Append($"</tr>");
                            textBuilder.Append($"</table>");
                            return;
                        }
                        tempBuilder.Append($"<td>{obj[rowNum + _currentobj]}</td>");
                        _currentobj++;
                    }
                    tempBuilder.Append($"</tr>");
                }
            }
            int currentObj = 0;
            for (int a = 0; a < vertRowNum; a++)
            {
                tempBuilder.Append($"<tr>");
                for (int x = 0; x < rowNum; x++)
                {
                    tempBuilder.Append($"<td>{obj[rowNum + currentObj]}</td>");
                    currentObj++;
                }
                tempBuilder.Append($"</tr>");
            }
            textBuilder.Append($"{tempBuilder.ToString()}</table>");
        }

        /// <summary>Add <paramref name="division"/> to the WebPage.</summary>
        /// <param name="division">The Division instance to add.</param>
        protected void AddDivision(Division division)
        {
            textBuilder.Append(division.Render());
            styleBuilder.Append(division.webPageStyleBuilder.ToString());
        }

        /// <summary>Add <paramref name="list"/> to the WebPage.</summary>
        /// <param name="list">The List instance to add.</param>
        protected void AddList(List list)
        {
            textBuilder.Append(list.Render());
        }

        /// <summary>Add <paramref name="scrollbar"/> to the WebPage.</summary>
        /// <param name="scrollbar">The Scrollbar instance to add.</param>
        protected void SetScrollBar(Scrollbar scrollbar)
        {
            switch (scrollbar.axis)
            {
                case Axis.X:
                    styleBuilder.Append(" overflow-x: auto;");
                    break;
                case Axis.Y:
                    styleBuilder.Append(" overflow-y: auto;");
                    break;
                default:
                    throw new ImpartError("Invalid axis!");
            }
            switch (scrollbar.width)
            {
                case Percent pct:
                    styleBuilder.Append($"::-webkit-scrollbar {{width: {scrollbar.width.Pct().percent}%;background-color: #808080; }}::-webkit-scrollbar-track {{");
                    break;
                case Pixels pxls:
                    styleBuilder.Append($"::-webkit-scrollbar {{width: {scrollbar.width.Px().pixels}px;background-color: #808080; }}::-webkit-scrollbar-track {{");
                    break;
            }
            switch (scrollbar.bgColor)
            {
                case Rgb rgb:
                    styleBuilder.Append($"background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});}}");
                    break;
                case Hsl hsl:
                    styleBuilder.Append($"background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);}}");
                    break;
                case Hex hex:
                    styleBuilder.Append($"background-color: #{hex.hex};}}");
                    break;
            }
            styleBuilder.Append($"::-webkit-scrollbar-thumb {{");
            switch (scrollbar.fgColor)
            {
                case Rgb rgb:
                    styleBuilder.Append($"background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});");
                    break;
                case Hsl hsl:
                    styleBuilder.Append($"background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);");
                    break;
                case Hex hex:
                    styleBuilder.Append($"background-color: #{hex.hex};");
                    break;
            }
            if (scrollbar.radius != null)
            {
                switch (scrollbar.radius)
                {
                    case Percent pct:
                        styleBuilder.Append($"border-radius: {scrollbar.radius.Pct().percent}%;}}");
                        break;
                    case Pixels pxls:
                        styleBuilder.Append($"border-radius: {scrollbar.radius.Px().pixels}px;}}");
                        break;
                }
            }
            else
            {
                styleBuilder.Append("}");
            }
        }

        /// <summary>Add <paramref name="form"/> to the WebPage.</summary>
        /// <param name="form">The Form instance to add.</param>
        protected void AddForm(Form form)
        {
            textBuilder.Append(form.Render());
        }

        /// <summary>Add <paramref name="button"/> to the WebPage.</summary>
        /// <param name="button">The Button instance to add.</param>
        protected void AddButton(Button button)
        {
            textBuilder.Append(button.Render());
        }

        /// <summary>Set the default margin of the WebPage to <paramref name="size"/>.</summary>
        /// <param name="size">The Measurement instance to add.</param>
        protected void SetDefaultMargin(Measurement size)
        {
            switch (size)
            {
                case Pixels pixels:
                    defaultMargin = $"{pixels.pixels}px";
                    break;
                case Percent percent:
                    defaultMargin = $"{percent.percent}%";
                    break;
            }
        }

        internal string GetCode()
        {
            styleBuilder.Replace($"* {{padding: 0;margin: {defaultMargin};}}", "");
            styleBuilder.Append($"* {{padding: 0;margin: {defaultMargin};}}");
            return $"<!-- Generated by Impart - https://github.com/PixelatedLagg/Impart --><!DOCTYPE html><html xmlns=\"http://www.w3.org/1999/xhtml\"><style>{styleBuilder.ToString()}</style><body>{textBuilder.ToString()}</body></html>";
        }
    }
}