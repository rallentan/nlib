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

        public static void SnapToParent(this Control c, SnapToSides parentSide)
        {
            if (c == null)
            {
                throw new ArgumentNullException(ARGNAME_C);
            }
            if (parentSide == 0 ||
                !Enum.IsDefined(typeof(SnapToSides), parentSide) ||
                (parentSide.HasFlag(SnapToSides.Left) && parentSide.HasFlag(SnapToSides.Right)) ||
                (parentSide.HasFlag(SnapToSides.Top) && parentSide.HasFlag(SnapToSides.Bottom)))
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

            if (parentSide.HasFlag(SnapToSides.Left))    /**/c.Location = new Point(parentBounds.X + c.Margin.Left + parentPadding.Left, c.Location.Y);
            if (parentSide.HasFlag(SnapToSides.Top))     /**/c.Location = new Point(c.Location.X, parentBounds.Y + c.Margin.Top + parentPadding.Top);
            if (parentSide.HasFlag(SnapToSides.Right))   /**/c.Location = new Point(parentBounds.Right - c.Width - c.Margin.Right - parentPadding.Right, c.Location.Y);
            if (parentSide.HasFlag(SnapToSides.Bottom))  /**/c.Location = new Point(c.Location.X, parentBounds.Bottom - c.Height - c.Margin.Bottom - parentPadding.Bottom);
        }
        
        public static void SnapToSibling(this Control c, SnapToSides thisSide, SnapToSides siblingsSide, Control sibling)
        {
            if (c == null)
            {
                throw new ArgumentNullException(ARGNAME_C);
            }
            if (thisSide == 0 ||
                !Enum.IsDefined(typeof(SnapToSides), thisSide) ||
                (thisSide == SnapToSides.Left && thisSide == SnapToSides.Right) ||
                (thisSide == SnapToSides.Top && thisSide == SnapToSides.Bottom))
            {
                throw new ArgumentOutOfRangeException(ARGNAME_THISSIDE, EXCMSG_INVALID_ENUM);
            }
            if (siblingsSide == 0 ||
                !Enum.IsDefined(typeof(SnapToSides), siblingsSide) ||
                (siblingsSide == SnapToSides.Left && siblingsSide == SnapToSides.Right) ||
                (siblingsSide == SnapToSides.Top && siblingsSide == SnapToSides.Bottom))
            {
                throw new ArgumentOutOfRangeException(ARGNAME_SIBLINGSSIDE, EXCMSG_INVALID_ENUM);
            }
            if ((((thisSide & SnapToSides.Left) != 0 || (thisSide & SnapToSides.Right) != 0)
                && (siblingsSide & SnapToSides.Left) == 0
                && (siblingsSide & SnapToSides.Right) == 0
                ) || (
                ((thisSide & SnapToSides.Top) != 0 || (thisSide & SnapToSides.Bottom) != 0)
                && (siblingsSide & SnapToSides.Top) == 0
                && (siblingsSide & SnapToSides.Bottom) == 0))
            {
                throw new ArgumentException(EXCMSG_INCOMPATIBLE_ENUMS);
            }

            if (thisSide == SnapToSides.Left && siblingsSide == SnapToSides.Left)         /**/c.Location = new Point(sibling.Location.X, c.Location.Y);
            else if (thisSide == SnapToSides.Left && siblingsSide == SnapToSides.Right)   /**/c.Location = new Point(sibling.Bounds.Right + (c.Margin.Left + sibling.Margin.Right), c.Location.Y);
            else if (thisSide == SnapToSides.Right && siblingsSide == SnapToSides.Left)   /**/c.Location = new Point(sibling.Location.X - c.Width - (c.Margin.Right + sibling.Margin.Left), c.Location.Y);
            else if (thisSide == SnapToSides.Right && siblingsSide == SnapToSides.Right)  /**/c.Location = new Point(sibling.Bounds.Right - c.Width, c.Location.Y);
            else if (thisSide == SnapToSides.Top && siblingsSide == SnapToSides.Top)      /**/c.Location = new Point(c.Location.X, sibling.Location.Y);
            else if (thisSide == SnapToSides.Top && siblingsSide == SnapToSides.Bottom)   /**/c.Location = new Point(c.Location.X, sibling.Bounds.Bottom + (c.Margin.Top + sibling.Margin.Bottom));
            else if (thisSide == SnapToSides.Bottom && siblingsSide == SnapToSides.Top)   /**/c.Location = new Point(c.Location.X, sibling.Location.Y - c.Height - (c.Margin.Bottom + sibling.Margin.Top));
            else if (thisSide == SnapToSides.Bottom && siblingsSide == SnapToSides.Bottom)/**/c.Location = new Point(c.Location.X, sibling.Bounds.Bottom - c.Height);
            else
            {
                throw new ArgumentException();
            }
        }

        public static void StretchToChild(this Control c, SnapToSides side, Control child)
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
                !Enum.IsDefined(typeof(SnapToSides), side) ||
                side.HasFlag(SnapToSides.Left) ||
                side.HasFlag(SnapToSides.Top))
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

            if (side.HasFlag(SnapToSides.Right))   /**/c.Size = new Size(child.Right + child.Margin.Right + padding.Right, c.Height);
            if (side.HasFlag(SnapToSides.Bottom))  /**/c.Size = new Size(c.Width, child.Bottom + child.Margin.Bottom + padding.Bottom);
        }

        public static void StretchToParent(this Control c, SnapToSides parentSide)
        {
            if (c == null)
            {
                throw new ArgumentNullException(ARGNAME_C);
            }
            if (parentSide == 0 ||
                !Enum.IsDefined(typeof(SnapToSides), parentSide) ||
                (parentSide.HasFlag(SnapToSides.Left) && parentSide.HasFlag(SnapToSides.Right)) ||
                (parentSide.HasFlag(SnapToSides.Top) && parentSide.HasFlag(SnapToSides.Bottom)))
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

            int newX;
            int newY;
            int newWidth;
            int newHeight;

            if (parentSide.HasFlag(SnapToSides.Left))
            {
                newX = parentBounds.X + c.Margin.Left + parentPadding.Left;
                newWidth = c.Width + c.Left - newX;
            }
            else if (parentSide.HasFlag(SnapToSides.Right))
            {
                newX = c.Left;
                newWidth = parentBounds.Width - c.Left;
            }
            else
            {
                newX = c.Left;
                newWidth = c.Width;
            }

            if (parentSide.HasFlag(SnapToSides.Top))
            {
                newY = parentBounds.Y + c.Margin.Top + parentPadding.Top;
                newHeight = c.Height + c.Top - newY;
            }
            else if (parentSide.HasFlag(SnapToSides.Bottom))
            {
                newY = c.Top;
                newHeight = parentBounds.Height - c.Top;
            }
            else
            {
                newY = c.Top;
                newHeight = c.Height;
            }

            c.SetBounds(newX, newY, newWidth, newHeight);
        }

        public static void StretchToSibling(this Control c, SnapToSides thisSide, SnapToSides siblingsSide, Control sibling)
        {
            if (c == null)
            {
                throw new ArgumentNullException(ARGNAME_C);
            }
            if (thisSide == 0 ||
                !Enum.IsDefined(typeof(SnapToSides), thisSide) ||
                (thisSide == SnapToSides.Left && thisSide == SnapToSides.Right) ||
                (thisSide == SnapToSides.Top && thisSide == SnapToSides.Bottom))
            {
                throw new ArgumentOutOfRangeException(ARGNAME_THISSIDE, EXCMSG_INVALID_ENUM);
            }
            if (siblingsSide == 0 ||
                !Enum.IsDefined(typeof(SnapToSides), siblingsSide) ||
                (siblingsSide == SnapToSides.Left && siblingsSide == SnapToSides.Right) ||
                (siblingsSide == SnapToSides.Top && siblingsSide == SnapToSides.Bottom))
            {
                throw new ArgumentOutOfRangeException(ARGNAME_SIBLINGSSIDE, EXCMSG_INVALID_ENUM);
            }
            if ((((thisSide & SnapToSides.Left) != 0 || (thisSide & SnapToSides.Right) != 0)
                && (siblingsSide & SnapToSides.Left) == 0
                && (siblingsSide & SnapToSides.Right) == 0
                ) || (
                ((thisSide & SnapToSides.Top) != 0 || (thisSide & SnapToSides.Bottom) != 0)
                && (siblingsSide & SnapToSides.Top) == 0
                && (siblingsSide & SnapToSides.Bottom) == 0))
            {
                throw new ArgumentException(EXCMSG_INCOMPATIBLE_ENUMS);
            }

            int newX = c.Left;
            int newY = c.Top;
            int newWidth = c.Width;
            int newHeight = c.Height;
            
            if (thisSide == SnapToSides.Left && siblingsSide == SnapToSides.Left)
            {
                newX = sibling.Left;
                newWidth = c.Width + c.Left - newX;
            }
            else if (thisSide == SnapToSides.Left && siblingsSide == SnapToSides.Right)
            {
                newX = sibling.Left + sibling.Margin.Right + c.Margin.Left;
                newWidth = c.Width + c.Left - newX;
            }
            else if (thisSide == SnapToSides.Right && siblingsSide == SnapToSides.Left)
            {
                newWidth = c.Width + sibling.Left - sibling.Margin.Left - c.Margin.Right - c.Right;
            }
            else if (thisSide == SnapToSides.Right && siblingsSide == SnapToSides.Right)
            {
                newWidth = c.Width + sibling.Right - c.Right;
            }
            else if (thisSide == SnapToSides.Top && siblingsSide == SnapToSides.Top)
            {
                newY = sibling.Top;
                newHeight = c.Height + c.Top - newY;
            }
            else if (thisSide == SnapToSides.Top && siblingsSide == SnapToSides.Bottom)
            {
                newY = sibling.Bottom + sibling.Margin.Bottom + c.Margin.Top;
                newHeight = c.Height + c.Top - newY;
            }
            else if (thisSide == SnapToSides.Bottom && siblingsSide == SnapToSides.Top)
            {
                newHeight = c.Height + sibling.Top - sibling.Margin.Top - c.Margin.Bottom - c.Bottom;
            }
            else if (thisSide == SnapToSides.Bottom && siblingsSide == SnapToSides.Bottom)
            {
                newHeight = c.Height + sibling.Bottom - c.Bottom;
            }
            else
            {
                throw new ArgumentException();
            }

            c.SetBounds(newX, newY, newWidth, newHeight);
        }
    }
}
