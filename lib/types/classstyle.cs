using System;
using System.Drawing;

namespace Csweb
{
    public class classstyle
    {
        private string textCache;
        private string id;
        private bool hasColor;
        public classstyle(string id)
        {
            Timer.StartTimer();
            this.id = id;
            hasColor = false;
            textCache = $".{id} {{%^";
            Debug.CallObjectEvent(new Log("[classstyle] created classstyle", Timer.GetTime()));
        }
        public void AddColor(Color color)
        {
            Timer.StartTimer();
            CheckColor();
            textCache = $"{textCache}{CheckLB()}    color: {color.ToKnownColor()};";
            hasColor = true;
            Debug.CallObjectEvent(new Log("[idstyle] added color (normal)", Timer.GetTime()));
        }
        public void AddHexColor(string hex)
        {
            Timer.StartTimer();
            CheckColor();
            if (hex.Length != 6)
            {
                throw new ArgumentException("Invalid hex value!");
            }
            textCache = $"{textCache}{CheckLB()}    color: #{hex};";
            Debug.CallObjectEvent(new Log("[classstyle] added color (hex)", Timer.GetTime()));
        }
        public void AddRGBColor(int x, int y, int z)
        {
            Timer.StartTimer();
            CheckColor();
            if (!(x >= 0 && y >= 0 && z >= 0 && x <= 255 && y <= 255 && z <= 255))
            {
                throw new ArgumentException("Invalid RGB value!");
            }
            textCache = $"{textCache}{CheckLB()}    color: rgb({x},{y},{z});";
            Debug.CallObjectEvent(new Log("[classstyle] added color (rgb)", Timer.GetTime()));
        }
        public void AddAlign(string alignment)
        {
            Timer.StartTimer();
            if (!Alignment.Any(alignment))
            {
                throw new ArgumentException("Invalid alignment value!");
            }
            textCache = $"{textCache}{CheckLB()}    text-align: {alignment};";
            Debug.CallObjectEvent(new Log("[classstyle] added alignment", Timer.GetTime()));
        }
        private string CheckLB()
        {
            if (textCache == $".{id} {{%^")
            {
                return "";
            }
            return "%^";
        }
        private void CheckColor()
        {
            if (hasColor)
            {
                throw new ArgumentException("Style already has color!");
            }
        }
        internal string Render()
        {
            textCache = textCache.Replace("%^", Environment.NewLine);
            return $"{textCache}{Environment.NewLine}}}";
        }
    }
}