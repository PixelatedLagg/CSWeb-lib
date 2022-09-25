using System.Text;
using Impart.Internal;
using Impart.Scripting;

namespace Impart
{
    /// <summary>Video element.</summary>
    public class Video : IElement, INested
    {
        /// <summary>The ID value of the instance. Returns null if ID is not set.</summary>
        public string ID
        {
            get
            {
                return ExtAttrs[ExtAttrType.ID]?.Value ?? null;
            }
        }
        private string _Source;

        /// <summary>The Video source file.</summary>
        public string Source
        {
            get
            {
                return _Source;
            }
            set
            {
                Changed = true;
                _Source = value;
            }
        }
        private (Length Width, Length Height) _Size;

        /// <summary>The Video player size.</summary>
        public (Length Width, Length Height) Size
        {
            get
            {
                return _Size;
            }
            set
            {
                Changed = true;
                _Size = value;
            }
        }
        private VideoOptions _Options;

        /// <summary>Options for the Video player.</summary>
        public VideoOptions Options
        {
            get
            {
                return _Options;
            }
            set
            {
                Changed = true;
                _Options = value;
            }
        }
        
        /// <summary>The Attr values of the instance.</summary>
        public AttrList Attrs = new AttrList();

        /// <summary>The Attr values of the instance.</summary>
        AttrList IElement.Attrs
        {
            get
            {
                return Attrs;
            }
        }

        /// <summary>The ExtAttr values of the instance.</summary>
        public ExtAttrList ExtAttrs = new ExtAttrList();

        /// <summary>The ExtAttr values of the instance.</summary>
        ExtAttrList IElement.ExtAttrs
        {
            get
            {
                return ExtAttrs;
            }
        }

        /// <summary>The internal ID of the instance.</summary>
        int IElement.IOID
        {
            get
            {
                return _IOID;
            }
        }

        internal int _IOID = Ioid.Generate();
        internal EventManager _Events = new EventManager();
        internal bool Changed = true;
        private string Render = "";

        /// <summary>Creates a Video instance.</summary>
        /// <param name="source">The Video source file.</param>
        /// <param name="width">The Video player width.</param>
        /// <param name="height">The Video player height.</param>
        /// <param name="options">Options for the Video player.</param>
        public Video(string source, Length width, Length height, VideoOptions options)
        {
            _Source = source;
            _Size = (width, height);
            _Options = options;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            if (!Changed && !Attrs.Changed && !ExtAttrs.Changed)
            {
                return Render;
            }
            Changed = false;
            Attrs.Changed = false;
            ExtAttrs.Changed = false;
            StringBuilder result = new StringBuilder($"<video src=\"{_Source}\" width=\"{_Size.Width}\" height=\"{_Size.Height}\"{(_Options.Autoplay ? " autoplay " : "")}{(_Options.ShowControls ? " controls " : "")}{(_Options.Mute ? " muted " : "")}");
            if (Attrs.Count != 0)
            {
                result.Append("style=\"");
                foreach (Attr attr in Attrs)
                {
                    result.Append(attr);
                }
                result.Append($"\"class=\"{_IOID}\"{_Events}");
            }
            foreach (ExtAttr extAttrs in ExtAttrs)
            {
                result.Append(extAttrs);
            }
            Render = result.Append("></video>").ToString();
            return Render;
        }

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        public IElement Clone()
        {
            Video result = new Video(_Source, _Size.Width, _Size.Height, _Options);
            result._IOID = _IOID;
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            return result;
        }

        /// <summary>Create an ElementRef referencing the IElement</summary>
        public ElementRef Reference() => new ElementRef(_IOID);

        /// <summary>Create an ElementRef referencing the IElement</summary>
        ElementRef IElement.Reference() => new ElementRef(_IOID);

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        IElement IElement.Clone()
        {
            Video result = new Video(_Source, _Size.Width, _Size.Height, _Options);
            result._IOID = _IOID;
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            return result;
        }

        /// <summary>Return the first part of the INested as a string.</summary>
        string INested.First()
        {
            return ToString().Remove(Render.Length - 8);
        }

        /// <summary>Return the last part of the INested as a string.</summary>
        string INested.Last()
        {
            return "</video>";
        }
    }
}