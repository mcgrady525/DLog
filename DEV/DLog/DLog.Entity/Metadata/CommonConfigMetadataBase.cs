
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码是根据模板生成的。
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，则所做更改将丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using DataAnnotationsExtensions;//http://dataannotationsextensions.org/


namespace DLog.Entity.Metadata
{

/// <summary>
///  CustomMetadata基類
/// </summary>
[DataContract]
public class CommonConfigMetadataBase
{


	/// <summary>
    /// 
	/// </summary>
    [Key]
        [Required]
        [Digits]
        [Max(int.MaxValue)]
        [DataMember]
    public object ID { get; set; }


	/// <summary>
    /// 
	/// </summary>
    [StringLength(1073741823)]
        [DataMember]
    public object Content { get; set; }


	/// <summary>
    /// 
	/// </summary>
    [Required]
        [DataType(DataType.Date)]
        [UIHint("DatePicker")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DataMember]
    public object LastUpdateTime { get; set; }


	/// <summary>
    /// 
	/// </summary>
    [Required]
        [StringLength(32)]
        [DataMember]
    public object LastUpdateUser { get; set; }

}


}
