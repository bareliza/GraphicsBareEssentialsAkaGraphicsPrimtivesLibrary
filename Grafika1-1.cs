//
/* // compile with: /unsafe */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;

using System.Drawing;
using System.Drawing.Imaging;

using System.Windows.Forms;

namespace Bareliza.Ekran.Drawable
{
  /// <summary>
  /// Odpowiada za rysowanie prymitywów graficznych na wewnętrznej bitmapie
  /// - będącej buforem wyświetlanym także na zewnętrznym obiekcie typu Image
  /// </summary>
  public class Grafika
  {
    /// <summary>
    /// Główny bufor, na którym odbywa się rysowanie.
    /// 
    /// Wszystkie bufory mają te same wymiary, z tym że _fontArea90 ma zamienioną wysokość 
    /// z szerokością - tekst renderowany jest zawsze poziomo, zatem dla tekstu pionowego
    /// maksymalna szerokość to maksymalna wysokość docelowej bitmapy, analogicznie maks. 
    /// wysokość to maks. szerokość.</summary>
    //private WriteableBitmap _b;

    /// <summary>
    /// Bufor pomocniczy, potrzebny przy kopiowaniu tekstu.
    /// 
    /// Wszystkie bufory mają te same wymiary, z tym że _fontArea90 ma zamienioną .wysokość 
    /// z szerokością - tekst renderowany jest zawsze poziomo, zatem dla tekstu pionowego
    /// maksymalna szerokość to maksymalna wysokość docelowej bitmapy, analogicznie maks. 
    /// wysokość to maks. szerokość.</summary>
    //private WriteableBitmap _fontArea;

    /// <summary>
    /// Bufor pomocniczy, potrzebny przy kopiowaniu tekstu obróconego o 90 stopni.
    /// 
    /// Wszystkie mają te same wymiary, z tym że _fontArea90 ma zamienioną wysokość 
    /// z szerokością - tekst renderowany jest zawsze poziomo, zatem dla tekstu pionowego
    /// maksymalna szerokość to maksymalna wysokość docelowej bitmapy, analogicznie maks. 
    /// wysokość to maks. szerokość.
    /// </summary>
    //private WriteableBitmap _fontArea90;

    /// <summary>
    /// Wskaźnik do serii 32 bitowych słów reprezentujących poszczególne piksele obrazu
    /// </summary>
    //private IntPtr rawImage;

    ///////////////////////////// //
    //private uint[] rawImage;    //
    //private int rawStride;      //
    ///////////////////////////// //

    /// <summary>
    /// całkowita ilość pikseli obrazu
    /// </summary>

    /////////////////////////////// //
    // private int rawImageSize;    //
    /////////////////////////////// //
	
    /// <summary>
    /// Szerokość obrazu
    /// </summary>
    int width;

    /// <summary>
    /// Wysokość obrazu
    /// </summary>
    int height;

#region konstruktor

    public Bitmap _b;
    PixelFormat _pf;
	
    public Grafika(int width1, int height1, PixelFormat pf1) {
      width = width1;
      height = height1;
      _pf = pf1;
      _b = new Bitmap(width1, height1, _pf);	  
    }
	
#endregion

#region proceduryGraficzne

    /// <summary>
    /// Czyści obszar rysowania wybranym kolorem.
    /// </summary>
    /// <param name="color"> kolor wypełnienia, najstarszy bajt to przeźroczystość </param>
    //unsafe
    public void Clear(Color color)
    {
      //_b.Lock();
      int i, j;
	  
      for (i = 0; i < width; ++i)
	for( j = 0; j < height; ++j)
	  _b.SetPixel(i, j, color);
      //_b.Unlock();
    }
    /// <summary>
    /// Powoduje skopiowanie obrazu z bufora - bitmapy _b - na ekran.
    /// Po zakończeniu rysowania konieczne jest wywołanie tej procedury, aby coś się
    /// wyświetliło
    /// </summary>
    public void Refresh()
    {
      //_b.Lock();
      //_b.AddDirtyRect(new Int32Rect(0, 0, width, height));
      //_b.Unlock();
    }
    /// <summary>
    /// Na podstawie podanych wymiarów rezerwuje niezbędne do pracy bufory,
    /// oraz ustala podstawowy bufor - _b, jako źródło treści dla wskazanego obrazu
    /// </summary>
    /// <param name="myImage"> Obraz, w którym będzie wyświetlana grafika</param>
    /// <param name="w">Szerokość głównego bufora w pikselach</param>
    /// <param name="h">Wysokość głównego bufora w pikselach</param>

