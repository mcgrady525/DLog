
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码是根据模板生成的。
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，则所做更改将丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using DLog.Entity.Metadata;


namespace DLog.Entity
{

/// <summary>
/// <para>【DLogDB.Entity.tt生成的，与数据库对应的】</para>
/// 

/// </summary>
[Serializable]
[DataContract(IsReference = true)]
[MetadataType(typeof(DebugLogMetadata))]  
public partial class DebugLog
{


	/// <summary>
    /// 獲取或設置
	/// </summary>
        [DatabaseTableColumn]
    [DataMember]
    public long ID { get; set; }


	/// <summary>
    /// 獲取或設置
	/// </summary>
        [DatabaseTableColumn]
    [DataMember]
    public int ThreadID { get; set; }


	/// <summary>
    /// 獲取或設置
	/// </summary>
        [DatabaseTableColumn]
    [DataMember]
    public string SystemCode { get; set; }


	/// <summary>
    /// 獲取或設置
	/// </summary>
        [DatabaseTableColumn]
    [DataMember]
    public string Source { get; set; }


	/// <summary>
    /// 獲取或設置
	/// </summary>
        [DatabaseTableColumn]
    [DataMember]
    public string Message { get; set; }


	/// <summary>
    /// 獲取或設置
	/// </summary>
        [DatabaseTableColumn]
    [DataMember]
    public string MachineName { get; set; }


	/// <summary>
    /// 獲取或設置
	/// </summary>
        [DatabaseTableColumn]
    [DataMember]
    public string AppDomainName { get; set; }


	/// <summary>
    /// 獲取或設置
	/// </summary>
        [DatabaseTableColumn]
    [DataMember]
    public int ProcessID { get; set; }


	/// <summary>
    /// 獲取或設置
	/// </summary>
        [DatabaseTableColumn]
    [DataMember]
    public string ProcessName { get; set; }


	/// <summary>
    /// 獲取或設置
	/// </summary>
        [DatabaseTableColumn]
    [DataMember]
    public string ThreadName { get; set; }


	/// <summary>
    /// 獲取或設置
	/// </summary>
        [DatabaseTableColumn]
    [DataMember]
    public byte[] Detail { get; set; }


	/// <summary>
    /// 獲取或設置
	/// </summary>
        [DatabaseTableColumn]
    [DataMember]
    public System.DateTime CreateTime { get; set; }


	/// <summary>
    /// 獲取或設置
	/// </summary>
        [DatabaseTableColumn]
    [DataMember]
    public string IpAddress { get; set; }

}


}
