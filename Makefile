all: dotnet

mono: Program.cs
	mcs Program.cs Grafika1-1.cs SkalowanaGrafika1-1.cs /r:System.Windows.Forms.dll /r:System.Drawing.dll
	chmod u+x Program.exe

dotnet: Program.cs
	csc Grafika1-1.cs Program.cs SkalowanaGrafika1-1.cs /r:System.Windows.Forms.dll /r:System.Drawing.dll
	chmod u+x Program.exe

clean:
	rm Program.exe -r -f