    /*
      public Grafika(int w, int h) /*
      /* Image myImage, */
    /*
      {
      // Define parameters used to create the BitmapSource.
      //pf = PixelFormats.Pbgra32;
      width = w;
      height = h;
      // Initialize the image with data.
      //Random value = new Random();
      //value.NextBytes(rawImage);

      // Create a BitmapSource.
      //_b = new WriteableBitmap(width, height, 96, 96, pf, null);
      //_fontArea = new WriteableBitmap(width, height, 96, 96, pf, null);
      //_fontArea90 = new WriteableBitmap(height, width, 96, 96, pf, null);
      //rawImage = _b.BackBuffer;
      //rawStride = _b.BackBufferStride / 4;
      //rawStride90 = _fontArea90.BackBufferStride / 4;
      //rawImageSize = _b.BackBufferStride * _b.PixelHeight / 4;
      rawImageSize = w * h;
      rawImage = new uint[rawImageSize];
      rawStride = w;


      //myImage.Width = w;
      //myImage.Height = h;
      //myImage.Source = _b;
      }
    */

    private string[] Shades = {" ",".",",","-","=","+","*","o","O","N","B","M","#","%","&","@"};
    
    public override string ToString()
    {
      int i, j, w1, h1;
      string out1;

      w1 = (width > 400) ? 400 : width;
      h1 = 50; // = height;
      
      out1 = "";
      for(i = 0; i < h1; i++) {
	for(j = 0; j < w1; j++)
	  out1 += Shades[(_b.GetPixel(i, j).R & 0xf0) >> 4];
	out1 += "\n";
      }

      return out1;
    }

    /// <summary>
    /// Podaje kolor punktu o podanych współrzędnych.
    /// </summary>
    /// <param name="x0">Współrzędna pozioma</param>
    /// <param name="y0">Współrzędna pionowa</param>
    /// <param name="kolor">wartosc zwracana: kolor punktu
    /// dla pełnego koloru 0xff</param>
    //unsafe

    private bool isZero(Color c) {
      return ((c.R | c.G | c.B) == 0);
    }
    
    public Color KolorPunktu(int x0, int y0)
    {
      // return Color.White;
      // Console.WriteLine("a) KolorPunktu( " + x0 + ", " + y0 + " )");
      // 4 - 32 bits per pixel
      Color k1;
      if(x0<width && y0<height && x0>=0 && y0 >=0) {
	k1 = _b.GetPixel(x0, y0);
	return isZero(k1) ? Color.White : k1; // Your dollar, my dollar:
	/// return ( ((Color k1 = _b.GetPixel(x0, y0)) == Color.Black) ? Color.White : k1 );
      }
      // Console.WriteLine("b) KolorPunktu( " + x0 + ", " + y0 + " )");
      return Color.White;
      // rawImage[rawStride * y0 + x0] = kolor;
    }

    /// <summary>
    /// Rysuje punkt o podanych współrzędnych.
    /// </summary>
    /// <param name="x0">Współrzędna pozioma</param>
    /// <param name="y0">Współrzędna pionowa</param>
    /// <param name="kolor">kolor punktu - najstarszy bajt to przeźroczystość, 
    /// dla pełnego koloru 0xff</param>
    //unsafe
    public void RysujPunkt(int x0, int y0, Color kolor)
    {
      // 4 - 32 bits per pixel
      if(x0<width && y0<height && x0>=0 && y0 >=0) _b.SetPixel(x0, y0, kolor);
      // rawImage[rawStride * y0 + x0] = kolor;
    }
    /// <summary>
    /// Blokuje główny bufor, wywoływane przed jego modyfikacją.
    /// </summary>
    public void Lock()
    {
      //_b.Lock();
    }
    /// <summary>
    /// Odblokowuje główny bufor, wywoływane po jego modyfikacji.
    /// </summary>
    public void Unlock()
    {
      //_b.Unlock();
    }

