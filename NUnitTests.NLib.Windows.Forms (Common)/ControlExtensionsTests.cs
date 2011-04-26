using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Windows.Forms;
using System.Drawing;
using NLib.Windows.Forms;

namespace NUnitTests.NLib.Windows.Forms
{
    [TestFixture]
    public class ControlExtensionsTests
    {
        //--- Constants ---

        const int CTRL_1_X = 1;                     // _testControl1.Location.X
        const int CTRL_1_Y = 2;                     // _testControl1.Location.Y
        const int CTRL_1_W = 230;                    // _testControl1.Width
        const int CTRL_1_H = 231;                    // _testControl1.Height
        const int CTRL_1_R = CTRL_1_X + CTRL_1_W;   // _testControl1.Bounds.Right
        const int CTRL_1_B = CTRL_1_Y + CTRL_1_H;   // _testControl1.Bounds.Bottom
        const int CTRL_1_ML = 3;                    // _testControl1.Margin.Left
        const int CTRL_1_MT = 4;                    // _testControl1.Margin.Top
        const int CTRL_1_MR = 5;                    // _testControl1.Margin.Right
        const int CTRL_1_MB = 6;                    // _testControl1.Margin.Bottom
        const int CTRL_2_X = 103;                   // _testControl2.Location.X
        const int CTRL_2_Y = 104;                   // _testControl2.Location.Y
        const int CTRL_2_W = 32;                    // _testControl2.Width
        const int CTRL_2_H = 33;                    // _testControl2.Height
        const int CTRL_2_R = CTRL_2_X + CTRL_2_W;   // _testControl2.Bounds.Right
        const int CTRL_2_B = CTRL_2_Y + CTRL_2_H;   // _testControl2.Bounds.Bottom
        const int CTRL_2_ML = 7;                    // _testControl2.Margin.Left
        const int CTRL_2_MT = 8;                    // _testControl2.Margin.Top
        const int CTRL_2_MR = 9;                    // _testControl2.Margin.Right
        const int CTRL_2_MB = 10;                   // _testControl2.Margin.Bottom
        const int FORM_1_W = 200;
        const int FORM_1_H = 201;
        const int FORM_2_W = 202;
        const int FORM_2_H = 203;
        const int FORM_1_PL = 0;
        const int FORM_1_PT = 1;
        const int FORM_1_PR = 2;
        const int FORM_1_PB = 3;


        //--- Fields ---

        Form _testForm1 = new Form();  // Explicitly padded
        Form _testForm2 = new Form();  // Null padding (defaults padding to 9,9,9,9)
        Control _testControl1 = new Control();
        Control _testControl2 = new Control();


        //--- Constructors ---

        public ControlExtensionsTests()
        {
            SetUpForms();
            _testForm1.MinimumSize = new Size(0, 0);
            _testForm1.Padding = new Padding(FORM_1_PL, FORM_1_PT, FORM_1_PR, FORM_1_PB);
            _testForm2.MinimumSize = new Size(0, 0);
            _testForm2.Padding = Padding.Empty;

            _testControl1.Padding = Padding.Empty;
            _testControl1.Controls.Add(_testControl2);
        }


        //--- Public Methods ---

        public void SetUpForms()
        {
            SetUpForm1();
            SetUpForm2();
        }

        public void SetUpForm1()
        {
            _testForm1.Size = new Size(FORM_1_W, FORM_1_H);
        }

        public void SetUpForm2()
        {
            _testForm2.Size = new Size(FORM_2_W, FORM_2_H);
        }

        public void SetUpControls()
        {
            SetUpControl1();
            SetUpControl2();
        }

        public void SetUpControl1()
        {
            _testControl1.Location = new Point(CTRL_1_X, CTRL_1_Y);
            _testControl1.Size = new Size(CTRL_1_W, CTRL_1_H);
            _testControl1.Margin = new Padding(CTRL_1_ML, CTRL_1_MT, CTRL_1_MR, CTRL_1_MB);
        }

