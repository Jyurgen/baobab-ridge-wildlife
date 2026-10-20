# Baobab Ridge Wildlife Rehabilitation Records System

A C# Windows Forms desktop application (.NET 8) for the Baobab Ridge Wildlife Rehabilitation Centre. It records every animal in care, calculates each animal's status and housing unit from its Recovery Score, and produces a summary report for the Centre manager. Data is stored in plain text files only — no databases.

## Team members

| Name | Student number | Feature(s) owned |
|------|----------------|------------------|
| [Sage Tyhali] | [603986] | Add animal, Validation|
| [Keketso Mashaba] | [603304] | View animals, File Loading |
| [Adwin Khobo] | [603817] | Update animal, Delete animal |
| [Thandiswa Magadlela] | [602219] | Summary Report, README, error handling |

## How to build and run

1. Clone the repository:https://github.com/Jyurgen/baobab-ridge-wildlife.git
2. Open the solution in Visual Studio 2022 (or later).
3. Ensure the .NET 8 SDK is installed.
4. Build the solution (`Build > Build Solution`).
5. Run with `F5`.

No databases or storage-related NuGet packages are used. `animals.txt` is read/written beside the `.exe`; `summary.txt` is generated beside the `.exe`.

## Features

- **Add animal** — validated input; status and housing unit calculated automatically.
- **View animals** — all valid records shown in a read-only grid on startup, in file order.
- **Update animal** — load a record from the grid, edit and save; revalidated and reclassified.
- **Delete animal** — select, confirm, and remove a record from file and grid.
- **Summary report** — totals, averages and per-status counts shown on screen and written to `summary.txt`.
- **Error handling** — missing files, files in use, and permission problems are reported with friendly messages; the program does not crash.

## File formats

### `animals.txt`
One animal per line, seven pipe-separated fields:ID|Name|Species|Age|Score|Status|Housing Unit


UTF-8, no header, no blank lines, stored beside the `.exe`. Malformed lines are skipped on load and the user is told how many were skipped.

Example: WR-0001|Thabo|African Penguin|4|85|Release-Ready|Pre-Release Camp
WR-0004|Zola|Cape Vulture|12|19|Critical|Intensive Care Unit


### `summary.txt`
Overwritten (never appended) each time a summary is generated, beside the `.exe`. Layout:
BAOBAB RIDGE WILDLIFE REHABILITATION CENTRE
ANIMAL SUMMARY REPORT
Generated: yyyy-MM-dd HH:mm

Total animals: N
Average age (years): X.XX
Average recovery score: Y.YY

Animals per status:
Critical: n
Serious: n
Stable: n
Recovering: n
Release-Ready: n


All five statuses always appear, even with a count of 0. Averages use two decimals and a full stop. With no animals, both averages show `N/A`.

## Known limitations

- Only one animal is deleted at a time.
- Records appear in file order; sorting is not re-applied after a save.
- The Animal ID cannot be changed once a record is saved.

## Repository

Hosted on GitHub at `https://github.com/Jyurgen/baobab-ridge-wildlife`.