    /// <summary>
    /// Pionowa linia ze wzorem.
    /// Wywołując, należy zapewnić, że linia przechodzi przez Ekran.
    /// Porządek współrzędnych pionowych jest nieistotny.
    /// </summary>
    /// <param name="x">współrzędna pozioma</param>
    /// <param name="y0">początkowa współrzędna pionowa</param>
    /// <param name="y1">końcowa współrzędna pionowa</param>
    /// <param name="kolor">kolor punktu - najstarszy bajt to przeźroczystość, dla pełnego koloru 0xff</param>
    /// <param name="pattern">32-bitowy wzór bit zapalony oznacza rysowanie punktu, 
    /// zgaszony pominięcie</param>
    //unsafe
    public void PatternVerticalLine(int x, int y0, int y1, Color kolor, uint pattern)
    {
      int tmp;
      if (y0 > y1) { tmp = y0;y0 = y1;y1 = tmp; }
      if (y0 < 0) y0 = 0;
      if (y1 >= height) y1 = height - 1;

      int start, stop;
      start = y0; // * rawStride + x;
      stop  = y1; // * rawStride + x;

      Lock();
      uint patternBit = 1;
      do {
	if ((patternBit & pattern) != 0) _b.SetPixel(x, start, kolor);
	patternBit <<= 1;
	if (patternBit == 0) patternBit = 1;
	start++; // += rawStride
      } while (start <= stop);
      Unlock();
    }

    /// <summary>
    /// Pozioma linia ze wzorem.
    /// Wywołując, należy zapewnić, że linia przechodzi przez Ekran.
    /// Porządek współrzędnych poziomych jest nieistotny.
    /// </summary>
    /// <param name="y">Współrzędna pionowa</param>
    /// <param name="x0">Początkowa współrzędna pozioma</param>
    /// <param name="x1">Końcowa współrzędna pozioma</param>
    /// <param name="kolor">kolor punktu - najstarszy bajt to przeźroczystość, dla pełnego koloru 0xff</param>
    /// <param name="pattern">32-bitowy wzór</param>
    //unsafe
    public void PatternHorizontalLine(int y, int x0, int x1, Color kolor, uint pattern)
    {
      int tmp;
      if (x0 > x1)
	{
	  tmp = x0;
	  x0 = x1;
	  x1 = tmp;
	}

      if (x0 < 0) x0 = 0;
      if (x1 >= width) x1 = width - 1;


      int start, stop;

      start = x0; // rawStride * y + x0;
      stop  = x1; // rawStride * y + x1;

      Lock();
      uint patternBit = 1;
      do {
	if ((patternBit & pattern) != 0) _b.SetPixel(start, y, kolor);
	patternBit <<= 1;
	if (patternBit == 0) patternBit = 1; 
	start++;
      } while (start <= stop);
      Unlock();
    }
    /// <summary>
    /// Ogólna linia ze wzorem, optymalizowana dla poziomych i pionowych linii -
    /// - w tych przypadkach wywołuje odpowiednie szybkie procedury.
    /// </summary>
    /// <param name="x0">Współrzędna pozioma początku</param>
    /// <param name="y0">Współrzędna pionowa początku</param>
    /// <param name="x1">Współrzędna pozioma końca</param>
    /// <param name="y1">Współrzędna pionowa końca</param>
    /// <param name="kolor">kolor punktu - najstarszy bajt to przeźroczystość, dla pełnego koloru 0xff</param>
    /// <param name="pattern">32-bitowy wzór</param>
    public void PatternLine(int x0, int y0, int x1, int y1, Color kolor, uint pattern)
    {
      // Proste klipowanie
      if ((x0 < 0 && x1 < 0) || (x0 >= width && x1 >= width) ||
	  (y0 < 0 && y1 < 0) || (y0 >= height && y1 >= height)) return;

      if (x0 == x1) { PatternVerticalLine(x0, y0, y1, kolor, pattern); return; }
      if (y0 == y1) { PatternHorizontalLine(y0, x0, x1, kolor, pattern); return; }

      int dx, dy, tmp, fract;
      dx = Math.Abs(x1 - x0);
      dy = Math.Abs(y1 - y0);

      Lock();
      if (dx > dy) {
	if (x0 > x1)  {
	  tmp = x0;x0 = x1;x1 = tmp; // XCHG x0, x1
	  tmp = y0;y0 = y1;y1 = tmp; // XCHG y0, y1
	}
	dx = x1 - x0; 
	dy = y1 - y0; 
	if (dx != 0) fract = (dy << 16) / dx;
	else fract = 0;

	y0 <<= 16;
	y0 += (1 << 15);
	uint patternBit = 1;
	for (; x0 <= x1; x0++)
	  {
	    if ((patternBit & pattern) != 0) RysujPunkt(x0, y0 >> 16, kolor);
	    y0 += fract;
	    patternBit <<= 1;
	    if (patternBit == 0) patternBit = 1;
	  }
      }
      else {
	if (y0 > y1) {
	  tmp = x0;x0 = x1;x1 = tmp; // XCHG x0, x1
	  tmp = y0;y0 = y1;y1 = tmp; // XCHG y0, y1
	}
	dx = x1 - x0; 
	dy = y1 - y0;
	if (dy != 0) fract = (dx << 16) / dy;
	else fract = 0;

	x0 <<= 16;
	x0 += (1 << 15);
	int patternBit = 1;
	for (; y0 <= y1; y0++) {
	  if ((patternBit & pattern) != 0) RysujPunkt(x0 >> 16, y0, kolor);
	  x0 += fract;
	  patternBit <<= 1;
	  if (patternBit == 0) patternBit = 1;
	}
      }
      Unlock();
    }

