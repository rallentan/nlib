// Credit: Judah Himango [http://stackoverflow.com/questions/97459/automatically-select-all-text-on-focus-in-winforms-textbox]

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NLib.Windows.Forms
{
    public class TextBox2 : TextBox
    {
        //--- Fields ---
        bool _alreadyFocused;

        //--- Public Properties ---

        public bool SelectAllOnFocus { get; set; }

        //--- Protected Methods ---

        protected override void OnLeave(EventArgs e)
        {
            _alreadyFocused = false;
            base.OnLeave(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            // Select all text only if the mouse isn't down.
            // This makes tabbing to the textbox give focus.
            if (MouseButtons == MouseButtons.None)
            {
                if (SelectAllOnFocus)
                    SelectAll();
                _alreadyFocused = true;
            }
            base.OnGotFocus(e);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            // Web browsers like Google Chrome select the text on mouse up.
            // They only do it if the textbox isn't already focused,
            // and if the user hasn't selected all text.
            if (!_alreadyFocused && SelectionLength == 0)
            {
                _alreadyFocused = true;
                if (SelectAllOnFocus)
                    SelectAll();
            }
            base.OnMouseUp(mevent);
        }
    }
}
