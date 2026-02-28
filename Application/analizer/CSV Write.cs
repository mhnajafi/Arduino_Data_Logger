using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace analizer
{
    class Class1
    {
                private sub DoSave()
        Dim SFD as new SaveFileDialog()
        Try
        With SFD
           .AddExtension = True
           .CheckPathExists = True
           .CreatePrompt = False
           .OverwritePrompt = True
           .ValidateNames = True
           .ShowHelp = True
           .DefaultExt = "txt"
           .Filter = _
           "CSV Files (*.csv)|*.csv|" & _
           "All files|*.*"
           .FilterIndex = 1

           If.ShowDialog() = Windows.Forms.DialogResult.OK Then
              Me.DoSaveItems(.FileName)
           End If

        End With
        Catch ex As Exception
           MsgBox(ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
        end sub


        private sub DoSaveItems(byval fileName as String)
        if fileName is nothing = false then
           if fileName.Length > 0 then
              using writer as new System.IO.StreamWriter(fileName)
                 for each currentItem as object in Me.ListBox1.Items
                    writer.Write(currentItem.ToString() & ",")
                 next
              end using
           end if
        end if
 
        end sub



    }
}