    /// <summary>
    /// Pionowa linia.
    /// Wywołując, należy zapewnić, że linia przechodzi przez Ekran.
    /// Porządek współrzędnych pionowych jest nieistotny.
    /// </summary>
    /// <param name="x">współrzędna pozioma</param>
    /// <param name="y0">początkowa współrzędna pionowa</param>
    /// <param name="y1">końcowa współrzędna pionowa</param>
    /// <param name="kolor">kolor punktu - najstarszy bajt to przeźroczystość, dla pełnego koloru 0xff</param>
    //unsafe
    public void VerticalLine(int x, int y0, int y1, Color kolor)
    {
      // Console.WriteLine("V.Line( "+x+", "+y0+", "+y1+" ); Start");
      int tmp;
      if (y0 > y1) { tmp = y0;y0 = y1;y1 = tmp; }
      if (y0 < 0) y0 = 0;
      if (y1 >= height) y1 = height - 1;

      int start, stop;

      start = y0; // rawStride * y0 + x;
      stop  = y1; // rawStride * y1 + x;

      Lock();
      do _b.SetPixel(x, start++, kolor); while (start <= stop);
      // do rawImage[start++] = kolor; while (...
      Unlock();
      // Console.WriteLine("V.Line: End");
    }

    /// <summary>
    /// Pozioma linia ze wzorem.
    /// Wywołując, należy zapewnić, że linia przechodzi przez Ekran.
    /// Porządek współrzędnych poziomych jest nieistotny.
    /// </summary>
    /// <param name="y">Współrzędna pionowa</param>
    /// <param name="x0">Początkowa współrzędna pozioma</param>
    /// <param name="x1">Końcowa współrzędna pozioma</param>
    /// <param name="kolor">kolor punktu - najstarszy bajt to przeźroczystość, dla pełnego koloru 0xff</param>
    //unsafe
    public void HorizontalLine(int y, int x0, int x1, Color kolor)
    {
      // Console.WriteLine("H.Line: Start");
      int tmp;
      if (x0 > x1) { tmp = x0;x0 = x1;x1 = tmp; }
      if (x0 < 0) x0 = 0;
      if (x1 >= width) x1 = width - 1;

      int start, stop;

      start = x0; // rawStride * y + x0;
      stop  = x1; // rawStride * y + x1;

      Lock();
      do // rawImage[start] = kolor;
	_b.SetPixel(start++, y, kolor);
	// start++; // += rawStride;
      while (start <= stop);
      Unlock();
      // Console.WriteLine("H.Line: End");
    }

