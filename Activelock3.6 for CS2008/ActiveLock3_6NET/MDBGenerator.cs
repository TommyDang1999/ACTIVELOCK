using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Security.Cryptography;
using System.text;

internal class MDBGenerator : _IALUGenerator
{
	//*   ActiveLock
	//*   Copyright 1998-2002 Nelson Ferraz
	//*   Copyright 2003-2006 The ActiveLock Software Group (ASG)
	//*   All material is the property of the contributing authors.
	//*
	//*   Redistribution and use in source and binary forms, with or without
	//*   modification, are permitted provided that the following conditions are
	//*   met:
	//*
	//*     [o] Redistributions of source code must retain the above copyright
	//*         notice, this list of conditions and the following disclaimer.
	//*
	//*     [o] Redistributions in binary form must reproduce the above
	//*         copyright notice, this list of conditions and the following
	//*         disclaimer in the documentation and/or other materials provided
	//*         with the distribution.
	//*
	//*   THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
	//*   "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
	//*   LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
	//*   A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
	//*   OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
	//*   SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
	//*   LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
	//*   DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
	//*   THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
	//*   (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
	//*   OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
	//*
	//*
	//===============================================================================
	// Name: Generator
	// Purpose: This is a concrete implementation of the IALUGenerator interface.
	// Functions:
	// Properties:
	// Methods:
	// Started: 08.15.2003
	// Modified: 03.24.2006
	//===============================================================================
	// @author activelock-admins
	// @version 3.3.0
	// @date 03.24.2006

	private _IActiveLock MyActiveLock;
	private OleDbConnection oConnect;
	private OleDbDataReader oRST;

	private OleDbCommand myCmd;

