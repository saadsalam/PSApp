namespace AppWorks.Utilities
{
    /// <summary>
    /// Sql constant used to define SQL queries which will run on database
    /// </summary>
    public static class SqlConstants
    {
        /// <summary>
        /// Query to get the lane summary report result
        /// </summary>
        public const string LANE_SUMMARY_QUERY = @"SELECT CASE WHEN CHARINDEX(' ',PSV.BayLocation) > 0 THEN SUBSTRING(PSV.BayLocation,1,CHARINDEX(' ',PSV.BayLocation)-1) ELSE PSV.BayLocation END AS 'Baylocation',
                                             CASE WHEN DATALENGTH(C.ShortName) > 0 THEN C.ShortName ELSE C.CustomerName END AS 'CustomerName', Count(*) AS 'RecordsCount'
                                             FROM PortStorageVehicles PSV
                                             LEFT JOIN Customer C ON PSV.CustomerID = C.CustomerID
                                             WHERE PSV.DateOut IS NULL 
                                             group by CASE WHEN DATALENGTH(C.ShortName) > 0 THEN C.ShortName ELSE C.CustomerName END,
                                             CASE WHEN CHARINDEX(' ',PSV.BayLocation) > 0 THEN SUBSTRING(PSV.BayLocation,1,CHARINDEX(' ',PSV.BayLocation)-1) ELSE PSV.BayLocation END
                                             ORDER BY CASE WHEN CHARINDEX(' ',PSV.BayLocation) > 0 THEN SUBSTRING(PSV.BayLocation,1,CHARINDEX(' ',PSV.BayLocation)-1) ELSE PSV.BayLocation END";


        /// <summary>
        /// Sql Query to get vehicle inspection and damage details
        /// Expects VIN 
        /// </summary>
        public const string INSPECTION_DAMAGE_DETAILS_QUERY = @"  SELECT PSVI.PSVehicleInspectionID AS PSVehicleInspectionID, PSVDD.PSVehicleDamageDetailID AS PSVehicleDamageDetailID, PSVI.InspectionDate,
                                             (SELECT TOP 1 CodeDescription as ws FROM Code WHERE CodeType = 'PSInspectionType' AND CODE = PSVI.InspectionType) as InspectionType,
                                             PSVI.InspectedBy,                                                      
                                             CASE WHEN PSVI.AttendedInd = 1 THEN 'YES' ELSE 'NO' END AS Attended,
                                             CASE WHEN PSVI.SubjectToInspectionInd = 1 THEN 'YES' ELSE 'NO' END AS SubjectToInspection, 
                                             CASE WHEN PSVI.CleanVehicleInd = 1 THEN 'YES' ELSE 'NO' END AS CleanVehicle, 
                                             PSVDD.DamageCode AS DamageCode,
                                             PSV.VehicleStatus, C1.CodeDescription as c1, ISNULL(C2.Value2,C2.CodeDescription) AS c2, C3.CodeDescription as c3
                                             FROM PSVehicleInspection PSVI
                                             LEFT JOIN PSVehicleDamageDetail PSVDD ON PSVI.PSVehicleInspectionID = PSVDD.PSVehicleInspectionID
                                             LEFT JOIN PortStorageVehicles PSV ON PSVI.PortStorageVehiclesID = PSV.PortStorageVehiclesID
                                             LEFT JOIN Code C1 ON LEFT(PSVDD.DamageCode,2) = C1.Code
                                             AND C1.CodeType = 'DamageAreaCode'
                                             LEFT JOIN Code C2 ON SUBSTRING(PSVDD.DamageCode,3,2) = C2.Code
                                             AND C2.CodeType = 'DamageTypeCode'
                                             LEFT JOIN Code C3 ON SUBSTRING(PSVDD.DamageCode,5,1) = C3.Code
                                             AND C3.CodeType = 'DamageSeverityCode'
                                             WHERE PSV.VIN = '{0}'
                                             ORDER BY PSVI.InspectionType, PSVI.InspectionDate, PSVDD.DamageCode";

        /// <summary>
        /// Query to get Portstorage Inbound List
        /// Expects StartDate, EndDate        
        /// </summary>
        public const string PORT_STORAGE_INBOUND_LIST_QUERY = @"SELECT L2.Locationname, L1.LoadNumber, V.VIN, V.Make, V.Model, PSV.BayLocation, PSV.DateIn, L.DeliveryBayLocation
		                                                       FROM  Vehicle V
		                                                         LEFT JOIN PortStorageVehicles PSV ON  V.VIN = PSV.VIN
		                                                         LEFT JOIN Legs L ON V.VehicleID = L.VehicleID
		                                                         LEFT JOIN Loads L1  ON L.LoadID = L1.LoadsID
		                                                         LEFT JOIN [DAIdb].[dbo].[Location] L2 on V.DropoffLocationID = L2.LocationID
		                                                       WHERE V.CustomerID NOT IN (SELECT CONVERT(int,ST.ValueDescription) FROM
		                                                            [DAIdb].[dbo].[SettingTable] ST WHERE ST.ValueKey = 'SOACustomerID' OR ST.ValueKey ='SDCCustomerID')
		                                                         AND  ((L2.State='MA'
		                                                         AND L2.City='Charlestown')
		                                                         OR L2.LocationID = 37067)
		                                                         AND L.DropoffDate >= {0}
		                                                         AND L.DropoffDate < DATEADD(day,1,{1})
		                                                       ORDER BY LoadNumber, PSV.BayLocation";      
        

        /// <summary>
        /// Used in YMS Vehicle Import module
        /// </summary>
        public const string VEHICLE_LOCATION_IMPORT_TRANSACTION = "EXEC [DataHub].[dbo].[DAIYMSP_GetStorageReceiptData]";
        
        
        public const string VEHICLE_BATCH_LIST_SEARCH = @"SELECT BatchID as BatchId,Vin as Vin, CreationDate as CreationDate
                                                     FROM PortStorageVehiclesImport WHERE Vin Like '%{0}%'                                                               
                                                     ORDER BY BatchID DESC";

        public const string VEHICLE_BATCH_LIST_ALL = @"SELECT BatchID as BatchId,Vin as VIN, CreationDate as CreationDate
                                                     FROM PortStorageVehiclesImport                                                     
                                                     ORDER BY BatchID DESC";

        public const string VEHICLE_COMPARISON_LIST = @"SELECT PSVLI.VIN VIN, PSV.Make, PSV.Model, PSV.Color, PSV.DateIN DateIn, 'NO MATCH' Status,
                                                            ISNULL(PSV.BayLocation,PSVLI.Location) Location
                                                        FROM PortStorageVehicleLocationImport PSVLI
                                                        LEFT JOIN PortStorageVehicles PSV ON PSVLI.VIN = PSV.VIN
                                                        AND PSV.DateOut IS NULL
                                                        WHERE PSVLI.BatchID =@batchId
                                                        AND PSV.VIN IS NULL
                                                        UNION SELECT PSV2.VIN, PSV2.Make, PSV2.Model, PSV2.Color, PSV2.DateIN, 'NOT SCANNED', PSV2.BayLocation Location
                                                        FROM PortStorageVehicles PSV2
                                                        WHERE PSV2.DateOut IS NULL
                                                        AND PSV2.VIN NOT IN (SELECT PSVLI2.VIN
                                                        FROM PortStorageVehicleLocationImport PSVLI2
                                                        WHERE PSVLI2.BatchID =@batchId)
                                                        ORDER BY Status, Location, VIN";

        public const string VEHICLE_INVOICE_SUMMARY = @"Select * from (SELECT C.CustomerCode as CustomerNumber,B.InvoiceNumber as InvoiceNumber,B.InvoiceDate InvoiceDate,CAST(B.InvoiceAmount as FLOAT) AS InvoiceAmount, CASE WHEN DATALENGTH(C.ShortName)>0 THEN C.ShortName ELSE C.CustomerName END CustomerName,
                                                                (SELECT COUNT(*) FROM PortStorageVehicles PSV WHERE PSV.BillingID = B.BillingID) AS Units
                                                            FROM Billing B
                                                            LEFT JOIN Customer C ON B.CustomerID = C.CustomerID
                                                                WHERE B.InvoiceType = 'StorageCharge'                                                                
                                                                AND (@BillingIDString = '0' OR B.BillingID IN (@BillingIDString))
                                                                AND (@StartDate Is null OR B.InvoiceDate >= @StartDate)
                                                                AND (@EndDate Is null OR B.InvoiceDate < DATEADD(day,1,@EndDate))
                                                            ) t where t.Units > 0 ORDER BY t.CustomerName, t.InvoiceNumber";
    }
}