    /// <summary>
    /// Ogólna linia, optymalizowana dla poziomych i pionowych linii -
    /// - w tych przypadkach wywołuje odpowiednie szybkie procedury.
    /// </summary>
    /// <param name="x0">Współrzędna pozioma początku</param>
    /// <param name="y0">Współrzędna pionowa początku</param>
    /// <param name="x1">Współrzędna pozioma końca</param>
    /// <param name="y1">Współrzędna pionowa końca</param>
    /// <param name="kolor">kolor punktu - najstarszy bajt to przeźroczystość, dla pełnego koloru 0xff</param>
    public void Line(int x0, int y0, int x1, int y1, Color kolor)
    {
	  
      // Console.WriteLine("Line("+x0+", "+y0+", "+x1+", "+y1+"): Start");
      // Proste klipowanie
      if ((x0 < 0 && x1 < 0) || (x0 >= width && x1 >= width) ||
	  (y0 < 0 && y1 < 0) || (y0 >= height && y1 >= height)) return;

      if (x0 == x1) {
	VerticalLine(x0, y0, y1, kolor);
	// Console.WriteLine("Line("+x0+", "+y0+", "+x1+", "+y1+"):\n #### END ####");
	return;
      }
      if (y0 == y1) {
	HorizontalLine(y0, x0, x1, kolor);
	// Console.WriteLine("Line("+x0+", "+y0+", "+x1+", "+y1+"):\n  #### END ####");
	return;
      }
            
      int dx, dy, tmp, fract;
      dx = Math.Abs(x1 - x0);
      dy = Math.Abs(y1 - y0);

      Lock();
      if (dx > dy) {
	if (x0 > x1) {
	  tmp = x0;x0 = x1;x1 = tmp; // XCHG x0, x1
	  tmp = y0;y0 = y1;y1 = tmp; // XCHG y0, y1
	}
	dx = x1 - x0;
	dy = y1 - y0;
	if (dx != 0) fract = (dy << 16) / dx;
	else fract = 0;

	y0 <<= 16;
	y0 += (1 << 15);
	for (; x0 <= x1; x0++) {
	  RysujPunkt(x0, y0 >> 16, kolor);
	  y0 += fract;
	}
      }
      else {
	if (y0 > y1) {
	  tmp = x0;x0 = x1;x1 = tmp; // XCHG x0, x1
	  tmp = y0;y0 = y1;y1 = tmp; // XCHG y0, y1
	}
	dx = x1 - x0;
	dy = y1 - y0;
	if (dy != 0) fract = (dx << 16) / dy;
	else fract = 0;

	x0 <<= 16;
	x0 += (1 << 15);
	for (; y0 <= y1; y0++) {
	  RysujPunkt(x0 >> 16, y0, kolor);
	  x0 += fract;
	}
      }
      Unlock();
      // MessageBox.Show("Line: End");
    }

