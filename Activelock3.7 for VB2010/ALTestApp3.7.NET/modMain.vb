Option Strict Off
Option Explicit On 
Imports System.IO
Module modMain
	'*   ActiveLock
	'*   Copyright 1998-2002 Nelson Ferraz
	'*   Copyright 2003 The ActiveLock Software Group (ASG)
	'*   All material is the property of the contributing authors.
	'*
	'*   Redistribution and use in source and binary forms, with or without
	'*   modification, are permitted provided that the following conditions are
	'*   met:
	'*
	'*     [o] Redistributions of source code must retain the above copyright
	'*         notice, this list of conditions and the following disclaimer.
	'*
	'*     [o] Redistributions in binary form must reproduce the above
	'*         copyright notice, this list of conditions and the following
	'*         disclaimer in the documentation and/or other materials provided
	'*         with the distribution.
	'*
	'*   THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
	'*   "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
	'*   LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
	'*   A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
	'*   OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
	'*   SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
	'*   LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
	'*   DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
	'*   THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
	'*   (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
	'*   OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
	'*
	'*
    ''
	' This module handles contains common utility routines that can be shared
	' between ActiveLock and the client application.
	'
    Private Declare Function MapFileAndCheckSum Lib "imagehlp" Alias "MapFileAndCheckSumA" (ByVal FileName As String, ByRef HeaderSum As Integer, ByRef CheckSum As Integer) As Integer
	Public Declare Function GetSystemDirectory Lib "kernel32.dll"  Alias "GetSystemDirectoryA"(ByVal lpBuffer As String, ByVal nSize As Integer) As Integer
    ' Trial information string
    Public strMsg, strRemainingTrialDays, strRemainingTrialRuns, strTrialLength As String
    Public strUsedDays, strExpirationDate, strRegisteredUser, strRegisteredLevel As String
    Public strLicenseClass, strMaxCount, strLicenseFileType, strLicenseType, strUsedLockType As String
    Public remainingDays, remainingRuns As Integer
    Public totalDays, totalRuns As Integer
    Public useTrial As Boolean

	' Application Encryption keys:
	' !!!WARNING!!!
	' It is alright to use these same keys for testing your application.  But it is highly recommended
	' that you generate your own set of keys to use before deploying your app.

    ' EXAMPLE
    'VCode for ALTestApp, version 3.7, NET RSA 1024-bit
    Public Const PUB_KEY As String = "RSA1024<RSAKeyValue><Modulus>pq4qdwkUmTV2S5uku6ImCgyYT94PK7GFyuk4AAO/3bCUXhx+0GLLziTq+qySuT+0CYUH4/j6kPHG4sOTWod3tiCeqvmHx8GthyhgJuSFaE3PonefCEWxJrYF1B62XYFx1QX/jERId0rX9gWKcFMlsafJTeH+GSBuofvV2WdnRR0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>"
    'VCode for TestApp, version 3.5, NET RSA 1024-bit
    'Public Const PUB_KEY As String = "386.391.2CB.21B.210.226.23C.294.386.391.2CB.339.457.533.3B2.42B.4A4.507.457.2AA.294.34F.4C5.44C.507.4A4.507.4F1.2AA.4FC.2D6.247.39C.4FC.4D0.2E1.3B2.273.512.1D9.4FC.34F.533.2EC.512.499.2D6.44C.499.4FC.4F1.2F7.386.323.344.21B.318.483.2D6.344.302.23C.4E6.533.44C.2CB.205.478.34F.4C5.226.231.4FC.483.4C5.344.226.231.30D.483.507.231.46D.370.35A.302.4DB.226.323.4DB.4C5.51D.3D3.499.441.4E6.42B.344.4AF.4C5.4FC.365.462.528.3C8.252.3C8.37B.3C8.268.42B.252.210.1D9.51D.3B2.4E6.48E.462.386.210.210.4AF.32E.436.25D.3BD.23C.4AF.205.507.457.507.528.3C8.4A4.4DB.4DB.4BA.34F.210.4C5.268.226.268.273.4F1.268.37B.273.210.3B2.252.51D.4D0.4C5.2D6.370.252.210.436.386.210.339.441.231.252.318.528.34F.499.370.436.48E.39C.365.2E1.3A7.21B.344.4A4.483.273.533.252.2E1.533.370.205.507.4A4.2CB.2D6.344.457.4FC.4DB.344.4AF.34F.29F.294.205.34F.4C5.44C.507.4A4.507.4F1.2AA.294.2F7.528.4D0.4C5.4BA.457.4BA.4FC.2AA.2CB.37B.2CB.2D6.294.205.2F7.528.4D0.4C5.4BA.457.4BA.4FC.2AA.294.205.386.391.2CB.339.457.533.3B2.42B.4A4.507.457.2AA"
    ''
    ' Verifies the checksum of the typelib containing the specified object.
    ' Returns the checksum.
    '
    Public Function VerifyActiveLockNETdll() As String
        ' CRC32 Hash...
        ' I have modified this routine to read the crc32
        ' of the Activelock3NET.dll directly
        ' since the assembly is not a COM object anymore
        ' the method below is very suitable for .NET and more appropriate
        Dim c As New CRC32
        Dim crc As Integer = 0
        Dim fileName As String = System.Windows.Forms.Application.StartupPath & "\ActiveLock37Net.dll"
        If File.Exists(fileName) Then
            Dim f As FileStream = New FileStream(System.Windows.Forms.Application.StartupPath & "\ActiveLock37Net.dll", FileMode.Open, FileAccess.Read, FileShare.Read, 8192)
            crc = c.GetCrc32(f)
            ' as the most commonly known format

            VerifyActiveLockNETdll = String.Format("{0:X8}", crc)
            f.Close()
            System.Diagnostics.Debug.WriteLine("Hash: " & crc)
            'Use Following Line only to get the CRC in Encoded format
            Dim GetCRC As String = Enc(VerifyActiveLockNETdll)
            If VerifyActiveLockNETdll <> Dec("231.268.268.21B.21B.210.2E1.210") Then
                ' Encrypted version of "activelock3NET.dll has been corrupted. If you were running a real application, it should terminate at this point."
                MsgBox(Dec("42B.441.4FC.483.512.457.4A4.4C5.441.499.231.35A.2F7.39C.1FA.44C.4A4.4A4.160.478.42B.4F1.160.436.457.457.4BA.160.441.4C5.4E6.4E6.507.4D0.4FC.457.44C.1FA") & vbCrLf & vbCrLf & "If you are getting this error message, it might mean that the Activelock DLL might be corrupted," & vbCrLf & "tampered with or the CRC of the Activelock DLL in your application directory does not match" & vbCrLf & "the CRC value embedded in your application." & vbCrLf & vbCrLf & "Just change the VerifyActiveLockNETdll() routine to make the embedded CRC the same as the" & vbCrLf & "actual CRC.", MsgBoxStyle.Information)
                End
            End If
        Else
            MsgBox(Dec("42B.441.4FC.483.512.457.4A4.4C5.441.499.231.35A.2F7.39C.1FA.44C.4A4.4A4.160.4BA.4C5.4FC.160.462.4C5.507.4BA.44C"))
            End
        End If
    End Function
    ' Simple encrypt of a string
    Public Function Enc(ByRef strdata As String) As String
        Dim i, n As Integer
        Dim sResult As String = Nothing
        n = Len(strdata)
        Dim l As Integer
        For i = 1 To n
            l = Asc(Mid(strdata, i, 1)) * 11
            If sResult = "" Then
                sResult = Hex(l)
            Else
                sResult = sResult & "." & Hex(l)
            End If
        Next i
        Enc = sResult
    End Function

    Public Function Dec(ByRef strdata As String) As String
        Dim arr() As String = Nothing
        arr = Split(strdata, ".")
        Dim sRes As String = Nothing
        Dim i As Integer
        For i = LBound(arr) To UBound(arr)
            sRes = sRes & Chr(CInt("&h" & arr(i)) / 11)
        Next
        Dec = sRes
    End Function

    ' Callback function for rsa_generate()
    Public Sub ProgressUpdate(ByVal param As Integer, ByVal action As Integer, ByVal phase As Integer, ByVal iprogress As Integer)
        'frmMain.DefInstance.UpdateStatus("Progress Update received " & param & ", action: " & action & ", iprogress: " & iprogress)
    End Sub
    '===============================================================================
    ' Name: Function WinSysDir
    ' Input: None
    ' Output:
    '   String - Windows system directory path
    ' Purpose: Gets the Windows system directory
    ' Remarks: None
    '===============================================================================
    Public Function WinSysDir() As String
        WinSysDir = System.Environment.SystemDirectory
    End Function
End Module