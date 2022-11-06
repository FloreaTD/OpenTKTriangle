using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

// EXERCITIU 5
namespace Florea
{

    internal class fereastra : GameWindow
    {

        bool showTriangle = true;
        KeyboardState lastKeyPress;
        private const int XYZ_SIZE = 100;
       
        private Color color1 = Color.Red, color2 = Color.Green, color3 = Color.Violet;

  

        private fereastra() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.DepthTest);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
            Console.WriteLine("\nApasati A, S, D sau Q, W, E pentru a schimba culorile!");
            Console.WriteLine("\nCuloarea Rosu pentru primul vertex!");
            Console.WriteLine("\nCuloarea Verde pentru al doilea vertex!");
            Console.WriteLine("\nCuloarea Violet pentru al treilea vertex!");
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            Matrix4 lookat = Matrix4.LookAt(30, 30, 30, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            showTriangle = true;
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            if (keyboard[Key.Escape])
            {
                Exit();
                return;
            }
            else if(keyboard[Key.A])
            {
                color1 = Color.White;
                Console.WriteLine("\n Schimb ALB pentru primul vertex!");
            }
            else if(keyboard[Key.S])
            {
                color2 = Color.Aqua;
                Console.WriteLine("\nSchimb AQUA pentru al doilea vertex!");
            }
            else if(keyboard[Key.D])
            {
                color3 = Color.Green;
                Console.WriteLine("\nSchimb VERDE pentru al doilea vertex!");
            }
            else if(keyboard[Key.Q])
            {
                color1 = color2 = color3 = Color.Red;
                Console.WriteLine("\nSchimb ROSU pentru toate vertexurile!");
            }
            else if (keyboard[Key.W])
            {
                color1 = color2 = color3 = Color.Green;
                Console.WriteLine("\nSchimb VERDE pentru toate vertexurile!");
            }
            else if (keyboard[Key.E])
            {
                color1 = color2 = color3 = Color.Blue;
                Console.WriteLine("\nSchimb ALBASTRU pentru toate vertexurile!");
            }
            lastKeyPress = keyboard;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            DrawTriangle();
            DrawAxes();
            

            if (showTriangle == true)
            {
                GL.PushMatrix();
                DrawTriangle();
                GL.PopMatrix();
            }
            SwapBuffers();
        }

        // EXERCITIU 1

        private void DrawAxes()
        {
            //ROSU
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(XYZ_SIZE, 0, 0);

            //GALBEN
            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, XYZ_SIZE, 0); ;

            //VERDE
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, XYZ_SIZE);
            GL.End();
        }

        // EXERCITIU 8 SI 9
        private void DrawTriangle()
        {
            GL.Begin(PrimitiveType.Triangles);
            
               GL.Color4(color1);
               GL.Vertex3(10f, -5f, -5f);
               GL.Color3(color2);
               GL.Vertex3(-5f, 11f, -5f);
               GL.Color3(color3);
               GL.Vertex3(-5f, -5f, 12f);

            GL.End();
        }

        [STAThread]
        static void Main(string[] args)
        {

            using (fereastra example = new fereastra())
            {
                example.Run(30.0, 0.0);
            }

        }
    }

}