	// g    _g   _h
	// 1 :   2    1
	// 2 :   2    0
	// 3 :   3    1
	// 4 :   3    0
	// 5 :   4    1
        // 6 :   4    0
    public void Line(int x0, int y0, int x1, int y1, Color kolor, int grubosc) {
	_Line(x0, y0, x1, y1, kolor, (grubosc + 2) >> 1, (grubosc + 1) & 1);
    }
    /// <summary>
    /// Ogólna linia, NIE optymalizowana dla poziomych i pionowych linii.7
    /// </summary>
    /// <param name="x0">Współrzędna pozioma początku</param>
    /// <param name="y0">Współrzędna pionowa początku</param>
    /// <param name="x1">Współrzędna pozioma końca</param>
    /// <param name="y1">Współrzędna pionowa końca</param>
    /// <param name="kolor">kolor punktu - najstarszy bajt to przeźroczystość, dla pełnego koloru 0xff</param>
    /// <param name="grubosc">określa grubość linii, 
    /// rzeczywista grubość wynosi 2*wartość+1-half
    /// (0 -> 1, 2 -> 3, 3 -> 5 itd.)</param>
    /// <param name="half">Modyfikator grubosci, zmniejsza o jeden lub nie, patrz parametr "grubosc".</param>
    // grubosc = 0 -> 1 piksel, 1 -> 3 piksele, 2 -> 5 pikseli, ...
    public void _Line(int x0, int y0, int x1, int y1, Color kolor, int grubosc, int half)
    {
      // Console.WriteLine($"DEBUG: _Line( {x0}, {y0}, {x1}, {y1}, {kolor}, {grubosc}, {half});");
      if ((x0 < 0 && x1 < 0) || (x0 >= width && x1 >= width) || 
	  (y0 < 0 && y1 < 0) || (y0 >= height && y1 >= height)) return; // Proste klipowanie

      int dx, dy, tmp, fract, wi;
      dx = Math.Abs(x1 - x0);
      dy = Math.Abs(y1 - y0);

      Lock();
      if (dx > dy) {
	if (x0 > x1) {
	  tmp = x0;x0 = x1;x1 = tmp; // XCHG x0, x1
	  tmp = y0;y0 = y1;y1 = tmp; // XCHG y0, y1
	}
	dx = x1 - x0;
	dy = y1 - y0;
	if (dx != 0) fract = (dy << 16) / dx;
	else fract = 0; 

	y0 <<= 16;
	y0 += (1 << 15);
	for (; x0 <= x1; x0++) {
	    if( true && ( grubosc != 0 ) )  //   W Y G L A D Z A N I E
	    {
	      // Console.WriteLine(KolorPunktu( x0,  grubosc - half + ( y0 >> 16 ) ) );
		RysujPunkt( x0,  grubosc - half + (y0 >> 16),
			kolorDopelniajacy( kolor,
				( 0xffff - ( y0 & 0xffff ) )
				, KolorPunktu( x0,  grubosc - half + ( y0 >> 16 ) )
			)
		); // TODO: DONE. NullPointerException:: zamienic ( <-> ) _b.GetPixel <-> WezPunkt(...) - klipowany
		RysujPunkt( x0, -grubosc + (y0 >> 16),
			kolorDopelniajacy( kolor,
				( y0 & 0xffff )
				, KolorPunktu( x0, -grubosc + ( y0 >> 16 ) ) 
			)
		);
		for (wi = -grubosc + 1; wi <= grubosc - 1 - half; wi++)
		RysujPunkt(x0, wi + (y0 >> 16), kolor);
	    }
	    y0 += fract;
	}
      }
      else {
	if (y0 > y1) {
	  tmp = x0;x0 = x1;x1 = tmp; // XCHG x0, x1
	  tmp = y0;y0 = y1;y1 = tmp; // XCHG y0, y1
	}
	dx = x1 - x0;
	dy = y1 - y0;
	if (dy != 0) fract = (dx << 16) / dy;
	else fract = 0;

	x0 <<= 16;
	x0 += (1 << 15);
	for (; y0 <= y1; y0++) {
	    if( true && ( grubosc != 0 ) )  //   W Y G L A D Z A N I E
	    {
	      // Console.WriteLine(KolorPunktu( x0,  grubosc - half + ( y0 >> 16 ) ) );
		RysujPunkt( grubosc - half + (x0 >> 16), y0,
			kolorDopelniajacy( kolor,
				( 0xffff - ( x0 & 0xffff ) )
				, KolorPunktu( grubosc - half + (x0 >> 16),  y0 )
			)
		); // TODO: DONE: NullPointerException:: zamienic ( <-> ) _b.GetPixel <-> WezPunkt(...) - klipowany
		RysujPunkt( -grubosc + (x0 >> 16), y0,
			kolorDopelniajacy( kolor,
				( x0 & 0xffff )
				, KolorPunktu( -grubosc + (x0 >> 16), y0 )
			) 
		);

		for (wi = -grubosc + 1; wi <= grubosc - 1 - half; wi++)
		    RysujPunkt(wi + (x0 >> 16), y0, kolor);
	    }
	    x0 += fract;
	}
      }
      Unlock();
    }

    /// <summary>
    /// Zakladany rozklad skladowych:
    ///   ABGR: 0xAaBbGgRr - alfa. blue. green. red.
    /// </summary>
    int kolor2r(Color k) { return k.R; } // (         (k & 0xff) ); }
    int kolor2g(Color k) { return k.G; } // ( ((k >>  8) & 0xff) ); }
    int kolor2b(Color k) { return k.B; } // ( ((k >> 16) & 0xff) ); }
    // inline?
    /*
      uint r2kolor(Color r) { return ( r ); }
      uint g2kolor(Color g) { return ( g << 8 ); }
      uint b2kolor(Color b) { return ( b << 16 ); }
    */

    // point /in ( 0x0000 .. 0xffff ), 0x0000 -> s0; 0xffff -> s1
    int skladowaDopelniajaca(int s0, int s1, int point)	{
      return ( ((s0 * (0xffff - point) + s1 * point) / 0xffff) )  ;
    }

