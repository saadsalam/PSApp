﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorks.UI.Model
{
    public class LocationModel
    {
        public int LocationID { get; set; }
        public Nullable<int> ParentRecordID { get; set; }
        public string ParentRecordTable { get; set; }
        public string LocationType { get; set; }
        public string LocationSubType { get; set; }
        public string LocationName { get; set; }
        public string LocationShortName { get; set; }
        public string CustomerLocationCode { get; set; }
        public string CustomerRegionCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string MainPhone { get; set; }
        public string FaxNumber { get; set; }
        public string PrimaryContactFirstName { get; set; }
        public string PrimaryContactLastName { get; set; }
        public string PrimaryContactPhone { get; set; }
        public string PrimaryContactExtension { get; set; }
        public string PrimaryContactCellPhone { get; set; }
        public string PrimaryContactEmail { get; set; }
        public string AlternateContactFirstName { get; set; }
        public string AlternateContactLastName { get; set; }
        public string AlternateContactPhone { get; set; }
        public string AlternateContactExtension { get; set; }
        public string AlternateContactCellPhone { get; set; }
        public string AlternateContactEmail { get; set; }
        public string OtherPhone1Description { get; set; }
        public string OtherPhone1 { get; set; }
        public string OtherPhone2Description { get; set; }
        public string OtherPhone2 { get; set; }
        public Nullable<int> AuctionPayOverrideInd { get; set; }
        public Nullable<decimal> AuctionPayRate { get; set; }
        public Nullable<int> FlatDeliveryPayInd { get; set; }
        public Nullable<decimal> FlatDeliveryPayRate { get; set; }
        public Nullable<int> MileagePayBoostOverrideInd { get; set; }
        public Nullable<decimal> MileagePayBoost { get; set; }
        public string DeliveryTimes { get; set; }
        public string LocationNotes { get; set; }
        public string DriverDirections { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<int> AlwaysShowInWIPInd { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string SPLCCode { get; set; }
        public Nullable<int> DeliveryHoldInd { get; set; }
        public Nullable<System.DateTime> DeliveryHoldDate { get; set; }
        public string DeliveryHoldBy { get; set; }
        public string DeliveryHoldReason { get; set; }
        public Nullable<int> NightDropAllowedInd { get; set; }
        public Nullable<int> STIAllowedInd { get; set; }
        public Nullable<int> AssignedDealerInd { get; set; }
        public Nullable<int> ShagPayAllowedInd { get; set; }
        public string ShortHaulPaySchedule { get; set; }
        public Nullable<int> NYBridgeAdditiveEligibleInd { get; set; }
        public Nullable<int> ShowInDriverQueueInd { get; set; }
        public string LocationMessage { get; set; }
        public Nullable<int> HotDealerInd { get; set; }
        public Nullable<int> DisableLoadBuildingInd { get; set; }
        public Nullable<int> LocationHasInspectorsInd { get; set; }
        public Nullable<int> SendDriversEmailInd { get; set; }
        public Nullable<int> NotesInd { get; set; }
        public Nullable<int> DirectionsInd { get; set; }
        public Nullable<int> HoursInd { get; set; }
        public Nullable<int> AddressInd { get; set; }
        public Nullable<System.DateTime> DeliveryHoldEndDate { get; set; }
        public Nullable<int> EnforceLoadBoardRulesInd { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPageCount { get; set; }
    }
}
