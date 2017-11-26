using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileUploadsInAspNetMvc.Models
{
    public class CareElderMedicine
    {
        [Key]
        public int Id {get;set;} 
        public string PId {get;set;}
        [Display (Name ="狀態")]
        public string Status {get;set;}
        public string CancelDate {get;set;}
        public string CancelMemo {get;set;}
        public string LegalDate {get;set;}
        public string ApproveDate {get;set;}
        public string LicenseType {get;set;}
        public string OldPId {get;set;}
        public string CustomId {get;set;}
        [Display(Name = "中文名字")]
        public string CName {get;set;}
        [Display(Name = "英文名字")]
        public string EName {get;set;}
        [Display(Name = "處方")]
        public string Indication {get;set;}
        public string Formulation {get;set;}
        public string Package {get;set;}
        public string Type {get;set;}
        public string Control {get;set;}
        [Display(Name = "成分")]
        public string Ingredient {get;set;}
        [Display(Name = "申請藥商")]
        public string ApplyCompany {get;set;}
        public string ACAddress {get;set;}
        public string ACId {get;set;}
        public string Manufacture {get;set;}
        public string MFactoryAddress {get;set;}
        public string MCompanyAddress {get;set;}
        public string MCountry {get;set;}
        public string Process {get;set;}
        public string UpdateDate {get;set;}
        public string Dosage {get;set;}
        [Display(Name = "注意事項")]
        public string PackageCode {get;set;}


    }
}