    Color kolorDopelniajacy(Color kolor, int level) {
      return kolorDopelniajacy(kolor, level, Color.White);
//      return kolorDopelniajacy(kolor, level, Color.White);
//      return kolorDopelniajacy(kolor, level, Color.Black);
    }
    Color kolorDopelniajacy(Color kolor, int level, Color kolorTla)
    {
      int r,g,b, rt,gt,bt, ro,go,bo;
      r = kolor2r(kolor);
      g = kolor2g(kolor);
      b = kolor2b(kolor);

      rt = kolor2r(kolorTla);
      gt = kolor2g(kolorTla);
      bt = kolor2b(kolorTla);

      ro = skladowaDopelniajaca(r, rt, level);
      go = skladowaDopelniajaca(g, gt, level);
      bo = skladowaDopelniajaca(b, bt, level);

      return Color.FromArgb(0xff, ro, go, bo);
      // (r2kolor(ro) | g2kolor(go) | b2kolor(bo)) );
    }

    /// <summary>
    /// Prostokąt. Kolejność punktów nie ma znaczenia. 
    /// Istotne, by wyznaczały przekątną.
    /// </summary>
    /// <param name="x0">Współrzędna pozioma rogu początkowego</param>
    /// <param name="y0">Współrzędna pionowa rogu początkowego</param>
    /// <param name="x1">Współrzędna pozioma rogu przeciwległego do początkowego</param>
    /// <param name="y1">Współrzędna pionowa rogu przeciwległego do początkowego</param>
    /// <param name="kolor">kolor punktu - najstarszy bajt to przeźroczystość, dla pełnego koloru 0xff</param>
    public void Rectangle(int x0, int y0, int x1, int y1, Color kolor)
    {
      Line(x0, y0, x1, y0, kolor);
      // Console.WriteLine(this.ToString());
      Line(x1, y0, x1, y1, kolor);
      Line(x1, y1, x0, y1, kolor);
      Line(x0, y1, x0, y0, kolor);
    }

    /// <summary>
    /// Wypełniony Prostokąt. Kolejność punktów nie ma znaczenia. 
    /// Istotne, by wyznaczały przekątną.
    /// </summary>
    /// <param name="x0">Współrzędna pozioma rogu początkowego</param>
    /// <param name="y0">Współrzędna pionowa rogu początkowego</param>
    /// <param name="x1">Współrzędna pozioma rogu przeciwległego do początkowego</param>
    /// <param name="y1">Współrzędna pionowa rogu przeciwległego do początkowego</param>
    /// <param name="kolor">kolor punktu - najstarszy bajt to przeźroczystość, dla pełnego koloru 0xff</param>
    public void FilledRectangle(int x0, int y0, int x1, int y1, Color kolor)
    {
      if (y0 > y1)
	{
	  int tmp = y1;
	  y1 = y0;
	  y0 = tmp;
	}

      for (int i = y0; i <= y1; i++) Line(x0, i, x1, i, kolor);
    }
    /// <summary>
    /// Okrąg. Potrafi narysować również dowolną kombinację jednych ósmych okręgu.
    /// </summary>
    /// <param name="ox">Współrzędna pozioma środka</param>
    /// <param name="oy">Współrzędna pionowa środka</param>
    /// <param name="r">Promień</param>
    /// <param name="kolor">kolor punktu - najstarszy bajt to przeźroczystość, dla pełnego koloru 0xff</param>
    /// <param name="jedne_osme">Osiem bitów określających, 
    /// które jedne ósme mają być narysowane, domyślnie 0xff - czyli pełen okrąg,
    /// Najmłodszy bit odpowiada za jedną ósmą pierwszą na dole na lewo od osi y,
    /// kolejne bity jedne ósme zgodnie z ruchem wskazówek zegara.
    /// 
    /// Domyślna wartość 0xff - to pełny okrąg.
    /// </param>
    public void Circle(int ox, int oy, int r, Color kolor, int jedne_osme = 255)
    {
      // Proste klipowanie
      if ((ox < -r ) || (ox > width+r) || (oy < -r) || (oy> height+r)) return;

      int px, py;

      Lock();

      px = 0;
      py = r;
      while (px <= py)
	{
	  if ((jedne_osme & 1) != 0) RysujPunkt(ox + px, oy + py, kolor);
	  if ((jedne_osme & 2) != 0) RysujPunkt(ox + py, oy + px, kolor);

	  if ((jedne_osme & 4) != 0) RysujPunkt(ox + py, oy - px, kolor);
	  if ((jedne_osme & 8) != 0) RysujPunkt(ox + px, oy - py, kolor);

	  if ((jedne_osme & 16) != 0) RysujPunkt(ox - px, oy - py, kolor);
	  if ((jedne_osme & 32) != 0) RysujPunkt(ox - py, oy - px, kolor);

	  if ((jedne_osme & 64) != 0) RysujPunkt(ox - py, oy + px, kolor);
	  if ((jedne_osme & 128) != 0) RysujPunkt(ox - px, oy + py, kolor);
	  if (Math.Abs((px + 1)*(px + 1) + (py - 1)*(py - 1) - r*r) < 
	      Math.Abs((px + 1)*(px + 1) + py*py - r*r))
	    {
	      px++;
	      py--;
	    }
	  else
	    {
	      px++;
	    }

	}

      Unlock();
    }

