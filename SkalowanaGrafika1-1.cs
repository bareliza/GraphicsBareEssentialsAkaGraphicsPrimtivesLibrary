using System.Drawing;
using System.Drawing.Imaging;

namespace Bareliza.Ekran.Drawable.Relative
{

class Grafika
{
  private Bareliza.Ekran.Drawable.Grafika g1;
  public int OriginX, OriginY;
  public double Scale;
  public Bitmap _b;
  
  public Grafika(
    int width1, int height1, PixelFormat pf1, int ox, int oy, double scale1) {    
    g1 = new Bareliza.Ekran.Drawable.Grafika(width1, height1, pf1);
    OriginX = ox;
    OriginY = oy;
    Scale = scale1;
    _b = g1._b;
  }
  
  public void Clear(Color kolor){ g1.Clear(kolor); }
  public void Refresh() { g1.Refresh(); }
  
  public void Rectangle(int x0, int y0, int x1, int y1, Color kolor) {
    g1.Rectangle(
		 (int)(Scale*x0) + OriginX, (int)(Scale*y0) + OriginY,
		 (int)(Scale*x1) + OriginX, (int)(Scale*y1) + OriginY,
		 kolor);
  }
  public void FilledRectangle(int x0, int y0, int x1, int y1, Color kolor) {
    g1.FilledRectangle(
		       (int)(Scale*x0) + OriginX, (int)(Scale*y0) + OriginY,
		       (int)(Scale*x1) + OriginX, (int)(Scale*y1) + OriginY,
		       kolor);
  }
  public void Line(int x0, int y0, int x1, int y1, Color kolor) {
    g1.Line( (int)(Scale*x0) + OriginX, (int)(Scale*y0) + OriginY,
	     (int)(Scale*x1) + OriginX, (int)(Scale*y1) + OriginY,
	     kolor );
  }
  public void Line(int x0, int y0, int x1, int y1, Color kolor, int grubosc) {
    g1.Line( (int)(Scale*x0) + OriginX, (int)(Scale*y0) + OriginY,
	     (int)(Scale*x1) + OriginX, (int)(Scale*y1) + OriginY,
	     kolor, (int)(Scale*grubosc) );
  }
  public void Circle(int ox, int oy, int r, Color kolor, int jedne_osme = 255) {
    g1.Circle( (int)(Scale*ox) + OriginX, (int)(Scale*oy) + OriginY,
	       (int)(Scale*r), kolor, jedne_osme );
  }
  public void FilledCircle(int ox, int oy, int r, Color kolor) {
    g1.FilledCircle( (int)(Scale*ox) + OriginX, (int)(Scale*oy) + OriginY,
	       (int)(Scale*r), kolor);
    }
  public void CircleDbl(double ox, double oy, double r, Color kolor, int jedne_osme = 255) {
    g1.Circle( (int)(Scale*ox) + OriginX, (int)(Scale*oy) + OriginY,
	       (int)(Scale*r), kolor, jedne_osme );
  }
}

}
