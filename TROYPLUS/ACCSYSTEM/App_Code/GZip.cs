using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO.Compression;
using System.IO;

/// <summary>

/// This enumeration enables user to precise

/// to select mode zip or unzip

/// </summary>

public enum Action{Zip,UnZip };

/// <summary>

/// This class performs files compression and decompression

/// </summary>

public class GZip

{

/// <summary>

/// This is a private field that represents

/// the full source file path

/// </summary>

private string _SourceFileName ="";

/// <summary>

/// This is a private field that represents

/// the full destination file path

/// </summary>

private string _DestinationFileName ="";

/// <summary>

/// This byte array is used to stock both

/// The input file contents and out put file

/// contents as bytes

/// </summary>

private byte[] oBuffer;

/// <summary>

/// This is the class responsible of

/// zipping and unzipping files

/// </summary>

private GZipStream oZipper;

/// <summary>

/// This is a default constructor

/// </summary>

public GZip() { }

/// <summary>

/// This is an overloaded constructor

/// </summary>

/// <param name="SourceFileName">This represents the

/// full source file name of the one going to be zipped

/// </param>

/// <param name="DestinationFileName">This represents the

/// full source file name of the one going to be unziped

/// </param>

/// <param name="action">Choose between zip or unzip mode</param>

public GZip(string SourceFileName, string DestinationFileName,Action action)

{

oZipper = null;

this.SourceFileName = SourceFileName;

this.DestinationFileName = DestinationFileName;

/* The user only precizes the zip mode

or the desired action in order to be performed

* instead of using the method directly that is

marked as protected, take a look below */

if (action == Action.Zip)

{ this.CompressFile(); }

if (action == Action.UnZip)

{ this.DecompressFile(); }

 

}

/// <summary>

/// This is the source file full path property

/// </summary>

public string SourceFileName
{
get
{
    return _SourceFileName;
}

set
{
_SourceFileName = value;
}

}

/// <summary>

/// This is the destination full path property

/// </summary>

public string DestinationFileName
{
get
{
return _DestinationFileName;
}
set
{
_DestinationFileName = value;
 
}

}

/// <summary>

/// This is the method responsible for compression, it is marked

/// as protected because we use it is called at the constructor

/// level when a compression mode is chosen instead of using it directly

/// </summary>

protected void CompressFile()
{
if (File.Exists(SourceFileName))
{     
    using (FileStream inputFile = File.Open(SourceFileName, FileMode.Open), outputFile = File.Create(DestinationFileName))
    {

        using (oZipper = new GZipStream(outputFile, CompressionMode.Compress))
        {
                oBuffer = new byte[inputFile.Length];
                int counter = 0;
               while ((counter = inputFile.Read(oBuffer, 0, oBuffer.Length)) != 0)
                {
                     oZipper.Write(oBuffer, 0, counter);
                }
            
        }
    
    oBuffer = null;

    }

}

//TO DO here notify user that the task is performed

}

/// <summary>

/// This is the method responsible for compression, it is marked

/// as protected because we use it is called at the constructor

/// level when a decompression mode is chosen instead of using it directly

/// </summary>

protected void DecompressFile()
{
    if (File.Exists(SourceFileName))
    {
     using (FileStream inputFile = File.Open(SourceFileName, FileMode.Open), outputFile = File.Create(DestinationFileName))
     {
        using (oZipper = new GZipStream(inputFile, CompressionMode.Decompress))
        {

            oBuffer = new byte[inputFile.Length];
            int counter;
            while ((counter = oZipper.Read(oBuffer, 0, oBuffer.Length)) != 0)
            {
                outputFile.Write(oBuffer, 0, counter);
            }

         }

        oBuffer = null;

     }

    }

 //MessageBox..Show("Decompression done");

}

}

//TO DO here notify user that the task is performed

 