        public void CircleDbl(double ox, double oy, double r, Color kolor, int jedne_osme = 255)
    {
      // Proste klipowanie
      if ((ox < -r ) || (ox > width+r) || (oy < -r) || (oy> height+r)) return;

      double px, py;

      Lock();

      px = 0;
      py = r;
      while (px <= py)
	{
	  if ((jedne_osme &  1) != 0) RysujPunkt((int)(ox + px), (int)(oy + py), kolor);
	  if ((jedne_osme &  2) != 0) RysujPunkt((int)(ox + py), (int)(oy + px), kolor);

	  if ((jedne_osme &  4) != 0) RysujPunkt((int)(ox + py), (int)(oy - px), kolor);
	  if ((jedne_osme &  8) != 0) RysujPunkt((int)(ox + px), (int)(oy - py), kolor);

	  if ((jedne_osme &  16) != 0) RysujPunkt((int)(ox - px), (int)(oy - py), kolor);
	  if ((jedne_osme &  32) != 0) RysujPunkt((int)(ox - py), (int)(oy - px), kolor);

	  if ((jedne_osme &  64) != 0) RysujPunkt((int)(ox - py), (int)(oy + px), kolor);
	  if ((jedne_osme & 128) != 0) RysujPunkt((int)(ox - px), (int)(oy + py), kolor);
	  if (Math.Abs((px + 1)*(px + 1) + (py - 1)*(py - 1) - r*r) < 
	      Math.Abs((px + 1)*(px + 1) + py*py - r*r))
	    {
	      px++;
	      py--;
	    }
	  else
	    {
	      px++;
	    }

	}

      Unlock();
    }
    
    /// <summary>
    /// Wypełniony okrąg, czyli koło. 
    /// </summary>
    /// <param name="ox">Współrzędna pozioma środka</param>
    /// <param name="oy">Współrzędna pionowa środka</param>
    /// <param name="r">Promień</param>
    /// <param name="kolor">kolor punktu - najstarszy bajt to przeźroczystość, dla pełnego koloru 0xff</param>
    public void FilledCircle(int ox, int oy, int r, Color kolor)
    {
      // MessageBox.Show("F.C. : Tiru Riru Start");
      // Proste klipowanie
      if ((ox < -r) || (ox > width + r) || (oy < -r) || (oy > height + r)) return;

      int px, py;

      px = 0;
      py = r;
      while (px <= py)
	{
	  if (Math.Abs((px + 1)*(px + 1) + (py - 1)*(py - 1) - r*r) <
	      Math.Abs((px + 1)*(px + 1) + py*py - r*r))
	    {                   
	      Line(ox - px, oy + py, ox + px, oy + py, kolor);
	      Line(ox - px, oy - py, ox + px, oy - py,  kolor);
	      Line(ox + py, oy - px, ox - py, oy - px, kolor);
	      Line(ox - py, oy + px, ox + py, oy + px, kolor);
	      px++;
	      py--;
	    }
	  else
	    {
	      Line(ox + py, oy - px, ox - py, oy - px, kolor);
	      Line(ox - py, oy + px, ox + py, oy + px, kolor);
	      px++;

	    }
	}
      // MessageBox.Show("F.C. : Tiru Riru End");
    }

#endregion

  }

}
