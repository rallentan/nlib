using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace NLib.Windows.Forms
{
    public static class ControlExtensions
    {
        //--- Constants ---

        const string ARGNAME_C = "c";
        const string ARGNAME_CHILD = "child";
        const string ARGNAME_SIDE = "side";
        const string ARGNAME_PARENTSIDE = "parentSide";
        const string ARGNAME_THISSIDE = "thisSide";
        const string ARGNAME_SIBLINGSSIDE = "siblingsSide";
        const string EXCMSG_INVALID_ENUM = "The specified enumeration value is not valid for this method.";
        const string EXCMSG_INCOMPATIBLE_ENUMS = "Parameters contain incompatible enumeration values.";

        //--- Public Static Methods ---

        /// <summary>
        /// Gets the rectangle encompassing the control and its margin.
        /// </summary>
        /// <param name="c">The control to get the margin rectangle of.</param>
        /// <returns>A <see cref="Rectangle"/> representing the margin rectangle of the specified control.</returns>
        public static Rectangle GetMarginRectangle(this Control c)
        {
            if (c == null)
            {
                throw new ArgumentNullException(ARGNAME_C);
            }
            return new Rectangle(
                c.Location.X - c.Margin.Left,
                c.Location.Y - c.Margin.Top,
                c.Size.Width + c.Margin.Right,
                c.Size.Height + c.Margin.Bottom
                );
        }

        public static void SnapToChild(this Control c, SnapToSide side, Control child)
        {
            if (c == null)
            {
                throw new ArgumentNullException(ARGNAME_C);
            }
            if (child == null)
            {
                throw new ArgumentNullException(ARGNAME_CHILD);
            }
            if (side == 0 ||
                !Enum.IsDefined(typeof(SnapToSide), side) ||
                side.HasFlag(SnapToSide.Left) ||
                side.HasFlag(SnapToSide.Top))
            {
                throw new ArgumentOutOfRangeException(ARGNAME_SIDE, EXCMSG_INVALID_ENUM);
            }

            Padding padding;
            if (c is Form && c.Padding.All == 0)
            {
                padding = new Padding(9, 9, 9, 9);
            }
            else
            {
                padding = c.Padding;
            }

            if (side.HasFlag(SnapToSide.Right))   /**/c.Size = new Size(child.Right + child.Margin.Right + padding.Right, c.Height);
            if (side.HasFlag(SnapToSide.Bottom))  /**/c.Size = new Size(c.Width, child.Bottom + child.Margin.Bottom + padding.Bottom);
        }

        public static void SnapToParent(this Control c, SnapToSide parentSide)
        {
            if (c == null)
            {
                throw new ArgumentNullException(ARGNAME_C);
            }
            if (parentSide == 0 ||
                !Enum.IsDefined(typeof(SnapToSide), parentSide) ||
                (parentSide.HasFlag(SnapToSide.Left) && parentSide.HasFlag(SnapToSide.Right)) ||
                (parentSide.HasFlag(SnapToSide.Top) && parentSide.HasFlag(SnapToSide.Bottom)))
            {
                throw new ArgumentOutOfRangeException(ARGNAME_PARENTSIDE, EXCMSG_INVALID_ENUM);
            }

            Padding parentPadding;
            Rectangle parentBounds;
            if (c.Parent is Form)
            {
                if (c.Parent.Padding.All == 0)
                {
                    parentPadding = new Padding(9, 9, 9, 9);
                }
                else
                {
                    parentPadding = c.Parent.Padding;
                }
                parentBounds = new Rectangle(Point.Empty, c.Parent.Size);
            }
            else
            {
                parentPadding = c.Parent.Padding;
                parentBounds = new Rectangle(c.Parent.Location, c.Parent.Size);
            }

            if (parentSide.HasFlag(SnapToSide.Left))    /**/c.Location = new Point(parentBounds.X + c.Margin.Left + parentPadding.Left, c.Location.Y);
            if (parentSide.HasFlag(SnapToSide.Top))     /**/c.Location = new Point(c.Location.X, parentBounds.Y + c.Margin.Top + parentPadding.Top);
            if (parentSide.HasFlag(SnapToSide.Right))   /**/c.Location = new Point(parentBounds.Right - c.Width - c.Margin.Right - parentPadding.Right, c.Location.Y);
            if (parentSide.HasFlag(SnapToSide.Bottom))  /**/c.Location = new Point(c.Location.X, parentBounds.Bottom - c.Height - c.Margin.Bottom - parentPadding.Bottom);
        }
        
        public static void SnapToSibling(this Control c, SnapToSide thisSide, SnapToSide siblingsSide, Control sibling)
        {
            if (c == null)
            {
                throw new ArgumentNullException(ARGNAME_C);
            }
            if (thisSide == 0 ||
                !Enum.IsDefined(typeof(SnapToSide), thisSide) ||
                (thisSide == SnapToSide.Left && thisSide == SnapToSide.Right) ||
                (thisSide == SnapToSide.Top && thisSide == SnapToSide.Bottom))
            {
                throw new ArgumentOutOfRangeException(ARGNAME_THISSIDE, EXCMSG_INVALID_ENUM);
            }
            if (siblingsSide == 0 ||
                !Enum.IsDefined(typeof(SnapToSide), siblingsSide) ||
                (siblingsSide == SnapToSide.Left && siblingsSide == SnapToSide.Right) ||
                (siblingsSide == SnapToSide.Top && siblingsSide == SnapToSide.Bottom))
            {
                throw new ArgumentOutOfRangeException(ARGNAME_SIBLINGSSIDE, EXCMSG_INVALID_ENUM);
            }
            if ((((thisSide & SnapToSide.Left) != 0 || (thisSide & SnapToSide.Right) != 0)
                && (siblingsSide & SnapToSide.Left) == 0
                && (siblingsSide & SnapToSide.Right) == 0
                ) || (
                ((thisSide & SnapToSide.Top) != 0 || (thisSide & SnapToSide.Bottom) != 0)
                && (siblingsSide & SnapToSide.Top) == 0
                && (siblingsSide & SnapToSide.Bottom) == 0))
            {
                throw new ArgumentException(EXCMSG_INCOMPATIBLE_ENUMS);
            }

            if (thisSide == SnapToSide.Left && siblingsSide == SnapToSide.Left)         /**/c.Location = new Point(sibling.Location.X, c.Location.Y);
            else if (thisSide == SnapToSide.Left && siblingsSide == SnapToSide.Right)   /**/c.Location = new Point(sibling.Bounds.Right + (c.Margin.Left + sibling.Margin.Right), c.Location.Y);
            else if (thisSide == SnapToSide.Right && siblingsSide == SnapToSide.Left)   /**/c.Location = new Point(sibling.Location.X - c.Width - (c.Margin.Right + sibling.Margin.Left), c.Location.Y);
            else if (thisSide == SnapToSide.Right && siblingsSide == SnapToSide.Right)  /**/c.Location = new Point(sibling.Bounds.Right - c.Width, c.Location.Y);
            else if (thisSide == SnapToSide.Top && siblingsSide == SnapToSide.Top)      /**/c.Location = new Point(c.Location.X, sibling.Location.Y);
            else if (thisSide == SnapToSide.Top && siblingsSide == SnapToSide.Bottom)   /**/c.Location = new Point(c.Location.X, sibling.Bounds.Bottom + (c.Margin.Top + sibling.Margin.Bottom));
            else if (thisSide == SnapToSide.Bottom && siblingsSide == SnapToSide.Top)   /**/c.Location = new Point(c.Location.X, sibling.Location.Y - c.Height - (c.Margin.Bottom + sibling.Margin.Top));
            else if (thisSide == SnapToSide.Bottom && siblingsSide == SnapToSide.Bottom)/**/c.Location = new Point(c.Location.X, sibling.Bounds.Bottom - c.Height);
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
