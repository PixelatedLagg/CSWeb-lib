using System;
using System.Drawing;

namespace Csweb
{
    public class idstyle
    {
        private string textCache;
        private string id;
        private int colorCheck;
        public idstyle(string id)
        {
            Timer.StartTimer();
            this.id = id;
            textCache = $"#{id} {{%^";
            colorCheck = 0;
            Debug.CallObjectEvent(new Log("[idstyle] created idstyle", Timer.GetTime()));
        }
        public void AddColor(Color color)
        {
            Timer.StartTimer();
            colorCheck++;
            textCache = $"{textCache}{CheckLB()}    color: {color.ToKnownColor()};";
            Debug.CallObjectEvent(new Log("[idstyle] added color (normal)", Timer.GetTime()));
        }
        public void AddHexColor(string hex)
        {
            Timer.StartTimer();
            colorCheck++;
            if (hex.Length != 6)
            {
                throw new ArgumentException("Invalid hex value!");
            }
            textCache = $"{textCache}{CheckLB()}    color: #{hex};";
            Debug.CallObjectEvent(new Log("[idstyle] added color (hex)", Timer.GetTime()));
        }
        public void AddRGBColor(int x, int y, int z)
        {
            Timer.StartTimer();
            colorCheck++;
            if (!(x >= 0 && y >= 0 && z >= 0 && x <= 255 && y <= 255 && z <= 255))
            {
                throw new ArgumentException("Invalid RGB value!");
            }
            textCache = $"{textCache}{CheckLB()}    color: rgb({x},{y},{z});";
            Debug.CallObjectEvent(new Log("[idstyle] added color (rgb)", Timer.GetTime()));
        }
        public void AddAlign(string alignment)
        {
            Timer.StartTimer();
            if (!Alignment.Any(alignment))
            {
                throw new ArgumentException("Invalid alignment value!");
            }
            textCache = $"{textCache}{CheckLB()}    text-align: {alignment};";
            Debug.CallObjectEvent(new Log("[idstyle] added alignment", Timer.GetTime()));
        }
        private string CheckLB()
        {
            if (textCache == $"#{id} {{%^")
            {
                return "";
            }
            return "%^";
        }
        internal string Render()
        {
            if (colorCheck > 1)
            {
                throw new ArgumentException("Cannot assign multiple color instances!");
            }
            textCache = textCache.Replace("%^", Environment.NewLine);
            return $"{textCache}{Environment.NewLine}}}";
        }
    }
}