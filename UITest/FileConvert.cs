using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UITest
{
    public class FileConvert
    {
        // Create a new file object to be used.
        UITest.File newFile = new UITest.File();

        public string selectFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Find Caption File";
            ofd.Filter = "SRT files|*.srt";
            ofd.InitialDirectory = @"C:\";
            if (ofd.ShowDialog() == true)
            {
                // Check to ensure that the file is of type .txt or .srt
                if (System.IO.Path.GetExtension(ofd.FileName.ToString()) == ".srt")
                {
                    // Write the files contents out into the srt text box.
                    newFile.FileName = ofd.FileName;
                    return ofd.FileName;
                }
            }
            // Write something other than the selected files content into srt text box.
            return "Wrong File Type";
        }

        public string getFileText()
        {
            if (newFile.FileName != null || newFile.FileName != "")
            {
                // Write the files contents out into the srt text box.
                return System.IO.File.ReadAllText(newFile.FileName);
            }
            return "File name was null or empty";
        }

        public string getConvertedFileText(string convertedFileName)
        {
                // Check to ensure that the file is of type .txt or .srt
                if (System.IO.Path.GetExtension(convertedFileName.ToString()) == ".xml")
                {
                    // Write the files contents out into the srt text box.
                    return System.IO.File.ReadAllText(convertedFileName);
                }
            // Write something other than the selected files content into srt text box.
            return "Wrong File Type";
        }

        string getFileName(string FilePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(FilePath);
            return fileName;
        }

        string getExecutingDirectory()
        {
            string DirectoryString = Directory.GetCurrentDirectory();
            return DirectoryString;
        }

        string setupHeader(string Language)
        {
            string header =
                "﻿<?xml version='1.0' encoding='UTF-8'?>\n" +
                "<tt xmlns='http://www.w3.org/2006/04/ttaf1' xmlns:tts='http://www.w3.org/2006/04/ttaf1#styling' xml:lang='en'> \n" +
                "<head> \n" +
                "<styling> \n" +
                "<style id='defaultCaption' tts:fontSize='18' tts:fontFamily='Trebuchet MS, Arial, SansSerif' tts:fontWeight='normal' tts:fontStyle='normal' tts:textDecoration='none' tts:color='white' tts:backgroundColor='black' tts:opacity='0.35' tts:textAlign='center'/> \n" +
                "</styling> \n" +
                "</head> \n" +
                "<body style='defaultCaption' id='thebody'> \n" +
                "<div xml:lang='" + Language + "'> \n";
            return header;
        }

        string setupFooter()
        {
            string footer =
                "</div> \n" +
                "</body> \n" +
                "</tt> \n";
            return footer;
        }

        int randomNumber()
        {
            Random rnd = new Random();
            int number = rnd.Next(100000, 2000000);
            return number;
        }

        public string ConvertFile(string language)
        {
            string[] lines = System.IO.File.ReadAllLines(newFile.FileName);
            string captionNumber = "";
            string oldCaptionNumber = "1";
            string startTime = "00:00:00.0";
            string endTime = "00:00:00.0";
            string content = "";
            string header1 = setupHeader(language);
            string footer1 = setupFooter();

            char oldChar = ',';
            char newChar = '.';

            List<string> newLines = new List<string>();

            // Add Header to new array;
            newLines.Add(header1);

            int index = 0;

            //Print out the paragraph tags with their content and values.
            foreach (string line in lines)
            {
                index++;
                if (line.Length > 0 && char.IsDigit(line[0]) && line[0].ToString() != "0")
                {
                    if (line.Length > 1)
                    {
                        captionNumber = line[0].ToString() + line[1].ToString();
                    }
                    else
                    {
                        captionNumber = line[0].ToString();
                    }
                }
                // Format Start and End Times
                else if (line.Length > 0 && line[0].ToString() == "0")
                {
                    string allCharacterLeftOfDash = line.Substring(0, line.IndexOf("-"));
                    startTime = "'" + allCharacterLeftOfDash.Trim() + "'";
                    // Replace the , with a .
                    startTime = startTime.Replace(oldChar, newChar);
                    string allCharacterRightOfArrow = line.Split('>').Last();
                    endTime = "'" + allCharacterRightOfArrow.Trim() + "'";
                    // Replace the , with a .
                    endTime = endTime.Replace(oldChar, newChar);
                }
                else if (line.Length == 0)
                {
                    // Blank line.
                }
                else
                {
                    // Catch all lines that aren't accounted for.
                }

                if (line.Length > 0 && char.IsLetter(line[0]) && line[0].ToString() != "0")
                {
                    if (oldCaptionNumber == captionNumber)
                    {
                        content += line + " ";
                    }
                }

                if (oldCaptionNumber != captionNumber || index >= lines.Length)
                {
                    string newPara = "\t <p begin=" + startTime + " end=" + endTime + ">" + content + "</p>";
                    // Add line to new array;
                    newLines.Add(newPara);
                    content = "";
                }

                if (line.Length > 0 && char.IsDigit(line[0]) && line[0].ToString() != "0")
                {
                    if (captionNumber != oldCaptionNumber)
                    {
                        oldCaptionNumber = captionNumber;
                    }
                }
            }

            //Add footer to new array
            newLines.Add(footer1);

            //Get Rando
            int rando = randomNumber();

            //Get the current directory
            string directoryForPath = getExecutingDirectory();

            //GetFileName
            string fileNameForPath = getFileName(newFile.FileName);

            string convertedFileName = @directoryForPath + "/" + fileNameForPath + "" + DateTime.Now.Millisecond + ".xml";

            //Write collection to new file.
            System.IO.File.WriteAllLines(@convertedFileName, newLines);

            //Return File
            return convertedFileName;
        }
    }
}
