using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

using Bareliza.Ekran.Drawable.Relative;

public class Program : Form {
  
  private int r1 = 0, g1 = 0, b1 = 0;
  private void Window_MouseMove(object sender, MouseEventArgs e)
  {
    /*
    Console.WriteLine(
      $"Mouse moved: e.Button: {e.Button} e.X: {e.X} e.Y: {e.Y}"
    );
    */

    /* */
    if(vScrollBar1.Value >= 90) {
      int i, j;

      bool Gladkie = true;
      bool GladkieSzybkie = true;
 
      for(i = 0; i < 800; i+= GladkieSzybkie ? 400 : 80)
	for(j = 0; j < 800; j += GladkieSzybkie ? 366 : 160) 
	  if( Gladkie ) g.Line(i, j, e.X, e.Y, Color.FromArgb(r1, g1 ,b1), 0);
	  else          g.Line(i, j, e.X, e.Y, Color.FromArgb(r1, g1 ,b1));
      //g.Circle(e.X, e.Y, 20, Color.Blue);
      r1 += 20;   g1 += 10;   b1 += 5;
      // r &= 0xff; g &= 0xff; b &= 0xff;
      if (r1 > 0xff) r1 = 0;
      if (g1 > 0xff) g1 = 0;
      if (b1 > 0xff) b1 = 0;
    
      box.Invalidate();
    }
    /* */
  }

  private Grafika g;
  private PictureBox box = null;
  private VScrollBar vScrollBar1;

  public void viewPortRepaint()
  {
    int t, v;
    double u; // , w;

    //    g.FilledRectangle(0,   0, 1199, 799,Color.FromArgb(0xff, 0x10, 0xff, 0x10));
    //    g.FilledRectangle(0,   0, 1599, 859,Color.White);
    //    g.FilledRectangle(0,   0,  799, 700,Color.Magenta);
    /// g.FilledRectangle(0,   0,  600, 600,Color.Black);
    //    g.FilledRectangle(40, 40,  560, 560,Color.Yellow);
    // Console.WriteLine(g.ToString());
    g.Rectangle(20, 20, 380, 380, Color.White);
    // Console.WriteLine(g.ToString());
    g.FilledCircle(40, 40, 30, Color.FromArgb(0xff, 0x6f, 0x00, 0xff));
    // Console.WriteLine(g.ToString());
    g.Rectangle(80, 80, 40, 40, Color.White);
    // Console.WriteLine(g);
    g.Rectangle(0,   0, 1600,  850, Color.Red);

    double SPEEDUP = 1; // 0.25;
    
    ///    for(t = 36; t > 0; t -= 25)
    ///  g.FilledCircle(900, 550, 150 + 3*t, Color.FromArgb(0xff, 2*t, 255-2*t, 255-2*t));

    // Console.WriteLine(g);
    // 2, 4, 32, 64
    int glR, glG, glB; // goldLevel{Red,Green,Blue}
    for(t = 0; t < 8; ++t) {
      v = 1 << t;
      for(u = 43; u < 47; u += SPEEDUP) { // 17
	// 172 120  45 ...
	// 249 229 132 ...
	glR = 172 + 16 - 16;
	glG = 120; // glR - 19;
	glB = 45; // 135;
	if( 0 != (v & (2 | 4)) ) {
	  g.CircleDbl(950.0 - 280.0, 550.0, 350.0  + u,
		      Color.FromArgb(0xff, glR, glG, glB), (1 << (t & 7))
		      );
	  g.CircleDbl(951.0 - 280.0, 550.0, 350.0 + u,
		      Color.FromArgb(0xff, glR, glG, glB), (1 << (t & 7))
		      );
	}
	if( 0 != (v & (32 | 64)) ) {
	  g.CircleDbl(950.0 + 280.0, 550.0, 350.0 + u,
		      Color.FromArgb(0xff, glR, glG, glB), (1 << (t & 7))
		      );
	  g.CircleDbl(951.0 + 280.0, 550.0, 350.0 + u,
		      Color.FromArgb(0xff, glR, glG, glB), (1 << (t & 7))
		      );
	}
      }
    }

    // var id = 0.0;

    // ...Grafika.Testing.SmoothLinesTest(...) :
    var degree10 = 10.0*Math.PI*2/360;
    for(var thick = 0; thick < 6; thick += 2)
      for(var id = 0.0; id < Math.PI * 2; id += 5*degree10) {
	//	var id = 280.0*Math.PI*2/360;
	g.Line(
	       230 + thick * 190, 
	       850, 
	       230 + thick * 190 + (int)(100*Math.Sin(id)), 
	       850               + (int)(100*Math.Cos(id)), 
	       Color.Green, thick
	       );

	g.Line(
	       230 + thick * 190 + (int)( 44*Math.Sin(id)), 
	       550               + (int)( 44*Math.Cos(id)),  
	       230 + thick * 190 + (int)(100*Math.Sin(id)), 
	       550               + (int)(100*Math.Cos(id)), 
	       Color.Green, thick
	       );

      }
    //	}
  }
  
  public Program ()
  {
    // PictureBox box = null;
    this.MouseMove += Window_MouseMove;
    box = new PictureBox ();
    box.SizeMode = PictureBoxSizeMode.AutoSize;
    box.MouseMove += Window_MouseMove;

    PixelFormat pf;
    pf = PixelFormat.Format32bppArgb;
    // Grafika g;
    g = new Grafika(1650, 900, pf, 100, 200, 0.663);
    Image i = g._b;

    box.Image = i;
    box.Left = 10;
    box.Top = 10;
    // Console.WriteLine(g);

    vScrollBar1 = new VScrollBar();
    vScrollBar1.Dock = DockStyle.Right;
    vScrollBar1.ValueChanged +=
      new EventHandler(this.vScrollBar1_ValueChanged);

    viewPortRepaint();

    Controls.Add(box);
    Controls.Add(vScrollBar1);
  }

  private void vScrollBar1_ValueChanged(object sender, EventArgs e) {
    Console.WriteLine($"VScrollBar1.Value: {vScrollBar1.Value}");
    g.OriginX = 2*vScrollBar1.Value;
    g.OriginY = 4*vScrollBar1.Value;
    viewPortRepaint();
    box.Invalidate();
  }

  public static void Main()
  {
    Console.WriteLine("Halo, Tu Kanna Galilejska!");
    System.Threading.Thread.CurrentThread.Name = "Main Thread";
    // MessageBox.Show("Tiru Riru");
    Program p = new Program ();
    Application.Run(p);
  }
}
