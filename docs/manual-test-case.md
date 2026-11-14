# Manual QA Artifact: Hotel Booking Creation & Validation

## Test Case ID: TC-BOOK-01
**Title:** Verify hotel reservation creation with invalid check-out date (check-out date earlier than check-in date)  
**Priority:** High  
**Preconditions:** Booking API (`POST /booking`) is available and accepts JSON payloads.

### Steps:
1. Send a `POST` request to `/booking` with valid guest details (`firstname`, `lastname`, `totalprice`, `depositpaid`).
2. Set `bookingdates.checkin` to `"2026-12-10"`.
3. Set `bookingdates.checkout` to `"2026-12-05"`.

### Expected Result:
- HTTP Status Code: `400 Bad Request`.
- Response body returns validation error message, indicating that check-out date cannot precede check-in date.
- No booking record is saved on database.

### Actual Result (Observed on Sandbox API):
- HTTP Status Code: `200 OK`.
- The system creates a booking ID with chronologically invalid dates.

---

## Bug Report: BUG-01
- **Summary:** `[Booking API] System allows creating a reservation when check-out date is earlier than check-in date`
- **Severity:** Major | **Priority:** High
- **Environment:** Google Chrome,Windows 11  RestfulAPI (`https://restful-booker.herokuapp.com/booking`)
- **Description:** Lack of server-side date range validation allows illogical reservations, which can cause pricing calculation errors and availability conflicts in downstream hotel inventory systems.