
using System.Collections;
using System.IO;

public class LocalFileMgr
{

    private static LocalFileMgr _instance;

    public static LocalFileMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LocalFileMgr();
            }
            return _instance;
        }
    }

    public byte[] GetBuffer(string path)
    {
        byte[] buffer = null;
        using (FileStream file = new FileStream(path, FileMode.Open))
        {
            buffer = new byte[file.Length];
            file.Read(buffer, 0, buffer.Length);
        }
        return buffer;
    }



}



