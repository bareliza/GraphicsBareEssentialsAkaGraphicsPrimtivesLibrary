all: dotnet

mono: Program.cs
	mcs Program.cs Grafika1-1.cs SkalowanaGrafika1-1.cs /r:System.Windows.Forms.dll /r:System.Drawing.dll

dotnet: Program.cs
	csc Grafika1-1.cs Program.cs SkalowanaGrafika1-1.cs /r:System.Windows.Forms.dll /r:System.Drawing.dll

clean:
	rm Program.exe -r -f
