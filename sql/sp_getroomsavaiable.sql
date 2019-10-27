DECLARE @StartDate AS DATETIME
DECLARE @EndDate AS DATETIME
DECLARE @CurrentDate AS DATETIME

SET @StartDate = '2019-04-14'
SET @EndDate = GETDATE()
SET @CurrentDate = @StartDate

WHILE (@CurrentDate < @EndDate)
BEGIN
INSERT INTO dbo.tblRoomsAvaiable
    SELECT tblTotalRoom.Id, tblTotalRoom.Name, tblTotalRoom.TotalRoom, tblRoomsUsingToday.NoRUsing, tblRoomsUsingToday.CruiseId, 
                      tblTotalRoom.TotalRoom - tblRoomsUsingToday.NoRUsing AS NoOFAvaiableRooms
FROM         (SELECT     COUNT(*) AS NoRUsing, Booking.CruiseId
FROM         dbo.os_Booking AS Booking INNER JOIN
                      dbo.os_BookingRoom AS BookingRoom ON Booking.Id = BookingRoom.BookId
WHERE     (Booking.StartDate <= @CurrentDate) AND (Booking.EndDate > @CurrentDate) AND (Booking.Deleted = 0) AND (Booking.Status = 1 OR
                      Booking.Status = 2)
GROUP BY Booking.CruiseId) AS tblRoomsUsingToday 
RIGHT OUTER JOIN
                      (SELECT     dbo.os_Cruise.Id, dbo.os_Cruise.Name, COUNT(*) AS TotalRoom
FROM         dbo.os_Cruise INNER JOIN
                      dbo.os_Room ON dbo.os_Cruise.Id = dbo.os_Room.CruiseId
GROUP BY dbo.os_Cruise.Id, dbo.os_Cruise.Name) AS tblTotalRoom ON tblRoomsUsingToday.CruiseId = tblTotalRoom.Id

    SET @CurrentDate = DATEADD(DAY, 1, @CurrentDate); /*increment current date*/
END