	private string fileMDB;
	//===============================================================================
	// Name: Property Get ActiveLockInterface
	// Input: None
	// Output: None
	// Purpose: Returns IActiveLock interface
	// Remarks: None
	//===============================================================================
	private _IActiveLock ActiveLockInterface {
		get { ActiveLockInterface = MyActiveLock; }
	}
	//===============================================================================
	// Name: Property Let IALUGenerator_StoragePath
	// Input:
	//   ByVal RHS As String -
	// Output: None
	// Purpose: None
	// Remarks: None
	//===============================================================================
	private string IALUGenerator_StoragePath {
		set {
			if (!modActiveLock.FileExists(value)) {
			}
			else {
				fileMDB = value;
			}
		}
	}
	string _IALUGenerator.StoragePath {
		set { IALUGenerator_StoragePath = value; }
	}
	//Class_Initialize was upgraded to Class_Initialize_Renamed
	private void Class_Initialize_Renamed()
	{
		// 05.13.05 - ialkan Modified to merge DLLs into one
		// Initialize AL
		MyActiveLock = new IActiveLock();
		oConnect = new OleDbConnection();
	}
	public MDBGenerator() : base()
	{
		Class_Initialize_Renamed();
	}
	//===============================================================================
	// Name: Sub LoadProdInfo
	// Input:
	//   ByRef Section As String - Section Name that contains ProdName and ProdVer in order to be unique
	//   ByRef ProdInfo As ProductInfo - Object containing product information to be saved.
	// Output: None
	// Purpose: Loads Product Info from the specified INI section.
	// Remarks: None
	//===============================================================================
	private void LoadProdInfo(ref OleDbDataReader pDataReader, ref ProductInfo ProdInfo)
	{
		ProdInfo.Name = (string)pDataReader["name"];
		ProdInfo.Version = (string)pDataReader["version"];
		ProdInfo.VCode = (string)pDataReader["vcode"];
		ProdInfo.GCode = (string)pDataReader["gcode"];
	}
	//===============================================================================
	// Name: Sub IALUGenerator_SaveProduct
	// Input:
	//   ByRef ProdInfo As ProductInfo - Object containing product information to be saved.
	// Output: None
	// Purpose: Saves product details in the store file
	// Remarks: IALUGenerator Interface implementation
	//===============================================================================
	private void IALUGenerator_SaveProduct(ref ProductInfo ProdInfo)
	{
		//save product in database

		string strSQL = null;
		strSQL = "INSERT INTO products (name, version, vcode, gcode) VALUES ('" + ProdInfo.Name + "', '" + ProdInfo.Version + "', '" + ProdInfo.VCode + "', '" + ProdInfo.GCode + "')";

		try {
			//open connection to MDB file
			oConnect.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source= " + fileMDB + ";";
			oConnect.Open();

			myCmd = new OleDbCommand();
			myCmd.Connection = oConnect;
			myCmd.CommandTimeout = 60;
			myCmd.CommandText = strSQL;
			myCmd.CommandType = CommandType.Text;
			myCmd.ExecuteNonQuery();
		}
		finally {
			oConnect.Close();
		}

	}
	void _IALUGenerator.SaveProduct(ref ProductInfo ProdInfo)
	{
		IALUGenerator_SaveProduct(ProdInfo);
	}
	//===============================================================================
	// Name: IALUGenerator_RetrieveProducts
	// Input: None
	// Output:
	//   ProductInfo - Product info object
	// Purpose: Retrieves all product information from MDB.
	// Remarks: Returns as an array.
	//===============================================================================
	private ProductInfo[] IALUGenerator_RetrieveProducts()
	{
		//Retrieve all product information from MDB file.  Return as an array.
		ProductInfo[] arrProdInfos = null;
		string strSQL = "SELECT * FROM products";

		try {
			//open connection to MDB file
			oConnect.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source= " + fileMDB + ";";
			oConnect.Open();

			myCmd = new OleDbCommand();
			myCmd.CommandText = strSQL;
			myCmd.CommandType = CommandType.Text;
			myCmd.CommandTimeout = 60;
			myCmd.Connection = oConnect;
			oRST = myCmd.ExecuteReader();

			int Count = 0;
			Count = 0;
			while (oRST.Read()) {
				Array.Resize(ref arrProdInfos, Count + 1);
				arrProdInfos[Count] = new ProductInfo();
				//load product info
				LoadProdInfo(ref oRST, ref arrProdInfos[Count]);
				Count += 1;
			}
		}
		finally {
			//close dataset
			if (!oRST.IsClosed) {
				oRST.Close();
			}
			//Close connection
			if (!(oConnect.State == ConnectionState.Closed)) {
				oConnect.Close();
			}
		}
		return arrProdInfos;
	}
	ProductInfo[] _IALUGenerator.RetrieveProducts()
	{
		return IALUGenerator_RetrieveProducts();
	}
	//===============================================================================
	// Name: Function IALUGenerator_RetrieveProduct
	// Input:
	//   ByVal name As String - Product name
	//   ByVal Ver As String - Product version
	// Output: None
	// Purpose: Retrieves product VCode and GCode from the store file
	// Remarks: todo Error Handling - Need to return Nothing if store file doesn't contain the product
	//===============================================================================
	private ProductInfo IALUGenerator_RetrieveProduct(string Name, string Ver)
	{
		//@todo Error Handling - Need to return Nothing if store file doesn't contain the product
		ProductInfo ProdInfo = new ProductInfo();
		ProdInfo.Name = Name;
		ProdInfo.Version = Ver;
		string strSQL = "SELECT * FROM products WHERE name='" + Name + "' AND version='" + Ver + "'";

		try {
			//open connection to MDB file
			oConnect.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source= " + fileMDB + ";";
			oConnect.Open();

			myCmd = new OleDbCommand();
			myCmd.CommandText = strSQL;
			myCmd.CommandType = CommandType.Text;
			myCmd.CommandTimeout = 60;
			myCmd.Connection = oConnect;
			oRST = myCmd.ExecuteReader();

			if (oRST.HasRows) {
				oRST.Read();
				ProdInfo.VCode = (string)oRST("vcode");
				ProdInfo.GCode = (string)oRST("gcode");
			}

		}
		finally {
			oRST.Close();
			oConnect.Close();
		}

		if (string.IsNullOrEmpty(ProdInfo.VCode) | string.IsNullOrEmpty(ProdInfo.GCode)) {
			modActiveLock.Set_Locale(modActiveLock.regionalSymbol);
			Err().Raise(AlugenGlobals.alugenErrCodeConstants.alugenProdInvalid, modTrial.ACTIVELOCKSTRING, "Product code set is invalid.");
		}
		return ProdInfo;
	}
	ProductInfo _IALUGenerator.RetrieveProduct(string Name, string Ver)
	{
		return IALUGenerator_RetrieveProduct(Name, Ver);
	}
	//===============================================================================
	// Name: Sub IALUGenerator_DeleteProduct
	// Input:
	//   ByVal name As String - Product name
	//   ByVal Ver As String - Product version
	// Output: None
	// Purpose: Removes the license keys section from a INI file, i.e. deletes product details in the license database
	// Remarks: Removes a section from the INI file
	//===============================================================================
	private void IALUGenerator_DeleteProduct(string name, string Ver)
	{
		// Remove the section from MDB file
		string strSQL = null;
		strSQL = "DELETE FROM products WHERE name='" + name + "' AND version='" + Ver + "'";

		try {
			//open connection to MDB file
			oConnect.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source= " + fileMDB + ";";
			oConnect.Open();

			myCmd = new OleDbCommand();
			myCmd.Connection = oConnect;
			myCmd.CommandTimeout = 60;
			myCmd.CommandText = strSQL;
			myCmd.CommandType = CommandType.Text;
			myCmd.ExecuteNonQuery();
		}
		finally {
			oConnect.Close();
		}
	}
	void _IALUGenerator.DeleteProduct(string name, string Ver)
	{
		IALUGenerator_DeleteProduct(name, Ver);
	}
	//===============================================================================
	// Name: Function IALUGenerator_GenKey
	// Input:
	//   ByRef Lic As ActiveLock3.ProductLicense - Product license
	//   ByVal InstCode As String - Installation Code sent by the user
	//   ByVal RegisteredLevel As String - Registration Level for the license. Default is "0"
	// Output:
	//   String - Liberation key for the license
	// Purpose: Given the Installation Code, generates an Activelock license liberation key.
	// Remarks: None
	//===============================================================================
	private string IALUGenerator_GenKey(ref ActiveLock3_6NET.ProductLicense Lic, string InstCode, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute("0")]  // ERROR: Optional parameters aren't supported in C#
string RegisteredLevel)
	{
		// Take request code and decrypt it.
		string strReq = null;

		// 05.13.05 - ialkan Modified to merge DLLs into one
		strReq = modBase64.Base64_Decode(ref InstCode);

		// strReq now contains the {LockCode + vbLf + User} string
		string strLock = string.Empty;
		string strUser = string.Empty;
		GetLockAndUserFromInstallCode(strReq, ref strLock, ref strUser);

		Lic.Licensee = strUser;
		// registration date
		string strRegDate = null;
		// registered level
		Lic.RegisteredLevel = RegisteredLevel;
		strRegDate = Lic.RegisteredDate;

		string strEncrypted = null;
		// @todo Rethink this bit about encrypting the dates.
		// We need to keep in mind that the app does not have access to the private key, so and any decryption that requires private key
		// would not be possible.
		// Perhaps instead of encrypting, we could do MD5 hash of (regdate+lockcode)?
		//ActiveLockEventSink_ValidateValue strRegDate, strEncrypted
		// hash it
		//strEncrypted = ActiveLock3.MD5Hash(strEncrypted)
		strEncrypted = strRegDate;

		// get software codes
		ProductInfo ProdInfo = null;
		ProdInfo = IALUGenerator_RetrieveProduct(Lic.ProductName, Lic.ProductVer);
		Lic.ProductKey = ProdInfo.VCode;

		//@todo Check for "ProdInfo Is Nothing" and handle appropriately

		string strLic = null;
		strLic = Lic.ToString_Renamed() + Constants.vbLf + strLock;
		System.Diagnostics.Debug.WriteLine("strLic: " + Constants.vbCrLf + strLic);

		if (modALUGEN.strLeft(ProdInfo.VCode, 3) != "RSA") {
			// sign it
			string strSig = null;
			strSig = new string(Strings.Chr(0), 1024);
			// 05.13.05 - ialkan Modified to merge DLLs into one. Moved RSASign into a module
			strSig = modActiveLock.RSASign(ProdInfo.VCode, ProdInfo.GCode, strLic);

			// Create liberation key.  This will be a base-64 encoded string of the whole license.
			string strLicKey = null;
			// 05.13.05 - ialkan Modified to merge DLLs into one
			strLicKey = modBase64.Base64_Encode(ref strSig);
			// update Lic with license key
			Lic.LicenseKey = strLicKey;
			// Print some info for debugging purposes
			System.Diagnostics.Debug.WriteLine("VCode: " + ProdInfo.VCode);
			System.Diagnostics.Debug.WriteLine("Lic: " + strLic);
			System.Diagnostics.Debug.WriteLine("Lic hash: " + modMD5.Hash(ref strLic));
			System.Diagnostics.Debug.WriteLine("LicKey: " + strLicKey);
			System.Diagnostics.Debug.WriteLine("Sig: " + strSig);
			System.Diagnostics.Debug.WriteLine("Verify: " + modActiveLock.RSAVerify(ProdInfo.VCode, strLic, modBase64.Base64_Decode(ref strLicKey)));
			System.Diagnostics.Debug.WriteLine("====================================================");
		}

		else {
			try {
				System.Security.Cryptography.RSACryptoServiceProvider rsaCSP = new System.Security.Cryptography.RSACryptoServiceProvider();
				string strPublicBlob = null;
				string strPrivateBlob = null;

				strPublicBlob = ProdInfo.VCode;
				strPrivateBlob = ProdInfo.GCode;

				if (modALUGEN.strLeft(ProdInfo.GCode, 6) == "RSA512") {
					strPrivateBlob = modALUGEN.strRight(ProdInfo.GCode, Strings.Len(ProdInfo.GCode) - 6);
				}
				else {
					strPrivateBlob = modALUGEN.strRight(ProdInfo.GCode, Strings.Len(ProdInfo.GCode) - 7);
				}
				// import private key params into instance of RSACryptoServiceProvider
				rsaCSP.FromXmlString(strPrivateBlob);
				RSAParameters rsaPrivateParams = default(RSAParameters);
				//stores private key
				rsaPrivateParams = rsaCSP.ExportParameters(true);
				rsaCSP.ImportParameters(rsaPrivateParams);

				byte[] userData = Encoding.UTF8.GetBytes(strLic);
				AsymmetricSignatureFormatter asf = new RSAPKCS1SignatureFormatter(rsaCSP);
				HashAlgorithm algorithm = new SHA1Managed();
				asf.SetHashAlgorithm(algorithm.ToString());
				byte[] myhashedData = null;
				// a byte array to store hash value
				string myhashedDataString = null;
				myhashedData = algorithm.ComputeHash(userData);
				myhashedDataString = BitConverter.ToString(myhashedData).Replace("-", string.Empty);
				byte[] mysignature = null;
				// holds signatures
				mysignature = asf.CreateSignature(algorithm);
				string mySignatureBlock = null;
				mySignatureBlock = Convert.ToBase64String(mysignature);
			}
			catch (Exception ex) {
				modActiveLock.Set_Locale(modActiveLock.regionalSymbol);
				Err().Raise(AlugenGlobals.alugenErrCodeConstants.alugenProdInvalid, modTrial.ACTIVELOCKSTRING, ex.Message);
			}

		}

		// Serialize it into a formatted string
		string strLibKey = string.Empty;
		Lic.Save(ref strLibKey);
		return strLibKey;
	}
	string _IALUGenerator.GenKey(ref ActiveLock3_6NET.ProductLicense Lic, string InstCode, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute("0")]  // ERROR: Optional parameters aren't supported in C#
string RegisteredLevel)
	{
		return IALUGenerator_GenKey(Lic, InstCode, RegisteredLevel);
	}
	//===============================================================================
	// Name: Sub GetLockAndUserFromInstallCode
	// Input:
	//   ByVal strReq As String - strLock combined with user name.
	//   ByRef strLock As String - Generated request code to Lock
	//   ByRef strUser As String - User name
	// Output: None
	// Purpose: Retrieves lock string and user info from the request string
	// Remarks: None
	//===============================================================================
	private void GetLockAndUserFromInstallCode(string strReq, ref string strLock, ref string strUser)
	{
		int Index = 0;
		int i = 0;
		Index = 0;
		i = 1;
		// Get to the last vbLf, which denotes the ending of the lock code and beginning of user name.
		while (i > 0) {
			i = Strings.InStr(Index + 1, strReq, Constants.vbLf);
			if (i > 0) Index = i; 
		}

		if (Index <= 0) return;
 
		// lockcode is from beginning to Index-1
		strLock = Strings.Left(strReq, Index - 1);
		// user name starts from Index+1 to the end
		strUser = Strings.Mid(strReq, Index + 1);
		strUser = modActiveLock.TrimNulls(ref strUser);
	}
}
