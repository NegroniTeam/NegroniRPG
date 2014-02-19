//namespace NegroniGame.Screens
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Windows.Forms;
//    using System.Drawing.Image;
//    using System.Drawing;

//    public sealed class StartScreen
//    {
//        // Singleton !
//        private static StartScreen instance;

//        private StartScreen()
//        {
//            System.Windows.Forms.Form MyGameForm = (System.Windows.Forms.Form)System.Windows.Forms.Form.FromHandle(Window.Handle);
//            MyGameForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
//        }

//        public static StartScreen Instance
//        {
//            get
//            {
//                if (instance == null)
//                {
//                    instance = new StartScreen();
//                }
//                return instance;
//            }
//        }

//        public void Unknown(PaintEventArgs e)
//        {
//            Image newImage = Image.FromFile("menu1.png");

//            // Create parallelogram for drawing image.
//            Point ulCorner = new Point(100, 100);
//            Point urCorner = new Point(550, 100);
//            Point llCorner = new Point(150, 250);
//            Point[] destPara = { ulCorner, urCorner, llCorner };

//            // Draw image to screen.
//            e.Graphics.DrawImage(newImage, destPara);

//            if (true)
//            {
//                using (Screens.GameScreen game = Screens.GameScreen.Instance)
//                {
//                    game.Run();
//                }
//            }
//        }

//    }
//}
