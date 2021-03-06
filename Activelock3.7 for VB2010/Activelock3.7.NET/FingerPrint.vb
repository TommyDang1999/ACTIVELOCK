Imports System

#Region "Copyright"
' This project is available from SVN on SourceForge.net under the main project, Activelock !
'
' ProjectPage: http://sourceforge.net/projects/activelock
' WebSite: http://www.activeLockSoftware.com
' DeveloperForums: http://forums.activelocksoftware.com
' ProjectManager: Ismail Alkan - http://activelocksoftware.com/simplemachinesforum/index.php?action=profile;u=1
' ProjectLicense: BSD Open License - http://www.opensource.org/licenses/bsd-license.php
' ProjectPurpose: Copy Protection, Software Locking, Anti Piracy
'
' //////////////////////////////////////////////////////////////////////////////////////////
' *   ActiveLock
' *   Copyright 1998-2002 Nelson Ferraz
' *   Copyright 2003-2009 The Activelock - Ismail Alkan (ASG)
' *   All material is the property of the contributing authors.
' *
' *   Redistribution and use in source and binary forms, with or without
' *   modification, are permitted provided that the following conditions are
' *   met:
' *
' *     [o] Redistributions of source code must retain the above copyright
' *         notice, this list of conditions and the following disclaimer.
' *
' *     [o] Redistributions in binary form must reproduce the above
' *         copyright notice, this list of conditions and the following
' *         disclaimer in the documentation and/or other materials provided
' *         with the distribution.
' *
' *   THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
' *   "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
' *   LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
' *   A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
' *   OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
' *   SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
' *   LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
' *   DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
' *   THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
' *   (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
' *   OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
' *
#End Region

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class FingerPrint

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private m_CpuID As String = ""
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private m_BiosID As String = ""
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private m_DiskID As String = ""
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private m_BaseID As String = ""
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private m_VideoID As String = ""
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private m_MacID As String = ""

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private m_UseCpuID As Boolean = True
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private m_UseBiosID As Boolean = True
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private m_UseDiskID As Boolean = True
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private m_UseBaseID As Boolean = True
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private m_UseVideoID As Boolean = True
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private m_UseMacID As Boolean = True

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private m_ReturnLength As Long = 8
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private m_TotalLength As Long = 8

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Text"></param>
    ''' <remarks></remarks>
    Public Event StartingWith(ByVal Text As String)
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Text"></param>
    ''' <remarks></remarks>
    Public Event DoneWith(ByVal Text As String)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TotalLength() As Long
        Get
            Return m_TotalLength
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReturnLength() As Long
        Get
            Return m_ReturnLength
        End Get
        Set(ByVal value As Long)
            If m_ReturnLength < 0 Then m_ReturnLength = 0
            m_ReturnLength = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UseCpuID() As Boolean
        Get
            Return m_UseCpuID
        End Get
        Set(ByVal value As Boolean)
            m_UseCpuID = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UseBiosID() As Boolean
        Get
            Return m_UseBiosID
        End Get
        Set(ByVal value As Boolean)
            m_UseBiosID = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UseDiskID() As Boolean
        Get
            Return m_UseDiskID
        End Get
        Set(ByVal value As Boolean)
            m_UseDiskID = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UseBaseID() As Boolean
        Get
            Return m_UseBaseID
        End Get
        Set(ByVal value As Boolean)
            m_UseBaseID = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UseVideoID() As Boolean
        Get
            Return m_UseVideoID
        End Get
        Set(ByVal value As Boolean)
            m_UseVideoID = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UseMacID() As Boolean
        Get
            Return m_UseMacID
        End Get
        Set(ByVal value As Boolean)
            m_UseMacID = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Value() As String
        Get
            RaiseEvent StartingWith("All")

            m_CpuID = ""
            If m_UseCpuID Then m_CpuID = CpuID()
            m_BiosID = ""
            If m_UseBiosID Then m_BiosID = BiosID()
            m_DiskID = ""
            If m_UseDiskID Then m_DiskID = DiskID()
            m_BaseID = ""
            If m_UseBaseID Then m_BaseID = BaseID()
            m_VideoID = ""
            If m_UseVideoID Then m_VideoID = VideoID()
            'm_MacID = ""
            'If m_UseMacID Then m_MacID = MacID()

            Return Pack(m_CpuID & m_BiosID & m_DiskID & m_BaseID & m_VideoID) '& m_MacID)

            RaiseEvent DoneWith("All")
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="wmiClass"></param>
    ''' <param name="wmiProperty"></param>
    ''' <param name="wmiMustBeTrue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Identifier(ByVal wmiClass As String, ByVal wmiProperty As String, ByVal wmiMustBeTrue As String) As String
        'Return a hardware identifier

        Dim Result As String = ""
        Dim mc As New System.Management.ManagementClass(wmiClass)
        Dim moc As System.Management.ManagementObjectCollection = mc.GetInstances
        Dim mo As System.Management.ManagementObject

        For Each mo In moc
            If mo(wmiMustBeTrue).ToString = "True" Then
                'Only get the first one
                If Result = "" Then
                    Try
                        Result = mo(wmiProperty).ToString
                        Exit For
                    Catch ex As Exception
                        'Ignore error
                    End Try
                End If
            End If
        Next mo

        Return Result
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="wmiClass"></param>
    ''' <param name="wmiProperty"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Identifier(ByVal wmiClass As String, ByVal wmiProperty As String) As String
        'Return a hardware identifier

        Dim Result As String = ""
        Dim mc As New System.Management.ManagementClass(wmiClass)
        Dim moc As System.Management.ManagementObjectCollection = mc.GetInstances
        Dim mo As System.Management.ManagementObject

        For Each mo In moc
            'Only get the first one
            If Result = "" Then
                Try
                    Result = mo(wmiProperty).ToString
                    Exit For
                Catch ex As Exception
                    'Ignore error
                End Try
            End If
        Next mo

        Return Result
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CpuID() As String
        RaiseEvent StartingWith("CpuID")

        'Uses first CPU identifier available in order of preference
        'Don't get all identifiers as very time consuming
        ' Do not get the following because it's mostly unavailable
        'Dim RetVal As String = Identifier("Win32_Processor", "UniqueId")

        Dim RetVal As String = String.Empty
        If RetVal = "" Then   'If no UniqueId, use ProcessorID
            RetVal = Identifier("Win32_Processor", "ProcessorId")

            If RetVal = "" Then   'If no ProcessorID, use Name
                RetVal = Identifier("Win32_Processor", "Name")

                If RetVal = "" Then   'If no Name, use Manufacturer
                    RetVal = Identifier("Win32_Processor", "Manufacturer")
                End If

                'Add clock speed for extra security
                ' Removed because this property can change between boots on some machines
                'RetVal += Identifier("Win32_Processor", "MaxClockSpeed")
            End If
        End If

        Return RetVal

        RaiseEvent DoneWith("CpuID")
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function BiosID() As String
        RaiseEvent StartingWith("BiosID")

        'BIOS Identifier

        Return Identifier("Win32_BIOS", "Manufacturer") _
          & Identifier("Win32_BIOS", "SMBIOSBIOSVersion") _
          & Identifier("Win32_BIOS", "SerialNumber") _
          & Identifier("Win32_BIOS", "ReleaseDate") _
          & Identifier("Win32_BIOS", "Version")
        '          & Identifier("Win32_BIOS", "IdentificationCode") _

        RaiseEvent DoneWith("BiosID")
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DiskID() As String
        RaiseEvent StartingWith("DiskID")

        'Main physical hard drive ID

        Return Identifier("Win32_DiskDrive", "Manufacturer") _
          & Identifier("Win32_DiskDrive", "Signature") _
          & Identifier("Win32_DiskDrive", "TotalHeads") _
          & GetHDSerialFirmware()
        'Identifier("Win32_DiskDrive", "Model") _

        RaiseEvent DoneWith("CpuID")
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function BaseID() As String
        RaiseEvent StartingWith("BaseID")

        'Motherboard ID

        Return Identifier("Win32_BaseBoard", "Model") _
          & Identifier("Win32_BaseBoard", "Manufacturer") _
          & Identifier("Win32_BaseBoard", "Name") _
          & Identifier("Win32_BaseBoard", "SerialNumber")

        RaiseEvent DoneWith("BaseID")
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function VideoID() As String
        RaiseEvent StartingWith("VideoID")

        'Primary video controller ID

        Return Identifier("Win32_VideoController", "DriverVersion") _
          & Identifier("Win32_VideoController", "Name")

        RaiseEvent DoneWith("VideoID")
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MacID() As String
        RaiseEvent StartingWith("MacID")

        'First enabled network card ID

        Return Identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled")

        RaiseEvent StartingWith("MacID")
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Text"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Pack(ByVal Text As String) As String
        RaiseEvent StartingWith("Packing")

        'Packs the string to m_ReturnLength digits
        'If m_ReturnLength=-1 : Return complete string

        Dim RetVal As String
        Dim X As Long
        Dim Y As Long
        Dim N As Char

        For Each N In Text
            Y += 1
            X += (Asc(N) * Y)
        Next N

        If m_ReturnLength > 0 Then
            RetVal = X.ToString.PadRight(CType(m_ReturnLength, Integer), Chr(48))
        Else
            RetVal = X.ToString
        End If

        If m_ReturnLength = 0 Then
            m_TotalLength = RetVal.Length
            Return RetVal
        Else
            m_TotalLength = RetVal.Length
            Return RetVal.Substring(0, CType(m_ReturnLength, Integer))
        End If

        RaiseEvent DoneWith("Packing")
    End Function
End Class