        public void SetUpControl2()
        {
            _testControl2.Location = new Point(CTRL_2_X, CTRL_2_Y);
            _testControl2.Size = new Size(CTRL_2_W, CTRL_2_H);
            _testControl2.Margin = new Padding(CTRL_2_ML, CTRL_2_MT, CTRL_2_MR, CTRL_2_MB);
        }

        [Test]
        public void GetMarginRectangle()
        {
            Control control = new Control();
            control.Location = new Point(1, 2);
            control.Size = new Size(3, 4);
            control.Margin = new Padding(8, 7, 6, 5);

            var marginRectangle = control.GetMarginRectangle();
            Assert.AreEqual(-7, marginRectangle.X);
            Assert.AreEqual(-5, marginRectangle.Y);
            Assert.AreEqual(9, marginRectangle.Width);
            Assert.AreEqual(9, marginRectangle.Height);

            control.Dispose();
            marginRectangle = control.GetMarginRectangle();
            Assert.AreEqual(-7, marginRectangle.X);
            Assert.AreEqual(-5, marginRectangle.Y);
            Assert.AreEqual(9, marginRectangle.Width);
            Assert.AreEqual(9, marginRectangle.Height);
        }

        [Test]
        public void SnapToChild()
        {
            // Add Control1 to Form1 for Explicitly Padded Form Testing
            _testForm1.Controls.Add(_testControl1);

            Assert.Throws<ArgumentNullException>(() => { ControlExtensions.StretchToChild(null, SnapToSides.Right, _testControl1); });
            Assert.Throws<ArgumentNullException>(() => { _testForm1.StretchToChild(SnapToSides.Right, null); });
            Assert.Throws<ArgumentOutOfRangeException>(() => { _testForm1.StretchToChild(0, _testControl1); });
            Assert.Throws<ArgumentOutOfRangeException>(() => { _testForm1.StretchToChild((SnapToSides)(-1), _testControl1); });
            Assert.Throws<ArgumentOutOfRangeException>(() => { _testForm1.StretchToChild(SnapToSides.Top | SnapToSides.Right, _testControl1); });

            // Form1 Left
            SetUpForm1();
            SetUpControl1();
            Assert.Throws<ArgumentOutOfRangeException>(() => { _testForm1.StretchToChild(SnapToSides.Left, _testControl1); });
            Assert.AreEqual(FORM_1_W, _testForm1.Size.Width);
            Assert.AreEqual(FORM_1_H, _testForm1.Size.Height);

            // Form1 Top
            SetUpForm1();
            SetUpControl1();
            Assert.Throws<ArgumentOutOfRangeException>(() => { _testForm1.StretchToChild(SnapToSides.Top, _testControl1); });
            Assert.AreEqual(FORM_1_W, _testForm1.Size.Width);
            Assert.AreEqual(FORM_1_H, _testForm1.Size.Height);

            // Form1 Right
            SetUpForm1();
            SetUpControl1();
            _testForm1.StretchToChild(SnapToSides.Right, _testControl1);
            Assert.AreEqual(CTRL_1_R + CTRL_1_MR + FORM_1_PR, _testForm1.Size.Width);
            Assert.AreEqual(FORM_1_H, _testForm1.Size.Height);

            // Form1 Bottom
            SetUpForm1();
            SetUpControl1();
            _testForm1.StretchToChild(SnapToSides.Bottom, _testControl1);
            Assert.AreEqual(FORM_1_W, _testForm1.Size.Width);
            Assert.AreEqual(CTRL_1_B + CTRL_1_MB + FORM_1_PB, _testForm1.Size.Height);


            // Add Control1 to Form2 for Null Padding Testing
            _testForm1.Controls.Add(_testControl1);

            // Form2 Right
            SetUpForm2();
            SetUpControl1();
            _testForm2.StretchToChild(SnapToSides.Right, _testControl1);
            Assert.AreEqual(CTRL_1_R + CTRL_1_MR + 9, _testForm2.Size.Width);
            Assert.AreEqual(FORM_2_H, _testForm2.Size.Height);

            // Form2 Bottom
            SetUpForm2();
            SetUpControl1();
            _testForm2.StretchToChild(SnapToSides.Bottom, _testControl1);
            Assert.AreEqual(FORM_2_W, _testForm2.Size.Width);
            Assert.AreEqual(CTRL_1_B + CTRL_1_MB + 9, _testForm2.Size.Height);


            // Control1 Right
            SetUpControls();
            _testControl1.StretchToChild(SnapToSides.Right, _testControl2);
            Assert.AreEqual(CTRL_2_R + CTRL_2_MR, _testControl1.Bounds.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);

            // Control1 Bottom
            SetUpControls();
            _testControl1.StretchToChild(SnapToSides.Bottom, _testControl2);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_2_B + CTRL_2_MB, _testControl1.Bounds.Height);
        }

