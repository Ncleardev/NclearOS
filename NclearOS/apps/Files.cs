using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using System.Threading;
using NclearOS.lib;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using System.IO;
using NclearOS.input;

namespace NclearOS.files
{
    public static class Files
    {
        public static string seldisk = "0";
        public static string filename = "New file";
        public static string content;
        public static string inputfile;
        public static string path = "Computer";
        public static string undopath = "Computer";
        public static string textfromfile;
        public static int disknumbermax = 0;
        public static bool acknowledged;
        public static void Main()
        {
            if (!acknowledged)
            {
                Console.WriteLine("\nInformation: Only FAT12 FAT16 FAT32 partitions are supported.\nDoes not work with VirtualBox, use VMware");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Attention: Do not use this program on disks with important files.\nData corruption may occur.");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nPress Enter if you understand the risk and want to continue\nor press any other key to quit");
                Console.ResetColor();
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    acknowledged = true;
                    Run();
                }
            }
            else
            {
                Run();
            }

        }
        public static void Run()
        {
            Files.Diskslist();
            path = "Computer";
            Console.WriteLine("\nEnter volume number");
            Console.Write("\n" + path + "> ");
            ConsoleKeyInfo name = Console.ReadKey();
            if(name.Key == ConsoleKey.Tab)
            {
                goto exit;
            } else
            { seldisk = name.KeyChar.ToString(); }
            try { if (VFSManager.GetFileSystemLabel(seldisk + ":\\") == null) ; }
            catch {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nDisk does not exist\n");
                Console.ResetColor();
                Files.Run(); }
            path = seldisk + ":\\";
            diskselected:
            inputfile = Input.Main(path, ConsoleColor.White);
            switch (inputfile)
            {
                case "help":
                    Console.WriteLine("Files App Help\n---------------\ninfo\ncd\ncd..\nq - quit app\ndir\n");
                    goto diskselected;
                case "info":
                    string label = VFSManager.GetFileSystemLabel(seldisk + ":\\");
                    string fs_type = VFSManager.GetFileSystemType(seldisk + ":\\");
                    double spaceB = VFSManager.GetTotalSize(seldisk + ":\\");
                    double available_spaceB = VFSManager.GetAvailableFreeSpace(seldisk + ":\\");
                    double used_spaceB = spaceB - available_spaceB;
                    double space = spaceB / 1024f / 1024f;
                    double available_space = available_spaceB / 1024f / 1024f;
                    double used_space = space - available_space;
                    Console.WriteLine(label + " (" + seldisk + ":\\)\n");
                    Console.WriteLine("File System: " + fs_type + " | Capacity: " + space + " MB - " + spaceB + " B");
                    Console.WriteLine("Used Space: " + Convert.ToInt32(used_space) + " MB - " + used_spaceB + " B | Free Space: " + Convert.ToInt32(available_space) + " MB - " + available_spaceB + " B");
                    goto diskselected;
                case "cd":
                    Console.WriteLine(path);
                    goto diskselected;
                case "cd\\":
                    path = seldisk + ":\\";
                    goto diskselected;
                case "cd..":
                    path = undopath;
                    if (path == seldisk + ":\\")
                    {
                        Run();
                    }
                    goto diskselected;
                case "copy":
                    goto diskselected;
                case "mkdir":
                    Console.Write("\nEnter path: ");
                    var bruh = Console.ReadLine();
                    Console.Write("\nCreating directory in " + bruh);
                    switch (bruh)
                    {
                        default:
                            VFSManager.CreateDirectory(bruh);
                            break;
                    }
                    goto diskselected;
                case "deldir":
                    Console.Write("\nEnter path: ");
                    var bruh1 = Console.ReadLine();
                    Console.Write("\nDeleting directory in " + bruh1);
                    switch (bruh1)
                    {
                        default:
                            VFSManager.DeleteDirectory(bruh1, false);
                            break;
                    }
                    goto diskselected;
                case "system/Return":
                    goto exit;
                case "dir":
                    {
                        int dircount = 0;
                        int filecount = 0;
                        int filesize = 0;
                        Console.WriteLine("\nDirectory of " + path + "\n");
                        var directory_list = VFSManager.GetDirectoryListing(path);
                        foreach (var directoryEntry in directory_list)
                        {
                            if (directoryEntry.mEntryType == Sys.FileSystem.Listing.DirectoryEntryTypeEnum.File)
                            {
                                Console.Write(" " + directoryEntry.mSize + " bytes");
                                Console.CursorLeft = 20;
                                Console.WriteLine(directoryEntry.mName);
                                filecount++;
                                filesize += Convert.ToInt32(directoryEntry.mSize);
                            }
                            else
                            {
                                Console.Write(" DIR");
                                Console.CursorLeft = 20;
                                Console.WriteLine(directoryEntry.mName);
                                dircount++;
                            }
                        }
                    if (dircount == 1) { Console.Write("\nTotal: " + dircount + " Dir | "); }
                    else { Console.Write("\nTotal: " + dircount + " Dirs | "); }
                    if (filecount == 1) { Console.Write(filecount + " File - "); }
                    else { Console.Write(filecount + " Files - "); }
                    Console.WriteLine(Convert.ToInt32(filesize / 1024f / 1024f)+ " MB - " + filesize + " B");
                    goto diskselected;
                    }
                case { } when inputfile.Contains("."):
                    if (VFSManager.FileExists(path + "\\" + inputfile))
                    {
                        Open();
                        Console.WriteLine("\n" + textfromfile);
                        goto diskselected;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("File does not exist");
                        Console.ResetColor();
                        goto diskselected;
                    }
                        
                    
                    break;
                case { } when inputfile.Contains(":"):
                    {
                        if ( disknumbermax >= Convert.ToInt32(inputfile) ) { }
                        
                        goto diskselected;
                    }
                case { } when inputfile.StartsWith("cd "):
                    if (inputfile.Substring(3).Contains(seldisk + ":\\"))
                    {
                        undopath = path;
                        path = inputfile.Substring(3);
                    }
                    else if (inputfile.Substring(3).Equals(".."))
                    {
                        path = undopath;
                    }
                    else
                    {
                        undopath = path;
                        if (path.EndsWith("\\")) { path += inputfile.Substring(3); }
                        else { path += "\\" + inputfile.Substring(3); }

                    }
                    if (!VFSManager.DirectoryExists(path))
                    {
                        Console.Write("\nDirectory not exist. Press Enter to abort, enter m to make new directory...");
                        try
                        {
                            switch (Console.ReadLine())
                            {
                                case "m" or "M":
                                    VFSManager.CreateDirectory(path);
                                    break;
                                default:
                                    path = undopath;
                                    break;
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("\nCouldn't create directory in " + path + "\n" + e);
                        }
                    }
                    goto diskselected;
                case "format":
                    Console.WriteLine("Formating is not supported yet");
                    goto diskselected;
                    label = VFSManager.GetFileSystemLabel(seldisk + ":\\");
                    Console.WriteLine("Information: Only FAT12 FAT16 FAT32 partitions are supported.");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Attention: All data on '" + label + "' will be deleted.");
                    Console.Write("\nPress Enter if you want to continue or enter q to cancel..");
                    Console.ForegroundColor = ConsoleColor.White;
                    switch (Console.ReadLine())
                    {
                        case "q" or "Q":
                            goto diskselected;
                        default:
                            goto diskselected;
                    }
                default:
                    goto diskselected;
            }
        exit:
            loading.Loading.Main(5);
            Console.WriteLine("\b");
        }
        public static void Diskslist()
        {
            Console.WriteLine("---FAT12 FAT16 FAT32 volumes---\n");
            int diskslistdone = 0;
            for (int diskslistnumber = 0; diskslistdone == 0;  diskslistnumber++)
            {
                try
                {
                    string label = VFSManager.GetFileSystemLabel(diskslistnumber + ":\\");
                    string fs_type = VFSManager.GetFileSystemType( diskslistnumber + ":\\");
                    double space = VFSManager.GetTotalSize(diskslistnumber + ":\\");
                    double available_space = VFSManager.GetAvailableFreeSpace(diskslistnumber + ":\\");
                    space = space / 1024f / 1024f;
                    double used_space = space - (available_space / 1024f / 1024f);
                    Console.WriteLine("Volume " + diskslistnumber + " - " + label + " (" + fs_type + ") | " + Convert.ToInt32(used_space) + " MB / " + space + " MB");
                }
                catch
                {
                    disknumbermax = diskslistnumber;
                    diskslistdone = 1; // =)
                }
            }
        }
        public static void Save()
        {
            Console.WriteLine("\nSelect path, then press TAB to continue...\n");
            Main();
            Console.WriteLine("Now saving content: " + content);
            Console.Write("Enter file name: ");
            filename = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(filename)) { filename = "New file"; }
            if (!filename.Contains("."))
                filename += ".txt";
            if (!VFSManager.FileExists(path + "\\" + filename))
                VFSManager.CreateFile(path + "\\" + filename);
            try
            {
                var hello_file = VFSManager.GetFile(path + "\\" + filename);
                var hello_file_stream = hello_file.GetFileStream();

                if (hello_file_stream.CanWrite)
                {
                    byte[] text_to_write = Encoding.ASCII.GetBytes(content);
                    hello_file_stream.Write(text_to_write, 0, text_to_write.Length);
                    Console.WriteLine("Saved as " + path + "\\" + filename);
                    content = null;
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.ToString());
                Console.ResetColor();
            }
        }
        public static void Open()
        {
            try
            {
                var hello_file = VFSManager.GetFile(path + "\\" + inputfile);
                var hello_file_stream = hello_file.GetFileStream();

                if (hello_file_stream.CanRead)
                {
                    byte[] text_to_read = new byte[hello_file_stream.Length];
                    hello_file_stream.Read(text_to_read, 0, (int)hello_file_stream.Length);
                    textfromfile = Encoding.Default.GetString(text_to_read);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public static void Check()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("| .. |");
                Console.ResetColor();
                Console.Write(" NclearOS File Manager");
                Console.CursorLeft = 0;
                CosmosVFS fs = new CosmosVFS();
                VFSManager.RegisterVFS(fs);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("| OK |");
                Console.ResetColor();
                Console.Write(" NclearOS File Manager \n");
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("| ERR |");
                Console.ResetColor();
                Console.Write(" NclearOS File Manager \n");
                Console.WriteLine(e.ToString());
            }
        }
    }
}