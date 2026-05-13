-- ============================================================
-- FILE: DatabaseMigration.sql
-- Run this ONCE in SSMS AFTER you have already run CreateDB.sql.
-- It upgrades the existing SmartWorkspaceDB schema to support
-- all new application features.
-- ============================================================

USE SmartWorkspaceDB;
GO

-- ────────────────────────────────────────────────────────────
-- 1. ALTER Member: drop old columns, add new ones
-- ────────────────────────────────────────────────────────────

-- Drop CompanyName
ALTER TABLE Member DROP COLUMN CompanyName;
GO

-- Drop Email
ALTER TABLE Member DROP COLUMN Email;
GO

-- Add new identity & affiliation fields
ALTER TABLE Member
    ADD DigitalID            NVARCHAR(50)  NULL,
        CorporateAffiliation NVARCHAR(150) NULL,
        TotalReservedHours   INT NOT NULL DEFAULT 0;
GO

-- ────────────────────────────────────────────────────────────
-- 2. ALTER Reservation: add Status column
-- ────────────────────────────────────────────────────────────

ALTER TABLE Reservation
    ADD Status NVARCHAR(20) NOT NULL DEFAULT 'Running';
GO

ALTER TABLE Reservation
    ADD CONSTRAINT CK_Reservation_Status
        CHECK (Status IN ('Running', 'Finished'));
GO

-- Backfill existing rows (already inserted before migration)
UPDATE Reservation SET Status = 'Running' WHERE Status IS NULL OR Status = '';
GO

-- ────────────────────────────────────────────────────────────
-- 3. ALTER Equipment: add EquipmentType and HubName columns
--    (The table already exists from CreateDB.sql with only
--     EquipmentID and EquipmentName)
-- ────────────────────────────────────────────────────────────

ALTER TABLE Equipment
    ADD EquipmentType NVARCHAR(50)  NOT NULL DEFAULT 'General',
        HubName       NVARCHAR(100) NOT NULL DEFAULT 'Unknown';
GO

-- Backfill sample equipment rows with sensible defaults
UPDATE Equipment SET EquipmentType = 'AV',       HubName = 'Downtown Hub'   WHERE EquipmentName = 'Projector';
UPDATE Equipment SET EquipmentType = 'Office',   HubName = 'Cairo Hub'      WHERE EquipmentName = 'Whiteboard';
UPDATE Equipment SET EquipmentType = 'AV',       HubName = 'Giza Hub'       WHERE EquipmentName = 'Video Conferencing System';
UPDATE Equipment SET EquipmentType = 'Furniture',HubName = 'Cairo Hub'      WHERE EquipmentName = 'Standing Desk';
UPDATE Equipment SET EquipmentType = 'Office',   HubName = 'Downtown Hub'   WHERE EquipmentName = 'Printer';
GO

-- ────────────────────────────────────────────────────────────
-- 4. ReservationEquipment already exists from CreateDB.sql
--    (ID, ReservationID, EquipmentID) — no changes needed.
-- ────────────────────────────────────────────────────────────
-- Nothing to do here.

-- ────────────────────────────────────────────────────────────
-- 5. Trigger: keep TotalReservedHours in sync automatically
-- ────────────────────────────────────────────────────────────

CREATE OR ALTER TRIGGER trg_UpdateTotalHours
ON Reservation
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE m
    SET    m.TotalReservedHours = ISNULL(
               (SELECT SUM(r.HoursReserved)
                FROM   Reservation r
                WHERE  r.MemberID = m.MemberID), 0)
    FROM   Member m
    WHERE  m.MemberID IN (
        SELECT MemberID FROM inserted
        UNION
        SELECT MemberID FROM deleted
    );
END;
GO

-- ────────────────────────────────────────────────────────────
-- 6. Backfill TotalReservedHours for all existing members
-- ────────────────────────────────────────────────────────────

UPDATE m
SET    m.TotalReservedHours = ISNULL(
           (SELECT SUM(r.HoursReserved)
            FROM   Reservation r
            WHERE  r.MemberID = m.MemberID), 0)
FROM   Member m;
GO

-- Drop existing constraints
ALTER TABLE Reservation
    DROP CONSTRAINT FK_Reservation_Member;

ALTER TABLE Reservation
    DROP CONSTRAINT FK_Reservation_Workspace;

-- Re-add with ON DELETE CASCADE
ALTER TABLE Reservation
    ADD CONSTRAINT FK_Reservation_Member
        FOREIGN KEY (MemberID) REFERENCES Member(MemberID)
        ON DELETE CASCADE;

ALTER TABLE Reservation
    ADD CONSTRAINT FK_Reservation_Workspace
        FOREIGN KEY (WorkspaceID) REFERENCES Workspace(WorkspaceID)
        ON DELETE CASCADE;
-- Drop existing constraints
ALTER TABLE ReservationEquipment
    DROP CONSTRAINT FK_RE_Reservation;

ALTER TABLE ReservationEquipment
    DROP CONSTRAINT FK_RE_Equipment;

-- Re-add with ON DELETE CASCADE
ALTER TABLE ReservationEquipment
    ADD CONSTRAINT FK_RE_Reservation
        FOREIGN KEY (ReservationID) REFERENCES Reservation(ReservationID)
        ON DELETE CASCADE;

ALTER TABLE ReservationEquipment
    ADD CONSTRAINT FK_RE_Equipment
        FOREIGN KEY (EquipmentID) REFERENCES Equipment(EquipmentID)
        ON DELETE CASCADE;


PRINT '================================================';
PRINT 'Migration completed successfully.';
PRINT 'Schema is now compatible with the new features.';
PRINT '================================================';