        [Test]
        public void SnapToParent()
        {
            SetUpForms();

            // Add Control1 to Form1 for Explicitly Padded Form Testing
            _testForm1.Controls.Add(_testControl1);

            // Form1 Left
            SetUpControl1();
            _testControl1.SnapToParent(SnapToSides.Left);
            Assert.AreEqual(FORM_1_PL + CTRL_1_ML, _testControl1.Location.X);
            Assert.AreEqual(CTRL_1_Y, _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);

            // Form1 Top
            SetUpControl1();
            _testControl1.SnapToParent(SnapToSides.Top);
            Assert.AreEqual(CTRL_1_X, _testControl1.Location.X);
            Assert.AreEqual(FORM_1_PT + CTRL_1_MT, _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);

            // Form1 Right
            SetUpControl1();
            _testControl1.SnapToParent(SnapToSides.Right);
            Assert.AreEqual(FORM_1_W - CTRL_1_W - (FORM_1_PR + CTRL_1_MR), _testControl1.Location.X);
            Assert.AreEqual(CTRL_1_Y, _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);

            // Form1 Bottom
            SetUpControl1();
            _testControl1.SnapToParent(SnapToSides.Bottom);
            Assert.AreEqual(CTRL_1_X, _testControl1.Location.X);
            Assert.AreEqual(FORM_1_H - CTRL_1_H - (FORM_1_PB + CTRL_1_MB), _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);

            
            // Add Control1 to Form2 for Null Padding Testing
            _testForm2.Controls.Add(_testControl1);
            
            // Form2 Left
            SetUpControl1();
            _testControl1.SnapToParent(SnapToSides.Left);
            Assert.AreEqual(9 + CTRL_1_ML, _testControl1.Location.X);
            Assert.AreEqual(CTRL_1_Y, _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);

            // Form2 Top
            SetUpControl1();
            _testControl1.SnapToParent(SnapToSides.Top);
            Assert.AreEqual(CTRL_1_X, _testControl1.Location.X);
            Assert.AreEqual(9 + CTRL_1_MT, _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);

            // Form2 Right
            SetUpControl1();
            _testControl1.SnapToParent(SnapToSides.Right);
            Assert.AreEqual(FORM_2_W - CTRL_1_W - (9 + CTRL_1_MR), _testControl1.Location.X);
            Assert.AreEqual(CTRL_1_Y, _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);

            // Form2 Bottom
            SetUpControl1();
            _testControl1.SnapToParent(SnapToSides.Bottom);
            Assert.AreEqual(CTRL_1_X, _testControl1.Location.X);
            Assert.AreEqual(FORM_2_H - CTRL_1_H - (9 + CTRL_1_MB), _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);

            // Control1 Left
            SetUpControls();
            _testControl2.SnapToParent(SnapToSides.Left);
            Assert.AreEqual(CTRL_1_X + CTRL_2_ML, _testControl2.Location.X);
            Assert.AreEqual(CTRL_2_Y, _testControl2.Location.Y);
            Assert.AreEqual(CTRL_2_W, _testControl2.Size.Width);
            Assert.AreEqual(CTRL_2_H, _testControl2.Size.Height);

            // Control1 Top
            SetUpControls();
            _testControl2.SnapToParent(SnapToSides.Top);
            Assert.AreEqual(CTRL_2_X, _testControl2.Location.X);
            Assert.AreEqual(CTRL_1_Y + CTRL_2_MT, _testControl2.Location.Y);
            Assert.AreEqual(CTRL_2_W, _testControl2.Size.Width);
            Assert.AreEqual(CTRL_2_H, _testControl2.Size.Height);

            // Control1 Right
            SetUpControls();
            _testControl2.SnapToParent(SnapToSides.Right);
            Assert.AreEqual(CTRL_1_R - CTRL_2_W - CTRL_2_MR, _testControl2.Location.X);
            Assert.AreEqual(CTRL_2_Y, _testControl2.Location.Y);
            Assert.AreEqual(CTRL_2_W, _testControl2.Size.Width);
            Assert.AreEqual(CTRL_2_H, _testControl2.Size.Height);

            // Control1 Bottom
            SetUpControls();
            _testControl2.SnapToParent(SnapToSides.Bottom);
            Assert.AreEqual(CTRL_2_X, _testControl2.Location.X);
            Assert.AreEqual(CTRL_1_B - CTRL_2_H - CTRL_2_MB, _testControl2.Location.Y);
            Assert.AreEqual(CTRL_2_W, _testControl2.Size.Width);
            Assert.AreEqual(CTRL_2_H, _testControl2.Size.Height);
        }

        [Test]
        public void SnapToSibling()
        {
            // Top to Top
            SetUpControls();
            _testControl2.SnapToSibling(SnapToSides.Top, SnapToSides.Top, _testControl1);
            Assert.AreEqual(CTRL_1_X, _testControl1.Location.X);
            Assert.AreEqual(CTRL_1_Y, _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);
            Assert.AreEqual(CTRL_2_X, _testControl2.Location.X);
            Assert.AreEqual(CTRL_1_Y, _testControl2.Location.Y);
            Assert.AreEqual(CTRL_2_W, _testControl2.Size.Width);
            Assert.AreEqual(CTRL_2_H, _testControl2.Size.Height);

            SetUpControls();
            _testControl1.SnapToSibling(SnapToSides.Top, SnapToSides.Top, _testControl2);
            Assert.AreEqual(CTRL_1_X, _testControl1.Location.X);
            Assert.AreEqual(CTRL_2_Y, _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);
            Assert.AreEqual(CTRL_2_X, _testControl2.Location.X);
            Assert.AreEqual(CTRL_2_Y, _testControl2.Location.Y);
            Assert.AreEqual(CTRL_2_W, _testControl2.Size.Width);
            Assert.AreEqual(CTRL_2_H, _testControl2.Size.Height);

            // Bottom to Bottom
            SetUpControls();
            _testControl2.SnapToSibling(SnapToSides.Bottom, SnapToSides.Bottom, _testControl1);
            Assert.AreEqual(CTRL_1_X, _testControl1.Location.X);
            Assert.AreEqual(CTRL_1_Y, _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);
            Assert.AreEqual(CTRL_2_X, _testControl2.Location.X);
            Assert.AreEqual(CTRL_1_B - CTRL_2_H, _testControl2.Location.Y);
            Assert.AreEqual(CTRL_2_W, _testControl2.Size.Width);
            Assert.AreEqual(CTRL_2_H, _testControl2.Size.Height);

            SetUpControls();
            _testControl1.SnapToSibling(SnapToSides.Bottom, SnapToSides.Bottom, _testControl2);
            Assert.AreEqual(CTRL_1_X, _testControl1.Location.X);
            Assert.AreEqual(CTRL_2_B - CTRL_1_H, _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);
            Assert.AreEqual(CTRL_2_X, _testControl2.Location.X);
            Assert.AreEqual(CTRL_2_Y, _testControl2.Location.Y);
            Assert.AreEqual(CTRL_2_W, _testControl2.Size.Width);
            Assert.AreEqual(CTRL_2_H, _testControl2.Size.Height);

            // Top to Bottom
            SetUpControls();
            _testControl2.SnapToSibling(SnapToSides.Top, SnapToSides.Bottom, _testControl1);
            Assert.AreEqual(CTRL_1_X, _testControl1.Location.X);
            Assert.AreEqual(CTRL_1_Y, _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);
            Assert.AreEqual(CTRL_2_X, _testControl2.Location.X);
            Assert.AreEqual(CTRL_1_B + (CTRL_1_MB + CTRL_2_MT), _testControl2.Location.Y);
            Assert.AreEqual(CTRL_2_W, _testControl2.Size.Width);
            Assert.AreEqual(CTRL_2_H, _testControl2.Size.Height);

            SetUpControls();
            _testControl1.SnapToSibling(SnapToSides.Top, SnapToSides.Bottom, _testControl2);
            Assert.AreEqual(CTRL_1_X, _testControl1.Location.X);
            Assert.AreEqual(CTRL_2_B + (CTRL_2_MB + CTRL_1_MT), _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);
            Assert.AreEqual(CTRL_2_X, _testControl2.Location.X);
            Assert.AreEqual(CTRL_2_Y, _testControl2.Location.Y);
            Assert.AreEqual(CTRL_2_W, _testControl2.Size.Width);
            Assert.AreEqual(CTRL_2_H, _testControl2.Size.Height);

            // Bottom to Top
            SetUpControls();
            _testControl2.SnapToSibling(SnapToSides.Bottom, SnapToSides.Top, _testControl1);
            Assert.AreEqual(CTRL_1_X, _testControl1.Location.X);
            Assert.AreEqual(CTRL_1_Y, _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);
            Assert.AreEqual(CTRL_2_X, _testControl2.Location.X);
            Assert.AreEqual(CTRL_1_Y - CTRL_2_H - (CTRL_2_MB + CTRL_1_MT), _testControl2.Location.Y);
            Assert.AreEqual(CTRL_2_W, _testControl2.Size.Width);
            Assert.AreEqual(CTRL_2_H, _testControl2.Size.Height);

            SetUpControls();
            _testControl1.SnapToSibling(SnapToSides.Bottom, SnapToSides.Top, _testControl2);
            Assert.AreEqual(CTRL_1_X, _testControl1.Location.X);
            Assert.AreEqual(CTRL_2_Y - CTRL_1_H - (CTRL_1_MB + CTRL_2_MT), _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);
            Assert.AreEqual(CTRL_2_X, _testControl2.Location.X);
            Assert.AreEqual(CTRL_2_Y, _testControl2.Location.Y);
            Assert.AreEqual(CTRL_2_W, _testControl2.Size.Width);
            Assert.AreEqual(CTRL_2_H, _testControl2.Size.Height);

            // Left to Left
            SetUpControls();
            _testControl2.SnapToSibling(SnapToSides.Left, SnapToSides.Left, _testControl1);
            Assert.AreEqual(CTRL_1_X, _testControl1.Location.X);
            Assert.AreEqual(CTRL_1_Y, _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);
            Assert.AreEqual(CTRL_1_X, _testControl2.Location.X);
            Assert.AreEqual(CTRL_2_Y, _testControl2.Location.Y);
            Assert.AreEqual(CTRL_2_W, _testControl2.Size.Width);
            Assert.AreEqual(CTRL_2_H, _testControl2.Size.Height);

            SetUpControls();
            _testControl1.SnapToSibling(SnapToSides.Left, SnapToSides.Left, _testControl2);
            Assert.AreEqual(CTRL_2_X, _testControl1.Location.X);
            Assert.AreEqual(CTRL_1_Y, _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);
            Assert.AreEqual(CTRL_2_X, _testControl2.Location.X);
            Assert.AreEqual(CTRL_2_Y, _testControl2.Location.Y);
            Assert.AreEqual(CTRL_2_W, _testControl2.Size.Width);
            Assert.AreEqual(CTRL_2_H, _testControl2.Size.Height);

            // Right to Right
            SetUpControls();
            _testControl2.SnapToSibling(SnapToSides.Right, SnapToSides.Right, _testControl1);
            Assert.AreEqual(CTRL_1_X, _testControl1.Location.X);
            Assert.AreEqual(CTRL_1_Y, _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);
            Assert.AreEqual(CTRL_1_R - CTRL_2_W, _testControl2.Location.X);
            Assert.AreEqual(CTRL_2_Y, _testControl2.Location.Y);
            Assert.AreEqual(CTRL_2_W, _testControl2.Size.Width);
            Assert.AreEqual(CTRL_2_H, _testControl2.Size.Height);

            SetUpControls();
            _testControl1.SnapToSibling(SnapToSides.Right, SnapToSides.Right, _testControl2);
            Assert.AreEqual(CTRL_2_R - CTRL_1_W, _testControl1.Location.X);
            Assert.AreEqual(CTRL_1_Y, _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);
            Assert.AreEqual(CTRL_2_X, _testControl2.Location.X);
            Assert.AreEqual(CTRL_2_Y, _testControl2.Location.Y);
            Assert.AreEqual(CTRL_2_W, _testControl2.Size.Width);
            Assert.AreEqual(CTRL_2_H, _testControl2.Size.Height);

            // Left to Right
            SetUpControls();
            _testControl2.SnapToSibling(SnapToSides.Left, SnapToSides.Right, _testControl1);
            Assert.AreEqual(CTRL_1_X, _testControl1.Location.X);
            Assert.AreEqual(CTRL_1_Y, _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);
            Assert.AreEqual(CTRL_1_R + (CTRL_2_ML + CTRL_1_MR), _testControl2.Location.X);
            Assert.AreEqual(CTRL_2_Y, _testControl2.Location.Y);
            Assert.AreEqual(CTRL_2_W, _testControl2.Size.Width);
            Assert.AreEqual(CTRL_2_H, _testControl2.Size.Height);

            SetUpControls();
            _testControl1.SnapToSibling(SnapToSides.Left, SnapToSides.Right, _testControl2);
            Assert.AreEqual(CTRL_2_R + (CTRL_1_ML + CTRL_2_MR), _testControl1.Location.X);
            Assert.AreEqual(CTRL_1_Y, _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);
            Assert.AreEqual(CTRL_2_X, _testControl2.Location.X);
            Assert.AreEqual(CTRL_2_Y, _testControl2.Location.Y);
            Assert.AreEqual(CTRL_2_W, _testControl2.Size.Width);
            Assert.AreEqual(CTRL_2_H, _testControl2.Size.Height);

            // Right to Left
            SetUpControls();
            _testControl2.SnapToSibling(SnapToSides.Right, SnapToSides.Left, _testControl1);
            Assert.AreEqual(CTRL_1_X, _testControl1.Location.X);
            Assert.AreEqual(CTRL_1_Y, _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);
            Assert.AreEqual(CTRL_1_X - CTRL_2_W - (CTRL_2_MR + CTRL_1_ML), _testControl2.Location.X);
            Assert.AreEqual(CTRL_2_Y, _testControl2.Location.Y);
            Assert.AreEqual(CTRL_2_W, _testControl2.Size.Width);
            Assert.AreEqual(CTRL_2_H, _testControl2.Size.Height);

            SetUpControls();
            _testControl1.SnapToSibling(SnapToSides.Right, SnapToSides.Left, _testControl2);
            Assert.AreEqual(CTRL_2_X - CTRL_1_W - (CTRL_1_MR + CTRL_2_ML), _testControl1.Location.X);
            Assert.AreEqual(CTRL_1_Y, _testControl1.Location.Y);
            Assert.AreEqual(CTRL_1_W, _testControl1.Size.Width);
            Assert.AreEqual(CTRL_1_H, _testControl1.Size.Height);
            Assert.AreEqual(CTRL_2_X, _testControl2.Location.X);
            Assert.AreEqual(CTRL_2_Y, _testControl2.Location.Y);
            Assert.AreEqual(CTRL_2_W, _testControl2.Size.Width);
            Assert.AreEqual(CTRL_2_H, _testControl2.Size.Height);
        }
    }
}
